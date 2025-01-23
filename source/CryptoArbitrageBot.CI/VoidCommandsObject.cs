using CryptoArbitrageBot.CI.Utilities;

namespace CryptoArbitrageBot.CI;

public abstract class VoidCommandsObject
{
    protected VoidCommandsObject()
    {
        InitVoidCommands();
    }
    
    public abstract void PrintCommands();
    
    public void ReadActionCommandKey()
    {
        var key = new ConsoleKey();
        while (key != ConsoleKey.Q)
        {
            key = Console.ReadKey(true).Key;
            InvokeActionCommand(key);
        }
    }

    private void InitVoidCommands()
    {
        var type = GetType();
        if (this == null) throw new ArgumentNullException(type.Name);
        VoidCommands = CommandHelper.GetConsoleCommands<Action>(this, type);
    }
   
    private Dictionary<ConsoleKey, Action>? VoidCommands { get; set; }

    private void InvokeActionCommand(ConsoleKey key)
    {
        if (VoidCommands != null)
            InvokeMethodByKey(key);
        else
            throw new NullReferenceException($"{nameof(VoidCommands)} uninitialized");
    }
    
    private void InvokeMethodByKey(ConsoleKey key)
    {
        var action = VoidCommands!.ContainsKey(key) ? VoidCommands[key] : null;
        if (action == null) return;
        
        action.Invoke();
        PrintCommands();// нужно ли это??
    }
}