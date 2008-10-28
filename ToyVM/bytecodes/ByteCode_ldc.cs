using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Pushes value of reference at 'index' onto stack
	/// </summary>
	public class ByteCode_ldc : ByteCode
	{
		ConstantPoolInfo reference;

		public ByteCode_ldc(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ldc";
			size = 2;

			byte index = reader.ReadByte();

			reference = pool[index - 1];
		}

	

		public override string ToString()
		{
			return base.ToString() + "->" + reference;
		}

		
		public override void execute (StackFrame frame)
		{
			
			if (reference is ConstantPoolInfo_String){
				// TODO: This should actually use an instance of java/lang/String
				ClassFile stringClass = ToyVMClassLoader.loadClass("java/lang/String");
				
				Heap.HeapReference stringRef = Heap.GetInstance().createString(stringClass,(ConstantPoolInfo_String)reference);
				
				
				frame.pushOperand(stringRef);
			}
			else if (reference is ConstantPoolInfo_Integer){
				frame.pushOperand(((ConstantPoolInfo_Integer)reference).getValue());
			}
			else if (reference is ConstantPoolInfo_Float){
				frame.pushOperand(((ConstantPoolInfo_Float)reference).getValue());
			}
			else {
				throw new ToyVMException("Expected string,integer or float, got " + reference,frame);
			}
		}
       
	}
}
