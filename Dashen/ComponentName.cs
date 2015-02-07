using System;

namespace Dashen
{
	public class ComponentName
	{
		private readonly string _name;

		public ComponentName(string name)
		{
			_name = name;
		}

		public ComponentName(object component)
			:this(component.GetType().Name)
		{
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as ComponentName;

			if (other == null)
			{
				return false;
			}

			return string.Equals(_name, other._name, StringComparison.OrdinalIgnoreCase);
		}

		public override string ToString()
		{
			return _name;
		}

		public static ComponentName From(string name)
		{
			return new ComponentName(name);
		}
	}
}
