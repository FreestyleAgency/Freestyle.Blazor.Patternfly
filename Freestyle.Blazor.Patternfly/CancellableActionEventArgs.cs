using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public class CancellableActionEventArgs : EventArgs
	{
		public bool Cancel { get; set; }
	}
}
