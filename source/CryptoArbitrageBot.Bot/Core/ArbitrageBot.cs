using CryptoArbitrageBot.Bot.Interfaces;
using CryptoArbitrageBot.Bot.Loggers;
using CryptoArbitrageBot.Bot.Models;
using CryptoArbitrageBot.Bot.Models.Logs;
using CryptoArbitrageBot.Bot.Types;
using CryptoArbitrageBot.ExchangeClients;
using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.Bot.Core;

public sealed class ArbitrageBot
{
    private BotLogger _botLogger;
    
    public ArbitrageBot(BotLogger botLogger)
    {
        _botLogger = botLogger;
    }

    public ILog StartArbitrage(ArbitrageInfo info)
    {
        ILog log;
        var currencyPair = $"{info.FirstCoin}{info.SecondCoin}";

        try
        {
            while (true)
            {
                
                var candle1 = info.FirstClient.GetCurrencyPrice(currencyPair);
                var candle2 = info.SecondClient.GetCurrencyPrice(currencyPair);

                decimal difference;
                string withdrawalAddress;
                IExchangeClient lowerPriceClient;
                IExchangeClient highestPriceClient;
                
                Console.Clear();
                var startLog = new ArbitrageLog(info, candle1, candle2);
                Console.WriteLine(startLog.ToString());
                ConsoleHelper.LoadingBar(10);
                Console.Clear();
                
                if (candle1 > candle2)
                {
                    lowerPriceClient = info.SecondClient;
                    withdrawalAddress = info.SecondClientAddress;
                    highestPriceClient = info.FirstClient;
                    difference = (candle1 / candle2 - 1) * 100;
                }
                else
                {
                    lowerPriceClient = info.FirstClient;
                    withdrawalAddress = info.FirstClientAddress;
                    highestPriceClient = info.SecondClient;
                    difference = (candle2 / candle1 - 1) * 100;
                }
                
                if (difference < 3)
                    continue;

                var firstOrder = lowerPriceClient.CreateSellOrder(
                    info.SecondCoin + info.FirstCoin, info.Amount);
                if (firstOrder is false)
                {
                    log = _botLogger.WriteErrorLog("[Арбитражный бот] первая сделка на покупки не удалась");
                    break;
                }

                //withdrawalCommission = lowerPriceClient.GetWithdrawalCommission(currencyPair);
                
                var withdrawal = lowerPriceClient.WithdrawalCurrency(
                    info.FirstCoin, info.Amount, withdrawalAddress);
                if (withdrawal is false)
                {
                    log = _botLogger.WriteErrorLog("[Арбитражный бот] перевод валюты на другую биржу не удался");
                    break;
                }

                Thread.Sleep(3000);

                // получить кол-во = amount - коммисия
                var newAmount = highestPriceClient.GetCurrencyBalance(
                    info.FirstCoin).AvailableBalance;

                var secondOrder = highestPriceClient.CreateSellOrder(currencyPair, newAmount);
                if (secondOrder is false)
                {
                    log = _botLogger.WriteErrorLog("[Арбитражный бот] вторая сделка на продажу не удалась");
                    break;
                }
                
                log = new ArbitrageLog(info, candle1, candle2);
                break;
            }
        }
        catch (Exception e)
        {
            log = _botLogger.WriteErrorLog(e.Message);
        }

        return log;
    }
    
}