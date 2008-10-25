using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Converter float to int.. naively
	/// </summary>
	public class ByteCode_f2i : ByteCode
	{
		

		public ByteCode_f2i(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "f2i";
			size = 1;

		}

		public override void execute (StackFrame frame)
		{
			float val1 = Single.Parse(frame.popOperand().ToString());
			frame.pushOperand((int) val1);
		}


		

	}
}
