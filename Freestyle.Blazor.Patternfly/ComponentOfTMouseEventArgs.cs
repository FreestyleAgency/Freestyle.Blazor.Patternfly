using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public class ComponentMouseEventArgs<TComponent> where TComponent : ComponentBase
	{
		public TComponent Component { get; private set; }
		public MouseEventArgs MouseEventArgs { get; private set; }

		public ComponentMouseEventArgs(TComponent component, MouseEventArgs mouseEventArgs)
		{
			Component = component;
			MouseEventArgs = mouseEventArgs;
		}
	}
}
