namespace Broker.Core.Entities;

public abstract class Filter
{
    public readonly object Value;
	public Filter(object value)
	{
		this.Value = value;
	}
}
