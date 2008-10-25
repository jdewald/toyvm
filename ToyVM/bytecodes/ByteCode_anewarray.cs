using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_anewarray : ByteCode
	{
		ConstantPoolInfo_Class reference;
		public ByteCode_anewarray(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "anewarray";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 fieldIndex = reader.ReadUInt16();
			reference = (ConstantPoolInfo_Class)pool[fieldIndex - 1];
		}

		public override string ToString()
		{
			return base.ToString () + "->" + reference.ToString(); 
		}

	
		public override void execute (StackFrame frame)
		{
			int count = (int) frame.popOperand();
			
			string className = reference.getClassName();
			if (! className.StartsWith("[")){ 
				frame.pushOperand(Heap.GetInstance().newArray(ToyVMClassLoader.loadClass(className),count));
			}
			else {
				frame.pushOperand(Heap.GetInstance().new2DArray(className.Substring(1),count));
			}
		}

	}
}
