using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	// Pop top value of stack
	/// </summary>
	public class ByteCode_pop : ByteCode
	{
		
		int count;
		public ByteCode_pop(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int num) : base(code)
		{
			name = "pop";
			if (num == 2){
				name = "pop2";
			}
			size = 1;
			this.count = num;
		}

		public override void execute (StackFrame frame)
		{
			frame.popOperand();
			if (count == 2){
				frame.popOperand();
			}
		}

	}
}
