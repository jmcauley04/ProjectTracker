namespace ProjectTracker.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class InjectServiceAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; }

    public enum ServiceLifetime
    {
        Transiet,
        Scoped,
        Singleton
    }

    public InjectServiceAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}
