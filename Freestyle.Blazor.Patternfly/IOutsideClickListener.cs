using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public interface IOutsideClickListener
	{
		event EventHandler ClickOutside;

		Task Register(ElementReference elementReference);
		Task Register(ElementReference elementReference, ElementReference clickTarget);
		Task Unregister(ElementReference elementReference);
	}
}