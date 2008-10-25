using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_iaload: ByteCode
	{
		

		public ByteCode_iaload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "iaload";
			size = 1;

		}

		
		/**
		 * Grabs an integer from the array and pushes onto stack as an integer
		 */
		public override void execute (StackFrame frame)
		{
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
			
			System.Int32[] arr = (System.Int32[]) heapRef.obj;
			
			frame.pushOperand((int)arr[index]);
		}
	}
}
