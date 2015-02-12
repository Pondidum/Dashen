namespace Dashen.Components
{
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
