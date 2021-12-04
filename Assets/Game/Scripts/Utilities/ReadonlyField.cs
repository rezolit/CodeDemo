namespace Utilities
{
	public class ReadonlyField<T>
	{
		public readonly T Value;

		public ReadonlyField(T value)
		{
			Value = value;
		}
	}
}