using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Call superclass,private and initialization methods
	/// </summary>
	public class ByteCode_invokespecial : ByteCode
	{
		ConstantPoolInfo_MethodRef method;
		public ByteCode_invokespecial(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "invokespecial";
			size = 3; // 2 bytes defining index into operand pool
			
			UInt16 methodIndex = reader.ReadUInt16();
			method = (ConstantPoolInfo_MethodRef)pool[methodIndex - 1];
		}

		public override string ToString()
		{
			return base.ToString () + "->" + method.ToString(); 
		}

		/**
		 * 
		 * Operand stack
		 * ..., objectref, [arg1, [arg2 ...]]  ...
		 */
		public override void execute (StackFrame frame)
		{
			
			ClassFile clazz = ToyVMClassLoader.loadClass(method.GetClassInfo().getClassName());
			
			ConstantPoolInfo_NameAndType nameAndType = method.GetMethodNameAndType();
			Console.WriteLine("Will be executing {0} on {1}",nameAndType,clazz.GetName());
			MethodInfo methodInfo = clazz.getMethod(nameAndType);			
			StackFrame frame2 = new StackFrame(frame);
			int paramCount = method.getParameterCount();
			frame2.setMethod(clazz,methodInfo,paramCount);
			
			
			Console.WriteLine("Have {0} parameters",paramCount);
			Console.WriteLine("Max Locals: {0}",methodInfo.getMaxLocals());
			// push "this" as local variable 0
			
			
			//frame2.setThis((ToyVMObject)(frame2.getLocalVariables()[0]));
					
			// Store the parameters from the operand stack
			// into the local variables of the outgoing frame
			for (int i = paramCount;i >= 0; i--){
				frame2.getLocalVariables()[i]=frame.popOperand();
				Console.WriteLine("Set variable {0}={1}",(i),frame2.getLocalVariables()[i]);
			}
			
				
			clazz.execute(nameAndType,frame2);
			
			
			//Environment.Exit(0);
		}

	}
}
