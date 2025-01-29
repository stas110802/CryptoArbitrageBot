using CryptoArbitrageBot.Bot;
using CryptoArbitrageBot.Bot.Models.Configs;
using CryptoArbitrageBot.CI.Commands;
using CryptoArbitrageBot.ExchangeClients.Clients;
using CryptoArbitrageBot.ExchangesRestAPI.Options;

FileManager.CreateAllNeedAppFolders();
var mainCommands = new MainCommands();
mainCommands.PrintCommands();
mainCommands.ReadActionCommandKey();


                 