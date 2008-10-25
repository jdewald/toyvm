using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Push localVariable[index] onto operand stack
	/// </summary>
	public class ByteCode_aload0 : ByteCode
	{
		int index; // which thing are we loading
		
		public ByteCode_aload0(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			this.index = reader.ReadByte();
			name = "aload " + index;
		}
		
		public ByteCode_aload0(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "aload_" + index;
			size = 1;
			this.index = index;
		}

		public override void execute (StackFrame frame)
		{
			try {
				//Console.WriteLine("Local variable {0} is {1}",index,frame.getLocalVariables()[index]);
				frame.pushOperand(frame.getLocalVariables()[index]);
			}
			catch (Exception e){
				throw new ToyVMException("Index is " + index,e,frame);
			}
		}

	}
}
