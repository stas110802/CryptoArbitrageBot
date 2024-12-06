using System.Text;
using static System.String;

namespace CryptoArbitrageBot.ExchangesRestAPI.Options;

public class NiceHashOptions : IApiOptions
{
    public string BaseUri { get; set; } = "https://api2.nicehash.com";
    public Encoding Encoding { get; } = Encoding.Default;
    public string? PublicKey { get; set; } = Empty;
    public string? SecretKey { get; set; } = Empty;
    public string? OrganizationId { get; set; } = Empty;
}