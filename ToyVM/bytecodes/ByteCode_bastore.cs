using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Store byte in array
	/// </summary>
	public class ByteCode_bastore : ByteCode
	{
		
		public ByteCode_bastore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "bastore";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			byte val = (byte)((int)frame.popOperand());
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
			
			System.Byte[] arr = (System.Byte[]) heapRef.obj;
			
			arr[index] = val;
			
		}
	}
}
