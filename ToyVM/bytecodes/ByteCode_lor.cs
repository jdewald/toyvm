using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Bitwise OR
	/// </summary>
	public class ByteCode_lor : ByteCode
	{
		

		public ByteCode_lor(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "lor";
			size = 1;

		}

	

		

	}
}
