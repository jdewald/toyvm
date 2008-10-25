using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_i2f : ByteCode
	{
		

		public ByteCode_i2f(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "i2f";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			int val = (int) frame.popOperand();
			frame.pushOperand((float)val);
		}


		

	}
}
