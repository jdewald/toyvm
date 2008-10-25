using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Convert integer to short
	/// </summary>
	public class ByteCode_i2s : ByteCode
	{
		

		public ByteCode_i2s(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "i2s";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			int val = (int) frame.popOperand();
			frame.pushOperand((int)((short)val));
		}

	

		

	}
}
