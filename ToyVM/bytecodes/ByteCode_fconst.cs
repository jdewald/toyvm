using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_fconst : ByteCode
	{
		int index; // which thing are we loading
		public ByteCode_fconst(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "fconst_" + index;
			size = 1;
			
			this.index = index;
		}

		public override void execute (StackFrame frame)
		{
			frame.pushOperand((float)index);
		}
	}
}
