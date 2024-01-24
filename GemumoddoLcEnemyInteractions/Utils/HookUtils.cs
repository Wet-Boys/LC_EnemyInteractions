using System;
using System.Reflection;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;

namespace EnemyInteractions.Utils;

internal static class HookUtils
{
    private const BindingFlags DefaultFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
    private const BindingFlags StaticFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

    public static Hook NewHook<TTarget, TDest>(string targetMethodName, string destMethodName, TDest instance)
    {
        var targetMethod = typeof(TTarget).GetMethod(targetMethodName, DefaultFlags);
        var destMethod = typeof(TDest).GetMethod(destMethodName, DefaultFlags);

        return new Hook(targetMethod, destMethod, instance);
    }

    public static Hook NewHook<TTarget>(string targetMethodName, MethodInfo destMethod)
    {
        var targetMethod = typeof(TTarget).GetMethod(targetMethodName, DefaultFlags);

        return new Hook(targetMethod, destMethod);
    }
    
    /// <summary>
    /// For static destination classes.
    /// </summary>
    public static Hook NewHook<TTarget>(string targetMethodName, Type destType, string destMethodName)
    {
        var targetMethod = typeof(TTarget).GetMethod(targetMethodName, DefaultFlags);
        var destMethod = destType.GetMethod(destMethodName, StaticFlags);

        return new Hook(targetMethod, destMethod);
    }

    public static ILHook NewILHook<TTarget>(string targetMethodName, ILContext.Manipulator manipulator)
    {
        var targetMethod = typeof(TTarget).GetMethod(targetMethodName, DefaultFlags);

        return new ILHook(targetMethod, manipulator);
    }
}