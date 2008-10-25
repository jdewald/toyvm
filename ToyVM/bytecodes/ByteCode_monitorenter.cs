using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Request access to the object's monitor for this thread
	/// </summary>
	public class ByteCode_monitorenter : ByteCode
	{
		
		public ByteCode_monitorenter(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "monitorenter";
			size = 1;
		}

		// TODO: Actually implement threads
		public override void execute (StackFrame frame)
		{
			Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			ToyVMObject obj = (ToyVMObject) heapRef.obj;
			obj.monitorEnter();
		}

	}
}
