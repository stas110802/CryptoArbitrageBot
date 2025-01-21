namespace CryptoArbitrageBot.Bot.Utilities;

public class FolderPathList
{
    public static string OrdersFolder => $@"{LogsFolder}orders\";
    public static string ErrorsFolder => $@"{LogsFolder}errors\";
    public static string LaunchesFolder => $@"{LogsFolder}launches\";
   
    public static string ProjectFolder =>
        Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
    
    private static string LogsFolder => $@"{ProjectFolder}\logs\";
}