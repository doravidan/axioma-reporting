namespace AxiomaReporting.Core.Entities;

public class ProjectProgram
{
	public int ProjectId { get; set; }

	public Project? Project { get; set; }

	public int ProgramId { get; set; }

	public Program? Program { get; set; }
}
