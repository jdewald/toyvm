using System;

namespace ToyVM.bytecodes
{
	/// <summary>
	/// Duplicates the top entry in the operand stack
	/// </summary>
	public class ByteCode_dup : ByteCode
	{
		int depth;
		public ByteCode_dup(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int depth) : base(code)
		{
			name = "dup";
			size = 1;
			this.depth = depth;
		}

		public override void execute (StackFrame frame)
		{
			Object obj = frame.popOperand();
			
			System.Collections.Stack temp = new System.Collections.Stack();
			for (int i = 0; i < depth; i++){
				temp.Push(frame.popOperand());
			}
			frame.pushOperand(obj); // insert at depth depth
			
			while (temp.Count > 0){
				frame.pushOperand(temp.Pop());
			}
			
			frame.pushOperand(obj); // put the duplicated one back on top
		}

	}
}
