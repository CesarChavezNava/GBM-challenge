namespace Broker.Core.ValueObjects;

/// <summary>
/// Template for generating value objects
/// </summary>
/// <typeparam name="T">It's the type of the value object, where the type corresponds to a primitive value</typeparam>
public abstract class ValueObject<T>
{
    public readonly T Value;

    public ValueObject(T value)
    {
        Check(value);
        Value = value;
    }

    /// <summary>
    /// Check if the value of the value object meets the minimum requirements
    /// </summary>
    /// <param name="value">The value of value object</param>
    protected abstract void Check(T value);
}
