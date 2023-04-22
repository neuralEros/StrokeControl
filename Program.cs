using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Nancy;
using Nancy.Hosting.Self;
using StrokeControl;
using StrokeControl.Framework;

var Bootstrapper = new Bootstrapper();

//var host = new NancyHost(new Uri("http://localhost:6969"));
var host = new NancyHost(Bootstrapper, new HostConfiguration
{
    UrlReservations = new UrlReservations { CreateAutomatically = true },
    RewriteLocalhost = true // Bind to loopback interface, to prevent remote connections
}, new Uri("http://localhost:6969"));
host.Start();
Console.WriteLine("Running on http://localhost:6969");

// Launch the default web browser and navigate to the URL
if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    Process.Start("xdg-open", "http://localhost:6969");
}
else
{
    throw new PlatformNotSupportedException("This platform is not supported.");
}

Console.ReadLine();
host.Stop();

public sealed class ServerModule : NancyModule
{
    public ServerModule()
    {
        Get("/", _ => View["WebRoot/index.html"]);
        
        Post("/{catchAll*}", async args =>
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            string requestBody = await reader.ReadToEndAsync();
            string requestPath = args.catchAll;
            
            var response = StateManager.DoPost(requestPath, requestBody);
            
            return Response.AsText(response);
        });
    }
}