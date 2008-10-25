using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Compares 2 integers
	/// </summary>
	public class ByteCode_if_icmp: ByteCode
	{
		short branch;

		int opval;
		const int OP_GE = 1;
		const int OP_LE = 2;
		const int OP_NE = 3;
		const int OP_EQUALS = 4;
		const int OP_LT = 5;
		const int OP_GT = 6;
		public ByteCode_if_icmp(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,string op) : base(code)
		{
			name = "if_icmp" + op;
			size = 3;

			branch = (short)reader.ReadUInt16();

			if ("ge".Equals(op)){
				opval = OP_GE;
			}
			else if ("gt".Equals(op)){
				opval = OP_GT;
			}
			else if ("le".Equals(op)){
				opval = OP_LE;
			}
			else if ("ne".Equals(op)){
				opval = OP_NE;
			}
			else if ("eq".Equals(op)){
				opval = OP_EQUALS;
			}
			else if ("lt".Equals(op)){
				opval = OP_LT;
			}
		}

		

		public override string ToString()
		{
			return base.ToString() + "->" + branch;
		}
		
		public override void execute (StackFrame frame)
		{
			int val2 = (int) frame.popOperand();
			int val1 = (int) frame.popOperand();

			bool eval = false;
			switch (opval){
			 case OP_GE:{
				eval = (val1 >= val2);
				
				break;
			}
			case OP_GT:{
				eval = (val1 > val2);
				break;
			}
			case OP_LE:{
				eval = (val1 <= val2);
				break;
			}
			case OP_LT:{
				eval = (val1 < val2);
				break;
			}
			case OP_NE:{
				eval = (val1 != val2);
				break;
			}
			case OP_EQUALS:{
				eval = (val1 == val2);
				break;
			}
			default: throw new ToyVMException("Not handling " + opval,frame);
			}
			
			if (eval){
				int pc = frame.getProgramCounter();
				frame.setProgramCounter(pc + branch - size);
			}
		}


	}
}
