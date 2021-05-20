using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	internal class JSFunctions : IJSFunctions
	{
		private IJSRuntime _jSRuntime;

		public JSFunctions(IJSRuntime jSRuntime)
		{
			_jSRuntime = jSRuntime;
		}

		public async Task ShowPopperAsync(ElementReference target, ElementReference tooltip, Placement placement)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.showPopper", target, tooltip, placement.ToString().ToLower());
		}

		public async Task HidePopperAsync(ElementReference target)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.hidePopper", target);
		}

		public async Task UpdatePopperAsync(ElementReference target)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.updatePopper", target);
		}

		public async Task MoveToBodyEndAsync(ElementReference element)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.moveToBodyEnd", element);
		}

		public async Task ReplaceInto(ElementReference source, ElementReference destination)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.replaceInto", source, destination);
		}

		public async Task AddBodyClassAsync(string className)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.addBodyClass", className);
		}

		public async Task RemoveBodyClassAsync(string className)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.removeBodyClass", className);
		}

		public async Task BlurAsync(ElementReference element)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.blur", element);
		}

		public async Task ClipboardWriteTextAsync(string text)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.clipboardWriteText", text);
		}

		public async Task<string> GetContentEditableInnerHtmlAsync(ElementReference element)
		{
			return await _jSRuntime.InvokeAsync<string>("patternflyBlazorFunctions.getContentEditableInnerHtml", element);
		}

		public async Task BindNavScrollerAsync(ElementReference navElement)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.bindNavScroller", navElement);
		}

		public async Task BindTabsScrollerAsync(ElementReference navElement)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.bindTabsScroller", navElement);
		}

		public async Task MoveCursorToEndAsync(ElementReference textInputElement)
		{
			await _jSRuntime.InvokeVoidAsync("patternflyBlazorFunctions.moveCursorToEnd", textInputElement);
		}
	}
}
