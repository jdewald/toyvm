using System;

namespace ToyVM
{
	/// <summary>
	/// Represents a DOUBLE entry in the ConstantPool
	/// </summary>
	public class ConstantPoolInfo_Double : ConstantPoolInfo
	{
		double value;
		


		public ConstantPoolInfo_Double(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			value = reader.ReadDouble();
		}

		
		public override String getName() { return "DOUBLE"; }

		
		public override void resolve(ConstantPoolInfo[] pool)
		{
			
		}

		public override string ToString()
		{
			
				return getName() + "[" + value + "]";
			
		}

	}
}
