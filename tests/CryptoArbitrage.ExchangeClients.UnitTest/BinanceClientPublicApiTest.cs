using CryptoArbitrageBot.Bot;
using CryptoArbitrageBot.ExchangeClients.Clients;
using CryptoArbitrageBot.ExchangesRestAPI.Options;


namespace CryptoArbitrage.ExchangeClients.UnitTest;

public class BinanceClientPublicApiTest
{
    private BinanceClient _publicClient;
    private ConfigInitializer _cfg;
    
    [SetUp]
    public void Setup()
    {
        _cfg = new ConfigInitializer();
        var configOptions = _cfg.GetExchangeConfig(ExchangeType.BinanceTestnet);
        var options = configOptions ?? new ExchangeApiOptions
        {
            BaseUri = "https://testnet.binance.vision"
        };
        
        _publicClient = new BinanceClient(options);
    }

    [Test]
    public void CurrencyPriceTest()
    {
        var btcPrice = _publicClient.GetCurrencyPrice("BTCUSDT");
        var ethPrice = _publicClient.GetCurrencyPrice("ETHUSDT");
        
        if(btcPrice > 0 && ethPrice > 0)
            Assert.Pass();
        
        Assert.Fail();
    }
    
    [Test]
    public void CurrencyInformationTest()
    {
        var currencyInfo = _publicClient.GetCurrencyInfo("BTCUSDT");
        if(currencyInfo.SellingPrice > 0)
            Assert.Pass();
        
        Assert.Fail();
    }
}