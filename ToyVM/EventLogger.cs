// EventLogger.cs created with MonoDevelop
// User: jdewald at 1:07 PM 10/25/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.IO;
namespace ToyVM
{
	
	
	public class EventLogger
	{
		TextWriter writer;
		public EventLogger(string filename)
		{
			writer = new StreamWriter(filename);
		}
		
		public void handleClassLoaded(string className){
			writer.WriteLine("Loaded class {0}", className);
			writer.Flush();
		}
		
		~EventLogger() {
			writer.Close();
		}
		
	
	}
}
