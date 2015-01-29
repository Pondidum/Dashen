namespace Dashen.Assets
{
	public class ComponentAssetInfo : AssetInfo
	{
		public string ModelPath { get; private set; }
		public string ComponentPath { get; private set; }

		public string Name { get; private set; }

		public ComponentAssetInfo(ComponentName componentName, int modelID)
		{
			Name = componentName.ToString();
			ComponentPath = "components/" + Name;
			ModelPath = "models/" + modelID;

			Tag = "script";
			SelfClosing = false;
			Location = AssetLocations.PreBody;

			AddAttribute("type", "text/jsx");
			AddAttribute("src", ComponentPath);
		}
	}
}
