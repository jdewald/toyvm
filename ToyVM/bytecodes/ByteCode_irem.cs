using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// int remainder
	/// </summary>
	public class ByteCode_irem : ByteCode
	{
		
		public ByteCode_irem(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "irem";
			size = 1;
		}

		// value1,value2 => ...
		public override void execute (StackFrame frame)
		{
			int value2 = (int) frame.popOperand();
			int value1 = (int) frame.popOperand();
			
			frame.pushOperand(value1 - (value1 / value2) * value2);
			// TODO: something is broken up higher
			//frame.pushOperand(0);
		}

	}
}
