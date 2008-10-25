using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_nop : ByteCode
	{
		
		public ByteCode_nop(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "nop";
			size = 1;
		}

		
	}
}
