using System;

namespace ToyVM
{
	/// <summary>
	/// Summary description for ConstantPoolInfoFactory.
	/// </summary>
	public class ConstantPoolInfoFactory
	{
		public const byte TYPE_UTF8 = 1;
		public const byte TYPE_INTEGER = 3;
		public const byte TYPE_FLOAT = 4;
		public const byte TYPE_LONG = 5;
		public const byte TYPE_DOUBLE = 6;
		public const byte TYPE_CLASS = 7;
		public const byte TYPE_STRING = 8;
		public const byte TYPE_FIELD_REF = 9;
		public const byte TYPE_METHOD_REF = 10;
		public const byte TYPE_INTERFACE_METHOD_REF = 11;
		public const byte TYPE_NAME_AND_TYPE = 12;
		public ConstantPoolInfoFactory()
		{
			
		}

		public static ConstantPoolInfo readConstantPoolEntry(MSBBinaryReaderWrapper reader)
		{
			byte tag = reader.ReadByte();

			ConstantPoolInfo info = null;
			switch (tag)
			{
				case TYPE_CLASS: 
				{ 
					info = new ConstantPoolInfo_Class(tag);
					break;
				}
				case TYPE_METHOD_REF:
				{
					info = new ConstantPoolInfo_MethodRef(tag);
					break;
				}
			case TYPE_INTERFACE_METHOD_REF:
			{
				info = new ConstantPoolInfo_InterfaceMethodRef(tag);
				break;
			}
				case TYPE_FIELD_REF:
				{
					info = new ConstantPoolInfo_FieldRef(tag);
					break;
				}
				case TYPE_STRING:
				{
					info = new ConstantPoolInfo_String(tag);
					break;
				}
				case TYPE_UTF8:
				{
					info = new ConstantPoolInfo_UTF8(tag);
					break;
				}
				case TYPE_NAME_AND_TYPE:
				{
					info = new ConstantPoolInfo_NameAndType(tag);
					break;
				}
			case TYPE_INTEGER:
			{
				info = new ConstantPoolInfo_Integer(tag);
				break;
			}
			case TYPE_FLOAT:
			{
				info = new ConstantPoolInfo_Float(tag);
				break;
			}
			case TYPE_DOUBLE:
			{
				info = new ConstantPoolInfo_Double(tag);
				break;
			}
			case TYPE_LONG:
			{
				info = new ConstantPoolInfo_Long(tag);
				break;
			}
			
			default: 
				{
					throw new UnknownConstantPoolTypeException(String.Format("Do not know how to parse {0}",tag));
				}

			}

			
			info.parse(reader);

			return info;
		}
	}

	public class UnknownConstantPoolTypeException : Exception 
	{
		public UnknownConstantPoolTypeException(String description) : base(description){}
	}
}


