using System;

namespace Dashen
{
	public class ModelInfo
	{
		public int ModelID { get; set; }
		public IJsxProvider Component { get; set; }
		public Func<Model> Model { get; set; }
	}
}
