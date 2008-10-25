// Main.cs created with MonoDevelop
// User: jdewald at 10:41 AM 8/30/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;

namespace ToyVM
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			ToyVM vm = new ToyVM();
			vm.start(args[0]);
		}
	}
}