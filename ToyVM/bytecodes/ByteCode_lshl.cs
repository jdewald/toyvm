using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Shift left long
	/// </summary>
	public class ByteCode_lshl : ByteCode
	{
		

		public ByteCode_lshl(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "lshl";
			size = 1;

		}

	
		/**
		 * Left shift val1 by amount in low 5 bits of val2
		 */
		public override void execute (StackFrame frame)
		{
			long val2 = (long) frame.popOperand();
			long val1 = (long) frame.popOperand();
			
			val1 = (0xFFFFFFFF & ((val1 << (int)(val2 & 0x3F))));
			
			frame.pushOperand(val1);
		}

		

	}
}
