// InnerClassesAttribute.cs created with MonoDevelop
// User: jdewald at 5:23 PM 8/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class InnerClassesAttribute : AttributeInfo
	{
		UInt16 innerClassCount;
		
		InnerClassInfo[] innerClasses;
		
		public InnerClassesAttribute(ConstantPoolInfo_UTF8 utf8Name, MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool) : base(utf8Name,reader,pool)
		{
		}
		
		public override void parse(MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool)
		{
			innerClassCount = reader.ReadUInt16();
			
			innerClasses = new InnerClassInfo[innerClassCount];
			for (int i = 0; i < innerClassCount; i++){
				innerClasses[i] = new InnerClassInfo(reader,pool);
			}
		}
		
	}
	
	public class InnerClassInfo 
	{
		public ConstantPoolInfo_Class innerClass;
		public ConstantPoolInfo_Class outerClass;
		public ConstantPoolInfo_UTF8 innerName;
		public UInt16 accessFlags;
		
		public InnerClassInfo(MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool){
			UInt16 innerClassIndex = reader.ReadUInt16();
			
			if (innerClassIndex != 0){
				innerClass = (ConstantPoolInfo_Class) pool[innerClassIndex - 1];
			}
			
			
			UInt16 outerClassIndex = reader.ReadUInt16();
			if (outerClassIndex != 0){
				outerClass = (ConstantPoolInfo_Class) pool[outerClassIndex - 1];
			}
			
			UInt16 innerNameIndex = reader.ReadUInt16();
			if (innerNameIndex != 0){
				innerName = (ConstantPoolInfo_UTF8) pool[innerNameIndex - 1];
			}
			
			accessFlags = reader.ReadUInt16();
		}
	}
}
