using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// multiply doubles
	/// </summary>
	public class ByteCode_dmul : ByteCode
	{
		
		public ByteCode_dmul(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "dmul";
			size = 1;
		}

		public override void execute (StackFrame frame)
		{
			double val1 = Double.Parse(frame.popOperand().ToString());
			double val2 = Double.Parse(frame.popOperand().ToString());

			
			//if (log.IsDebugEnabled) log.DebugFormat("val1 is {0}",val1.GetType());
			//if (log.IsDebugEnabled) log.DebugFormat("val2 is {0}",val2.GetType());
			
			
			frame.pushOperand(val1 * val2);
		}

	}
}
