using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Loggers;
using CryptoArbitrageBot.Bot.Models;
using CryptoArbitrageBot.Bot.Models.Logs;
using CryptoArbitrageBot.ExchangeClients.Models;
using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.Bot.Core;

public class SLTPBot
{
    private BotLogger _botLogger;
    private ConfigManager _configManager;
    private SLTPInfo _sltpInfo;
    
    public SLTPBot(SLTPInfo sltpInfo, ConfigManager configManager, BotLogger botLogger)
    {
        _sltpInfo = sltpInfo;
        _configManager = configManager;
        _botLogger = botLogger;
    }
    
    public ILog StartSLTP()
    {
        ILog log;
        var currency = _sltpInfo.FirstCoin + _sltpInfo.SecondCoin;
        var client = _sltpInfo.Client;
        
        try
        {
            var launchLog = GetTotalCurrencyInfo();
            _botLogger.AddLog(launchLog);

            while (true)
            {
                var balance = client.GetAccountBalance()
                    .FirstOrDefault(x => x.Currency == _sltpInfo.FirstCoin);
                if (balance is null)
                   throw new Exception("Нулевой баланс!");
                    
                var parseLog = GetTotalCurrencyInfo(balance);

                var currentPrice = parseLog.TotalPrice;
                if (currentPrice <= _sltpInfo.UpperPrice &&
                    currentPrice >= _sltpInfo.BottomPrice)
                {
                    Thread.Sleep(10000);
                    continue;
                }
                
                // if (balance.IsActive is false)
                // {
                //     log = WriteErrorLog("Заброкированнный баланс!");
                //     break;
                // }

                var amount = balance.AvailableBalance;
                if (amount < _sltpInfo.BalanceLimit)
                {
                    ConsoleHelper.LoadingBar(10);
                    continue;
                }

                // create sell order
                var orderResult = client.CreateSellOrder(currency, amount);// MARKET ORDER
                if (orderResult)
                {
                    var orderLog = new OrderLog(
                    options: _sltpInfo, sellPrice: currentPrice, amount: amount);
                    _botLogger.AddLog(orderLog);
                    log = orderLog;
                }
                else
                {
                    log = _botLogger.WriteErrorLog($"Неудачная попытка разместить ордер на продажу {_sltpInfo.FirstCoin}-{_sltpInfo.SecondCoin}");
                }
            }
        }
        catch (Exception error)
        {
            log = _botLogger.WriteErrorLog(error.Message);
        }

        return log;
    }
    
    private CurrencyLog GetTotalCurrencyInfo(CurrencyBalance? balance = null)
    {
        var currency = _sltpInfo.FirstCoin + _sltpInfo.SecondCoin;
        var client = _sltpInfo.Client;
        
        var price = client.GetCurrencyPrice(currency); 
        balance ??= client
            .GetAccountBalance()
            .FirstOrDefault(x => x.Currency == _sltpInfo.FirstCoin);
        var accBalance = balance?.AvailableBalance ?? 0 ;
        var log = new CurrencyLog(_sltpInfo, price, accBalance);

        return log;
    }
}