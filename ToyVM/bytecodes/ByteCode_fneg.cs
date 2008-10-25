using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_fneg : ByteCode
	{
		

		public ByteCode_fneg(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "fneg";
			size = 1;

		}

	

		

	}
}
