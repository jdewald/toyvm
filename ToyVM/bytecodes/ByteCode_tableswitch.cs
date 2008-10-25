using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Representation of switch statement
	/// </summary>
	public class ByteCode_tableswitch: ByteCode
	{
		int defaultBranch;
		int low;
		int high;
		int[] offsets;
		public ByteCode_tableswitch(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool, int offset) : base(code)
		{
			// there is a 0-3 byte padding based on what the bytecode
			// offset is so that the default reference will end up
			// on a 4-byte boundary
			offset++; // handle bytecode
			name = "tableswitch";
			
			size = 1;
			
			
			int padCount = (4 - (offset % 4));
			if (padCount == 4) { padCount = 0; }
			for (int i = 0; i < padCount; i++){
				reader.ReadByte();
			}
			size += padCount;
			
			Console.WriteLine("Read {0} pad characters, offset = {1}",padCount,offset);
			
			defaultBranch = (int)reader.ReadUInt32();
			Console.WriteLine("Default: {0}/{0:X}",defaultBranch);
			low = (int)reader.ReadUInt32(); 
			high = (int)reader.ReadUInt32();
			Console.WriteLine("{0} offsets ({1}/{1:X},{2}/{2:X})",high - low + 1,high,low);
			size += 12;
			int offsetCount = (int)(high - low + 1);
			offsets = new int[offsetCount];
			
			for (int i = 0; i < offsetCount; i++){
				offsets[i] = (int) reader.ReadUInt32();
				Console.WriteLine("Offset: {0}",offsets[i]);
				size += 4;
			}
		}

	
		public override string ToString()
		{
			return base.ToString();
		}

	

	}
}
