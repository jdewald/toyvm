using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_dneg : ByteCode
	{
		

		public ByteCode_dneg(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "dneg";
			size = 1;

		}

	

		

	}
}
