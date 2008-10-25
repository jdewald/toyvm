using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_fstore : ByteCode
	{
		int index; // which thing are we loading
		
		public ByteCode_fstore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			
			index = reader.ReadByte();
			name = "fstore " + index;
			
		}
		public ByteCode_fstore(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "fstore_" + index;
			size = 1;
			
			this.index = index;
		}

		
		
		
	}
}
