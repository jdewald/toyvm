// VMSystem.cs created with MonoDevelop
// User: jdewald at 2:57 PM 9/6/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class VMSystem
	{
		
		public VMSystem()
		{
		}
		
		/**
		 * Calculate hash code... currently uses C# version
		 * this: int identityHashCode(Object obj)
		 */
		public static void identityHashCode(StackFrame frame){
			Console.WriteLine("Calculating hash for {0}",frame.getLocalVariables()[0]);
			Object obj = frame.getLocalVariables()[0];
			// valid?
			if (obj is NullValue){
				frame.getPrev().pushOperand(0);
			}
			else {
				frame.getPrev().pushOperand(((Heap.HeapReference)obj).address);
			}
		}
		
		/**
		 * array copy:
		 * this: void arraycopy(Object source,int start,Object dest,int start,int length) 
		 */
		public static void arraycopy(StackFrame frame){
			Console.WriteLine("Performing array copy");
			
			Object source = frame.getLocalVariables()[0];
			Console.WriteLine("Source is {0}",source);
			int start = (int)frame.getLocalVariables()[1];
			Object target = frame.getLocalVariables()[2];
			Console.WriteLine("target is {0}",target);
			int end = (int)frame.getLocalVariables()[3];
			int length = (int)frame.getLocalVariables()[4];
			
			Heap.HeapReference arrayRef = (Heap.HeapReference) target;
			Console.WriteLine("arrayref obj is {0}",arrayRef.obj);
			
			
			if (arrayRef.obj is System.Char[]){
				char[] targetChars = (System.Char[])(arrayRef.obj);
				char[] sourceChars;
				if (source is string){
					sourceChars = ((string)source).ToCharArray();
				}
				else if (source is ConstantPoolInfo_String){
					sourceChars = ((ConstantPoolInfo_String)source).getValue().ToCharArray();
				}
				else if (source is Heap.HeapReference){
					Heap.HeapReference heapRef = (Heap.HeapReference)source;
					
					if (heapRef.isArray && heapRef.isPrimitive && heapRef.primitiveType.FullName.Equals("System.Char")){
						sourceChars = (System.Char[])heapRef.obj;
					}
					else {
						throw new ToyVMException("Can only handle char arrays, have " + heapRef,frame);
					}
				}
				else {
					throw new ToyVMException("Can only copy strings, have " + source.GetType(),frame);
				}
				
				System.Array.Copy(sourceChars,start,targetChars,end,length);
				Console.Write("Target is now: ");
				for (int i = 0; i < targetChars.Length; i++){
					if (targetChars[i] > 13 && targetChars[i] < 128){
						Console.Write(targetChars[i]);
					}
				}
				Console.WriteLine("");
			}
			else if (arrayRef.obj is System.Byte[]){
				byte[] targetBytes = (System.Byte[]) arrayRef.obj;
				
				Heap.HeapReference sourceRef = (Heap.HeapReference) source;
				byte[] sourceBytes = (System.Byte[]) sourceRef.obj;
				
				System.Array.Copy(sourceBytes,start,targetBytes,end,length);
			}
			else {
				throw new ToyVMException("Target is neither a byte or a char: " + arrayRef,frame);
			}
		}
	}
}
