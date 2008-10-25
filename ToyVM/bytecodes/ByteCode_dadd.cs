using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Add 2 floats
	/// </summary>
	public class ByteCode_dadd : ByteCode
	{
		

		public ByteCode_dadd(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "dadd";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			double val1 = (double) frame.popOperand();
			double val2 = (double) frame.popOperand();
		
			frame.pushOperand(val1 + val2);
		}


		

	}
}
