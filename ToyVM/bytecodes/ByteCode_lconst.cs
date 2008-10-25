using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_lconst : ByteCode
	{
		int index; // which thing are we loading
		public ByteCode_lconst(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "lconst_" + index;
			size = 1;
			
			this.index = index;
		}

	
	}
}
