using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_fcmp : ByteCode
	{
		static int OP_LESS = 1;
		static int OP_GREATER = 2;
		int opval;
		public ByteCode_fcmp(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool, string op) : base(code)
		{
			name = "fcmp"+op;
			size = 1;
			
			if ("l".Equals(op)) { opval = OP_LESS; }
			if ("g".Equals(op)) { opval = OP_GREATER; }
		}

		public override void execute (StackFrame frame)
		{
			float value2 = Single.Parse(frame.popOperand().ToString());
			float value1 = Single.Parse(frame.popOperand().ToString());
			
			// TODO: handle NaN
			if (value1 > value2) { frame.pushOperand(1); }
			else if (value1.Equals(value2)) { frame.pushOperand(0); }
			else { frame.pushOperand(-1); }
		}

	}
}
