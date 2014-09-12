namespace Dashen
{
	public class ApplicationModel
	{
		public string Title { get; set; }
		public string Version { get; set; }

		public string DashenVersion { get { return GetType().Assembly.GetName().Version.ToString(); } }
	}
}
