using Nancy.ViewEngines;

namespace StrokeControl.Framework;

public static class Terminal
{
    public static void Post(string input)
    {
        var split = SplitInput(input);
        var command = split[0];
        var args = split[1];
        
        Global.LogTerminal(input, true);
        
        switch (command.ToLower())
        {
            case "debugmode":
                ToggleDebug(args);
                break;
            default:
                InvalidCommand();
                break;
        }
    }

    private static string[] SplitInput(string input)
    {
        if (string.IsNullOrEmpty(input)) // Properly handle an empty input
        {
            return new string[] {"", ""};
        }

        input = input.TrimStart(); // Remove leading spaces
        var firstSpaceIndex = input.IndexOf(' '); // Find the index of the first space

        if (firstSpaceIndex == -1)
        {
            return new string[] { input.TrimEnd(), "" }; // Return input without trailing spaces and an empty second element
        }

        var firstPart = input.Substring(0, firstSpaceIndex).TrimEnd(); // Get the first part and remove trailing spaces
        var secondPart = input.Substring(firstSpaceIndex + 1); // Get the remaining part of the string

        return new string[] { firstPart, secondPart }; // Return the array with the two parts
    }

    private static void InvalidCommand()
    {
        Global.LogTerminal("ERROR: Invalid command.");
    }
    private static void InvalidArg()
    {
        Global.LogTerminal("ERROR: Invalid argument");
    }
    
    #region Terminal Commands

    private static void ToggleDebug(string args)
    {
        switch (args.ToLower())
        {
            case "true":
            case "on":
            case "1":
                Global.DebugMode = true;
                break;
            case "false":
            case "off":
            case "0":
                Global.DebugMode = false;
                break;
            case "":
                break;
            default:
                InvalidArg();
                return;
        }
        
        var result = Global.DebugMode ? "ON" : "OFF";
        Global.LogTerminal($"Debug mode is currently {result}");
    }
    
    #endregion
}