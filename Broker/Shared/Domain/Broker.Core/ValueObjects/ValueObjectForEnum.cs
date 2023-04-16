namespace Broker.Core.ValueObjects;

/// <summary>
/// Template for generating value object for enums
/// </summary>
/// <typeparam name="TEnum">It's the type of the value object, where the type corresponds to a enum</typeparam>
public abstract class ValueObjectForEnum<TEnum>
{
    public readonly TEnum Value;

    public ValueObjectForEnum(string value, bool check)
    {
        if(check)
            Check(value);

        TEnum enumValue = (TEnum)Enum.Parse(typeof(TEnum), value);
        Value = enumValue;
    }

    public ValueObjectForEnum(TEnum value) 
    { 
        Value = value;
    }

    /// <summary>
    /// Check if the value of the value object meets the minimum requirements
    /// </summary>
    /// <param name="value">The value of value object in string type</param>
    protected abstract void Check(string value);
}
