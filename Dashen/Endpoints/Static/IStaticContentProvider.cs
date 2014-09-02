namespace Dashen.Endpoints.Static
{
	public interface IStaticContentProvider
	{
		StaticContent GetContent(string path);
	}
}
