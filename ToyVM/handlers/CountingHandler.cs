// CountingHandler.cs created with MonoDevelop
// User: jdewald at 3:27 PM 10/25/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;
using System.IO;
namespace ToyVM.handlers
{
	
	
		
	public class CountingHandler
	{
		
		
		string baseName;
		Hashtable methodCounters = new Hashtable();
		Hashtable byteCodeCounters = new Hashtable();
		
		public CountingHandler(string baseName)
		{
			this.baseName = baseName;
		}
		
		public void HandleMethodInfoEnterEvent(string className, string methodName, string descriptor){
			String methodRef = String.Format("{0}::{1}::{2}",className,methodName,descriptor);
			if (methodCounters[methodRef] == null){
				methodCounters[methodRef] = 1;
			}
			else {
				methodCounters[methodRef] = (int)methodCounters[methodRef] + 1;
			}
		}
		
				
		public void HandleByteCodeEnterEvent(string byteCodeName){
			
			if (byteCodeCounters[byteCodeName] == null){
				byteCodeCounters[byteCodeName] = 1;
			}
			else {
				byteCodeCounters[byteCodeName] = (int)byteCodeCounters[byteCodeName] + 1;
			}
		}
		
		public Hashtable GetMethodCounters(){
			return methodCounters;
		}
		
		public Hashtable GetByteCodeCounters(){
			return byteCodeCounters;
		}
		public void Complete(){
			
			// write out methods
			TextWriter methodWriter = new StreamWriter(baseName + "_methods.log");
			Hashtable methodCounters = GetMethodCounters();
			foreach (string key in methodCounters.Keys){
				methodWriter.WriteLine("{0},{1}",key,methodCounters[key]);
			}
			
			methodWriter.Close();
			
			// write out byte codes
			TextWriter bcWriter = new StreamWriter(baseName + "_bc.log");
			Hashtable bcCounters = GetByteCodeCounters();
			foreach (string key in bcCounters.Keys){
				bcWriter.WriteLine("{0},{1}",key,bcCounters[key]);
			}
			
			bcWriter.Close();
			
		}
	}
}
