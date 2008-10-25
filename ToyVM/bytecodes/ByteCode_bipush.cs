using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Pushes byte onto stack (sign extended to int)
	/// </summary>
	public class ByteCode_bipush : ByteCode
	{
		byte value;
		public ByteCode_bipush(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "bipush";
			
			value = reader.ReadByte();
			size = 2;
		}

		public override void execute (StackFrame frame)
		{
			frame.pushOperand((int) value);
		}

	}
}
