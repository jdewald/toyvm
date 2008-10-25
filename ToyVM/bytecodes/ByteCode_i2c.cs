using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Convert integer to char
	/// </summary>
	public class ByteCode_i2c : ByteCode
	{
		

		public ByteCode_i2c(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "i2c";
			size = 1;

		}

		/**
		 * The document calls for it to be "truncated" to char
		 * but C# sees char as being 16-bit but for java it is 8-bit 
		 * I think
		 * */
		public override void execute (StackFrame frame)
		{
			int val = (int) frame.popOperand();
			frame.pushOperand((int)((char)(val&0xFF)));
		}

	

		

	}
}
