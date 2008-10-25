using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Push null
	/// </summary>
	public class ByteCode_aconst_null : ByteCode
	{
		
		public ByteCode_aconst_null(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "aconst_null";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			frame.pushOperand(NullValue.INSTANCE);
		}

	}
}
