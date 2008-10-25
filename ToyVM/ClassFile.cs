using System;
using System.IO;
using System.Collections;
using System.Reflection.Emit;
using ToyVM.native.gnu.java;
using log4net;
//using ToyVM.native;
namespace ToyVM
{
	/// <summary>
	/// Summary description for ClassFile.
	/// </summary>
	public class ClassFile
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ClassFile));
		public static readonly ClassFile CLASS_INT = new ClassFile();
		public static readonly ClassFile CLASS_FLOAT = new ClassFile();
		public static readonly ClassFile CLASS_CHAR = new ClassFile();
		public static readonly ClassFile CLASS_BOOLEAN = new ClassFile();
		
		public const UInt32 CLASS_FILE_MAGIC = 0xCAFEBABE;
		public const UInt16 ACCESS_FLAG_PUBLIC = 0x0001;
		public const UInt16 ACCESS_FLAG_FINAL = 0x0010;
		public const UInt16 ACCESS_FLAG_SUPER = 0x0020;
		public const UInt16 ACCESS_FLAG_INTERFACE = 0x0200;
		public const UInt16 ACCESS_FLAG_ABSTRACT = 0x0400;
		
		Hashtable methodsByName = new Hashtable();
		bool initialized = false;
		
		UInt16 major = 0;
		UInt16 minor = 0;
		UInt16 constantPoolCount = 0;
		ConstantPoolInfo[] constantPool;
		UInt16 accessFlags = 0;
		ConstantPoolInfo_Class thisClass;
		ConstantPoolInfo_Class superClass;
		UInt16 interfaceCount = 0;
		ConstantPoolInfo_Class[] interfaces;
		UInt16 fieldCount = 0;
		FieldInfo[] fields;

		UInt16 methodCount = 0;
		MethodInfo[] methods;

		UInt16 attributeCount = 0;
		AttributeInfo[] attributes;

		ClassFile superClassFile;
		
		Hashtable staticFieldValues = new Hashtable(); // FieldInfo,value
		
		public ClassFile()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool isInitialized(){
			return initialized;
		}

		public void setInitialized(bool init){
			this.initialized = init;
		}
		
		
		/**
		 * Constructs new ClassFile from the given reader_
		 */
		public ClassFile(BinaryReader reader_)
		{
			MSBBinaryReaderWrapper reader = new MSBBinaryReaderWrapper(reader_);
			System.UInt32 magic = reader.ReadUInt32();
			
			// if it doesn't start with CLASS_FILE_MAGIC, it ain't even valid!
			if (magic != CLASS_FILE_MAGIC)
			{
				if (log.IsDebugEnabled) log.DebugFormat("Did not get proper magic...");
				if (log.IsDebugEnabled) log.DebugFormat("{0:X}",CLASS_FILE_MAGIC);
				if (log.IsDebugEnabled) log.DebugFormat("{0:X}",magic);
			}
			else 
			{
				minor = reader.ReadUInt16();
				major = reader.ReadUInt16();
				if (log.IsDebugEnabled) log.DebugFormat("{0}.{1}",major,minor);

				/**
				 * The constant pool is the set of Strings, method names, class names and numbers
				 * that will be referenced throughout the class 
				 */
				constantPoolCount = (UInt16)(reader.ReadUInt16() - 1); // the value here is the maximum 1-indexed entry
				if (log.IsDebugEnabled) log.DebugFormat("{0} constant pool entries",constantPoolCount);
				constantPool = new ConstantPoolInfo[constantPoolCount];

				/**
				 * Pass 1 - Read in the actual entries
				 */
				for (int i = 0; i < constantPoolCount; i++)
				{
					constantPool[i] = ConstantPoolInfoFactory.readConstantPoolEntry(reader);
					// Long entries take up 2 constants
					if (constantPool[i] is ConstantPoolInfo_Long || constantPool[i] is ConstantPoolInfo_Double){
						constantPool[i+1] = constantPool[i];
				        i++;                         
					}
					
					if (constantPool[i] is ConstantPoolInfo_UTF8){
						//ConstantPoolInfo_UTF8 utf8 = (ConstantPoolInfo_UTF8) constantPool[i];
						
						//if (log.IsDebugEnabled) log.DebugFormat("{0} Read {1}:{2}",i+1,constantPool[i].getName(),utf8.isUnicode ?  "(UNICODE STRING)" : constantPool[i].ToString());
					}
					else {
						//if (log.IsDebugEnabled) log.DebugFormat("{0} Read {1}:{2}",i+1,constantPool[i].getName(),constantPool[i].ToString());
					}
				}
		
				/**
				 * Pass 2 - Resolve constant pool references
				 * /
				 */
				for (int i = 0; i < constantPool.Length; i++)
				{
					constantPool[i].resolve(constantPool);
					//if (log.IsDebugEnabled) log.DebugFormat("{0} Read {1}:{2}",i+1,constantPool[i].getName(),constantPool[i].ToString());
				}

				accessFlags = reader.ReadUInt16();
				
				UInt16 thisClassIndex = reader.ReadUInt16();
				thisClass = (ConstantPoolInfo_Class)constantPool[thisClassIndex - 1];

				if (log.IsDebugEnabled) log.DebugFormat("This class: {0}",constantPool[thisClass.getNameIndex()-1]);

				UInt16 superClassIndex = reader.ReadUInt16();
				if (superClassIndex != 0){
					superClass = (ConstantPoolInfo_Class)constantPool[superClassIndex - 1];
					if (log.IsDebugEnabled) log.DebugFormat("Super class: {0}",constantPool[superClass.getNameIndex()-1]);
				}
				else if (! thisClass.getClassName().Equals("java/lang/Object")){
					throw new Exception("Only Object can have no superclasses");
				}
				interfaceCount = reader.ReadUInt16();
				if (log.IsDebugEnabled) log.DebugFormat("{0} interfaces",interfaceCount);

				if (interfaceCount > 0)
				{
					interfaces = new ConstantPoolInfo_Class[interfaceCount];
					for (int i = 0; i < interfaceCount; i++)
					{
						UInt16 ifaceIdx = reader.ReadUInt16();
						interfaces[i] = (ConstantPoolInfo_Class) constantPool[ifaceIdx -1];
					}
				}

				fieldCount = reader.ReadUInt16();
				if (log.IsDebugEnabled) log.DebugFormat("{0} fields",fieldCount);

				if (fieldCount > 0){
					fields = new FieldInfo[fieldCount];
					
					for (int i = 0; i < fieldCount; i++){
						fields[i] = new FieldInfo(reader,constantPool);
						
						if (fields[i].isStatic()){
							staticFieldValues.Add(fields[i].GetFieldName(),NullValue.INSTANCE);
						}
					}
				}
				
				methodCount = reader.ReadUInt16();
				if (log.IsDebugEnabled) log.DebugFormat("{0} methods",methodCount);
				if (methodCount > 0)
				{
					methods = new MethodInfo[methodCount];
				
					for (int i = 0; i < methodCount; i++)
					{
						methods[i] = new MethodInfo(reader,constantPool);
						//methods[i].resolve(constantPool);
						//if (log.IsDebugEnabled) log.DebugFormat("Method: {0}",methods[i]);
						//if (log.IsDebugEnabled) log.DebugFormat("Storing under {0}",methods[i].getMethodName()+methods[i].getDescriptor());
						methodsByName[methods[i].getMethodName()+methods[i].getDescriptor()] = methods[i];
						methods[i].SetClassFile(this);
					}
				}

				attributeCount = reader.ReadUInt16();
				if (log.IsDebugEnabled) log.DebugFormat("{0} attributes",attributeCount);
				attributes = new AttributeInfo[attributeCount];
				for (int i = 0; i < attributeCount; i++)
				{
					attributes[i] = AttributeInfo.readAttributeInfo(reader,constantPool);
					
					if (log.IsDebugEnabled) log.DebugFormat(attributes[i].ToString());
				}

			
			}
		}

		public void execute(string methodName, string descriptor, StackFrame frame){
			MethodInfo method = getMethod(methodName,descriptor);
			
			if (method != null){
				//frame.setMethod(this,method);
				if (! method.isNative()){
					if (log.IsDebugEnabled) log.DebugFormat("**** START {0}.{1}{2} ****",this.GetName(),methodName,descriptor);
					try {
						method.execute(frame);
					}
					catch (ToyVMException te) { throw te; }
					catch (Exception e) {
						throw new ToyVMException("Unable to execute",e,frame);
					}
					if (log.IsDebugEnabled) log.DebugFormat("**** END {0}.{1}{2} ****",this.GetName(),methodName,descriptor);
				}
				else {
					if (! handleNative(method,frame)) 
					throw new ToyVMException("Method is native!",frame);
				}
			}
			else {
				throw new Exception("Unable to retrieve " + methodName + descriptor); 
			}
		}
		
		public void execute(ConstantPoolInfo_NameAndType nameAndType, StackFrame frame){
			execute(nameAndType.getRefName(),nameAndType.getDescriptor(),frame);
		}
		
		public MethodInfo getMethod(ConstantPoolInfo_NameAndType nameAndType){
			return getMethod(nameAndType.getRefName(),nameAndType.getDescriptor());
			
		}
		
		public MethodInfo getMethod(string methodName,string descriptor){
			MethodInfo info = (MethodInfo) methodsByName[methodName + descriptor];
			if (info == null){ // look for super-class version
				
				if (superClassFile != null){
					if (log.IsDebugEnabled) log.DebugFormat("Searching for {0} in superclass {1}",methodName,superClassFile.GetName());
					return superClassFile.getMethod(methodName,descriptor);
				}
				else {
					if (log.IsDebugEnabled) log.DebugFormat("Could not find {0} and have no superclass",methodName);
				}
			}
			else {
				if (log.IsDebugEnabled) log.DebugFormat("Found {0} in {1}",methodName,thisClass.getClassName());
			}
			return info;
			
		}
		
		public System.Collections.ICollection getMethods(){
			return methodsByName.Values;
		}
		
		public Object getStaticFieldValue(string fieldName){
			if (staticFieldValues.ContainsKey(fieldName)){
				return staticFieldValues[fieldName];
			}
			else if (superClassFile != null){
				return superClassFile.getStaticFieldValue(fieldName);
			}
			else {
				throw new Exception(thisClass.getClassName() + " does not contain " + fieldName);
			}
		}
		
		public void setStaticFieldValue(string fieldName, Object value){
			if (staticFieldValues.ContainsKey(fieldName)){
				staticFieldValues[fieldName] = value;
			}
			else if (superClassFile != null){
				superClassFile.setStaticFieldValue(fieldName,value);
			}
			else {
				throw new Exception(thisClass.getClassName() + " does not contain " + fieldName);
			}
		}
		
		
		
		public ConstantPoolInfo[] getConstantPool(){
			return constantPool;
		}
		public FieldInfo[] getFields(){
			return fields;
		}
		
		public void staticInitialize(StackFrame frame){
			MethodInfo staticInit = getMethod("<clinit>","()V");
				
			if (staticInit != null){
				StackFrame newFrame = null;
				if (frame != null){
					newFrame = new StackFrame(frame);
				}
				else {
					newFrame = new StackFrame(getConstantPool());
				}
				newFrame.setMethod(this,staticInit);
				execute("<clinit>","()V", newFrame);
				//StackFrame frame = new StackFrame(getConstantPool());
				//frame.setMethod(this,staticInit);
				//staticInit.execute(frame);
			}
		}
		
		public string GetName()
		{
			return thisClass.getClassName();
		}

		
		public void SetSuperClassFile(ClassFile superClassFile){
			this.superClassFile = superClassFile;
			if (log.IsDebugEnabled) log.DebugFormat(GetName() + ": Set super class instance " + superClassFile);
		}
		
		public ClassFile GetSuperClassFile(){
			return superClassFile;
		}
		public string GetSuperClassName()
		{
			if ( superClass != null ) {
				
				return superClass.getClassName();
			}
			else {
				return null;
			}
		}
		
		public override string ToString(){
			return GetName();
		}
		
		// TODO: reflection, bitches
		public bool handleNative(MethodInfo method,StackFrame frame){
			if (this.GetName().StartsWith("gnu/classpath")){
				if (this.GetName().Equals("gnu/classpath/VMStackWalker")){
					if (method.getMethodName().Equals("getClassContext")){
						VMStackWalker.getClassContext(frame);
						return true;
					}
					if (method.getMethodName().Equals("getClassLoader")){
						VMStackWalker.getClassLoader(frame);
						return true;
					}
				}
				else if (this.GetName().Equals("gnu/classpath/VMSystemProperties")){
					if (method.getMethodName().Equals("preInit")){
						VMSystemProperties.preInit(frame);
						return true;
					}
				}
			}
			else if (this.GetName().StartsWith("gnu/java")){
				if (this.GetName().Equals("gnu/java/nio/VMChannel")){
					if (method.getMethodName().Equals("initIDs")){
						if (log.IsDebugEnabled) log.DebugFormat("DOING NOTHING FOR initIDs");
						/*unsafe {
						
							void * jniEnv = null;
							void * jclass = null;
							javanio.Java_gnu_java_nio_VMChannel_initIDs(jniEnv,jclass);
						}
						*/
						return true;
					}
					if (method.getMethodName().Equals("stdin_fd")){
						if (log.IsDebugEnabled) log.DebugFormat("RETURNING 1 FOR stdin_fd");
						/*unsafe {
						
							void * jniEnv = null;
							void * jclass = null;
							javanio.Java_gnu_java_nio_VMChannel_initIDs(jniEnv,jclass);
						}
						*/
						frame.getPrev().pushOperand(1);
						return true;
					}
					else if (method.getMethodName().Equals("stdout_fd")){
						VMChannel.stdout_fd(frame);
						return true;
					}
					else if (method.getMethodName().Equals("stderr_fd")){
						VMChannel.stderr_fd(frame);
						return true;
					}
					else if (method.getMethodName().Equals("write")){
						VMChannel.write(frame);
						return true;
					}
				}
			}
			else if (this.GetName().StartsWith("java/lang")){
				if (this.GetName().Equals("java/lang/VMSystem")){
					if (method.getMethodName().Equals("identityHashCode")){
						VMSystem.identityHashCode(frame);
						return true;
					}
					else if (method.getMethodName().Equals("arraycopy")){
						VMSystem.arraycopy(frame);
						return true;
					}
				}
				else if (this.GetName().Equals("java/lang/VMObject")){
					if (method.getMethodName().Equals("clone")){
						VMObject.clone(frame);
						return true;
					}
					if (method.getMethodName().Equals("getClass")){
						VMObject.getClass(frame);
						return true;
					}
				}
				else if (this.GetName().Equals("java/lang/VMClass")){
					if (method.getMethodName().Equals("getName")){
						VMClass.getName(frame);
						return true;
					}
				}
				else if (this.GetName().Equals("java/lang/VMClassLoader")){
					if (method.getMethodName().Equals("getPrimitiveClass")){
						VMClassLoader.getPrimitiveClass(frame);
						return true;
					}
				}
				else if (this.GetName().Equals("java/lang/VMRuntime")){
					if (method.getMethodName().Equals("mapLibraryName")){
						VMRuntime.mapLibraryName(frame);
						return true;
					}
					if (method.getMethodName().Equals("nativeLoad")){
						VMRuntime.nativeLoad(frame);
						return true;
					}
				}
				else if (this.GetName().Equals("java/lang/VMThread")){
					if (method.getMethodName().Equals("currentThread")){
						VMThread.currentThread(frame);
						return true;
					}
				}
			}
			else if (this.GetName().StartsWith("java/io")){
				if (this.GetName().Equals("java/io/VMFile")){
					if (method.getMethodName().Equals("isDirectory")){
						VMFile.isDirectory(frame);
						return true;
					}
					if (method.getMethodName().Equals("isFile")){
						VMFile.isDirectory(frame);
						return true;
					}
				}
			}
			return false;
			
		}
		
		public bool IsInterface(){
			return (accessFlags & ACCESS_FLAG_INTERFACE) != 0;
		}
		
		public bool implements(string ifaceName){
			if (interfaces != null){
			for (int i = 0; i < interfaces.Length; i++){
				if (interfaces[i].getClassName().Equals(ifaceName)) {
					return true;
				}
			}
			}
			return false;
		}
	}

	
}
