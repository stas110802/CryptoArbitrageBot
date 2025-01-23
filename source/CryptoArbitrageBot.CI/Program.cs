using CryptoArbitrageBot.Bot;
using CryptoArbitrageBot.CI.Commands;

FileManager.CreateAllNeedAppFolders();
var p = new MainCommands();
p.PrintCommands();
p.ReadActionCommandKey();
