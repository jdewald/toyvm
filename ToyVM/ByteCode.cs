using System;

namespace ToyVM
{
	/// <summary>
	/// Summary description for ByteCode.
	/// </summary>
	public abstract class ByteCode
	{
		byte opCode;
		protected string name = "";
		protected int size = 1;
		public ByteCode(byte code)
		{
			this.opCode = code;
		}

		
		public string getName() { return String.Format("{0:X} {1}",opCode,name); }
		public int getSize() { return size; }
		public override string ToString()
		{
			return getName();
		}

		public virtual void execute(StackFrame frame){
			throw new ToyVMException("Not yet implemented " + getName(),frame );
		}
		
		
	}
}
