using System.Reflection;

namespace Host.ModuleIntegration;

/// <summary>
/// Contains information about a module
/// </summary>
public class Module
{
    /// <summary>
    /// Gets the route prefix to all controller and endpoints in the module.
    /// </summary>
    public string RoutePrefix { get; }

    /// <summary>
    /// Gets the startup class of the module.
    /// </summary>
    public TournaBet.Shared.IStartup Startup { get; }

    /// <summary>
    /// Gets the assembly of the module.
    /// </summary>
    public Assembly Assembly => Startup.GetType().Assembly;

    public Module(string routePrefix, TournaBet.Shared.IStartup startup)
    {
        RoutePrefix = routePrefix;
        Startup = startup;
    }
}