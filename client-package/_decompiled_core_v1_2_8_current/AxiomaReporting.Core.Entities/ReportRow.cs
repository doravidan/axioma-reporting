using System;
using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class ReportRow : BaseEntity
{
	public int ReportId { get; set; }

	public Report? Report { get; set; }

	public int? AllocationId { get; set; }

	public Allocation? Allocation { get; set; }

	public int SequenceNumber { get; set; }

	public DateTime MeetingDate { get; set; }

	public decimal MeetingDuration { get; set; }

	public int DistrictId { get; set; }

	public District? District { get; set; }

	public int LocalityId { get; set; }

	public Locality? Locality { get; set; }

	public int FrameworkId { get; set; }

	public Framework? Framework { get; set; }

	public int EducationalProgramId { get; set; }

	public EducationalProgram? EducationalProgram { get; set; }

	public int DomainId { get; set; }

	public Domain? Domain { get; set; }

	public int Subject1Id { get; set; }

	public Subject? Subject1 { get; set; }

	public int? Subject2Id { get; set; }

	public Subject? Subject2 { get; set; }

	public int? DiscussionCodeId { get; set; }

	public DiscussionCode? DiscussionCode { get; set; }

	public int? ConclusionClassId { get; set; }

	public SchoolClass? ConclusionClass { get; set; }

	public int? ConclusionFrameworkId { get; set; }

	public Framework? ConclusionFramework { get; set; }

	public int? ConclusionLocationId { get; set; }

	public LocalityDistrictNational? ConclusionLocation { get; set; }

	public int? GradeLevelId { get; set; }

	public GradeLevel? GradeLevel { get; set; }

	public int? ClassId { get; set; }

	public SchoolClass? Class { get; set; }

	public int? ReportTypeId { get; set; }

	public ReportType? ReportType { get; set; }

	public string? Notes { get; set; }

	public byte[] RowVersion { get; set; } = Array.Empty<byte>();

}
