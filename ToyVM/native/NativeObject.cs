// Object.cs created with MonoDevelop
// User: jdewald at 8:36 PM 8/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Runtime.InteropServices;
namespace ToyVM.native
{
	
	
	public class NativeObject
	{
		
		public NativeObject()
		{
		}
		
		[DllImport("libjava.so")]
		unsafe public static extern void Java_java_lang_Object_registerNatives(void * env,void * clazz, void* methods, int blah);
	}
}
