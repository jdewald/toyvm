using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_lstore : ByteCode
	{
		int index; // which thing are we loading
		
		public ByteCode_lstore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			
			index = reader.ReadByte();
			name = "lstore_" + index;
			
		}
		public ByteCode_lstore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "lstore_" + index;
			size = 1;
			
			this.index = index;
		}

		
		
		
	}
}
