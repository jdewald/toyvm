using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_monitorexit : ByteCode
	{
		
		public ByteCode_monitorexit(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "monitorexit";
			size = 1;
		}

		// TODO: Actually implement threads
		public override void execute (StackFrame frame)
		{
			Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			ToyVMObject obj = (ToyVMObject) heapRef.obj;
			obj.monitorExit();
		}
	}
}
