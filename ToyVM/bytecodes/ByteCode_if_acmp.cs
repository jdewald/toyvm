using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_if_acmp: ByteCode
	{
		UInt16 branch;

		int opval;
		const int OP_NE = 1;
		const int OP_EQ = 2;
		
		
		public ByteCode_if_acmp(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,string op) : base(code)
		{
			name = "if_acmp" + op;
			size = 3;

			branch = reader.ReadUInt16();

			if (op.Equals("ne")){
				opval = OP_NE;
			}
			else if (op.Equals("eq")){
				opval = OP_EQ;
			}
		}

		public override void execute (StackFrame frame)
		{
			Object val2 = frame.popOperand();
			Object val1 = frame.popOperand();
			//Heap.HeapReference val2 = (Heap.HeapReference) frame.popOperand();
			//Heap.HeapReference val1 = (Heap.HeapReference) frame.popOperand();

			bool eval = false;
			switch (opval){
			 case OP_EQ:{
				eval = (val1 == val2);
				
				break;
			}
			case OP_NE:{
				eval = (val1 != val2);
				break;
			}
			default: throw new ToyVMException("Not handling " + opval,frame);
			}
			
			if (eval){
				int pc = frame.getProgramCounter();
				frame.setProgramCounter(pc + branch - size);
			}
		}


		public override string ToString()
		{
			return base.ToString() + "->" + branch;
		}

	}
}
