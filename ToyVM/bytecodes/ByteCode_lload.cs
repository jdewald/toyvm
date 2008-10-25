using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_lload : ByteCode
	{
		int index; // which thing are we loading
		public ByteCode_lload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			
			size = 2;
			this.index = reader.ReadByte();
			name = "lload " + index;
		}
		
		public ByteCode_lload(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int index) : base(code)
		{
			name = "lload_" + index;
			size = 1;
			this.index = index;
		}

	
		
		public override string ToString()
		{
			return base.ToString () + "->" + index; 
		}
	}
}
