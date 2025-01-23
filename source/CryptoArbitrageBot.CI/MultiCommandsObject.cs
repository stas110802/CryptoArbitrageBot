using CryptoArbitrageBot.CI.Utilities;

namespace CryptoArbitrageBot.CI;

public abstract class MultiCommandsObject<TResult> : VoidCommandsObject
    where TResult : class
{
    protected MultiCommandsObject()
    {
        
    }
    
    public TResult? ReadFuncCommandKey()
    {
        var key = new ConsoleKey();
        TResult? result = null;
        
        while (key != ConsoleKey.Q &&
               result == null)
        {
            key = Console.ReadKey(true).Key;
            result = InvokeFuncCommand(key);
        }
        
        return result;
    }
    
    protected void InitFuncCommands<TValue>(TValue target)
    {
        if (target == null) 
            throw new ArgumentNullException(nameof(target));
        FuncCommands = CommandHelper.GetConsoleCommands<Func<TResult>>(target, target.GetType());
    }

    private Dictionary<ConsoleKey, Func<TResult>>? FuncCommands { get; set; }
    
    private TResult? InvokeFuncCommand(ConsoleKey key)
    {
        if (FuncCommands == null) 
            throw new NullReferenceException("Commands uninitialized. You need to create at least one command");
        
        var res = GetMethodValueByKey(key);
            
        return res;
    }
    
    private TResult? GetMethodValueByKey(ConsoleKey key)
    {
        if (FuncCommands == null)
            throw new ArgumentNullException($"{FuncCommands}");
        
        var action = FuncCommands.GetValueOrDefault(key);

        return action?.Invoke();
    }
}