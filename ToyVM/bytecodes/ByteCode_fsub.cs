using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Floating point subtraction
	/// </summary>
	public class ByteCode_fsub : ByteCode
	{
		

		public ByteCode_fsub(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "fsub";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			float val1 = (float) frame.popOperand();
			float val2 = (float) frame.popOperand();
		
			frame.pushOperand(val2 - val1);
		}

		

	}
}
