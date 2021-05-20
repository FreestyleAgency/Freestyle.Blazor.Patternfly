using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public static class InputExtensions
	{
        public static bool TryParseSelectableValueFromString<TValue>(this InputBase<TValue> input, string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            try
            {
                if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue))
                {
                    result = parsedValue;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = $"The {input.DisplayName} field is not valid.";
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"{input.GetType()} does not support the type '{typeof(TValue)}'.", ex);
            }
        }
    }
}
