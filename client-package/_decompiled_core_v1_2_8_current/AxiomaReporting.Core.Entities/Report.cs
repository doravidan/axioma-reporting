using System;
using System.Collections.Generic;
using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class Report : BaseEntity
{
	public int UserId { get; set; }

	public User? User { get; set; }

	public int ReportingMonthId { get; set; }

	public ReportingMonth? ReportingMonth { get; set; }

	public int StatusId { get; set; }

	public ReportStatus? Status { get; set; }

	public DateTime? SubmittedAt { get; set; }

	public DateTime? ApprovedAt { get; set; }

	public int? ApprovedBy { get; set; }

	public User? ApprovedByUser { get; set; }

	public string? RejectionReason { get; set; }

	public DateTime? RejectedAt { get; set; }

	public int? RejectedBy { get; set; }

	public User? RejectedByUser { get; set; }

	public bool ImportedFromExcel { get; set; }

	public bool IsArchived { get; set; }

	public byte[] RowVersion { get; set; } = Array.Empty<byte>();


	public ICollection<ReportRow> ReportRows { get; set; } = new List<ReportRow>();

}
