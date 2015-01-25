namespace Dashen.Assets
{
	public class JavaScriptAssetInfo : AssetInfo
	{
		public JavaScriptAssetInfo(string path)
		{
			Tag = "script";
			SelfClosing = false;
			Location = AssetLocations.PostHead;

			AddAttribute("src", path);
		}
	}
}
