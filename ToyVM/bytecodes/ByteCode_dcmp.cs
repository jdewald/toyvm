using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_dcmp : ByteCode
	{
		static int OP_LESS = 1;
		static int OP_GREATER = 2;
		int opval;
		public ByteCode_dcmp(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool, string op) : base(code)
		{
			name = "dcmp"+op;
			size = 1;
			
			if ("l".Equals(op)) { opval = OP_LESS; }
			if ("g".Equals(op)) { opval = OP_GREATER; }
		}

		public override void execute (StackFrame frame)
		{
			double value2 = Double.Parse(frame.popOperand().ToString());
			double value1 = Double.Parse(frame.popOperand().ToString());
			
			// TODO: handle NaN
			if (value1 > value2) { frame.pushOperand(1); }
			else if (value1.Equals(value2)) { frame.pushOperand(0); }
			else { frame.pushOperand(-1); }
		}

	}
}
