using System;
using System.Collections.Generic;

public sealed class GameServices
{
    private readonly Dictionary<Type, object> _services = new();

    public void Register<T>(T instance) where T : class
        => _services[typeof(T)] = instance;

    public T Get<T>() where T : class
        => (T)_services[typeof(T)];
}