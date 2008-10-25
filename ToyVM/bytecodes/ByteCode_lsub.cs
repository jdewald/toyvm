using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Duplicates the top entry in the operand stack
	/// </summary>
	public class ByteCode_lsub : ByteCode
	{
		
		public ByteCode_lsub(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "lsub";
			size = 1;
		}

		
	}
}
