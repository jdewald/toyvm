using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// double return
	/// </summary>
	public class ByteCode_dreturn : ByteCode
	{
		
		public ByteCode_dreturn(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "dreturn";
			size = 1;
		}
		
		public override void execute (StackFrame frame)
		{
			frame.getPrev().pushOperand((double) frame.popOperand());
		}

	}
}
