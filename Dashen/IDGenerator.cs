namespace Dashen
{
	public class IDGenerator
	{
		private int _current;

		public int NextID()
		{
			return _current++;
		}
	}
}
