using System;
using System.Collections;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_aastore : ByteCode
	{
		
		public ByteCode_aastore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "aastore";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			Object val = frame.popOperand();
			int index = (int) frame.popOperand();
		    Heap.HeapReference heapRef = (Heap.HeapReference) frame.popOperand();
			
			if (! heapRef.isArray){
				throw new ToyVMException("Expected array, got " + heapRef,frame);
			}
			
			if (! heapRef.isPrimitive){
				ArrayList arr = (ArrayList) heapRef.obj;
			
				arr[index] = val;
			}
			else if (heapRef.primitiveType.Equals(Type.GetType("System.Char[]"))){
				ArrayList arr = (ArrayList) heapRef.obj;
			
				arr[index] = val;
				//System.Char[][] arr = (System.Char[][]) heapRef.obj;
				//Heap.HeapReference arrVal = (Heap.HeapReference)val;
				//arr[index] = (System.Char[])arrVal.obj;
			}
			else {
				throw new ToyVMException("Can't handle " + heapRef,frame);
			}
			Console.WriteLine("Stored {0} at index {1}",val,index);
			
		}
	}
}
