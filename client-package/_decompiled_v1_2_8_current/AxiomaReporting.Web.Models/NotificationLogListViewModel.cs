using System;
using System.Collections.Generic;

namespace AxiomaReporting.Web.Models;

public class NotificationLogListViewModel
{
	public List<NotificationLogListItem> Items { get; set; } = new List<NotificationLogListItem>();


	public int Page { get; set; }

	public int PageSize { get; set; }

	public int TotalCount { get; set; }

	public int TotalPages
	{
		get
		{
			if (PageSize > 0)
			{
				return (int)Math.Ceiling((double)TotalCount / (double)PageSize);
			}
			return 0;
		}
	}

	public string? Type { get; set; }

	public string? Status { get; set; }

	public string? TemplateType { get; set; }

	public DateTime? FromDate { get; set; }

	public DateTime? ToDate { get; set; }

	public string? RecipientEmail { get; set; }

	public int? UserId { get; set; }
}
