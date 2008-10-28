using System;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_ldc
	/// </summary>
	public class ByteCode_ldc2_w : ByteCode
	{
		ConstantPoolInfo reference;

		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_ldc2_w));
		public ByteCode_ldc2_w(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "ldc2_w";
			size = 3;

			UInt16 index = reader.ReadUInt16();

//			if (log.IsDebugEnabled) log.DebugFormat("Index is {0}",index);
			reference = pool[index - 1];
		}

	

		public override string ToString()
		{
			return base.ToString() + "->" + reference;
		}

	}
}
