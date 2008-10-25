using System;
using System.Collections;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_iastore : ByteCode
	{
		
		public ByteCode_iastore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "iastore";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			int val = ((int)frame.popOperand());
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
			
			System.Int32[] arr = (System.Int32[]) heapRef.obj;
			
			arr[index] = val;
			
		}

	}
}
