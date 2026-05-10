using AxiomaReporting.Core.Validators;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

public class IsraeliIdValidatorTests
{
  // Each digit at index i is multiplied by (i % 2) + 1; sum-of-digits if product > 9; total must be % 10 == 0.
  // Sample valid IDs verified against multiple independent implementations.
  [Theory]
  [InlineData("000000018")]
  [InlineData("123456782")]
  public void IsValid_ReturnsTrue_ForValidIds(string id)
  {
    IsraeliIdValidator.IsValid(id).Should().BeTrue();
  }

  [Theory]
  [InlineData("123456789")]
  [InlineData("12345")]              // wrong checksum after padding
  [InlineData(null)]
  [InlineData("")]
  [InlineData("   ")]
  [InlineData("abcdefghi")]
  [InlineData("12345678a")]
  [InlineData("1234567890")]         // > 9 digits
  public void IsValid_ReturnsFalse_ForInvalidIds(string? id)
  {
    IsraeliIdValidator.IsValid(id).Should().BeFalse();
  }

  [Fact]
  public void IsValid_PadsShortIdsWithLeadingZeros()
  {
    // "18" padded to "000000018" should pass.
    IsraeliIdValidator.IsValid("18").Should().BeTrue();
  }
}
