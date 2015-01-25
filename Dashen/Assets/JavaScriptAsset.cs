namespace Dashen.Assets
{
	public class JavaScriptAssetInfo : AssetInfo
	{
		public JavaScriptAssetInfo(string path)
		{
			Tag = "script";
			SelfClosing = false;

			AddAttribute("src", path);
		}
	}
}
