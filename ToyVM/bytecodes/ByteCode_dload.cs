using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Push double from local variables
	/// </summary>
	public class ByteCode_dload : ByteCode
	{
		int index; // which thing are we loading
		
		public ByteCode_dload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			
			index = reader.ReadByte();
			name = "dload " + index;
			
		}
		public ByteCode_dload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "dload_" + index;
			size = 1;
			
			this.index = index;
		}

		
		
		
	}
}
