using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// logical shift right
	/// </summary>
	public class ByteCode_lushr : ByteCode
	{
		
		public ByteCode_lushr(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "lushr";
			size = 1;
		}

		/**
		 * Shift up to 63 bits
		 */
		public override void execute (StackFrame frame)
		{
			int val2 = (int)(((long) frame.popOperand()) & 0x3f);
			long val1 = (long) frame.popOperand();
			
			
			// C# only
			if (val1 >= 0){
				val1 = val1 >> val2;
			}
			else {
				val1 = (val1 >> val2) + (2 << ~val2);
			}
			
			frame.pushOperand(val1);
		}

	}
}
