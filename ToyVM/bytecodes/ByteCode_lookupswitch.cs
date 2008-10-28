using System;
using System.Collections;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Representation of switch statement
	/// </summary>
	public class ByteCode_lookupswitch: ByteCode
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_lookupswitch));
		int defaultBranch;
		int pairs;
		
		Hashtable matchPairs = new Hashtable(); // int->int
		public ByteCode_lookupswitch(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int instructionOffset) : base(code)
		{
			instructionOffset++; // handle the byte code itself
			name = "lookupswitch";
			
			size = 1;
			// read in the padding
			
			int padCount = (4 - (instructionOffset % 4));
			if (padCount == 4) { padCount = 0; }
			for (int i = 0; i < padCount; i++){
				reader.ReadByte();
			}
			size += padCount;
			
			//if (log.IsDebugEnabled) log.DebugFormat("Read {0} pad characters, offset={1}",padCount,instructionOffset);
			// these are actually signed... will casting actually work?
			// for some reason these aren't MSB->LSB
			defaultBranch = (int)reader.ReadUInt32();
			pairs = (int)reader.ReadUInt32();
//			if (log.IsDebugEnabled) log.DebugFormat("{0}/{0:X} pairs ",pairs);
			size += 8;
			
			for (int i = 0; i < pairs; i++){
				int match = (int)reader.ReadUInt32();
				int offset = (int)reader.ReadUInt32();
				matchPairs.Add(match,offset);
				//if (log.IsDebugEnabled) log.DebugFormat("Pair: {0}->{1}",match,offset);
				size += 8;
			}
		}

	
		public override string ToString()
		{
			return base.ToString();
		}

	

	}
}
