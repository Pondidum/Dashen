namespace Dashen.Assets
{
	public class StyleAssetInfo : AssetInfo
	{
		public StyleAssetInfo(string path)
		{
			Tag = "link";
			SelfClosing = true;
			Location = AssetLocations.PreHead;

			AddAttribute("rel", "stylesheet");
			AddAttribute("href", path);
		}
	}
}
