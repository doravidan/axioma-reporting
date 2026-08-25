using AxiomaReporting.Infrastructure.Services;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

public class InstitutionSymbolNormalizerTests
{
  [Theory]
  [InlineData(" 001234 ", "001234")]
  [InlineData("0", "0")]
  [InlineData("872903", "872903")]
  public void InstitutionSymbol_IsTrimmedAndLeadingZeroesArePreserved(string input, string expected)
  {
    InstitutionSymbolNormalizer.TryNormalizeInstitution(input, out var normalized).Should().BeTrue();
    normalized.Should().Be(expected);
  }

  [Theory]
  [InlineData("")]
  [InlineData("12 34")]
  [InlineData("12-34")]
  [InlineData("ABC")]
  public void InstitutionSymbol_RejectsEmptyOrNonNumericValues(string input)
  {
    InstitutionSymbolNormalizer.TryNormalizeInstitution(input, out _).Should().BeFalse();
  }

  [Theory]
  [InlineData("00123", "123")]
  [InlineData("123", "123")]
  [InlineData("000", "0")]
  public void NumericComparisonKey_EquatesTextualNumericRepresentations(string input, string expected)
  {
    InstitutionSymbolNormalizer.NumericComparisonKey(input).Should().Be(expected);
  }
}
