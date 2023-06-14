public class HowItWorks
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Creating Map:");
        Console.WriteLine("Keys of type INT, Values of type STRING");
        Map<int, string> map = new Map<int, string>();
        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            map.Add((int)day, day.ToString());
        }

        Console.WriteLine();
        Console.WriteLine("----  Keys  ----");
        Console.WriteLine(string.Join(Environment.NewLine, map.Keys.Select(k => $"{k.Key} : \"{k.Value}\"")));
        Console.WriteLine("---- Values ----");
        Console.WriteLine(string.Join(Environment.NewLine, map.Values.Select(v => $"\"{v.Key}\" : {v.Value}")));
        Console.WriteLine("----------------");

        Console.WriteLine();
        int key = 1;
        //REMOVE KEYS
        bool removed = map.Remove(key);
        Console.WriteLine($"Removed Key '{key}' - {removed}");
        //equal to
        removed = map.RemoveKey(key);
        Console.WriteLine($"Removing Key '{key}' - {removed} (already removed)");

        Console.WriteLine();
        string value = DayOfWeek.Tuesday.ToString();
        //REMOVE VALUES
        removed = map.Remove(value, MapDirection.Values);
        Console.WriteLine($"Removed Value \"{value}\" - {removed}");
        //equal to
        removed = map.RemoveValue(value);
        Console.WriteLine($"Removing Value \"{value}\" - {removed} (already removed)");

        Console.WriteLine();
        Console.WriteLine("---- EXCEPTIONS ----");
        Console.WriteLine();
        Console.WriteLine("Trying to remove null key:");
        try
        {
            map.Remove(null);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
        Console.WriteLine("Trying to remove null value:");
        try
        {
            map.Remove(null, MapDirection.Values);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine($"Trying to remove key of different type [Double]:");
        double toRemove = 1.234;
        try
        {
            map.Remove(toRemove);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
        Console.WriteLine($"Trying to remove value of different type [Double]:");
        try
        {
            map.Remove(toRemove, MapDirection.Values);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }

    }
}
