using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_l2i : ByteCode
	{
		

		public ByteCode_l2i(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "l2i";
			size = 1;

		}

	

		

	}
}
