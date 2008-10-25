using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Determines of operand is an instance of our resolved class,array or interface
	/// </summary>
	public class ByteCode_instanceof : ByteCode
	{
		ConstantPoolInfo_Class clazz;
		string className;
		public ByteCode_instanceof(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "instanceof";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 methodIndex = reader.ReadUInt16();
			clazz = (ConstantPoolInfo_Class)pool[methodIndex - 1];
			className = clazz.getClassName();
		}

		public override string ToString()
		{
			return base.ToString () + "->" + clazz.ToString(); 
		}

		
		public override void execute (StackFrame frame)
		{
			Object o = frame.popOperand();
			if (! (o is NullValue)){
				Heap.HeapReference heapRef = (Heap.HeapReference) o;
		
			
				// we only handle class right now
				ClassFile tClass = (ClassFile) heapRef.type;
				
				do {
					if (tClass.GetName().Equals(className)){
						frame.pushOperand(1);
						return;
					}
					
					if (tClass.implements(className)){
						frame.pushOperand(1);
						return;
					}
					tClass = tClass.GetSuperClassFile();
					
				} while (tClass != null);
				
			}
			frame.pushOperand(0);
		}
	}
}
