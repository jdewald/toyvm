// VMStackWalker.cs created with MonoDevelop
// User: jdewald at 7:08 PM 9/3/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;

namespace ToyVM
{
	
	
	public class VMStackWalker
	{
		
		public VMStackWalker()
		{
		}

		/**
		 * "native" implementation of Class[] VMStackWalker::getClassContext()
		 */
		public static void getClassContext(StackFrame frame){
			
			// should be an array ref
			Heap.HeapReference heapRef = Heap.GetInstance().newArray(ToyVMClassLoader.loadClass("java/lang/Class"),4);
			
			((ArrayList)heapRef.obj).Add(frame.getThis());
			((ArrayList)heapRef.obj).Add(frame.getThis());
			((ArrayList)heapRef.obj).Add(frame.getThis());
			
			// since this is a method call we are just
			// storing the return value
			frame.getPrev().pushOperand(heapRef);
			
		}
		
		// TODO: Right now we always assume bootstrap loader
		public static void getClassLoader(StackFrame frame){
			frame.getPrev().pushOperand(NullValue.INSTANCE);
		}
	}
}
