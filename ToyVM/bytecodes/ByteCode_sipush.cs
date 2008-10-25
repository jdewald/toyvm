using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Push short value
	/// </summary>
	public class ByteCode_sipush : ByteCode
	{
		UInt16 value;
		public ByteCode_sipush(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "sipush";
			
			value = reader.ReadUInt16();
			size = 3;
		}

		public override void execute (StackFrame frame)
		{
			frame.pushOperand((int)((short)value));
		}

	}
}
