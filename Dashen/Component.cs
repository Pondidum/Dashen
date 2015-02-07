namespace Dashen
{
	public abstract class Component
	{
		public ComponentName Name { get; protected set; }
		public bool Unlisted { get; protected set; }
		
		public abstract string GetJsx();
	}

	public abstract class Component<TModel> : Component
		where TModel : Model
	{
		protected Component()
		{
			Name = new ComponentName(this);
		}
	}
}
