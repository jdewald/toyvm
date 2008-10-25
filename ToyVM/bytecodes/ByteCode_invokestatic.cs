using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Invokes a static method on a class
	/// </summary>
	public class ByteCode_invokestatic : ByteCode
	{
		ConstantPoolInfo_MethodRef method;
		public ByteCode_invokestatic(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "invokestatic";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 methodIndex = reader.ReadUInt16();
			method = (ConstantPoolInfo_MethodRef)pool[methodIndex - 1];
		}

		public override string ToString()
		{
			return base.ToString () + "->" + method.ToString(); 
		}

		public override void execute(StackFrame frame){
			ClassFile clazz = ToyVMClassLoader.loadClass(method.GetClassInfo().getClassName());
			
			ConstantPoolInfo_NameAndType nameAndType = method.GetMethodNameAndType();
			Console.WriteLine("Will be executing {0} on {1}",nameAndType,clazz.GetName());
			MethodInfo methodInfo = clazz.getMethod(nameAndType);
			// TODO: Need better way of handling access to the method
			if (methodInfo == null){
				throw new ToyVMException("Unable to locate method " + nameAndType + " on " + clazz,frame);
			}
			StackFrame frame2 = new StackFrame(frame);
			
			int paramCount = method.getParameterCount();
			frame2.setMethod(clazz,methodInfo,paramCount);
			
		
			
			Console.WriteLine("Have {0} parameters",paramCount);
			// Store the parameters from the operand stack
			// into the local variables of the outgoing frame
			for (int i = paramCount; i > 0; i--){
				frame2.getLocalVariables()[i-1]=frame.popOperand();
				Console.WriteLine("Parameter {0} = {1}",i,frame2.getLocalVariables()[i-1]);
			}
			clazz.execute(nameAndType,frame2);
			/*methodInfo.execute(frame2);
			if (methodInfo != null){
				
			}
			else {
				throw new Exception("Unable to locate " + nameAndType.ToString());
			}
			*/
		}
		                             
	}
}
