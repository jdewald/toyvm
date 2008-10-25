using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// int return
	/// </summary>
	public class ByteCode_ireturn : ByteCode
	{
		
		public ByteCode_ireturn(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ireturn";
			size = 1;
		}
		
		public override void execute (StackFrame frame)
		{
			frame.getPrev().pushOperand((int) frame.popOperand());
		}

	}
}
