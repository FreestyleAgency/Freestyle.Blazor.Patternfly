using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public class OutsideClickListener : IOutsideClickListener
	{
		private readonly IJSRuntime _jsRuntime;

		private List<ElementReference> _registered = new List<ElementReference>();

		public event EventHandler ClickOutside; 

		public OutsideClickListener(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		[JSInvokable]
		public Task InvokeClickOutsideAsync()
		{
			ClickOutside?.Invoke(this, EventArgs.Empty);

			return Task.FromResult(true);
		}

		public async Task Register(ElementReference elementReference)
		{
			if (!_registered.Contains(elementReference))
			{
				await _jsRuntime.InvokeAsync<object>("patternflyBlazorFunctions.addOutsideClickHandler", new object[] { elementReference, DotNetObjectReference.Create(this) });
				_registered.Add(elementReference);
			}
		}

		public async Task Register(ElementReference elementReference, ElementReference clickTarget)
		{
			if (!_registered.Contains(elementReference))
			{
				await _jsRuntime.InvokeAsync<object>("patternflyBlazorFunctions.addOutsideClickHandler", new object[] { elementReference, DotNetObjectReference.Create(this), clickTarget });
				_registered.Add(elementReference);
			}
		}

		public async Task Unregister(ElementReference elementReference)
		{
			if (_registered.Contains(elementReference))
			{
				await _jsRuntime.InvokeAsync<object>("patternflyBlazorFunctions.removeOutsideClickHandler", new object[] { elementReference });
				_registered.Remove(elementReference);
			}
		}
	}
}
