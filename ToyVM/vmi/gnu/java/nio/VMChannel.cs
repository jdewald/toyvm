// VMChannel.cs created with MonoDevelop
// User: jdewald at 3:12 PM 10/18/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace ToyVM
{
	
	
	public class VMChannel
	{
		
		public VMChannel()
		{
		}
		
		/**
		 * Returns the file descriptor for stdout, note this is "supposed" to be using actual c code
		 */
		public static void stdout_fd(StackFrame frame){
			frame.getPrev().pushOperand(2);
		}
		
		public static void stderr_fd(StackFrame frame){
			frame.getPrev().pushOperand(3);
		}
		
		/**
		 * int write(int fd,ByteBuffer src)
		 */
		public static void write(StackFrame frame){
			int fd = (int)frame.getLocalVariables()[1];
			Heap.HeapReference byteBufRef = (Heap.HeapReference) frame.getLocalVariables()[2];
			
			if (fd != 2){
				throw new ToyVMException("Can only handle stdout(2), have " + fd, frame);
			}
			
			// need to get the actual byte array
			StackFrame frame2 = new StackFrame(frame);

						
			ClassFile byteBufferClass = byteBufRef.type;
			string getterType = "()[B";
			string getterName = "array";
			MethodInfo method = byteBufferClass.getMethod(getterName,getterType);
			
			if (method == null){
				foreach (MethodInfo m in byteBufferClass.getMethods()){
					Console.WriteLine(m);
				}
				throw new ToyVMException("Unable to find " + getterName + getterType,frame);
			}
			
			frame2.setMethod(byteBufferClass,method);
			
			frame2.getLocalVariables()[0] = byteBufRef;
			byteBufferClass.execute(getterName,getterType,frame2);
			
			Heap.HeapReference byteArrRef = (Heap.HeapReference) frame.popOperand();
			byte[] bytes = (byte[]) byteArrRef.obj;
		
			for (int i = 0; i < bytes.Length; i++){
				Console.Write((char)bytes[i]);
			}
			
			frame.getPrev().pushOperand(bytes.Length);
		}
	}
}
