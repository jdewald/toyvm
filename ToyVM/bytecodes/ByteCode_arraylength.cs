using System;
using System.Collections;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_arraylength : ByteCode
	{
		
		public ByteCode_arraylength(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "arraylength";
			size = 1;
		}

		// ..., arrayref => ..., length
		public override void execute (StackFrame frame)
		{
			Object obj = frame.popOperand();
			
			// special handling
			if (obj is ConstantPoolInfo_String){
				ConstantPoolInfo_String stringObj = (ConstantPoolInfo_String) obj;
				frame.pushOperand(stringObj.getValue().ToCharArray().Length);
			}
			else if (obj is string){
				frame.pushOperand(((string) obj).ToCharArray().Length);
			}
			else {
				Heap.HeapReference arrayRef = (Heap.HeapReference) obj;
			
				if (! arrayRef.isArray){
					throw new ToyVMException("Expecting array but have " + arrayRef,frame);
				}
			
				frame.pushOperand(arrayRef.length);
			}
		}

		
	}
}
