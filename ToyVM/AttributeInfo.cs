using System;

namespace ToyVM
{
	/// <summary>
	/// Summary description for AttributeInfo.
	/// </summary>
	public class AttributeInfo
	{
		
		protected UInt32 length;
		byte[] info;

		// Resolved later
		ConstantPoolInfo_UTF8 name;

		public AttributeInfo(ConstantPoolInfo_UTF8 name, MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool)
		{
			this.name = name;
			length = reader.ReadUInt32();
			parse(reader,pool);
		}

		public static AttributeInfo readAttributeInfo(MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool)
		{
			int nameIdx = reader.ReadUInt16();

			//Console.WriteLine("Read Index {0}/{0:X}",nameIdx);
			if (pool[nameIdx - 1] is ConstantPoolInfo_UTF8){
				ConstantPoolInfo_UTF8 utf8Name = (ConstantPoolInfo_UTF8)pool[nameIdx - 1];
			
				//Console.WriteLine("Attribute name: {0}",utf8Name);
				
				String attrName = utf8Name.getUTF8String();
				if (attrName.Equals("Code"))
				{
					return new CodeAttribute(utf8Name,reader,pool);
				}
				else if (attrName.Equals("InnerClasses")){
					return new InnerClassesAttribute(utf8Name,reader,pool);
				}
				else if (attrName.Equals("ConstantValue")){
					return new ConstantValueAttribute(utf8Name,reader,pool);
				}
				else if (attrName.Equals("LineNumberTable")){
					return new LineNumberTableAttribute(utf8Name,reader,pool);
				}
				else if (attrName.Equals("SourceFile")){
					return new SourceFileAttribute(utf8Name,reader,pool);
				}
				else if (		         
				         attrName.Equals("LocalVariableTable") ||
				         attrName.Equals("Signature") ||
				         attrName.Equals("Exceptions") ||
				         attrName.Equals("Deprecated") ||
				         attrName.Equals("EnclosingMethod") ||
				         attrName.Equals("RuntimeVisibleAnnotations")){
					return new AttributeInfo(utf8Name,reader,pool);
				}
				else {
					throw new Exception("Don't know how to handle " + attrName);
				}
			}
			else {
				throw new Exception("Expected UTF8, instead got " + pool[nameIdx - 1]);
			}
		}

		public virtual void parse(MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool)
		{
			
			info = reader.getReader().ReadBytes((int)length);
			
		}

	
		public override string ToString()
		{
			return String.Format("Attribute: [{0}]",name);
		}

	}
}
