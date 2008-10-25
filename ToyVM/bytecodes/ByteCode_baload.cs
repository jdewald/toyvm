using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_baload : ByteCode
	{
		
		public ByteCode_baload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "baload";
			size = 1;
		}
		
		
		/**
		 * Grabs a byte from the array and pushes onto stack as an integer
		 */
		public override void execute (StackFrame frame)
		{
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
			
			System.Byte[] arr = (System.Byte[]) heapRef.obj;
			
			frame.pushOperand((int)arr[index]);
		}
	}
}
