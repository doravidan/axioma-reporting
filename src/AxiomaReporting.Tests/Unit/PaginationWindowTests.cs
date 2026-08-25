using AxiomaReporting.Web.Models;
using FluentAssertions;

namespace AxiomaReporting.Tests.Unit;

public class PaginationWindowTests
{
  [Theory]
  [InlineData(1, 704, 1, 10)]
  [InlineData(10, 704, 1, 10)]
  [InlineData(11, 704, 11, 20)]
  [InlineData(347, 704, 341, 350)]
  [InlineData(704, 704, 701, 704)]
  public void Create_LimitsVisiblePageNumbersToTen(
    int currentPage,
    int totalPages,
    int expectedStart,
    int expectedEnd)
  {
    var pager = PaginationWindow.Create(currentPage, totalPages);

    pager.StartPage.Should().Be(expectedStart);
    pager.EndPage.Should().Be(expectedEnd);
    pager.Pages.Should().HaveCountLessThanOrEqualTo(10);
    pager.Pages.Should().Equal(Enumerable.Range(expectedStart, expectedEnd - expectedStart + 1));
  }

  [Fact]
  public void Create_ExposesAdjacentWindowNavigationWithoutOverflow()
  {
    var middle = PaginationWindow.Create(347, 704);
    var last = PaginationWindow.Create(704, 704);

    middle.HasPreviousWindow.Should().BeTrue();
    middle.PreviousWindowPage.Should().Be(340);
    middle.HasNextWindow.Should().BeTrue();
    middle.NextWindowPage.Should().Be(351);
    last.HasNextWindow.Should().BeFalse();
    last.NextWindowPage.Should().Be(704);
  }
}
