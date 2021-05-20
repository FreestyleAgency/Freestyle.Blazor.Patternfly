using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public class DocumentEvents
	{
		public static event EventHandler OnEscapeKeyPressed;

		[JSInvokable("DocumentEventsEscapePressed")]
		public static void EscapePressed()
		{
			OnEscapeKeyPressed?.Invoke(null, EventArgs.Empty);
		}
	}
}
