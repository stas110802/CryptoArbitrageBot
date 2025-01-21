using CryptoArbitrageBot.Utilities;

namespace CryptoArbitrageBot.Bot.Types;

public class SubjectType : BaseType
{
    private SubjectType(string value) : base(value) { }
    
    public static readonly SubjectType Error = new("Ошибка");
    public static readonly SubjectType WithdrawError = new("Ошибка во время перевода монеты на счет");
    public static readonly SubjectType SellError = new("Ошибка при продаже монеты");
    
    public static readonly SubjectType Sell = new("Продажа монеты");
    public static readonly SubjectType SellActivate = new("Активация продажи");
}