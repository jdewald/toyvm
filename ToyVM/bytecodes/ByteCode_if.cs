using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_if: ByteCode
	{
		short branch;
		int opval = -1;
		
		const int OP_NOTNULL = 0;
		const int OP_NULL = 1;
		const int OP_EQUALS = 2;
		const int OP_GE = 3;
		const int OP_G = 4;
		const int OP_NE = 5;
		const int OP_LT = 6;
		const int OP_LE = 7;
		public ByteCode_if(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,string op) : base(code)
		{
			name = "if" + op;
			size = 3;

			branch = (short)reader.ReadUInt16();
			
			if (op.Equals("null")){
				opval = OP_NULL;
			}
			else if (op.Equals("nonnull")){
				opval = OP_NOTNULL;
			}
			else if (op.Equals("eq")){
				opval = OP_EQUALS;
			}
			else if (op.Equals("ge")){
				opval = OP_GE;
			}
			else if (op.Equals("gt")){
				opval = OP_G;
			}
			else if (op.Equals("ne")){
				opval = OP_NE;
			}
			else if (op.Equals("lt")){
				opval = OP_LT;
			}
			else if (op.Equals("le")){
				opval = OP_LE;
			}
		}

	
		public override string ToString()
		{
			return base.ToString() + "->" + branch;
		}

		public override void execute (StackFrame frame)
		{
			Object oper = frame.popOperand();
			Console.WriteLine("Oper is {0}",oper);
			int pc = frame.getProgramCounter();
			bool eval = false;
			switch (opval){
			case OP_NULL:{
				eval = (oper is NullValue);
				break;
			}
			case OP_GE:{
				eval = ((int) oper >= 0);
				break;
			}
			case OP_G: {
				eval = ((int) oper > 0);
				break;
			}
			case OP_NE: {
				eval = ((int) oper != 0);
				break;
			}
			case OP_NOTNULL: {
				eval = ! (oper is NullValue);
				break;
			}
			case OP_EQUALS: {
				eval = ((int)oper == 0);
				break;
			}
			case OP_LT: {
				eval = ((int) oper == -1);
				break;
			}
			case OP_LE: {
				eval = ((int) oper <= 0);
				break;
			}
			default:throw new ToyVMException("Don't know how to handle",frame);
			}
			
			if (eval){
				frame.setProgramCounter(pc + branch - size);
				Console.WriteLine("Jumping to " + (frame.getProgramCounter() + size)); 
			}
		}

	}
}
