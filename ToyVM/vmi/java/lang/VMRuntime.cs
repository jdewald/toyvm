// VMRuntime.cs created with MonoDevelop
// User: jdewald at 11:18 PM 10/17/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Runtime.InteropServices;

namespace ToyVM
{
	
	
	public class VMRuntime
	{
		
		public VMRuntime()
		{
		}
		
		/**
		 * Returns full path to file based on short name of lib
		 */
		public static void mapLibraryName(StackFrame frame){
			Heap.HeapReference nameRef = (Heap.HeapReference) frame.getLocalVariables()[0];
			
			string shortName = Heap.GetInstance().GetStringVal(nameRef);
			
			string fullName = "lib"+shortName+".so";
			frame.getPrev().pushOperand(Heap.GetInstance().createString(ToyVMClassLoader.loadClass("java/lang/String"),fullName));
		}
	
		
		/*
		 * Pretend to load a library, but not really
		 * since we will use DllImport for now
		 */
		public static void nativeLoad(StackFrame frame){
			//loadNio();
			frame.getPrev().pushOperand(1); // "success"
		}
	}
}
