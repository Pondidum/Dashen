namespace Dashen.Static
{
	public interface IStaticContentProvider
	{
		bool Handles(string directory);
		Resource GetResource(string directory, string filename);
	}
}
