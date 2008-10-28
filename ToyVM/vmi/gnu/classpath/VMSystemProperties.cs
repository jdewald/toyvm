// VMSystemProperties.cs created with MonoDevelop
// User: jdewald at 2:51 PM 9/6/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using log4net;
namespace ToyVM
{
	
	
	public class VMSystemProperties
	{

		static readonly ILog log = LogManager.GetLogger(typeof(VMSystemProperties));
		public VMSystemProperties()
		{
		}
		
		/**
		 * Sets the core default system properties
		 */
		public static void preInit(StackFrame sf){
			
			Heap.HeapReference propsObj = (Heap.HeapReference) sf.getLocalVariables()[0];
			ClassFile stringType = ToyVMClassLoader.loadClass("java/lang/String");
			ClassFile propsClass = (ClassFile)(propsObj.type);
			string setterType = "(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/Object;";
			string setterName = "setProperty";
			MethodInfo method = propsClass.getMethod(setterName,setterType);
			
			if (method == null){
				foreach (MethodInfo m in propsClass.getMethods()){
					if (log.IsDebugEnabled) log.DebugFormat(m.ToString());
				}
				throw new ToyVMException("Unable to find " + setterName + setterType,sf);
			}
			StackFrame frame = new StackFrame(sf);
			frame.setMethod(propsClass,method);
			
			Heap.HeapReference prop = Heap.GetInstance().createString(stringType,"java.version");
			Heap.HeapReference val = Heap.GetInstance().createString(stringType,"5");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
				
			prop = Heap.GetInstance().createString(stringType,"java.boot.class.path");
			val = Heap.GetInstance().createString(stringType,"/home/jdewald/ToyVM/ToyVM/openjdk");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vendor");
			val = Heap.GetInstance().createString(stringType,"jdewald");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vendor.url");
			val = Heap.GetInstance().createString(stringType,"http://quay.wordpress.com");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vm.vendor");
			val = Heap.GetInstance().createString(stringType,"jdewald");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vm.name");
			val = Heap.GetInstance().createString(stringType,"ToyVM");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			
			prop = Heap.GetInstance().createString(stringType,"java.vm.vendor.url");
			val = Heap.GetInstance().createString(stringType,"http://quay.wordpress.com");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vm.specification.version");
			val = Heap.GetInstance().createString(stringType,"2");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vm.specification.vendor");
			val = Heap.GetInstance().createString(stringType,"Sun");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vm.specification.url");
			val = Heap.GetInstance().createString(stringType,"http://quay.wordpress.com");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.vm.specification.version");
			val = Heap.GetInstance().createString(stringType,"http://quay.wordpress.com");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.class.version");
			val = Heap.GetInstance().createString(stringType,"49");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.home");
			val = Heap.GetInstance().createString(stringType,"/home/jdewald/ToyVM/ToyVM");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.class.path");
			val = Heap.GetInstance().createString(stringType,"/home/jdewald/ToyVM/ToyVM/openjdk");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.library.path");
			val = Heap.GetInstance().createString(stringType,"/home/jdewald/nativelibs");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"java.io.tmpdir");
			val = Heap.GetInstance().createString(stringType,"/tmp");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"os.name");
			val = Heap.GetInstance().createString(stringType,"Linux");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"os.arch");
			val = Heap.GetInstance().createString(stringType,"i386");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			
			prop = Heap.GetInstance().createString(stringType,"file.separator");
			val = Heap.GetInstance().createString(stringType,"/");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"path.separator");
			val = Heap.GetInstance().createString(stringType,":");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			
			prop = Heap.GetInstance().createString(stringType,"line.separator");
			val = Heap.GetInstance().createString(stringType,"\n");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"user.name");
			val = Heap.GetInstance().createString(stringType,"jdewald");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"user.home");
			val = Heap.GetInstance().createString(stringType,"/home/jdewald");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"user.dir");
			val = Heap.GetInstance().createString(stringType,Environment.CurrentDirectory);
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
			
			prop = Heap.GetInstance().createString(stringType,"gnu.cpu.endian");
			val = Heap.GetInstance().createString(stringType,"little");
			frame.getLocalVariables()[0] = propsObj;
			frame.getLocalVariables()[1] = prop;
			frame.getLocalVariables()[2] = val;
			propsClass.execute(setterName,setterType,frame);
		}
		
	}
}
