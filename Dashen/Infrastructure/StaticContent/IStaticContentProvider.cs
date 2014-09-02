namespace Dashen.Infrastructure.StaticContent
{
	public interface IStaticContentProvider
	{
		StaticContent GetContent(string path);
	}
}
