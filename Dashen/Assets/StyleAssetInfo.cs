namespace Dashen.Assets
{
	public class StyleAssetInfo : AssetInfo
	{
		public StyleAssetInfo(string path)
		{
			Tag = "link";
			SelfClosing = true;

			AddAttribute("rel", "stylesheet");
			AddAttribute("href", path);
		}
	}
}
