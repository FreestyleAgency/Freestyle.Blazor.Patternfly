using System;
using System.Collections.Generic;
using System.Text;

namespace Freestyle.Blazor.Patternfly
{
	public class PaginationState
	{
		public int PageNumber { get; set; }
		public int FirstItemIndex { get; set; }
		public int LastItemIndex { get; set; }
		public int PageItemCount { get; set; }
		public int TotalItems { get; set; }
	}
}
