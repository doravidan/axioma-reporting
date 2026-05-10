using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

/// <summary>
/// Extended coverage for <see cref="PasswordService"/> that does NOT duplicate
/// the cases already present in <see cref="PasswordServiceTests"/>.
/// </summary>
public class PasswordServiceExtendedTests
{
  private readonly PasswordService _sut = new();

  // -------------------------------------------------------------------------
  // IsPasswordStrong – boundary / edge cases
  // -------------------------------------------------------------------------

  [Fact]
  public void IsPasswordStrong_ExactlyEightCharsWithRequiredComposition_IsValid()
  {
    _sut.IsPasswordStrong("Abc123!@").Should().BeTrue(
      "exactly 8 characters with uppercase, lowercase, digit and symbol satisfies all policy criteria");
  }

  [Fact]
  public void IsPasswordStrong_SevenChars_Fails()
  {
    // One character short of the minimum length
    _sut.IsPasswordStrong("abc1234").Should().BeFalse(
      "a password with fewer than 8 characters must be rejected regardless of composition");
  }

  [Fact]
  public void IsPasswordStrong_AllLetters_Fails()
  {
    // 8 letters but no digit
    _sut.IsPasswordStrong("abcdefgh").Should().BeFalse(
      "a password with no digit must be rejected even when the length requirement is satisfied");
  }

  [Fact]
  public void IsPasswordStrong_AdjacentRepeatedCharacter_Fails()
  {
    _sut.IsPasswordStrong("Abc1123!").Should().BeFalse(
      "the client policy rejects the same character twice or more in a row");
  }

  [Fact]
  public void IsPasswordStrong_AllDigits_Fails()
  {
    // 8 digits but no letter
    _sut.IsPasswordStrong("12345678").Should().BeFalse(
      "a password with no letter must be rejected even when the length requirement is satisfied");
  }

  // The [Theory] covering additional combinations is intentionally placed here as
  // parameterised data to complement the single-case facts above.
  [Theory]
  [InlineData("Ab1cdef!", true)]   // 8 chars, mixed
  [InlineData("AAAAAAAA", false)]  // all uppercase letters, no digit
  [InlineData("1234567",  false)]  // 7 digits, no letter
  [InlineData("a1",       false)]  // 2 chars, has letter + digit but too short
  public void IsPasswordStrong_Theory_AdditionalCombinations(string password, bool expected)
  {
    _sut.IsPasswordStrong(password).Should().Be(expected);
  }

  // -------------------------------------------------------------------------
  // IsPasswordExpired – boundary conditions
  // -------------------------------------------------------------------------

  [Fact]
  public void IsPasswordExpired_Exactly90DaysOld_IsExpired()
  {
    // Implementation: (UtcNow - lastPasswordChange).TotalDays > PASSWORD_EXPIRY_DAYS (90)
    //
    // DateTime.UtcNow.AddDays(-90) produces a timestamp 90 * 24 * 3600 seconds in the
    // past. By the time the assertion runs, UtcNow has advanced by a few milliseconds,
    // so TotalDays is slightly *above* 90.0, making the > 90 comparison true.
    // In practice "exactly 90 days ago" is always expired because clock time has
    // advanced past the threshold by the time the check executes.
    // The 91-days test covers the firm expired boundary; we document the 90-day
    // edge case here as a floating-point timing artefact that the implementation
    // treats as expired.
    _sut.IsPasswordExpired(DateTime.UtcNow.AddDays(-90)).Should().BeTrue(
      "at the instant of check, 90 days ago is already slightly over the 90-day threshold");
  }

  [Fact]
  public void IsPasswordExpired_91DaysOld_IsExpired()
  {
    // One day past the boundary must be treated as expired
    _sut.IsPasswordExpired(DateTime.UtcNow.AddDays(-91)).Should().BeTrue(
      "a password changed 91 days ago has exceeded the 90-day expiry threshold");
  }

  [Fact]
  public void IsPasswordExpired_89DaysOld_IsNotExpired()
  {
    // One day inside the safe zone must not be treated as expired
    _sut.IsPasswordExpired(DateTime.UtcNow.AddDays(-89)).Should().BeFalse(
      "a password changed 89 days ago is still within the 90-day validity window");
  }

  [Fact]
  public void IsPasswordExpired_NullLastChanged_IsExpired()
  {
    // A null last-changed date means the user has never set a password
    // (or the record pre-dates tracking); must be treated as expired.
    _sut.IsPasswordExpired(null).Should().BeTrue(
      "a null last-change date must be treated as an expired password");
  }

  [Fact]
  public void IsPasswordExpired_Today_IsNotExpired()
  {
    // A password changed today (0 elapsed days) must never be considered expired
    _sut.IsPasswordExpired(DateTime.UtcNow).Should().BeFalse(
      "a password changed today cannot possibly have exceeded the 90-day expiry window");
  }

  // Edge case: far-future date (clock skew or test data anomaly)
  [Fact]
  public void IsPasswordExpired_FutureDate_IsNotExpired()
  {
    _sut.IsPasswordExpired(DateTime.UtcNow.AddDays(1)).Should().BeFalse(
      "a last-change date in the future should not trigger expiry");
  }
}
