using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Nancy;
using Nancy.Hosting.Self;
using StrokeControl;

var Bootstrapper = new Bootstrapper();

//var host = new NancyHost(new Uri("http://localhost:6969"));
var host = new NancyHost(Bootstrapper, new HostConfiguration
{
    UrlReservations = new UrlReservations { CreateAutomatically = true },
    RewriteLocalhost = true // Bind to loopback interface, to prevent remote connections
}, new Uri("http://localhost:6969"));
host.Start();
Console.WriteLine("Running on http://localhost:6969");

// Launch the default web browser and navigate to the URL using the appropriate method
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    Process.Start(new ProcessStartInfo
    {
        FileName = "http://localhost:6969",
        UseShellExecute = true
    });
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    Process.Start("xdg-open", "http://localhost:6969");
}
else
{
    throw new PlatformNotSupportedException("This platform is not supported.");
}

Console.ReadLine();
host.Stop();

public class ServerModule : NancyModule
{
    public ServerModule()
    {
        Get("/", _ => View["WebRoot/index.html"]);
        
        Post("/post", async args =>
        {
            // Read the request body as a string
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            string requestBody = await reader.ReadToEndAsync();

            // Call the StateManager.Post() method with the request string
            var response = StateManager.DoPost(requestBody);

            // Return the response from StateManager.Post() as the response of this route
            return response;
        });
    }
}