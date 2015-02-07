namespace Dashen
{
	public abstract class Component<TModel> : IJsxProvider
		where TModel : Model
	{
		public ComponentName Name { get; private set; }

		protected Component()
		{
			Name = new ComponentName(this);
		}

		public abstract string GetJsx();
	}
}
