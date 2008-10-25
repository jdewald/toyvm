using System;
using System.Collections;
using System.Text.RegularExpressions;
using ToyVM.bytecodes;
using log4net;
namespace ToyVM
{
	public delegate void MethodInfoEnterEvent(string className, string methodName, string descriptor);
	public delegate void MethodInfoExitEvent(string className,string methodName,string descriptor);
	public delegate void MethodInfoByteCodeEnterEvent(string byteCodeName);
	public delegate void MethodInfoByteCodeExitEvent(string byteCodeName);

	/// <summary>
	/// Represents a Java method, including its actual bytecode (contained in a "Code" attribute)
	/// </summary>
	public class MethodInfo
	{
		static readonly ILog log = LogManager.GetLogger(typeof(MethodInfo));
		
		public static event MethodInfoEnterEvent EnterEvent;
		public static event MethodInfoExitEvent ExitEvent;
		public static event MethodInfoByteCodeEnterEvent ByteCodeEnterEvent;
		public static event MethodInfoByteCodeExitEvent ByteCodeExitEvent;
		
		UInt16 accessFlags;
		UInt16 nameIndex;
		UInt16 descriptorIndex;
		UInt16 attributeCount;
		AttributeInfo[] attributes;
		CodeAttribute methodCode;
		LineNumberTableAttribute lineNumbers;
		// resolved later
		ConstantPoolInfo_UTF8 name;
		ConstantPoolInfo_UTF8 descriptor;
		ClassFile classFile;
		string classFileName;
		public static readonly int ACCESS_PUBLIC =    0x0001;
		public static readonly int ACCESS_PRIVATE =   0x0002;
		public static readonly int ACCESS_PROTECTED = 0x0004;
		public static readonly int ACCESS_STATIC = 0x0008;
		public static readonly int ACCESS_FINAL = 0x0010;
		public static readonly int ACCESS_SYNCHRONIZED = 0x0020;
		public static readonly int ACCESS_NATIVE = 0x0100;
		public static readonly int ACCESS_ABSTRACT = 0x0400;
		public static readonly int ACCESS_STRICT = 0x0800;
		
		int parameterCount;
		
		public MethodInfo(MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool)
		{
			accessFlags = reader.ReadUInt16();
			nameIndex = reader.ReadUInt16();
			descriptorIndex = reader.ReadUInt16();
			attributeCount = reader.ReadUInt16();

			if (log.IsDebugEnabled) log.DebugFormat("Method = {0}{1}",pool[nameIndex - 1],pool[descriptorIndex-1]);
			attributes = new AttributeInfo[attributeCount];
			for (int i = 0; i < attributeCount; i++)
			{
				attributes[i] = AttributeInfo.readAttributeInfo(reader,pool);
				
				if (attributes[i] is CodeAttribute){
					methodCode = (CodeAttribute) attributes[i];
				}
				
			}

		//	if (log.IsDebugEnabled) log.DebugFormat("Name should be {0}",pool[nameIndex-1]);
			name = (ConstantPoolInfo_UTF8)pool[nameIndex - 1];
			descriptor = (ConstantPoolInfo_UTF8)pool[descriptorIndex - 1];
		
//			deriveParameterCount();
		}

		public void SetClassFile(ClassFile clazz){
			this.classFile = clazz;
			classFileName = classFile.GetName();
		}
		public void execute(StackFrame frame){
			if ( EnterEvent != null) { EnterEvent(classFileName, name.getUTF8String(),descriptor.getUTF8String()); }
			if (! isNative()) {
				Hashtable byteCodes = methodCode.getCode();
			
				ByteCode bc = null;
				frame.setProgramCounter(0);
				do {
				//foreach (ByteCode bc in byteCode){
					bc = (ByteCode) byteCodes[frame.getProgramCounter()];
					if (bc != null){
						frame.setByteCode(bc);
						
						if (log.IsDebugEnabled) log.DebugFormat("{0}: {1}",frame.getProgramCounter(),bc);
						if (methodCode.hasLineNumbers() && methodCode.getLineNumber((UInt16)frame.getProgramCounter()) != 0){
							if (log.IsDebugEnabled) log.DebugFormat(classFile.GetName() + ":{0}",methodCode.getLineNumber((UInt16)frame.getProgramCounter()));
						}
						if (ByteCodeEnterEvent != null) { ByteCodeEnterEvent(bc.getName()); }
						bc.execute(frame);
						if (ByteCodeExitEvent != null) { ByteCodeExitEvent(bc.getName()); }
						// the instruction might have moved the counter
						// but it is assumed to not have accounted
						// for its own length
						frame.setProgramCounter(frame.getProgramCounter() + bc.getSize());
					}					
					
				} while (bc != null && ! (bc is ByteCode_areturn ||
				                          bc is ByteCode_ireturn || 
				                          bc is ByteCode_freturn ||
				                          bc is ByteCode_lreturn ||
				                          bc is ByteCode_dreturn ||
				                          bc is ByteCode_return));
			}
			else {
				throw new ToyVMException("Method is native!",frame);
			}
			if (ExitEvent != null) { ExitEvent(classFileName, name.getUTF8String(),descriptor.getUTF8String()); }
		}
		
		public bool isNative(){
			return (accessFlags & ACCESS_NATIVE) != 0;
		}
		
		public bool isAbstract(){
			return (accessFlags & ACCESS_ABSTRACT) != 0;
		}
		public override string ToString()
		{
			if (name == null && descriptor == null)
			{
				return String.Format("MethodInfo:[af:{0},ni:{1},di:{2},ac:{3}]",accessFlags,nameIndex,descriptorIndex,attributeCount);
			}
			else 
			{
				string attributeString = "";
				foreach (AttributeInfo attr in attributes)
				{
					attributeString += attr;
				}

				return String.Format("MethodInfo:[af:{0},name:{1},desc:{2},ac:{3}]",accessFlags,name,descriptor,attributeString);
			}
		}

		public string getMethodName(){
			if (name != null) {
				return name.getUTF8String();
			}
			return null;
		}
		
		public string getDescriptor(){
			if (descriptor != null){
				return descriptor.getUTF8String();
			}
			return null;
		}

		public int getMaxLocals(){
			if (methodCode != null){
				return methodCode.getMaxLocals();
			}
			else {
				return 0;
			}
		}
		
		
	}
}
