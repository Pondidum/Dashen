namespace Dashen
{
	public interface IJsxProvider
	{
		ComponentName Name { get; }
		string GetJsx();
	}
}
