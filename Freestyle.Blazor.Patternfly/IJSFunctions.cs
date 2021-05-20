using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	internal interface IJSFunctions
	{
		Task AddBodyClassAsync(string className);
		Task BindNavScrollerAsync(ElementReference navElement);
		Task BindTabsScrollerAsync(ElementReference navElement);
		Task BlurAsync(ElementReference element);
		Task ClipboardWriteTextAsync(string text);
		Task<string> GetContentEditableInnerHtmlAsync(ElementReference element);
		Task HidePopperAsync(ElementReference target);
		Task MoveCursorToEndAsync(ElementReference textInputElement);
		Task MoveToBodyEndAsync(ElementReference element);
		Task ReplaceInto(ElementReference source, ElementReference destination);
		Task RemoveBodyClassAsync(string className);
		Task ShowPopperAsync(ElementReference target, ElementReference tooltip, Placement placement);
		Task UpdatePopperAsync(ElementReference target);
	}
}