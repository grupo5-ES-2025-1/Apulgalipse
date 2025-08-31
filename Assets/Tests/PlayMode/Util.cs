using System;
using System.Linq;

public static class TestTypeUtils
{
    public static Type FindMono(string simpleName)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } })
            .FirstOrDefault(t => t.Name == simpleName && typeof(UnityEngine.MonoBehaviour).IsAssignableFrom(t));
    }
}
