using System;

namespace ToyVM
{
	/// <summary>
	/// Summary description for ConstantPoolInfo_MethodRef.
	/// </summary>
	public class ConstantPoolInfo_FieldRef : ConstantPoolInfo
	{
		UInt16 classIndex; // will reference another entry in the pool
		UInt16 nameAndTypeIndex; // will reference another entry in the pool

		// resolved later
		ConstantPoolInfo_Class theClass;
		ConstantPoolInfo_NameAndType nameAndType;

		int fieldType;
		
		public const int TYPE_INT = 0;
		public const int TYPE_FLOAT = 1;
		public const int TYPE_REF = 2;
		public const int TYPE_BOOLEAN = 3;
		public const int TYPE_LONG = 4;
		public ConstantPoolInfo_FieldRef(byte tag) : base(tag)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override void parse(MSBBinaryReaderWrapper reader)
		{
			classIndex = reader.ReadUInt16();
			nameAndTypeIndex = reader.ReadUInt16();
		}

		public override String getName()
		{
			return "FIELD_REF";
		}

		public int getFieldType(){
			return fieldType;
		}
		public override void resolve(ConstantPoolInfo[] pool)
		{
			theClass = (ConstantPoolInfo_Class)pool[classIndex - 1];
			nameAndType = (ConstantPoolInfo_NameAndType)pool[nameAndTypeIndex - 1];

			nameAndType.resolve(pool);
			if (nameAndType.getDescriptor().Equals("I")){
				fieldType = TYPE_INT;
			}
			else if (nameAndType.getDescriptor().Equals("F")){
				fieldType = TYPE_FLOAT;
			}
			else if (nameAndType.getDescriptor().Equals("Z")){
				fieldType = TYPE_BOOLEAN;
			}
			else if (nameAndType.getDescriptor().Equals("L")){
				fieldType = TYPE_LONG;
			}
			else {
				fieldType = TYPE_REF;
			}
		}

		public ConstantPoolInfo_Class getTheClass(){
			return theClass;
		}
		
		public ConstantPoolInfo_NameAndType getNameAndType(){
			return nameAndType;
		}
		public override string ToString()
		{
			if (theClass != null && nameAndType != null)
			{
				return getName() + String.Format("[{0},{1}]",theClass,nameAndType);
			}
			else 
			{
				return getName() + String.Format("[{0},{1}]",classIndex,nameAndTypeIndex);
			}
		}

	}
}
