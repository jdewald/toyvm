using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_istore : ByteCode
	{
		int index; // which thing are we loading
		
		public ByteCode_istore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			
			index = reader.ReadByte();
			name = "istore " + index;
			
		}
		public ByteCode_istore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "istore_" + index;
			size = 1;
			
			this.index = index;
		}

		public override void execute (StackFrame frame)
		{
			frame.getLocalVariables()[index]=frame.popOperand();
		}

		
		
	}
}
