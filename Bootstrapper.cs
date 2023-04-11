using Nancy;
using Nancy.Conventions;

namespace StrokeControl;

public class Bootstrapper : DefaultNancyBootstrapper
{
    protected override void ConfigureConventions(NancyConventions conventions)
    {
        base.ConfigureConventions(conventions);

        // Serve static content from the public folder
        conventions.StaticContentsConventions.Add(
            StaticContentConventionBuilder.AddDirectory("", @"WebRoot")
        );
    }
}