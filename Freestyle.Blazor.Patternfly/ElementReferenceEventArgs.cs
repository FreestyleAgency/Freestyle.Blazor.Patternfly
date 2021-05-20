using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Blazor.Patternfly
{
	public class ElementReferenceEventArgs : EventArgs
	{
		public ElementReference ElementReference { get; private set; }

		public ElementReferenceEventArgs(ElementReference elementReference)
		{
			this.ElementReference = elementReference;
		}
	}
}
