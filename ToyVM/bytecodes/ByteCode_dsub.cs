using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Floating point subtraction
	/// </summary>
	public class ByteCode_dsub : ByteCode
	{
		

		public ByteCode_dsub(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "dsub";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			double val1 = (double) frame.popOperand();
			double val2 = (double) frame.popOperand();
		
			frame.pushOperand(val2 - val1);
		}

		

	}
}
