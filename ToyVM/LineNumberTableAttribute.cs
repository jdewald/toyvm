// InnerClassesAttribute.cs created with MonoDevelop
// User: jdewald at 5:23 PM 8/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;

namespace ToyVM
{
	
	
	public class LineNumberTableAttribute : AttributeInfo
	{
		
		Hashtable lineNumberTable = new Hashtable();
		
		
		public LineNumberTableAttribute(ConstantPoolInfo_UTF8 utf8Name, MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool) : base(utf8Name,reader,pool)
		{
		}
		
		public override void parse(MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool)
		{
			UInt16 count = reader.ReadUInt16();
			//if (log.IsDebugEnabled) log.DebugFormat("Will read in {0} line number mappings",count);
			for (int i = 0; i < count; i++){
				UInt16 position = reader.ReadUInt16();
				UInt16 lineNumber = reader.ReadUInt16();
			
				lineNumberTable.Add(position,lineNumber);
				//if (log.IsDebugEnabled) log.DebugFormat("{0}->{1}",position,lineNumber);
			}
		}
		
		public UInt16 getLineNumber(UInt16 position){
			if (lineNumberTable[position] != null){
				return (UInt16) lineNumberTable[position];
			}
			else {
				return 0;
			}
		}
		
	}
	
	
}
