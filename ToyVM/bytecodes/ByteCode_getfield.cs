using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_getfield : ByteCode
	{
		ConstantPoolInfo_FieldRef field;
		public ByteCode_getfield(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "getfield";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 fieldIndex = reader.ReadUInt16();
			field = (ConstantPoolInfo_FieldRef)pool[fieldIndex - 1];
		}

		public override string ToString()
		{
			return base.ToString () + "->" + field.ToString(); 
		}

		public override void execute(StackFrame frame){
			ConstantPoolInfo_Class theClass = field.getTheClass();
			Console.WriteLine("Field class is {0}",theClass);
						
			ClassFile fieldClass = ToyVMClassLoader.loadClass(theClass.getClassName());
			
			
			Heap.HeapReference href = (Heap.HeapReference) frame.popOperand();
			ToyVMObject obj = (ToyVMObject) href.obj;
			
			frame.pushOperand(obj.getFieldValue(field));
		}
		
	}
}
