using System;

namespace ToyVM
{
	/// <summary>
	/// Represents a FLOAT entry in the ConstantPool
	/// </summary>
	public class ConstantPoolInfo_Float : ConstantPoolInfo
	{
		//UInt32 value;
		float value;


		public ConstantPoolInfo_Float(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			// TODO... swap the values...
			value = reader.ReadFloat(); // crossing fingers that this is the same...
		}

		public float getValue() {
			return value;
		}
		public override String getName() { return "FLOAT"; }

		
		public override void resolve(ConstantPoolInfo[] pool)
		{
			
		}

		public override string ToString()
		{
			
				return getName() + "[" + value + "]";
			
		}

	}
}
