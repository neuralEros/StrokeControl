namespace StrokeControl;

public class StateManager
{
    public static string DoPost(string requestBody)
    {
        Console.WriteLine($"POST: {requestBody}");
        return "Hello World";
    }

}