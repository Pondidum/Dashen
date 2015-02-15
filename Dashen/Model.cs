namespace Dashen
{
	public class Model
	{
		public string Title { get; set; }
		public int Columns { get; set; }

		protected Model()
		{
			Title = string.Empty;
			Columns = 3;
		}
	}
}
