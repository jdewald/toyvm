using System;

namespace ToyVM
{
	/// <summary>
	/// Summary description for FieldInfo.
	/// </summary>
	public class FieldInfo
	{
		public static readonly int ACCESS_PUBLIC = 0x0001;
		public static readonly int ACCESS_PRIVATE = 0x0002;
		public static readonly int ACCESS_PROTECTED = 0x0004;
		public static readonly int ACCESS_STATIC = 0x0008;
		public static readonly int ACCESS_FINAL = 0x0010;
		public static readonly int ACCESS_VOLATAILE = 0x0040;
		public static readonly int ACCESS_TRANSIENT = 0x0080;
		
		UInt16 accessFlags;
		UInt16 nameIndex;
		UInt16 descriptorIndex;
		UInt16 attributeCount;
		AttributeInfo[] attributes;

		// resolved later
		ConstantPoolInfo_UTF8 name;
		ConstantPoolInfo_UTF8 descriptor;

		public FieldInfo(MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool)
		{
			accessFlags = reader.ReadUInt16();
			nameIndex = reader.ReadUInt16();
			descriptorIndex = reader.ReadUInt16();
			//if (log.IsDebugEnabled) log.DebugFormat("Name should be {0}",pool[nameIndex-1]);
			name = (ConstantPoolInfo_UTF8)pool[nameIndex - 1];
			descriptor = (ConstantPoolInfo_UTF8)pool[descriptorIndex - 1];
			
			attributeCount = reader.ReadUInt16();

			attributes = new AttributeInfo[attributeCount];
			for (int i = 0; i < attributeCount; i++)
			{
				attributes[i] = AttributeInfo.readAttributeInfo(reader,pool);
			}

			
		}

		public bool isStatic(){
			return (accessFlags & ACCESS_STATIC) != 0; 
		}

		public string GetFieldName(){
			return name.getUTF8String();
		}
		
		public override string ToString()
		{
			if (name == null && descriptor == null)
			{
				return String.Format("FieldInfo:[af:{0},ni:{1},di:{2},ac:{3}]",accessFlags,nameIndex,descriptorIndex,attributeCount);
			}
			else 
			{
				string attributeString = "";
				foreach (AttributeInfo attr in attributes)
				{
					attributeString += attr;
				}

				return String.Format("FieldInfo:[af:{0},name:{1},desc:{2},ac:{3}]",accessFlags,name,descriptor,attributeString);
			}
		}


	}
}
