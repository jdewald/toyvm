using System;
using System.Collections;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Invoke interface method on object on stack
	/// 
	/// </summary>
	public class ByteCode_invokeinterface : ByteCode
	{
		ConstantPoolInfo_InterfaceMethodRef method;
		int count;
		public ByteCode_invokeinterface(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "invokeinterface";
			size = 5; // 2 bytes defining index into operand pool
			
			UInt16 methodIndex = reader.ReadUInt16();
			count = reader.ReadByte();
			reader.ReadByte(); // 0;
			method = (ConstantPoolInfo_InterfaceMethodRef)pool[methodIndex - 1];
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
			
			//ClassFile clazz = ToyVMClassLoader.loadClass(method.GetClassInfo().getClassName());

			
			ConstantPoolInfo_NameAndType nameAndType = method.GetMethodNameAndType();
			Console.WriteLine("Will be executing {0} on interface {1}",nameAndType,method.GetClassInfo().getClassName());
			
			int paramCount = method.getParameterCount();
			
			ArrayList locals = new ArrayList();
			for (int i = 0; i <= paramCount; i++){
				locals.Add(0);
			}
			
			// create temp version of the variables
			// so that we can get to the object
			// on the stack
			for (int i = paramCount; i >= 0; i--){
				locals[i] = frame.popOperand();
			}
			
			ClassFile clazz = ((Heap.HeapReference)locals[0]).type;
			
			StackFrame frame2 = new StackFrame(frame);
			
			frame2.setMethod(clazz,clazz.getMethod(nameAndType));
			
			Console.WriteLine("Have {0} parameters",paramCount);
		
			
			for (int i = 0;i <= paramCount; i++){
				frame2.getLocalVariables()[i] = locals[i];
				
			}
		
			clazz.execute(nameAndType,frame2);
			
			
			//Environment.Exit(0);
		}
		
	}
}
