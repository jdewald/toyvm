// VMClass.cs created with MonoDevelop
// User: jdewald at 12:33 AM 10/18/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class VMClass
	{
		
		public VMClass()
		{
		}
		
		public static void getName(StackFrame frame){
			ClassFile clazz = (ClassFile) frame.getLocalVariables()[0];
			
			
			frame.getPrev().pushOperand(Heap.GetInstance().createString(ToyVMClassLoader.loadClass("java/lang/String"),clazz.GetName()));
		}
	}
}
