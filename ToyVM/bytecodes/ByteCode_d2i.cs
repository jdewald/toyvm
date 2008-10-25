using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Converter double to int.. naively
	/// </summary>
	public class ByteCode_d2i : ByteCode
	{
		

		public ByteCode_d2i(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "d2i";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			double val1 = Double.Parse(frame.popOperand().ToString());
			frame.pushOperand((int) val1);
		}


		

	}
}
