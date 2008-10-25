// VMObject.cs created with MonoDevelop
// User: jdewald at 9:39 PM 10/17/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class VMObject
	{
		
		public VMObject()
		{
		}
		
		/**
		 * Performs shallow copy
		 * this: int identityHashCode(Object obj)
		 */
		public static void clone(StackFrame frame){
			Heap.HeapReference heapRef = (Heap.HeapReference) frame.getLocalVariables()[0];
			
			Heap.HeapReference newRef = heapRef.copy();
			
			frame.getPrev().pushOperand(newRef);
		}
		
		/**
		 * Returns Class (ClassFile) of the given object
		 */
		public static void getClass(StackFrame frame){
			Heap.HeapReference heapRef = (Heap.HeapReference)frame.getLocalVariables()[0];
			
			frame.getPrev().pushOperand(heapRef.type);
			
		}
	}
}
