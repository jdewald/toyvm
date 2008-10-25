// ToyVMException.cs created with MonoDevelop
// User: jdewald at 8:05 PM 9/2/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class ToyVMException : Exception
	{
		StackFrame sf;
		public ToyVMException(string message,StackFrame sf) : base(message)
		{
			this.sf = sf;
		}
		
		public ToyVMException(string message, Exception source, StackFrame sf) : base(message, source){
			this.sf = sf;
		}
		public StackFrame getStackFrame() {
			return sf;
		}
	}
}
