using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freestyle.Blazor.Patternfly
{
	public static class CssClassConstants
	{
		public const string Expanded = "pf-m-expanded";
		public const string Expandable = "pf-m-expandable";
		public const string AlignRight = "pf-m-align-right";
		public const string Top = "pf-m-top";
		public const string Bottom = "pf-m-bottom";
		public const string Left = "pf-m-left";
		public const string Right = "pf-m-right";
		public const string Fixed = "pf-m-fixed";
		public const string NoBoxShadow = "pf-m-no-box-shadow";
		public const string Toast = "pf-m-toast";
		public const string Inline = "pf-m-inline";
		public const string External = "pf-m-external";
		public const string Favorite = "pf-m-favorite";
		public const string Disabled = "pf-m-disabled";
		public const string Read = "pf-m-read";
		public const string Unread = "pf-m-unread";
		public const string Current = "pf-m-current";
		public const string Primary = "pf-m-primary";
		public const string Secondary = "pf-m-secondary";
		public const string Tertiary = "pf-m-tertiary";
		public const string Danger = "pf-m-danger";
		public const string Link = "pf-m-link";
		public const string Plain = "pf-m-plain";
		public const string Control = "pf-m-control";
		public const string Block = "pf-m-block";
		public const string Overflow = "pf-m-overflow";
		public const string ReadOnly = "pf-m-read-only";
		public const string Compact = "pf-m-compact";
		public const string Selectable = "pf-m-selectable";
		public const string Selected = "pf-m-selected";
		public const string PanelLeft = "pf-m-panel-left";
		public const string PanelBottom = "pf-m-panel-bottom";
		public const string Icon = "pf-m-icon";
		public const string Horizontal = "pf-m-horizontal";
		public const string Success = "pf-m-success";
		public const string Dark = "pf-m-dark";
		public const string Info = "pf-m-info";
		public const string Warning = "pf-m-warning";
		public const string Collapsed = "pf-m-collapsed";
		public const string Text = "pf-m-text";
		public const string Footer = "pf-m-footer";
		public const string Sm = "pf-m-sm";
		public const string Md = "pf-m-md";
		public const string Lg = "pf-m-lg";
		public const string Xl = "pf-m-xl";
		public const string Inside = "pf-m-inside";
		public const string Outside = "pf-m-outside";
		public const string FilterGroup = "pf-m-filter-group";
		public const string IconButtonGroup = "pf-m-icon-button-group";
		public const string ButtonGroup = "pf-m-button-group";
		public const string BulkSelect = "pf-m-bulk-select";
		public const string OverflowMenu = "pf-m-overflow-menu";
		public const string Pagination = "pf-m-pagination";
		public const string SearchFilter = "pf-m-search-filter";
		public const string ChipGroup = "pf-m-chip-group";
		public const string Blue = "pf-m-blue";
		public const string Green = "pf-m-green";
		public const string Orange = "pf-m-orange";
		public const string Red = "pf-m-red";
		public const string Purple = "pf-m-purple";
		public const string Cyan = "pf-m-cyan";
		public const string Outline = "pf-m-outline";
		public const string NoPadding = "pf-m-no-padding";
		public const string Default = "pf-m-default";
		public const string Hoverable = "pf-m-hoverable";
		public const string Flat = "pf-m-flat";
		public const string DisplayLarge = "pf-m-display-lg";
		public const string InProgress = "pf-m-in-progress";
		public const string Category = "pf-m-category";
		public const string Vertical = "pf-m-vertical";
		public const string Icons = "pf-m-icons";
		public const string AdjacentMonth = "pf-m-adjacent-month";
		public const string StartRange = "pf-m-start-range";
		public const string EndRange = "pf-m-end-range";
		public const string InRange = "pf-m-in-range";
		public const string Resizable = "pf-m-resizable";
		public const string Sticky = "pf-m-sticky";
		public const string StickyTop = "pf-m-sticky-top";
		public const string StickyBottom = "pf-m-sticky-bottom";
		public const string Light = "pf-m-light";
		public const string DisplaySummary = "pf-m-display-summary";
		public const string Box = "pf-m-box";
		public const string Fill = "pf-m-fill";
		public const string WidthAuto = "pf-m-width-auto";
		public const string Small = "pf-m-small";
		public const string Stacked = "pf-m-stacked";
		public const string LimitWidth = "pf-m-limit-width";

		public static string GetButtonStyleClass(ButtonStyle buttonStyle)
		{
			switch (buttonStyle)
			{
				case ButtonStyle.Primary: return Primary;
				case ButtonStyle.Secondary: return Secondary;
				case ButtonStyle.Tertiary: return Tertiary;
				case ButtonStyle.Danger: return Danger;
				case ButtonStyle.Link: return Link;
				case ButtonStyle.Plain: return Plain;
				case ButtonStyle.InlineLink: return $"{Inline} {Link}";
				case ButtonStyle.Control: return Control;
				case ButtonStyle.Warning: return Warning;
				default: throw new NotImplementedException();
			}
		}

		public static string GetPlacementCssClass(Placement placement)
		{
			switch (placement)
			{
				case Placement.Top: return Top;
				case Placement.Bottom: return Bottom;
				case Placement.Left: return Left;
				case Placement.Right: return Right;
				default: throw new NotImplementedException();
			}
		}

		public static string GetImportanceCssClass(Importance notificationLevel)
		{
			switch (notificationLevel)
			{
				case Importance.Default: return Default;
				case Importance.Information: return Info;
				case Importance.Success: return Success;
				case Importance.Warning: return Warning;
				case Importance.Danger: return Danger;
				default: throw new NotImplementedException();
			}
		}

		public static string GetImportanceIconCssClass(Importance notificationLevel)
		{
			switch (notificationLevel)
			{
				case Importance.Default: return "fa-info-circle";
				case Importance.Information: return "fa-info-circle";
				case Importance.Success: return "fa-check-circle";
				case Importance.Warning: return "fa-exclamation-triangle";
				case Importance.Danger: return "fa-exclamation-circle";
				default: throw new NotImplementedException();
			}
		}

		public static string GetSizeCssClass(Size size)
		{
			switch (size)
			{
				case Size.Default: return String.Empty;
				case Size.Small: return Sm;
				case Size.Medium: return Md;
				case Size.Large: return Lg;
				case Size.ExtraLarge: return Xl;
				default: throw new NotImplementedException();
			}
		}

		public static string GetInsideOutsideCssClass(InsideOutsidePosition size)
		{
			switch (size)
			{
				case InsideOutsidePosition.Default: return String.Empty;
				case InsideOutsidePosition.Inside: return Inside;
				case InsideOutsidePosition.Outside: return Outside;
				default: throw new NotImplementedException();
			}
		}

		public static string GetToolbarGroupTypeCssClass(ToolbarGroupType type)
		{
			switch (type)
			{
				case ToolbarGroupType.Default: return String.Empty;
				case ToolbarGroupType.Filter: return FilterGroup;
				case ToolbarGroupType.IconButton: return IconButtonGroup;
				case ToolbarGroupType.Button: return ButtonGroup;
				default: throw new NotImplementedException();
			}
		}

		public static string GetToolbarItemTypeCssClass(ToolbarItemType type)
		{
			switch (type)
			{
				case ToolbarItemType.Default: return String.Empty;
				case ToolbarItemType.BulkSelect: return BulkSelect;
				case ToolbarItemType.ChipGroup: return ChipGroup;
				case ToolbarItemType.OverflowMenu: return OverflowMenu;
				case ToolbarItemType.Pagination: return Pagination;
				case ToolbarItemType.SearchFilter: return SearchFilter;
				default: throw new NotImplementedException();
			}
		}

		public static string GetColourCssClass(NamedColour colour)
		{
			switch (colour)
			{
				case NamedColour.Default: return String.Empty;
				case NamedColour.Blue: return Blue;
				case NamedColour.Green: return Green;
				case NamedColour.Orange: return Orange;
				case NamedColour.Red: return Red;
				case NamedColour.Purple: return Purple;
				case NamedColour.Cyan: return Cyan;
				default: throw new NotImplementedException();
			}
		}
	}
}
