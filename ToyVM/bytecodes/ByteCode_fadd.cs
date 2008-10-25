using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Add 2 floats
	/// </summary>
	public class ByteCode_fadd : ByteCode
	{
		

		public ByteCode_fadd(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "fadd";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			float val1 = (float) frame.popOperand();
			float val2 = (float) frame.popOperand();
		
			frame.pushOperand(val1 + val2);
		}


		

	}
}
