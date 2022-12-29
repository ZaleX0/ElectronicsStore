namespace ElectronicsStore.Data.Queries;

public enum SortDirection
{
    ASC,
    DESC
}

public class ProductQuery
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
