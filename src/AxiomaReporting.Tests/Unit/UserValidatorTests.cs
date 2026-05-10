using AxiomaReporting.Core.DTOs;
using AxiomaReporting.Infrastructure.Validators;
using FluentAssertions;
using FluentValidation.Results;

namespace AxiomaReporting.Tests.Unit;

public class UserValidatorTests
{
  private static EmployeeDto Valid() => new()
  {
    EmployeeCode = "1001",
    FirstName = "Avi",
    LastName = "Cohen",
    IdNumber = "123456782",
    RoleId = 1,
    UserRoleId = 6,
    StatusId = 1
  };

  [Fact]
  public void RejectsInvalidIsraeliId_WithHebrewMessage()
  {
    var dto = Valid();
    dto.IdNumber = "123456789"; // wrong checksum

    var result = new UserValidator().Validate(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(EmployeeDto.IdNumber)
      && e.ErrorMessage == "מספר תעודת זהות אינו תקין");
  }

  [Fact]
  public void AcceptsValidIsraeliId()
  {
    new UserValidator().Validate(Valid()).IsValid.Should().BeTrue();
  }

  [Fact]
  public void RejectsNonDigitEmployeeCode_WithHebrewMessage()
  {
    var dto = Valid();
    dto.EmployeeCode = "E001";

    var result = new UserValidator().Validate(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(EmployeeDto.EmployeeCode)
      && e.ErrorMessage == "קוד עובד חייב להכיל ספרות בלבד");
  }

  [Theory]
  [InlineData("0501234567")]
  [InlineData("0521234567")]
  [InlineData("037654321")]
  [InlineData("026543210")]
  [InlineData("0721234567")]
  public void AcceptsValidIsraeliPhone(string phone)
  {
    var dto = Valid();
    dto.Phone = phone;
    new UserValidator().Validate(dto).IsValid.Should().BeTrue();
  }

  [Theory]
  [InlineData("123")]
  [InlineData("0511234567")]   // 051 not allowed (5[02-9] excludes 1)
  [InlineData("0991234567")]   // 099 not in list
  [InlineData("050123456")]    // too short
  public void RejectsInvalidPhone_WithHebrewMessage(string phone)
  {
    var dto = Valid();
    dto.Phone = phone;
    var result = new UserValidator().Validate(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(EmployeeDto.Phone)
      && e.ErrorMessage == "מספר טלפון אינו תקין");
  }

  [Fact]
  public void EmptyPhoneIsAllowed()
  {
    var dto = Valid();
    dto.Phone = null;
    new UserValidator().Validate(dto).IsValid.Should().BeTrue();

    dto.Phone = "";
    new UserValidator().Validate(dto).IsValid.Should().BeTrue();
  }

  [Theory]
  [InlineData(0)]   // ראשון
  [InlineData(5)]   // שישי
  [InlineData(6)]   // שבת
  [InlineData(null)]
  public void AcceptsAllowedRestDays(int? restDay)
  {
    var dto = Valid();
    dto.RestDay = restDay;
    new UserValidator().Validate(dto).IsValid.Should().BeTrue();
  }

  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  [InlineData(4)]
  [InlineData(7)]
  [InlineData(-1)]
  public void RejectsForbiddenRestDays_WithHebrewMessage(int restDay)
  {
    var dto = Valid();
    dto.RestDay = restDay;

    var result = new UserValidator().Validate(dto);

    result.IsValid.Should().BeFalse();
    result.Errors.Should().Contain(e => e.PropertyName == nameof(EmployeeDto.RestDay)
      && e.ErrorMessage == "יום מנוחה חייב להיות ראשון, שישי או שבת");
  }
}
