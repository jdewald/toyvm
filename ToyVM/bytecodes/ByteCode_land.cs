using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Bitwise and
	/// </summary>
	public class ByteCode_land : ByteCode
	{
		

		public ByteCode_land(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "land";
			size = 1;

		}

	
		public override void execute (StackFrame frame)
		{
			long val1 = (long) frame.popOperand();
			long val2 = (long) frame.popOperand();
			
			frame.pushOperand(val1 & val2);
		}

		

	}
}
