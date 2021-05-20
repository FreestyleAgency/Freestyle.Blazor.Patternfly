using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public abstract class EditFormInputBase<TValue> : InputBase<TValue>
	{
		[CascadingParameter(Name = "FormGroup")]
		public EditFormGroup FormGroup { get; set; }

		[CascadingParameter(Name = "InInputGroup")]
		public bool InInputGroup { get; set; }

		[Parameter]
		public bool Enabled { get; set; } = true;

		[Parameter]
		public string AdditionalCssClass { get; set; }

		[Parameter]
		public bool AutoLabel { get; set; } = true;

		[Parameter]
		public bool AutoRequired { get; set; } = true;

		internal event EventHandler<EventArgs> AfterValidate;

		protected int _inputNumber = 0;
		
		internal string Id { get; private set; }
		internal bool? IsValid { get; set; }

		protected bool Required
		{
			get
			{
				return FormGroup == null ? false : FormGroup.Required;
			}
		}

		internal bool HasValidationMessages
		{
			get { return EditContext.GetValidationMessages(FieldIdentifier).Any(); }
		}

		protected override void OnInitialized()
		{
			if (FormGroup != null)
			{
				_inputNumber = FormGroup.InputComponentCount;
				Id = FormGroup.Id + "-input-" + _inputNumber;

				FormGroup.AddInputComponent(this);

				if (AutoLabel && String.IsNullOrEmpty(FormGroup.Label))
				{
					try
					{
						var fieldProperty = this.FieldIdentifier.Model.GetType().GetProperty(this.FieldIdentifier.FieldName);

						var displayAttribute = fieldProperty.GetCustomAttribute<DisplayAttribute>();
						var displayNameAttribute = fieldProperty.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();

						if (displayAttribute != null)
						{
							FormGroup.SetLabel(displayAttribute.GetName());
						}
						else if (displayNameAttribute != null)
						{
							FormGroup.SetLabel(displayNameAttribute.DisplayName);
						}
						else
						{
							FormGroup.SetLabel(this.FieldIdentifier.FieldName);
						}
					}
					catch { }
				}

				if (AutoRequired)
				{
					try
					{
						var fieldProperty = this.FieldIdentifier.Model.GetType().GetProperty(this.FieldIdentifier.FieldName);

						var requiredAttribute = fieldProperty.GetCustomAttribute<RequiredAttribute>();

						if (requiredAttribute != null)
						{
							FormGroup.SetRequired(true);
						}
					}
					catch { }
				}
			}

			base.OnInitialized();
		}

		public override async Task SetParametersAsync(ParameterView parameters)
		{
			Dictionary<string, object> newAdditionalAttributes;

			await base.SetParametersAsync(parameters);
			if (EditContext.GetValidationMessages(FieldIdentifier).Any())
			{
				// Copy the existing attributes to the new, writeable dictionary
				newAdditionalAttributes = new Dictionary<string, object>();
				if (AdditionalAttributes != null)
				{
					foreach (KeyValuePair<string, object> attribute in AdditionalAttributes)
					{
						newAdditionalAttributes.Add(attribute.Key, attribute.Value);
					}
				}

				// Set the invalid attribute we were here to manipulate
				newAdditionalAttributes["aria-invalid"] = "true";

				// Assign the new list of attributes back to the underlying property
				AdditionalAttributes = newAdditionalAttributes;

				IsValid = false;
			}
		}

		protected void OnAfterValidate()
		{
			AfterValidate?.Invoke(this, EventArgs.Empty);
		}
	}
}
