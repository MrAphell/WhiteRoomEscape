using System.Reflection;

public static class PlayModeReflectionHelper
{
    public static void SetPrivateField(object target, string fieldName, object value)
    {
        FieldInfo field = target.GetType().GetField(fieldName,
            BindingFlags.Instance | BindingFlags.NonPublic);

        field.SetValue(target, value);
    }

    public static T GetPrivateField<T>(object target, string fieldName)
    {
        FieldInfo field = target.GetType().GetField(fieldName,
            BindingFlags.Instance | BindingFlags.NonPublic);

        return (T)field.GetValue(target);
    }

    public static object InvokePrivateMethod(object target, string methodName, params object[] args)
    {
        MethodInfo method = target.GetType().GetMethod(methodName,
            BindingFlags.Instance | BindingFlags.NonPublic);

        return method.Invoke(target, args);
    }
}