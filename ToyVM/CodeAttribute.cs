using System;
using System.Collections;
namespace ToyVM
{
	/// <summary>
	/// Represents the bytecode for a method
	/// </summary>
	public class CodeAttribute : AttributeInfo
	{
		UInt16 maxLength;
		UInt16 maxLocals;
		UInt32 codeLength;
		UInt16 exceptionTableLength;

		UInt16 attributeCount;

		AttributeInfo[] attributes;
		
		LineNumberTableAttribute lineNumbers = null;
		ExceptionTableEntry[] exceptions;
		
		Hashtable code = new Hashtable();

		public CodeAttribute(ConstantPoolInfo_UTF8 utf8Name, MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool) : base(utf8Name,reader,pool)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader, ConstantPoolInfo[] pool)
		{
			//base.parse(reader,pool);
			
			maxLength = reader.ReadUInt16();
			maxLocals = reader.ReadUInt16();
			codeLength = reader.ReadUInt32();

			int readLength = 0;
			while (readLength < codeLength)
			{
				ByteCode bc = ByteCodeFactory.readByteCode(reader,pool,readLength);
				//code.Add(bc);
				//Console.WriteLine("{0}: {1}",readLength,bc);
				code.Add(readLength,bc);
				readLength += bc.getSize();
			}

			exceptionTableLength = reader.ReadUInt16();

			//Console.WriteLine("Exception table length: {0}",exceptionTableLength);
			
			exceptions = new ExceptionTableEntry[exceptionTableLength];
			
			for (int i = 0; i < exceptionTableLength; i++){
				exceptions[i] = new ExceptionTableEntry(reader);
			}
			
			attributeCount = reader.ReadUInt16();
		
			
			//Console.WriteLine("Have {0} code attributes",attributeCount);
			attributes = new AttributeInfo[attributeCount];

			for (int i = 0; i < attributeCount; i++)
			{
				attributes[i] = AttributeInfo.readAttributeInfo(reader,pool);
				
				if (attributes[i] is LineNumberTableAttribute){
					lineNumbers = (LineNumberTableAttribute) attributes[i];
					
				}
			}
		}
		
		//public ArrayList getCode(){
		/**
		 * Returns what is basically a "sparse" array of ByteCodes
		 * This is rather than having the original array because
		 * we actually pre-resolve the operands (bad idea?)
		 * TODO: This could be a big area of optimization
		 */
		public Hashtable getCode(){
			return code;
		}
		
		public int getMaxLocals(){
			return maxLocals;
		}
		
		public bool hasLineNumbers() {
			return lineNumbers != null;
		}
		
		public UInt16 getLineNumber(UInt16 position){
			return (UInt16) lineNumbers.getLineNumber(position);
		}
		                                    
		
	}
	
	class ExceptionTableEntry {
		public UInt16 startPC;
		public UInt16 endPC;
		public UInt16 handlerPC;
		public UInt16 catchType;
		
		public ExceptionTableEntry(MSBBinaryReaderWrapper reader){
			startPC = reader.ReadUInt16();
			endPC = reader.ReadUInt16();
			handlerPC = reader.ReadUInt16();
			catchType = reader.ReadUInt16();
		}
	}
}
