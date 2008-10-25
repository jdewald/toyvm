// javanio.cs created with MonoDevelop
// User: jdewald at 11:58 PM 10/17/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Runtime.InteropServices;
namespace ToyVM.native.gnu.java
{
	
	/**
	 * Represents the gnu.java.nio stuff
	 */
	public class javanio
	{
		
		public javanio()
		{
		}
		
		// gnu.java.nio.VMChannel::initIDs
		[DllImport("/home/jdewald/nativelibs/libjavanio.so")]
		unsafe public static extern void Java_gnu_java_nio_VMChannel_initIDs(void * jniEnv,void * jclass);
	}
}
