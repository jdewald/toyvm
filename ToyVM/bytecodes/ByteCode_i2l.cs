using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_i2l : ByteCode
	{
		

		public ByteCode_i2l(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "i2l";
			size = 1;

		}

	

		

	}
}
