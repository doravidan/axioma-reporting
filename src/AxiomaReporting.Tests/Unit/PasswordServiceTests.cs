using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

public class PasswordServiceTests
{
  private readonly PasswordService _sut = new();

  [Fact]
  public void HashPassword_ProducesVerifiableNonPlaintextHash()
  {
    var hash = _sut.HashPassword("Password123");

    hash.Should().NotBe("Password123");
    _sut.VerifyPassword("Password123", hash).Should().BeTrue();
    _sut.VerifyPassword("WrongPassword123", hash).Should().BeFalse();
  }

  [Theory]
  [InlineData("PaSw0rd!", true)]
  [InlineData("pass1", false)]
  [InlineData("Password", false)]
  [InlineData("12345678", false)]
  [InlineData("Password11!", false)]
  public void IsPasswordStrong_EnforcesClientPasswordPolicy(string password, bool expected)
  {
    _sut.IsPasswordStrong(password).Should().Be(expected);
  }

  [Fact]
  public void IsPasswordExpired_TreatsMissingOrOldPasswordAsExpired()
  {
    _sut.IsPasswordExpired(null).Should().BeTrue();
    _sut.IsPasswordExpired(DateTime.UtcNow.AddDays(-91)).Should().BeTrue();
    _sut.IsPasswordExpired(DateTime.UtcNow.AddDays(-10)).Should().BeFalse();
  }
}
