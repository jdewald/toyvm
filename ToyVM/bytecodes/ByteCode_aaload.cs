using System;
using System.Collections;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_aaload : ByteCode
	{
		
		public ByteCode_aaload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "aaload";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
		
			
			ArrayList arr = (ArrayList) heapRef.obj;
			
			frame.pushOperand(arr[index]);
			
		}

	}
}
