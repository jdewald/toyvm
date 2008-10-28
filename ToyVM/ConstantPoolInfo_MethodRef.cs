using System;
using System.Text.RegularExpressions;
using log4net;
namespace ToyVM
{
	/// <summary>
	/// Summary description for ConstantPoolInfo_MethodRef.
	/// </summary>
	public class ConstantPoolInfo_MethodRef : ConstantPoolInfo
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ConstantPoolInfo_MethodRef));
		UInt16 classIndex; // will reference another entry in the pool
		UInt16 nameAndTypeIndex; // will reference another entry in the pool

		// resolved later
		ConstantPoolInfo_Class theClass;
		ConstantPoolInfo_NameAndType nameAndType;

		static Regex paramAndReturnRegex = new Regex("(\\(.*\\))(.*)");
		static Regex paramRegex = new Regex("\\[?[CBIFJZD]|L[\\/\\w\\d$]+;");
		
		int parameterCount;
		public ConstantPoolInfo_MethodRef(byte tag) : base(tag)
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
			return "METHOD_REF";
		}

		public ConstantPoolInfo_Class GetClassInfo() {
			return theClass;
		}

		public ConstantPoolInfo_NameAndType GetMethodNameAndType(){
			return nameAndType;
		}
		public override void resolve(ConstantPoolInfo[] pool)
		{
			theClass = (ConstantPoolInfo_Class)pool[classIndex - 1];
			nameAndType = (ConstantPoolInfo_NameAndType)pool[nameAndTypeIndex - 1];
			nameAndType.resolve(pool);
			deriveParameterCount();
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

		public int getParameterCount(){
			return parameterCount;
		}
		
		protected void deriveParameterCount(){
			//if (log.IsDebugEnabled) log.DebugFormat("Descriptor: {0}",nameAndType.getDescriptor());
			// first break into parameters and return
			
			Match paramAndReturn = paramAndReturnRegex.Match(nameAndType.getDescriptor());
			
			// grab the 1st one - the parameters
			
			string paramString = paramAndReturn.Groups[1].ToString();
			                                         
			
			//if (log.IsDebugEnabled) log.DebugFormat("Parameters: {0}",paramString);
			MatchCollection matches = paramRegex.Matches(paramString);
			
		
			
			parameterCount = matches.Count;
		
		}
	}
}
