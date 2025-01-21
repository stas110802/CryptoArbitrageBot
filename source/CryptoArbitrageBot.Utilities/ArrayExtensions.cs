using CryptoArbitrageBot.Utilities.Models;

namespace CryptoArbitrageBot.Utilities;

public static class ArrayExtensions
{
    public static void ForEach(this Array array, Action<Array, int[]> action)
    {
        if (array.LongLength == 0) return;
        var walker = new ArrayTraverse(array);
        do action(array, walker.Position);
        while (walker.Step());
    }
}