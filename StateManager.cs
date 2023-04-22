namespace StrokeControl;

public class StateManager
{
    public static string DoPost(string requestPath, string requestBody)
    {
        Console.WriteLine($"POST: [{requestPath}] {requestBody}");
        return "Hello World";
    }

}