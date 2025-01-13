using System.Diagnostics;
using System.Globalization;
using CryptoArbitrageBot.ExchangeClients.Clients;
using CryptoArbitrageBot.ExchangeClients.Models;
using CryptoArbitrageBot.ExchangesRestAPI.Api;
using CryptoArbitrageBot.ExchangesRestAPI.Endpoints;
using CryptoArbitrageBot.ExchangesRestAPI.Options;
using Newtonsoft.Json.Linq;
using RestSharp;

var b = new BinanceClient(
    new BaseRestApi<BinanceRequest>(
        new BinanceOptions()));
b.GetCurrencyInfo("BTCUSDT");

Console.ReadKey();

