using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Push int constant
	/// </summary>
	public class ByteCode_iconst : ByteCode
	{
		int index; // which thing are we loading
		public ByteCode_iconst(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			if (index != -1){
				name = "iconst_" + index;
			}
			else {
				name = "iconst_m1";
			}
			size = 1;
			this.index = index;
		}

		public override void execute (StackFrame frame)
		{
			frame.pushOperand(index);
		}

	}
}
