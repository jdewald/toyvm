using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_f2d : ByteCode
	{
		

		public ByteCode_f2d(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "f2d";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			float val = (float) frame.popOperand();
			frame.pushOperand((double)val);
		}


		

	}
}
