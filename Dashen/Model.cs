namespace Dashen
{
	public class Model
	{
		public int ID { get; internal set; }
		public string Title { get; set; }
		public int Columns { get; set; }

		protected Model()
		{
			Title = string.Empty;
			Columns = 3;
		}
	}
}
