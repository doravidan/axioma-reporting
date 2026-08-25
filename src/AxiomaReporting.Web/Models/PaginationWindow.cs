namespace AxiomaReporting.Web.Models;

public sealed class PaginationWindow
{
  private PaginationWindow(int currentPage, int totalPages, int startPage, int endPage)
  {
    CurrentPage = currentPage;
    TotalPages = totalPages;
    StartPage = startPage;
    EndPage = endPage;
  }

  public int CurrentPage { get; }
  public int TotalPages { get; }
  public int StartPage { get; }
  public int EndPage { get; }
  public IEnumerable<int> Pages => Enumerable.Range(StartPage, EndPage - StartPage + 1);
  public bool HasPreviousWindow => StartPage > 1;
  public bool HasNextWindow => EndPage < TotalPages;
  public int PreviousWindowPage => Math.Max(1, StartPage - 1);
  public int NextWindowPage => Math.Min(TotalPages, EndPage + 1);

  public static PaginationWindow Create(int currentPage, int totalPages, int windowSize = 10)
  {
    if (totalPages < 1)
      throw new ArgumentOutOfRangeException(nameof(totalPages));
    if (windowSize < 1)
      throw new ArgumentOutOfRangeException(nameof(windowSize));

    var normalizedCurrentPage = Math.Clamp(currentPage, 1, totalPages);
    var startPage = ((normalizedCurrentPage - 1) / windowSize * windowSize) + 1;
    var endPage = Math.Min(totalPages, startPage + windowSize - 1);

    return new PaginationWindow(normalizedCurrentPage, totalPages, startPage, endPage);
  }
}
