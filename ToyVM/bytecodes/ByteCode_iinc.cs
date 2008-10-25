using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Increment local variable by const
	/// </summary>
	public class ByteCode_iinc : ByteCode
	{
		int index; // which thing are we loading
		sbyte constant;
		public ByteCode_iinc(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 3;
			
			index = reader.ReadByte();
			
			constant = reader.getReader().ReadSByte();
			
			name = "iinc";
			
		}
	
		public override string ToString(){
			return getName() + " " + index + " " + constant;
		}
	
		public override void execute (StackFrame frame)
		{
			int old = (int)frame.getLocalVariables()[index];
			Console.WriteLine("old:{0},new:{1}",old,(old + constant));
			frame.getLocalVariables()[index] = old + constant;
		}

	}
}
