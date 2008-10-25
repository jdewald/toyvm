using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_lcmp : ByteCode
	{
		
		public ByteCode_lcmp(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "lcmp";
			size = 1;
		}

		
	}
}
