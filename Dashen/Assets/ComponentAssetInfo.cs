namespace Dashen.Assets
{
	public class ComponentAssetInfo : AssetInfo
	{
		public ComponentAssetInfo(string path)
		{
			Tag = "script";
			SelfClosing = false;
			Location = AssetLocations.PostBody;

			AddAttribute("type", "text/jsx");
			AddAttribute("src", path);
		}
	}
}