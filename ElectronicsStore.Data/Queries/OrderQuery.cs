namespace ElectronicsStore.Data.Queries;

public class OrderQuery
{
    public bool ShowNotAccepted { get; set; }
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
