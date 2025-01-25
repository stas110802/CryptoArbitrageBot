using CryptoArbitrageBot.Bot;
using CryptoArbitrageBot.CI.Commands;

FileManager.CreateAllNeedAppFolders();
var mainCommands = new MainCommands();
mainCommands.PrintCommands();
mainCommands.ReadActionCommandKey();
