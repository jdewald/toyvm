// VMClass.cs created with MonoDevelop
// User: jdewald at 12:33 AM 10/18/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Threading;
namespace ToyVM
{
	
	
	public class VMThread
	{
		
		public VMThread()
		{
		}

		// Thread currentThread()
		public static void currentThread(StackFrame frame){
			Thread thread = Thread.CurrentThread;
			// will return or create new java.lang.Thread instance holding the current Thread
			frame.getPrev().pushOperand(Heap.GetInstance().createThread(thread));
		}
	}
}
