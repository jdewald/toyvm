using System;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Retrieves value of static field and pushes onto operand stack
	/// </summary>
	public class ByteCode_getstatic : ByteCode
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_getstatic));
		ConstantPoolInfo_FieldRef field;
		public ByteCode_getstatic(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "getstatic";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 fieldIndex = reader.ReadUInt16();
			field = (ConstantPoolInfo_FieldRef)pool[fieldIndex - 1];
		}

		public override string ToString()
		{
			return base.ToString () + "->" + field.ToString(); 
		}

		
		public override void execute (StackFrame frame)
		{
			if (log.IsDebugEnabled) log.DebugFormat("Executing {0}",frame);
			ConstantPoolInfo_Class classInfo = field.getTheClass();
			
			ClassFile theClass = ToyVMClassLoader.loadClass(classInfo.getClassName());
			
			if (log.IsDebugEnabled) log.DebugFormat("Retrieved {0} and will get value of {1}",classInfo,field);
			
			string fieldName = field.getNameAndType().getRefName();
			frame.pushOperand(theClass.getStaticFieldValue(fieldName));
			
			
		}

		
	}
}
