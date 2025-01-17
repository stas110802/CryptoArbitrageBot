using CryptoArbitrageBot.Bot;
using CryptoArbitrageBot.ExchangeClients.Clients;
using CryptoArbitrageBot.ExchangesRestAPI.Options;


namespace CryptoArbitrage.ExchangeClients.UnitTest;

public class BinanceClientAuthApiTest
{
    private BinanceClient _client;
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
        _client = new BinanceClient(options);
    }
    
    [Test]
    public void GetAccountBalanceTest()
    {
        var balances = _client.GetAccountBalance();
        if(balances.Any())
            Assert.Pass();
        
        Assert.Fail();
    }
    
    [Test]
    public void CreateSellOrderTest()
    {
        var marketOrder = _client.CreateSellOrder("BTCUSDT", 0.0001m);
        var limitOrder = _client.CreateSellOrder("BTCUSDT", 0.0001m, 100000);
        if(marketOrder && limitOrder)
            Assert.Pass();
        else
            Assert.Fail();
    }
}