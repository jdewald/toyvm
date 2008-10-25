using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// float return
	/// </summary>
	public class ByteCode_freturn : ByteCode
	{
		
		public ByteCode_freturn(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "freturn";
			size = 1;
		}
		
		public override void execute (StackFrame frame)
		{
			frame.getPrev().pushOperand((float) frame.popOperand());
		}

	}
}
