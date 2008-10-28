using System;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Sets static field at index from value in operand stack
	/// </summary>
	public class ByteCode_putstatic : ByteCode
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_putstatic));
		ConstantPoolInfo_FieldRef field;
		public ByteCode_putstatic(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "putstatic";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 fieldIndex = reader.ReadUInt16();
			field = (ConstantPoolInfo_FieldRef)pool[fieldIndex - 1];
		}

		public override string ToString()
		{
			return base.ToString () + "->" + field.ToString(); 
		}

		//..., value  ...
		public override void execute(StackFrame frame){
			if (log.IsDebugEnabled) log.DebugFormat("Executing " + this);
			ConstantPoolInfo_Class theClass = field.getTheClass();
			if (log.IsDebugEnabled) log.DebugFormat("Field class is {0}",theClass);
			
			
			ClassFile fieldClass = ToyVMClassLoader.loadClass(theClass.getClassName());
			fieldClass.setStaticFieldValue(field.getNameAndType().getRefName(),frame.popOperand());
			
			if (log.IsDebugEnabled) log.DebugFormat("Static field {0} now has value {1}",field,fieldClass.getStaticFieldValue(field.getNameAndType().getRefName()));
		}
	}
}
