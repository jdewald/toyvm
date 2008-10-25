using System;

namespace ToyVM
{
	/// <summary>
	/// Represents a STRING entry in the ConstantPool
	/// </summary>
	public class ConstantPoolInfo_String : ConstantPoolInfo
	{
		UInt16 nameIndex;

		ConstantPoolInfo_UTF8 name;

		public ConstantPoolInfo_String(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			nameIndex = reader.ReadUInt16();
		}

		
		public override String getName() { return "STRING"; }

		public UInt16 getNameIndex() 
		{
			return nameIndex;
		}

		public override void resolve(ConstantPoolInfo[] pool)
		{
			name = (ConstantPoolInfo_UTF8)pool[nameIndex - 1];
		}

		public string getValue(){
			return name.getUTF8String();
		}
		
		public override string ToString()
		{
			if (name != null)
			{
				return getName() + "[" + name + "]";
			}
			else 
			{
				return getName() + "[" + nameIndex + "]";
			}
		}

	}
}
