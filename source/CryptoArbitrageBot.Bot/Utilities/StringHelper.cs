namespace CryptoArbitrageBot.Bot.Utilities;

public static class StringHelper
{
    /// <summary>
    /// Returns all string values of a class
    /// </summary>
    /// <param name="obj">Your class object</param>
    public static string[] GetStringValues<T>(T obj)
        where T : class
    {
        var type = obj.GetType();
        var props = type.GetProperties();
        var result = new string[props.Length];

        for (var i = 0; i < props.Length; i++)
        {
            var value = props[i].GetValue(obj, null)?.ToString();
            if(value is not null)
                result[i] = value;
        }
           
        return result;
    }
    
    public static bool IsAnyNullOrEmpty(object? myObject)
    {
        if(myObject == null)
            return true;
        
        foreach(var pi in myObject.GetType().GetProperties())
        {
            if (pi.PropertyType != typeof(string)) 
                continue;
            
            var value = (string)pi.GetValue(myObject);
            if(string.IsNullOrEmpty(value))
                return true;
        }
        return false;
    }
}