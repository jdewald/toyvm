using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Store operand at index i in the local variables
	/// </summary>
	public class ByteCode_astore : ByteCode
	{
		int index; // which thing are we loading
		
		public ByteCode_astore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			
			index = reader.ReadByte();
			name = "astore_" + index;
			
		}
		public ByteCode_astore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "astore_" + index;
			size = 1;
			
			this.index = index;
		}

		
		public override void execute (StackFrame frame)
		{
			frame.getLocalVariables().Insert(index,frame.popOperand());
		}

		
	}
}
