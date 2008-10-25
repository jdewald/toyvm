using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Duplicates the top entry 1 or 2 entries in the operand stack
	/// </summary>
	public class ByteCode_dup2 : ByteCode
	{
		int depth = 0;
		public ByteCode_dup2(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int depth) : base(code)
		{
			name = "dup2";
			size = 1;
			this.depth = depth;
		}

		public override void execute (StackFrame frame)
		{
			if (depth > 0){
				throw new ToyVMException("Don't support dup2 with depth of " + depth,frame);
			}
			Object obj = frame.popOperand();
			Object obj2 = null;
			if (frame.hasMoreOperands()){
				obj2 = frame.popOperand();
			}
			
			/*System.Collections.Stack temp = new System.Collections.Stack();
			for (int i = 0; i < depth; i++){
				temp.Push(frame.popOperand());
			}
			frame.pushOperand(obj); // insert at depth depth
			
			while (temp.Count > 0){
				frame.pushOperand(temp.Pop());
			}
			*/
			frame.pushOperand(obj); // put the duplicated one back on top
			if (obj2 != null){
				frame.pushOperand(obj2);
			}
			
			frame.pushOperand(obj); // put the duplicated one back on top
			if (obj2 != null){
				frame.pushOperand(obj2);
			}
		}

	}
}
