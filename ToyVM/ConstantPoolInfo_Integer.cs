using System;

namespace ToyVM
{
	/// <summary>
	/// Represents a INTEGER entry in the ConstantPool
	/// </summary>
	public class ConstantPoolInfo_Integer : ConstantPoolInfo
	{
		UInt32 value;



		public ConstantPoolInfo_Integer(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			value = reader.ReadUInt32();
		}

		
		public override String getName() { return "INTEGER"; }

		public int getValue() {
			return (int) value;
		}
		public override void resolve(ConstantPoolInfo[] pool)
		{
			
		}

		public override string ToString()
		{
			
				return getName() + "[" + value + "]";
			
		}

	}
}
