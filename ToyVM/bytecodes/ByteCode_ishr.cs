using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_ishr : ByteCode
	{
		

		public ByteCode_ishr(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ishr";
			size = 1;

		}

		/**
		 * Right shift val1 by amount in low 5 bits of val2
		 */
		public override void execute (StackFrame frame)
		{
			int val2 = (int) frame.popOperand();
			int val1 = (int) frame.popOperand();
			
			val1 = (0xFFFF & ((val1 >> (val2 & 0x1F))));
			
			frame.pushOperand(val1);
		}	

		

	}
}
