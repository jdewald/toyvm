using System;

namespace ToyVM
{
	/// <summary>
	/// Represents a INTEGER entry in the ConstantPool
	/// </summary>
	public class ConstantPoolInfo_Long : ConstantPoolInfo
	{
		UInt64 value;
		


		public ConstantPoolInfo_Long(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			value = reader.ReadUInt64();
		}

		
		public override String getName() { return "LONG"; }

		
		public override void resolve(ConstantPoolInfo[] pool)
		{
			
		}

		public override string ToString()
		{
			
				return getName() + "[" + value + "]";
			
		}

	}
}
