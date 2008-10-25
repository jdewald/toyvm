using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Add 2 integers
	/// </summary>
	public class ByteCode_iadd : ByteCode
	{
		

		public ByteCode_iadd(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "iadd";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			int val1 = (int) frame.popOperand();
			int val2 = (int) frame.popOperand();
		
			frame.pushOperand(val1 + val2);
		}


		

	}
}
