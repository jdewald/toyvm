using System;

namespace ToyVM
{
	/// <summary>
	/// Represents a CLASS entry in the ConstantPool
	/// </summary>
	public class ConstantPoolInfo_Class : ConstantPoolInfo
	{
		UInt16 nameIndex;

		// resolved later
		ConstantPoolInfo_UTF8 name;

		public ConstantPoolInfo_Class(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			nameIndex = reader.ReadUInt16();
		}

		
		public override String getName() { return "CLASS"; }

		public override void resolve(ConstantPoolInfo[] pool)
		{
			name = (ConstantPoolInfo_UTF8)pool[nameIndex-1];
		}

		public UInt16 getNameIndex() 
		{
			return nameIndex;
		}

		public override string ToString()
		{
			if (name != null)
			{
				return getName() + "[" + name.ToString() + "]";
			}
			else 
			{
				return getName();
			}
		}

		public string getClassName() 
		{
			return name.getUTF8String();
		}
	}
}
