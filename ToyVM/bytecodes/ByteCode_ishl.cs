using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Shift left integer
	/// </summary>
	public class ByteCode_ishl : ByteCode
	{
		

		public ByteCode_ishl(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ishl";
			size = 1;

		}

	
		/**
		 * Left shift val1 by amount in low 5 bits of val2
		 */
		public override void execute (StackFrame frame)
		{
			int val2 = (int) frame.popOperand();
			int val1 = (int) frame.popOperand();
			
			val1 = (0xFFFF & ((val1 << (val2 & 0x1F))));
			
			frame.pushOperand(val1);
		}

		

	}
}
