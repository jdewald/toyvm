using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_athrow : ByteCode
	{
		
		public ByteCode_athrow(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "athrow";
			size = 1;
		}

		
	}
}
