using System.Collections.Generic;
using AxiomaReporting.Core.Entities.Base;

namespace AxiomaReporting.Core.Entities;

public class Project : LookupEntity
{
	public ICollection<ProjectProgram> ProjectPrograms { get; set; } = new List<ProjectProgram>();

}
