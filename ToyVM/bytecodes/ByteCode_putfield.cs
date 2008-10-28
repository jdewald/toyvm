using System;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_putfield : ByteCode
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_putfield));
		ConstantPoolInfo_FieldRef field;
		public ByteCode_putfield(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "putfield";
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
			if (log.IsDebugEnabled) log.DebugFormat("Field class is {0}",theClass);
			
			
			//ClassFile fieldClass = ToyVMClassLoader.loadClass(theClass.getClassName());
		
			Object value = frame.popOperand();
			Heap.HeapReference href = (Heap.HeapReference) frame.popOperand();
			ToyVMObject obj = (ToyVMObject) href.obj;
			
			obj.setFieldValue(field,value);
		}
		
	}
}
