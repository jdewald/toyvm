// NullValue.cs created with MonoDevelop
// User: jdewald at 9:04 PM 9/2/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class NullValue
	{
		public static readonly NullValue INSTANCE = new NullValue();
		private NullValue()
		{
		}
		
		public override string ToString ()
		{
			return "NULL";
		}

	}
}
