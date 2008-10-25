// InnerClassesAttribute.cs created with MonoDevelop
// User: jdewald at 5:23 PM 8/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class ConstantValueAttribute : AttributeInfo
	{
		
		ConstantPoolInfo value; // should point to Long,String,Int..
		
		public ConstantValueAttribute(ConstantPoolInfo_UTF8 utf8Name, MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool) : base(utf8Name,reader,pool)
		{
		}
		
		public override void parse(MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool)
		{
			UInt16 valueRef = reader.ReadUInt16();
			
			value = pool[valueRef - 1];
		}
		
	}
	
	
}
