using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_i2d : ByteCode
	{
		

		public ByteCode_i2d(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "i2d";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			int val = (int) frame.popOperand();
			frame.pushOperand((double)val);
		}


		

	}
}
