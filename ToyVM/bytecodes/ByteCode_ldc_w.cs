using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_ldc_w : ByteCode
	{
		ConstantPoolInfo reference;

		public ByteCode_ldc_w(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ldc_w";
			size = 3;

			UInt16 index = reader.ReadUInt16();

			reference = pool[index - 1];
		}

	

		public override string ToString()
		{
			return base.ToString() + "->" + reference;
		}

	}
}
