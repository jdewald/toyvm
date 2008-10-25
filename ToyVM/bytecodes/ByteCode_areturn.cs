using System;
using System.Threading;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Returns reference
	/// </summary>
	public class ByteCode_areturn : ByteCode
	{
		
		public ByteCode_areturn(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "areturn";
			size = 1;
		}

		
		public override void execute (StackFrame frame)
		{
			Object returnVal = frame.popOperand();
			if (returnVal is NullValue || returnVal is Heap.HeapReference || returnVal is ClassFile || returnVal is Thread){ 
				frame.getPrev().pushOperand(returnVal);
			}
			else throw new ToyVMException("areturn inconsistent " + returnVal,frame);
				
		}

	}
}
