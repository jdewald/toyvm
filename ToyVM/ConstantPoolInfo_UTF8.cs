using System;
using System.Collections;
namespace ToyVM
{
	/// <summary>
	/// A UTF-8 String
	/// </summary>
	public class ConstantPoolInfo_UTF8 : ConstantPoolInfo
	{
		UInt16 length;
		string chars;
		public bool isUnicode;
		int[] codePoints;
		
		public ConstantPoolInfo_UTF8(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			length = reader.ReadUInt16();
			
			chars = "";
			ArrayList codePointArr = new ArrayList();
			byte[] originalChars = reader.getReader().ReadBytes(length);
			for (int i = 0; i < length; i++){
				if ((originalChars[i] & 0x80) ==0){ // ascii
					
					char theChar = (char) originalChars[i];
					chars += theChar.ToString(); // yeah bad
					codePointArr.Add((int)theChar);
				}
				else if ((originalChars[i] & 0xE0) == 0xC0){ // u0080-u07ff
					byte[] temp = new byte[2];
					int val = (originalChars[i] & 0x1F) << 6;
					i++;
					val = val | (originalChars[i] & 0x3F);
					temp[0] = (byte)((val & 0x700) >> 8);
					temp[1] = (byte)(val & 0xFF);
					chars += BitConverter.ToChar(temp,0);
					isUnicode = true;
					codePointArr.Add(val);
				}
				else if ((originalChars[i] & 0xE0) == 0xE0){ // u0800-ffff
					byte[] temp = new byte[2];
					int val = (originalChars[i] & 0xF) << 12;
					i++;
					val = val | (originalChars[i] & 0x3F);
					i++;
					val = val | (originalChars[i] & 0x3F);
					temp[0] = (byte)((val & 0xFF00) >> 8);
					temp[1] = (byte)(val & 0xFF);
					chars += BitConverter.ToChar(temp,0);
					isUnicode = true;
					codePointArr.Add(val);
				}
			}
			                                        
			
		}

		public override String getName()
		{
			
			return "UTF8";
		}

		public override string ToString()
		{
			if (! isUnicode){
				return "[" + getUTF8String() + "]";
			}
			else {
				return "[<UNICODE STRING>]";
			}
		}

		
		
		public string getUTF8String()
		{
			
			return chars;
			
		}
	}
}
