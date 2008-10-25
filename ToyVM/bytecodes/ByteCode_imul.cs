using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// integer multiplication
	/// </summary>
	public class ByteCode_imul : ByteCode
	{
		
		public ByteCode_imul(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "imul";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			int val1 = (int) frame.popOperand();
			int val2 = (int) frame.popOperand();
		
			frame.pushOperand(val1 * val2);
		}
	}
}
