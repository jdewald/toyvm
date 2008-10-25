using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_jsr: ByteCode
	{
		short branch;

		public ByteCode_jsr(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "jsr";
			size = 3;

			branch = (short)reader.ReadUInt16();

			
		}

	

		public override string ToString()
		{
			return base.ToString() + "->" + branch;
		}

		public override void execute (StackFrame frame)
		{
			int pc = frame.getProgramCounter();
			frame.pushOperand(pc + size); 
			frame.setProgramCounter(pc + branch - size);
		}

	}
}
