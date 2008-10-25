// StackFrame.cs created with MonoDevelop
// User: jdewald at 3:02 PM 8/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;
namespace ToyVM
{
	
	
	public class StackFrame
	{
		
		Stack operands = new Stack();
		ConstantPoolInfo[] constantPool;
		StackFrame prev;
		// TODO: handle per-method max local variables
		ArrayList localVariables;// = new ArrayList();
		ClassFile clazz;
		MethodInfo method;
		ByteCode bc;
		ToyVMObject thisObj;
		int pc = 0;
		
		public StackFrame(StackFrame prev){
			this.prev = prev;
			this.constantPool = prev.getConstantPool();
			
		}
		
		public StackFrame(ConstantPoolInfo[] pool)
		{
			this.constantPool = pool;
			
		}

		public StackFrame getPrev(){
			return prev;
		}
		
		public ConstantPoolInfo[] getConstantPool() {
			return constantPool;
		}

		public void setMethod(ClassFile file, MethodInfo method){
			setMethod(file,method,0);
		}
		
		public void setMethod(ClassFile clazz,MethodInfo method, int paramCount){
			this.clazz = clazz;
			this.method = method;

			if (localVariables == null){
			
				if (! method.isNative()){
					localVariables = new ArrayList(method.getMaxLocals());
				}				
				else {
					localVariables = new ArrayList(paramCount+1); // allow for single
					
				}
				
				for (int i = 0; i < localVariables.Capacity; i++){
					localVariables.Add(0);
				}
			}
		}
		
		public void setThis(ToyVMObject obj){
			this.thisObj = obj;
		}
		
		public ToyVMObject getThis(){
			return thisObj;
		}
		
		public void setByteCode(ByteCode bc){
			this.bc = bc;
		}
		
		public ArrayList getLocalVariables(){
			return localVariables;
		}
		
		public void pushOperand(Object operand){
			operands.Push(operand);
			if (operand is string && containsUnicode((string) operand)){
				Console.WriteLine("Popped <UNICODE>");
			}
			else Console.WriteLine("Pushed {0}",operand);
			
		}
		
		
		
		public Object popOperand(){
			
			Object popped = operands.Pop();
			
			if (popped is string && containsUnicode((string) popped)){
				Console.WriteLine("Popped <UNICODE>");
			}
			else if (popped is System.Char[]){
				Console.Write("Popped ");
				char[] chars = (char[]) popped;
				for (int i = 0; i < chars.Length; i++){
					if (chars[i] > 13 && chars[i] < 128){
						Console.Write(chars[i]);
					}
				}
				Console.WriteLine("");
			}
			else {
				Console.WriteLine("Popped {0}",popped);
			}
			return popped;
		}
		
		public bool hasMoreOperands(){
			return (operands.Peek() != null);
		}
		public int getProgramCounter(){
			return pc;
		}
		
		public void setProgramCounter(int pc){
			this.pc = pc;
		}
		
		public override string ToString(){
			return String.Format("{0}.{1}.{2}:{3}\n{4}",clazz != null ? clazz.GetName() : "Unknown Class",
			                     method != null ? method.getMethodName() : "",
			                     pc,
			                     bc != null ? bc.ToString() : "",
			                     prev != null ? prev.ToString() : "");
		}
		
		protected bool containsUnicode(string theString){
			return new System.Globalization.StringInfo(theString).LengthInTextElements < theString.Length;
		}
	}
}
