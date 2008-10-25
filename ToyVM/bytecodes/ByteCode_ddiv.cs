using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// double division
	/// </summary>
	public class ByteCode_ddiv : ByteCode
	{
		
		public ByteCode_ddiv(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ddiv";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			double val2 = (double) frame.popOperand();
			double val1 = (double) frame.popOperand();
		
			frame.pushOperand(val1 / val2);
		}
	}
}
