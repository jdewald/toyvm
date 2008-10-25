using System;

namespace ToyVM
{
	/// <summary>
	/// Summary description for ConstantPoolInfo.
	/// </summary>
	public abstract class ConstantPoolInfo
	{
		byte tag;
		public ConstantPoolInfo(byte tag)
		{
			this.tag = tag;
		}

		public abstract void parse(MSBBinaryReaderWrapper reader);
		public abstract String getName();	
		public virtual void resolve(ConstantPoolInfo[] pool){}
	}
}
