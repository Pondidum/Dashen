namespace Dashen.Components
{
	public class FooterComponent : Component<FooterModel>
	{
		public FooterComponent()
		{
			Unlisted = true;
			Name = new ComponentName("Footer");
		}

		public override string GetJsx()
		{
			return string.Empty;
		}
	}
}
