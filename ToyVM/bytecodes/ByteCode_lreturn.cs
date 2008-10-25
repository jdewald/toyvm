using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_lreturn : ByteCode
	{
		
		public ByteCode_lreturn(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "lreturn";
			size = 1;
		}

	}
}
