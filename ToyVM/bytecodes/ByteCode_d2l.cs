using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Converter double to long.. naively
	/// </summary>
	public class ByteCode_d2l : ByteCode
	{
		

		public ByteCode_d2l(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "d2l";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			double val1 = Double.Parse(frame.popOperand().ToString());
			frame.pushOperand((long) val1);
		}


		

	}
}
