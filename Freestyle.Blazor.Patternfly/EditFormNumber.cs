using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Freestyle.Blazor.Patternfly
{
	public class EditFormNumber<TValue> : EditFormInputBase<TValue>
	{
		private static string _stepAttributeValue; // Null by default, so only allows whole numbers as per HTML spec

		static EditFormNumber()
		{
			// Unwrap Nullable<T>, because InputBase already deals with the Nullable aspect
			// of it for us. We will only get asked to parse the T for nonempty inputs.
			var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
			if (targetType == typeof(int) ||
				targetType == typeof(long) ||
				targetType == typeof(float) ||
				targetType == typeof(double) ||
				targetType == typeof(decimal))
			{
				_stepAttributeValue = "any";
			}
			else
			{
				throw new InvalidOperationException($"The type '{targetType}' is not a supported numeric type.");
			}
		}

		/// <summary>
		/// Gets or sets the error message used when displaying an a parsing error.
		/// </summary>
		[Parameter] public string ParsingErrorMessage { get; set; } = "The {0} field must be a number.";

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(0, "input");
			builder.AddAttribute(1, "step", _stepAttributeValue);
			builder.AddMultipleAttributes(2, AdditionalAttributes);
			builder.AddAttribute(2, "id", Id);
			builder.AddAttribute(3, "required", Required);
			builder.AddAttribute(4, "disabled", !Enabled);
			builder.AddAttribute(5, "type", "number");
			builder.AddAttribute(6, "class", $"{CssClass} pf-c-form-control {(IsValid == true && !HasValidationMessages ? CssClassConstants.Success : String.Empty)} {AdditionalCssClass}");
			builder.AddAttribute(7, "value", BindConverter.FormatValue(CurrentValueAsString));
			builder.AddAttribute(8, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
			builder.AddAttribute(9, "aria-invalid", HasValidationMessages.ToString().ToLower());
			builder.AddAttribute(10, "aria-describedby", $"{Id}-helper");
			builder.AddAttribute(11, "onblur", EventCallback.Factory.Create<FocusEventArgs>(this, OnBlur));
			builder.CloseElement();
		}

		/// <inheritdoc />
		protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
		{
			if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.InvariantCulture, out result))
			{
				validationErrorMessage = null;
				return true;
			}
			else
			{
				validationErrorMessage = string.Format(ParsingErrorMessage, FieldIdentifier.FieldName);
				return false;
			}
		}

		/// <summary>
		/// Formats the value as a string. Derived classes can override this to determine the formatting used for <c>CurrentValueAsString</c>.
		/// </summary>
		/// <param name="value">The value to format.</param>
		/// <returns>A string representation of the value.</returns>
		protected override string FormatValueAsString(TValue value)
		{
			// Avoiding a cast to IFormattable to avoid boxing.
			switch (value)
			{
				case null:
					return null;

				case int @int:
					return BindConverter.FormatValue(@int, CultureInfo.InvariantCulture);

				case long @long:
					return BindConverter.FormatValue(@long, CultureInfo.InvariantCulture);

				case float @float:
					return BindConverter.FormatValue(@float, CultureInfo.InvariantCulture);

				case double @double:
					return BindConverter.FormatValue(@double, CultureInfo.InvariantCulture);

				case decimal @decimal:
					return BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture);

				default:
					throw new InvalidOperationException($"Unsupported type {value.GetType()}");
			}
		}

		private void OnBlur()
		{
			EditContext.NotifyFieldChanged(FieldIdentifier);

			IsValid = !HasValidationMessages;
			OnAfterValidate();
			StateHasChanged();
		}
	}
}
