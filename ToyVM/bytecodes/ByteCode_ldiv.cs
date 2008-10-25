using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_ldiv : ByteCode
	{
		
		public ByteCode_ldiv(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ldiv";
			size = 1;
		}

		
	}
}
