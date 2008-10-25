using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_isub : ByteCode
	{
		

		public ByteCode_isub(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "isub";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			int val1 = (int) frame.popOperand();
			int val2 = (int) frame.popOperand();
		
			frame.pushOperand(val2 - val1);
		}

		

	}
}
