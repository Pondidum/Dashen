namespace Dashen.Components
{
	public class HeaderModel : Model
	{
		public string AppName { get; set; }
		public string AppVersion { get; set; }
	}

	public class HeaderComponent : Component<HeaderModel>
	{
		public HeaderComponent()
		{
			Unlisted = true;
			Name = new ComponentName("Header");
		}

		public override string GetJsx()
		{
			return string.Empty;
		}
	}
}
