using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// negate int
	/// </summary>
	public class ByteCode_ineg : ByteCode
	{
		
		public ByteCode_ineg(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ineg";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			frame.pushOperand(-((int) frame.popOperand()));
		}

	}
}
