using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AxiomaReporting.Web.Helpers;

public static class SelectListProviders
{
	public static IReadOnlyList<SelectListItem> RestDayOptions { get; } = new List<SelectListItem>
	{
		new SelectListItem
		{
			Value = string.Empty,
			Text = "— ללא —"
		},
		new SelectListItem
		{
			Value = "0",
			Text = "ראשון"
		},
		new SelectListItem
		{
			Value = "5",
			Text = "שישי"
		},
		new SelectListItem
		{
			Value = "6",
			Text = "שבת"
		}
	};


	public static List<SelectListItem> RestDayOptionsWithSelection(int? selectedValue)
	{
		string sel = selectedValue?.ToString() ?? string.Empty;
		return RestDayOptions.Select((SelectListItem o) => new SelectListItem
		{
			Value = o.Value,
			Text = o.Text,
			Selected = (o.Value == sel)
		}).ToList();
	}

	public static List<SelectListItem> TriStateBoolOptions(bool? selected, string allText = "-- הכל --", string yesText = "כן", string noText = "לא")
	{
		return new List<SelectListItem>
		{
			new SelectListItem
			{
				Value = string.Empty,
				Text = allText,
				Selected = !selected.HasValue
			},
			new SelectListItem
			{
				Value = "true",
				Text = yesText,
				Selected = selected.GetValueOrDefault()
			},
			new SelectListItem
			{
				Value = "false",
				Text = noText,
				Selected = (selected == false)
			}
		};
	}
}
