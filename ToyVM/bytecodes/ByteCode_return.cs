using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_return : ByteCode
	{
		
		public ByteCode_return(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "return";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			return;
		}

	}
}
