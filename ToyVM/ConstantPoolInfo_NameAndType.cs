using System;

namespace ToyVM
{
	/// <summary>
	/// Summary description for ConstantPoolInfo_MethodRef.
	/// </summary>
	public class ConstantPoolInfo_NameAndType : ConstantPoolInfo
	{
		UInt16 nameIndex; // will reference another entry in the pool
		UInt16 descriptorIndex; // will reference another entry in the pool

		// resolved later
		ConstantPoolInfo_UTF8 name;
		ConstantPoolInfo_UTF8 descriptor;

		public ConstantPoolInfo_NameAndType(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			nameIndex = reader.ReadUInt16();
			descriptorIndex = reader.ReadUInt16();
		}

		public override String getName()
		{
			return "NAME_AND_TYPE";
			
		}

		public string getRefName(){
			return name.getUTF8String();
		}
		public string getDescriptor(){
			return descriptor.getUTF8String();
		}
		public override void resolve(ConstantPoolInfo[] pool)
		{
			name = (ConstantPoolInfo_UTF8)pool[nameIndex-1];
			descriptor = (ConstantPoolInfo_UTF8)pool[descriptorIndex-1];
		}

		public override string ToString()
		{
			if (name != null && descriptor != null)
			{
				return getName() + String.Format("[{0},{1}]",name,descriptor);
			}
			else 
			{
				return getName() + String.Format("[{0},{1}]",nameIndex,descriptorIndex);
			}
		}

	}
}
