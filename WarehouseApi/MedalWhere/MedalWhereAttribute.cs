using System;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class MedalWhereAttribute : Attribute
{
    public string Category { get; }
    public int Priority { get; }

    public MedalWhereAttribute(string category, int priority)
    {
        Category = category;
        Priority = priority;
    }
}

public enum ErrorType
{
    [MedalWhere("Critical", 1)]
    Critical,

    [MedalWhere("Warning", 2)]
    Warning,

    [MedalWhere("Information", 3)]
    Information
}

