namespace Dashen
{
	public class Component<TModel> where TModel : Model
	{
		public virtual string GetJsx()
		{
			return string.Empty;
		}
	}
}
