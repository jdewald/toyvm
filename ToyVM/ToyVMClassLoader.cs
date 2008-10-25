using System;
//using gnu.classpath;

using System.IO;
using System.Collections;

namespace ToyVM
{
	/// <summary>
	/// Summary description for ToyVMClassLoader.
	/// </summary>
	public class ToyVMClassLoader 
	{
		static Hashtable classCache = new Hashtable();
		
		public ToyVMClassLoader()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static ClassFile loadClass(string className){
			return loadClass(className, null);
		}
		public static ClassFile loadClass(string className,StackFrame frame) 
		{
			if (classCache[className] == null){
			//	try {
				string classFileName = className + ".class";
				string path = classFileName;
				Console.WriteLine("Trying {0}",path);
				if (!File.Exists(path)){
					path = "openjdk/" + path;
					Console.WriteLine("Trying {0}",path);
				}
				BinaryReader reader = new BinaryReader(File.OpenRead(path),System.Text.Encoding.UTF8);

				ClassFile classFile = new ClassFile(reader);
				
				classCache[className] = classFile;
				
				string superClassName = classFile.GetSuperClassName();
			
				if (superClassName != null){
					
					ClassFile superClass = loadClass(superClassName);
					classFile.SetSuperClassFile(superClass);
				}	
				
				bool initialized = doInitialize(classFile,frame);
				
				if (! initialized){
					throw new ToyVMException("Unable to initialize " + classFile,frame);
				}
			//	} catch (ToyVMException e){
			//		throw e;
				
			//	} catch (Exception e){
			//		if (! (e is ToyVMException)){
			//			throw new ToyVMException("Error",e,frame);
			//		}
			//	}
			}
			
			return (ClassFile) classCache[className];
								
		}

		protected static bool doInitialize(ClassFile theClass,StackFrame frame){
			if (! theClass.isInitialized()){
				Console.WriteLine("Initializing {0}",theClass);
				string superClassName = theClass.GetSuperClassName();

				if (superClassName != null) { // got to the top
					Console.WriteLine("Need to initialize {0}",superClassName);
					ClassFile superClass = (ClassFile) classCache[superClassName];
					
					if (! doInitialize(superClass,frame)){
						return false;
					}
				}

				// TODO: According to the spec, this should be in "textual order"
				Console.WriteLine("Executing static initializer");
				
				theClass.staticInitialize(frame);
				
				// now we can initialize the original class
				Console.WriteLine("Loading fields...");
				FieldInfo[] fields = theClass.getFields();
				
				
				if (fields != null){
				foreach (FieldInfo field in fields){
					if (field == null) {
						throw new Exception("FIeld is null?");
					}
					if (field.isStatic()){
						Console.WriteLine("Static initializing {0}",field);
							
						
					}
				}
				}
				
				theClass.setInitialized(true);
				return true;
			}
			return true;
		}
	}
}
