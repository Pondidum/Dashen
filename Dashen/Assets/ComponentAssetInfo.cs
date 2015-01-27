namespace Dashen.Assets
{
	public class ComponentAssetInfo : AssetInfo
	{
		public string Name { get; private set; }

		public ComponentAssetInfo(string directory, string name)
		{
			Tag = "script";
			SelfClosing = false;
			Location = AssetLocations.PreBody;
			Name = name;

			AddAttribute("type", "text/jsx");
			AddAttribute("src", directory + "/" + name);
		}
	}
}
