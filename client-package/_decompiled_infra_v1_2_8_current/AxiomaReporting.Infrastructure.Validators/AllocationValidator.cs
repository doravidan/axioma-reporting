using System.Collections.Generic;
using System.Linq;
using AxiomaReporting.Core.DTOs;
using FluentValidation;

namespace AxiomaReporting.Infrastructure.Validators;

public class AllocationValidator : AbstractValidator<AllocationDto>
{
	public AllocationValidator()
	{
		RuleFor((AllocationDto a) => a.ProjectId).GreaterThan(0).WithMessage("יש לבחור פרויקט");
		RuleFor((AllocationDto a) => a.ProgramIds).Must((List<int> values) => values != null && values.Count > 0 && values.Any((int v) => v > 0)).WithMessage("יש לבחור תוכנית");
		RuleFor((AllocationDto a) => a).Must(HasOutputDuration).WithName("OutputDuration").WithMessage("יש לבחור משך תפוקה אחד לפחות");
		RuleFor((AllocationDto a) => a.MonthlyEmploymentScope).Must(BeIntegerOrNull).WithMessage("יש להזין מספר שלם");
		RuleFor((AllocationDto a) => a.AnnualEmploymentScope).Must(BeIntegerOrNull).WithMessage("יש להזין מספר שלם");
		RuleFor((AllocationDto a) => a.DailyEmploymentScope).Must(BeIntegerOrNull).WithMessage("יש להזין מספר שלם");
		RuleFor((AllocationDto a) => a.MonthlyEmploymentScope).GreaterThanOrEqualTo(0m).When<AllocationDto, decimal?>((AllocationDto a) => a.MonthlyEmploymentScope.HasValue).WithMessage("היקף פעילות חייב להיות גדול או שווה לאפס");
		RuleFor((AllocationDto a) => a.AnnualEmploymentScope).GreaterThanOrEqualTo(0m).When<AllocationDto, decimal?>((AllocationDto a) => a.AnnualEmploymentScope.HasValue).WithMessage("היקף פעילות חייב להיות גדול או שווה לאפס");
		RuleFor((AllocationDto a) => a.DailyEmploymentScope).InclusiveBetween(0m, 9m).When<AllocationDto, decimal?>((AllocationDto a) => a.DailyEmploymentScope.HasValue).WithMessage("היקף יומי חייב להיות בין 0 ל-9");
		RuleFor((AllocationDto a) => a.MonthlyRowAllocation).GreaterThanOrEqualTo(0).When<AllocationDto, int?>((AllocationDto a) => a.MonthlyRowAllocation.HasValue).WithMessage("הקצאת שורות חודשית חייבת להיות חיובית");
		RuleFor((AllocationDto a) => a.AnnualRowAllocation).GreaterThanOrEqualTo(0).When<AllocationDto, int?>((AllocationDto a) => a.AnnualRowAllocation.HasValue).WithMessage("הקצאת שורות שנתית חייבת להיות חיובית");
	}

	private static bool BeIntegerOrNull(decimal? value)
	{
		if (!value.HasValue)
		{
			return true;
		}
		return value.Value == decimal.Truncate(value.Value);
	}

	private static bool HasOutputDuration(AllocationDto dto)
	{
		List<decimal> outputDurationValues = dto.OutputDurationValues;
		if (outputDurationValues != null && outputDurationValues.Count > 0)
		{
			return true;
		}
		return !string.IsNullOrWhiteSpace(dto.OutputDuration);
	}
}
