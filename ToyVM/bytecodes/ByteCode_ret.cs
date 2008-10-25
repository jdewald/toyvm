using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Return from JSR
	/// </summary>
	public class ByteCode_ret : ByteCode
	{
		//ConstantPoolInfo reference;
		byte index;
		
		public ByteCode_ret(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ret";
			size = 2;

			index = reader.ReadByte();

			//reference = pool[index - 1];
		}

	

		public override string ToString()
		{
			return base.ToString() + "->" + index;
		}

		/**
		 * Value at index should be of type returnAddress, but we just use int
		 */
		public override void execute (StackFrame frame)
		{
			int pc = (int) frame.getLocalVariables()[index];
			frame.setProgramCounter(pc - size); // we have to decrement because executor will add
		}

	}
}
