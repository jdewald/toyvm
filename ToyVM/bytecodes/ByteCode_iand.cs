using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Bitwise and
	/// </summary>
	public class ByteCode_iand : ByteCode
	{
		

		public ByteCode_iand(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "iand";
			size = 1;

		}

	
		public override void execute (StackFrame frame)
		{
			int val1 = (int) frame.popOperand();
			int val2 = (int) frame.popOperand();
			
			frame.pushOperand(val1 & val2);
		}

		

	}
}
