using System;
using System.Collections.Generic;

namespace AxiomaReporting.Web.Models;

public class AuditLogListViewModel
{
	public List<AuditLogListItem> Items { get; set; } = new List<AuditLogListItem>();


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

	public string? Action { get; set; }

	public string? EntityType { get; set; }

	public string? EntityId { get; set; }

	public int? ActorUserId { get; set; }

	public DateTime? FromDate { get; set; }

	public DateTime? ToDate { get; set; }
}
