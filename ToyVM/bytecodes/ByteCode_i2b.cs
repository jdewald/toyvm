using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_i2b : ByteCode
	{
		
		public ByteCode_i2b(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "i2b";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			int val = (int) frame.popOperand();
			frame.pushOperand((int)((byte)(val&0xFF)));
		}
	}
}
