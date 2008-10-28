using System;
using System.Collections;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_invokevirtual : ByteCode
	{
		ConstantPoolInfo_MethodRef method;
		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_invokevirtual));
		public ByteCode_invokevirtual(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "invokevirtual";
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
			ConstantPoolInfo_NameAndType nameAndType = method.GetMethodNameAndType();
			if (log.IsDebugEnabled) log.DebugFormat("Will be executing {0} on class {1}",nameAndType,method.GetClassInfo().getClassName());
			
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
				if (log.IsDebugEnabled) log.DebugFormat("Parameter {0} is {1}",i,locals[i]);
			}
			
			ClassFile clazz = null;
			// for Object::getClass we actually
			// push the ClassFile onto the stack
			if (locals[0] is Heap.HeapReference){
				clazz = ((Heap.HeapReference)locals[0]).type;
			}
			else if (locals[0] is ClassFile){
				clazz = (ClassFile) locals[0];
			}
			StackFrame frame2 = new StackFrame(frame);
			
			frame2.setMethod(clazz,clazz.getMethod(nameAndType));
			
			if (log.IsDebugEnabled) log.DebugFormat("Have {0} parameters",paramCount);
		
			
			for (int i = 0;i <= paramCount; i++){
				frame2.getLocalVariables()[i] = locals[i];
				
			}
		
			clazz.execute(nameAndType,frame2);
			
			/*
			ClassFile clazz = ToyVMClassLoader.loadClass(method.GetClassInfo().getClassName());
			
			ConstantPoolInfo_NameAndType nameAndType = method.GetMethodNameAndType();
			if (log.IsDebugEnabled) log.DebugFormat("Will be executing {0} on {1}",nameAndType,clazz.GetName());
			
			StackFrame frame2 = new StackFrame(frame);
			frame2.setMethod(clazz,clazz.getMethod(nameAndType));
			int paramCount = method.getParameterCount();
			if (log.IsDebugEnabled) log.DebugFormat("Have {0} parameters",paramCount);
			// push "this" as local variable 0
			
			
			// Store the parameters from the operand stack
			// into the local variables of the outgoing frame
			for (int i = paramCount;i >= 0; i--){
				frame2.getLocalVariables()[i]=frame.popOperand();
				if (log.IsDebugEnabled) log.DebugFormat("Set variable {0}={1}",(i),frame2.getLocalVariables()[i]);
			}
			//frame2.getLocalVariables().Insert(0,frame.popOperand());
			
			clazz.execute(nameAndType,frame2);
			
			*/
			//Environment.Exit(0);
		}
	}
}
