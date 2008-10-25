using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// integer division
	/// </summary>
	public class ByteCode_idiv : ByteCode
	{
		
		public ByteCode_idiv(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "idiv";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			int val2 = (int) frame.popOperand();
			int val1 = (int) frame.popOperand();
		
			frame.pushOperand(val1 / val2);
		}
	}
}
