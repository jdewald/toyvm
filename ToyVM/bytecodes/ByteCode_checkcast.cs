using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Determine if the operand on the stack can be casted to the reference
	/// </summary>
	public class ByteCode_checkcast : ByteCode
	{
		ConstantPoolInfo_Class reference;
		
		string className;
		bool isArray = false;
		public ByteCode_checkcast(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "checkcast";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 methodIndex = reader.ReadUInt16();
			reference = (ConstantPoolInfo_Class)pool[methodIndex - 1];
			
			// check for array type
			if (reference.getClassName().StartsWith("[L")){
				
				className = reference.getClassName();
				// [L...;
				className = className.Substring(2,className.Length-3);
				isArray = true;
			}
			else {
				className = reference.getClassName();
			}
		}

		public override string ToString()
		{
			return base.ToString () + "->" + reference; 
		}
		
		public override void execute (StackFrame frame)
		{
			Object obj = frame.popOperand();
			if (obj is NullValue){
				frame.pushOperand(obj);
				return;
			}
			
			Heap.HeapReference heapRef = (Heap.HeapReference) obj;
			ClassFile S = heapRef.type;
			
			ClassFile T = ToyVMClassLoader.loadClass(className);
			
			if (! isArray){
				
				while (S != null){
					if ((! heapRef.isArray) && S == T){
						frame.pushOperand(obj);
						return;
					}
					
					if (S.implements(className)){
						frame.pushOperand(obj);
						return;
					}
					S = S.GetSuperClassFile();
				}
				
			}
			else {
				if (heapRef.isArray && S == T){
					frame.pushOperand(obj);
					return;
				}
				
			}
			
			throw new ToyVMException(String.Format("ClassCastException {0} {1} {2}",isArray,T,S),frame);
		}


		
	}
}
