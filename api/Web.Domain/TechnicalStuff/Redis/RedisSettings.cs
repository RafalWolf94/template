namespace Web.Domain.TechnicalStuff.Redis;

public class RedisSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Password { get; set; } = string.Empty;
    public string ChannelPrefix { get; set; } = string.Empty;
}