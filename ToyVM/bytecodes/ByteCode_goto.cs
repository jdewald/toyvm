using System;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_goto: ByteCode
	{
		int branch;

		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_goto));
		public ByteCode_goto(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "goto";
			size = 3;

			UInt16 temp = reader.ReadUInt16();
			if ((temp & 0x8000) != 0){
				branch = (-65536) + (temp);
			}
			else {
				branch = (int) temp;
			}
			
		}

		public override void execute (StackFrame frame)
		{
			int pc = frame.getProgramCounter();
			frame.setProgramCounter(pc + branch - size);
			if (log.IsDebugEnabled) log.DebugFormat("Going to {0}",frame.getProgramCounter() + size);
			
			
		}

	

		public override string ToString()
		{
			return base.ToString() + "->" + branch;
		}

	}
}
