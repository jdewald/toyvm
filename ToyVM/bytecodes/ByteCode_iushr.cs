using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// logical shift right
	/// </summary>
	public class ByteCode_iushr : ByteCode
	{
		
		public ByteCode_iushr(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "iushr";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			int val2 = ((int) frame.popOperand()) & 0x1f;
			int val1 = (int) frame.popOperand();
			
			if (val1 >= 0){
				val1 = val1 >> val2;
			}
			else {
				val1 = (val1 >> val2) + (2 << ~val2);
			}
			
			frame.pushOperand(val1);
		}

	}
}
