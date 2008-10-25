using System;
using System.Collections;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_castore : ByteCode
	{
		
		public ByteCode_castore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "castore";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			char val = (char)((int)frame.popOperand());
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
			
			System.Char[] arr = (System.Char[]) heapRef.obj;
			
			arr[index] = val;
			
		}

	}
}
