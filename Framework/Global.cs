using System.Globalization;

namespace StrokeControl.Framework;

public static class Global
{
    public static bool DebugMode = false;
    
    private static readonly List<string> Console = new List<string>(); // The user-facing console, containing basic log information
    private static readonly List<string> Terminal = new List<string>(); // The power-user terminal, containing debug information

    public static readonly object LockJson = new object();
    public static readonly object LockSql = new object();
    private static readonly object LockDebug = new object();

    public static void LogConsole(string input)
    {
        Console.Add(TimestampEntry(input, false));
        Terminal.Add(TimestampEntry(input, true));
        LogDebug(input, false);
    }

    public static void LogTerminal(string input, bool command = false)
    {
        Terminal.Add(TimestampEntry(!command ? input : $"> \"{input}\"", true));
        LogDebug(input, false);
    }

    public static void LogDebug(string input, bool debugMessage = true)
    {
        if(!DebugMode){ return; }

        var debugEntry = TimestampEntry(debugMessage ? "[DEBUG]" + input : input, true);

        lock (LockDebug)
        {
            File.AppendAllText(DateTime.Now.ToString("yyyyMMdd") + "_debug.txt", debugEntry + Environment.NewLine);
        }
        
        Terminal.Add(debugEntry);
    }

    private static string TimestampEntry(string input, bool verbose)
    {
        var timestamp = verbose
            ? DateTime.Now.ToString("s", CultureInfo.InvariantCulture)
            : DateTime.Now.ToString("hh:mm", CultureInfo.InvariantCulture);

        var output = $"[{timestamp}] {input}".Trim();

        if (output.Length > 512)
        {
            output = output[..507].TrimEnd() + "[...]";
        }

        return output;
    }

    public static List<string> GetLog(int count)
    {
        return GetLogSubset(Console, count);
    }

    public static List<string> GetTerminal(int count)
    {
        return GetLogSubset(Terminal, count);
    }

    private static List<string> GetLogSubset(List<string> list, int count)
    {
        count = count < 1 ? 1 : count;
        
        return list.Count <= count ? list : list.GetRange(list.Count - count, count);
    }
}