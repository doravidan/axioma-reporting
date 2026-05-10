using AxiomaReporting.Core.DTOs;
using FluentValidation;

namespace AxiomaReporting.Infrastructure.Validators;

/// <summary>
/// FluentValidation rules for the AllocationDto (Green Card).
/// Enforces the integer-only employment scope per client feedback Fix #17.
/// </summary>
public class AllocationValidator : AbstractValidator<AllocationDto>
{
  public AllocationValidator()
  {
    RuleFor(a => a.ProjectId)
      .GreaterThan(0).WithMessage("יש לבחור פרויקט");

    RuleFor(a => a.ProgramIds)
      .Must(values => values is { Count: > 0 } && values.Any(v => v > 0))
      .WithMessage("יש לבחור תוכנית");

    RuleFor(a => a)
      .Must(HasOutputDuration)
      .WithName(nameof(AllocationDto.OutputDuration))
      .WithMessage("יש לבחור משך תפוקה אחד לפחות");

    RuleFor(a => a.MonthlyEmploymentScope)
      .Must(BeIntegerOrNull)
      .WithMessage("יש להזין מספר שלם");

    RuleFor(a => a.AnnualEmploymentScope)
      .Must(BeIntegerOrNull)
      .WithMessage("יש להזין מספר שלם");

    RuleFor(a => a.MonthlyEmploymentScope)
      .GreaterThanOrEqualTo(0).When(a => a.MonthlyEmploymentScope.HasValue)
      .WithMessage("היקף פעילות חייב להיות גדול או שווה לאפס");

    RuleFor(a => a.AnnualEmploymentScope)
      .GreaterThanOrEqualTo(0).When(a => a.AnnualEmploymentScope.HasValue)
      .WithMessage("היקף פעילות חייב להיות גדול או שווה לאפס");

    RuleFor(a => a.DailyEmploymentScope)
      .InclusiveBetween(0, 9).When(a => a.DailyEmploymentScope.HasValue)
      .WithMessage("היקף יומי חייב להיות בין 0 ל-9");

    RuleFor(a => a.MonthlyRowAllocation)
      .GreaterThanOrEqualTo(0).When(a => a.MonthlyRowAllocation.HasValue)
      .WithMessage("הקצאת שורות חודשית חייבת להיות חיובית");

    RuleFor(a => a.AnnualRowAllocation)
      .GreaterThanOrEqualTo(0).When(a => a.AnnualRowAllocation.HasValue)
      .WithMessage("הקצאת שורות שנתית חייבת להיות חיובית");
  }

  private static bool BeIntegerOrNull(decimal? value)
  {
    if (!value.HasValue) return true;
    return value.Value == decimal.Truncate(value.Value);
  }

  private static bool HasOutputDuration(AllocationDto dto)
  {
    if (dto.OutputDurationValues is { Count: > 0 }) return true;
    return !string.IsNullOrWhiteSpace(dto.OutputDuration);
  }
}
