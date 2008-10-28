using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// void return
	/// </summary>
	public class ByteCode_fmul : ByteCode
	{
		
		public ByteCode_fmul(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "fmul";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			float val1 = Single.Parse(frame.popOperand().ToString());
			float val2 = Single.Parse(frame.popOperand().ToString());

			
			//if (log.IsDebugEnabled) log.DebugFormat("val1 is {0}",val1.GetType());
			//if (log.IsDebugEnabled) log.DebugFormat("val2 is {0}",val2.GetType());
			
			
			frame.pushOperand((float)val1 * (float)val2);
		}

	}
}
