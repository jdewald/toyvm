using System;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Push float from local variables
	/// </summary>
	public class ByteCode_fload : ByteCode
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_fload));
		int index; // which thing are we loading
		
		public ByteCode_fload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			
			index = reader.ReadByte();
			name = "fload " + index;
			
		}
		public ByteCode_fload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "fload_" + index;
			size = 1;
			
			this.index = index;
		}

		
		public override void execute (StackFrame frame)
		{
			if (log.IsDebugEnabled) log.DebugFormat("Variable at {0} is {1}",index,frame.getLocalVariables()[index]);
			if (frame.getLocalVariables()[index] is ConstantPoolInfo_Float){		
				frame.pushOperand(((ConstantPoolInfo_Float)frame.getLocalVariables()[index]).getValue());
			}
			else if (frame.getLocalVariables()[index] is float){
				frame.pushOperand((frame.getLocalVariables()[index]));
			}
			else {
				throw new ToyVMException("Expected float got " + frame.getLocalVariables()[index],frame);
			}
		}
		
	}
}
