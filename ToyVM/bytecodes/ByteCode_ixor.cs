using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Bitwise xor
	/// </summary>
	public class ByteCode_ixor : ByteCode
	{
		
		public ByteCode_ixor(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ixor";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			int val1 = (int) frame.popOperand();
			int val2 = (int) frame.popOperand();
			
			frame.pushOperand(val1 ^ val2);
		}
	}
}
