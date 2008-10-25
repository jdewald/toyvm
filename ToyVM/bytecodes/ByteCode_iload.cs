using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Load int value from local variable (at index)
	/// </summary>
	public class ByteCode_iload : ByteCode
	{
		int index; // which thing are we loading
		
		public ByteCode_iload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			
			index = reader.ReadByte();
			name = "iload_" + index;
			
		}
		public ByteCode_iload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "iload_" + index;
			size = 1;
			
			this.index = index;
		}

	
		public override void execute (StackFrame frame)
		{
			try {
				frame.pushOperand((int) frame.getLocalVariables()[index]);
			}
			catch (InvalidCastException e){
				foreach (Object o in frame.getLocalVariables()){
					Console.WriteLine("Var:{0}",o);
				}
				throw new ToyVMException("Wanted int at " + index + " but got " + frame.getLocalVariables()[index],e,frame);
			}
		}

	}
}
