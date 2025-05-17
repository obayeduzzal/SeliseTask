namespace TTM.Core.Infrastructure.SignalR;

public class SignalRSettings
{
    public class Backplane
    {
        public string? Provider { get; set; }
        public string? Connection { get; set; }
    }

    public bool UseBackplane { get; set; }
}