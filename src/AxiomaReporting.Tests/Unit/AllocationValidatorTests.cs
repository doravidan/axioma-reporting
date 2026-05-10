using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Infrastructure.Validators;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

public class AllocationValidatorTests
{
  private static AllocationDto Base() => new()
  {
    UserId = 1,
    ProjectId = 1,
    ProgramIds = new List<int> { 1 },
    OutputDurationValues = new List<decimal> { 1m }
  };

  [Theory]
  [InlineData(1.5)]
  [InlineData(2.25)]
  [InlineData(0.1)]
  public void RejectsNonIntegerMonthlyScope_WithHebrewMessage(double scope)
  {
    var dto = Base();
    dto.MonthlyEmploymentScope = (decimal)scope;

    var result = new AllocationValidator().Validate(dto);
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(AllocationDto.MonthlyEmploymentScope)
      && e.ErrorMessage == "יש להזין מספר שלם");
  }

  [Theory]
  [InlineData(1.5)]
  [InlineData(2.25)]
  public void RejectsNonIntegerAnnualScope_WithHebrewMessage(double scope)
  {
    var dto = Base();
    dto.AnnualEmploymentScope = (decimal)scope;

    var result = new AllocationValidator().Validate(dto);
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(AllocationDto.AnnualEmploymentScope)
      && e.ErrorMessage == "יש להזין מספר שלם");
  }

  [Theory]
  [InlineData(0)]
  [InlineData(1)]
  [InlineData(50)]
  public void AcceptsIntegerScope(int scope)
  {
    var dto = Base();
    dto.MonthlyEmploymentScope = scope;
    dto.AnnualEmploymentScope = scope * 12;

    new AllocationValidator().Validate(dto).IsValid.Should().BeTrue();
  }

  [Fact]
  public void NullScopesAreAllowed()
  {
    var dto = Base();
    new AllocationValidator().Validate(dto).IsValid.Should().BeTrue();
  }

  [Fact]
  public void RejectsMissingProject()
  {
    var dto = Base();
    dto.ProjectId = 0;

    var result = new AllocationValidator().Validate(dto);
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(AllocationDto.ProjectId)
      && e.ErrorMessage == "יש לבחור פרויקט");
  }

  [Fact]
  public void RejectsMissingProgram()
  {
    var dto = Base();
    dto.ProgramIds.Clear();

    var result = new AllocationValidator().Validate(dto);
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(AllocationDto.ProgramIds)
      && e.ErrorMessage == "יש לבחור תוכנית");
  }

  [Fact]
  public void RejectsMissingOutputDuration()
  {
    var dto = Base();
    dto.OutputDurationValues.Clear();
    dto.OutputDuration = null;

    var result = new AllocationValidator().Validate(dto);
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(AllocationDto.OutputDuration)
      && e.ErrorMessage == "יש לבחור משך תפוקה אחד לפחות");
  }

  [Fact]
  public void RejectsDailyScopeAbove9()
  {
    var dto = Base();
    dto.DailyEmploymentScope = 10m;

    var result = new AllocationValidator().Validate(dto);
    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(AllocationDto.DailyEmploymentScope));
  }
}
