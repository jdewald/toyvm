using System;
using System.IO;
namespace ToyVM
{
	/// <summary>
	/// Summary description for MSBBinaryReader.
	/// </summary>
	public class MSBBinaryReaderWrapper 
	{
		BinaryReader reader;
		public MSBBinaryReaderWrapper(BinaryReader reader){ 
			this.reader = reader;
			
		}

		public UInt64 ReadUInt64(){
			return Swap64(reader.ReadUInt64());
		}
		
		public UInt32 ReadUInt32()
		{
			return Swap32(reader.ReadUInt32());
		}

		public UInt16 ReadUInt16()
		{
			
			return Swap16(reader.ReadUInt16());
		}

		public byte ReadByte()
		{
			return reader.ReadByte();
		}

		public String ReadString(UInt16 count)
		{
			return new String(reader.ReadChars(count));
		}

		
		public float ReadFloat(){
			// I tried hard to successfully read in IEEE-754 format
			// but failed. Plus why reinvent the wheel
		
			byte[] floatBytes = new byte[4];
			for (int i = 0; i < 4; i++){
				floatBytes[3-i] = reader.ReadByte();
			}
			
			float val = System.BitConverter.ToSingle(floatBytes,0);
			return val;
		}
		
		public double ReadDouble(){
			// I tried hard to successfully read in IEEE-754 format
			// but failed. Plus why reinvent the wheel
		
			byte[] doubleBytes = new byte[8];
			for (int i = 0; i < 8; i++){
				doubleBytes[7-i] = reader.ReadByte();
			}
			
			double val = System.BitConverter.ToDouble(doubleBytes,0);
			return val;
		}

		private UInt32 Swap32(UInt32 original)
		{
			//if (log.IsDebugEnabled) log.DebugFormat("Original: {0}/{0:X}",original);
			UInt16 firstWord = (UInt16)(original & 0xFFFF);
			UInt16 secondWord = (UInt16)((original & 0xFFFF0000) >> 16);

			return (UInt32)((Swap16(firstWord) << 16) | Swap16(secondWord));
		}
		
		private UInt64 Swap64(UInt64 original){
			UInt32 first32 = (UInt32)(original & 0xFFFFFFFF);
			UInt32 second32 = (UInt32)((original & 0xFFFFFFFF00000000) >> 32);
			
			return (UInt64)((Swap32(first32) << 32) | Swap32(second32)); 
			                
		}

		private UInt16 Swap16(UInt16 original)
		{
			return (UInt16)(((original & 0xFF) << 8) | ((original & 0xFF00) >> 8));
		}

		public BinaryReader getReader()
		{
			return reader;
		}
	}
}
