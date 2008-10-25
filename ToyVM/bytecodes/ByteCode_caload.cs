using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// load character from array
	/// </summary>
	public class ByteCode_caload : ByteCode
	{
		
		public ByteCode_caload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "caload";
			size = 1;
		}

		/**
		 * Grabs a character from the array and pushes onto stack as an integer
		 */
		public override void execute (StackFrame frame)
		{
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
			
			System.Char[] arr = (System.Char[]) heapRef.obj;
			
			frame.pushOperand((int)arr[index]);
		}
	}
}
