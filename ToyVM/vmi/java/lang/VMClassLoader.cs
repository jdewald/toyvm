// VMClassLoader.cs created with MonoDevelop
// User: jdewald at 9:06 AM 10/18/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class VMClassLoader
	{
		
		public VMClassLoader()
		{
		}
		
		/**
		 * Put a class representation of the primitive type
		 */
		public static void getPrimitiveClass(StackFrame frame){
			char typeChar = (char)((int)(frame.getLocalVariables())[0]);
			
			Console.WriteLine("Returning class for {0}",typeChar);
			
			switch (typeChar){
			case 'I':{
				frame.getPrev().pushOperand(Type.GetType("System.Int32"));
				break;
			}
			case 'Z':{
				frame.getPrev().pushOperand(Type.GetType("System.Boolean"));
				break;
			}
			case 'C':{
				frame.getPrev().pushOperand(Type.GetType("System.Char"));
				break;
			}
			default:
			{
				throw new ToyVMException("Not handling " + typeChar,frame);
			}
			}
		}
	}
}
