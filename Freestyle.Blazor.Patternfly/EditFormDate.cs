using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Freestyle.Blazor.Patternfly
{
	public partial class EditFormDate<TValue> : EditFormInputBase<TValue>
	{
        private const string DateFormat = "yyyy-MM-dd"; // Compatible with HTML date inputs

        private string _editingValue = null;

        /// <summary>
        /// Gets or sets the error message used when displaying an a parsing error.
        /// </summary>
        [Parameter] public string ParsingErrorMessage { get; set; } = "The {0} field must be a date.";

		protected override void OnParametersSet()
		{
            _editingValue = FormatValueAsString(Value);

			base.OnParametersSet();
		}

		/// <inheritdoc />
		protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "input");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "id", Id);
            builder.AddAttribute(3, "required", Required);
            builder.AddAttribute(4, "disabled", !Enabled);
            builder.AddAttribute(5, "type", "date");
            builder.AddAttribute(6, "class", $"{CssClass} pf-c-form-control {(IsValid == true  && !HasValidationMessages ? CssClassConstants.Success : String.Empty)} {AdditionalCssClass}");
            builder.AddAttribute(7, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(8, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => _editingValue = __value, CurrentValueAsString));
            builder.AddAttribute(9, "aria-invalid", HasValidationMessages.ToString().ToLower());
            builder.AddAttribute(10, "aria-describedby", $"{Id}-helper");
            builder.AddAttribute(11, "onblur", EventCallback.Factory.Create<FocusEventArgs>(this, OnBlur));
            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override string FormatValueAsString(TValue value)
        {
            switch (value)
            {
                case DateTime dateTimeValue:
                    return BindConverter.FormatValue(dateTimeValue, DateFormat, CultureInfo.InvariantCulture);
                case DateTimeOffset dateTimeOffsetValue:
                    return BindConverter.FormatValue(dateTimeOffsetValue, DateFormat, CultureInfo.InvariantCulture);
                default:
                    return string.Empty; // Handles null for Nullable<DateTime>, etc.
            }
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            // Unwrap nullable types. We don't have to deal with receiving empty values for nullable
            // types here, because the underlying InputBase already covers that.
            var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

            bool success;
            if (targetType == typeof(DateTime))
            {
                success = TryParseDateTime(value, out result);
            }
            else if (targetType == typeof(DateTimeOffset))
            {
                success = TryParseDateTimeOffset(value, out result);
            }
            else
            {
                throw new InvalidOperationException($"The type '{targetType}' is not a supported date type.");
            }

            if (success)
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

        static bool TryParseDateTime(string value, out TValue result)
        {
            var success = BindConverter.TryConvertToDateTime(value, CultureInfo.InvariantCulture, DateFormat, out var parsedValue);
            if (success)
            {
                result = (TValue)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        static bool TryParseDateTimeOffset(string value, out TValue result)
        {
            var success = BindConverter.TryConvertToDateTimeOffset(value, CultureInfo.InvariantCulture, DateFormat, out var parsedValue);
            if (success)
            {
                result = (TValue)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        private void OnBlur()
        {
            CurrentValueAsString = _editingValue;

            EditContext.NotifyFieldChanged(FieldIdentifier);

            IsValid = !HasValidationMessages;
            OnAfterValidate();
            StateHasChanged();
        }
    }
}
