using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// negate int
	/// </summary>
	public class ByteCode_lneg : ByteCode
	{
		
		public ByteCode_lneg(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "lneg";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			frame.pushOperand(-((long) frame.popOperand()));
		}

	}
}
