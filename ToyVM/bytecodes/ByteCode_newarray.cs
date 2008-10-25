using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_newarray : ByteCode
	{
		// can acctually be Array,Class or Interface
		static readonly int T_BOOLEAN =  4;
		static readonly int T_CHAR	= 5;
		static readonly	int T_FLOAT	= 6;
		static readonly int T_DOUBLE = 7;
		static readonly int T_BYTE = 8;
		static readonly int T_SHORT	= 9;
		static readonly int T_INT = 10;
		static readonly int T_LONG	= 11;
		
		int arrayType;
		public ByteCode_newarray(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "newarray";
			size = 2; // array type
			arrayType = reader.ReadByte();
		}

		public override string ToString()
		{
			return base.ToString () + "->" + arrayType; 
		}

		public override void execute (StackFrame frame)
		{
			int count = (int) frame.popOperand();
			if (arrayType == T_CHAR){
				frame.pushOperand(Heap.GetInstance().newPrimitiveArray('\0'.GetType(),count));
			}
			else if (arrayType == T_BYTE){
				frame.pushOperand(Heap.GetInstance().newPrimitiveArray(Type.GetType("System.Byte"),count));
				
			}
			else if (arrayType == T_INT){
				frame.pushOperand(Heap.GetInstance().newPrimitiveArray(Type.GetType("System.Int32"),count));
			}
			else {
				throw new ToyVMException("Do not yet support type " + arrayType,frame);
			}
		}
	}
}
