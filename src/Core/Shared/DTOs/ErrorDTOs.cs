namespace TTM.Core.Shared.DTOs;
public class ErrorDTO
{
    public ErrorInfo? error { get; set; }
}

public class ErrorInfo
{
    public string? logID { get; set; }
    public int statusCode { get; set; }
    public string? type { get; set; }
    public Dictionary<string, string>? messages { get; set; }
}