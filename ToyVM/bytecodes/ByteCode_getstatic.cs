using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Retrieves value of static field and pushes onto operand stack
	/// </summary>
	public class ByteCode_getstatic : ByteCode
	{
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
			Console.WriteLine("Executing {0}",frame);
			ConstantPoolInfo_Class classInfo = field.getTheClass();
			
			ClassFile theClass = ToyVMClassLoader.loadClass(classInfo.getClassName());
			
			Console.WriteLine("Retrieved {0} and will get value of {1}",classInfo,field);
			
			string fieldName = field.getNameAndType().getRefName();
			frame.pushOperand(theClass.getStaticFieldValue(fieldName));
			
			
		}

		
	}
}
