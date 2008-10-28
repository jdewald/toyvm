using System;
using log4net;
namespace ToyVM.bytecodes
{
	/// <summary>
	/// Summary description for ByteCode_aload0.
	/// </summary>
	public class ByteCode_new : ByteCode
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ByteCode_new));
		// can acctually be Array,Class or Interface
		ConstantPoolInfo_Class classRef;
		
		UInt16 classIndex;
		public ByteCode_new(byte code,MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool) : base(code)
		{
			name = "new";
			size = 3; // 2 bytes defining index into operand pool
			
			classIndex = reader.ReadUInt16();
			classRef = (ConstantPoolInfo_Class)pool[classIndex - 1];
		}

		public override string ToString()
		{
			return base.ToString () + "->" + classRef.ToString(); 
		}

		public override void execute (StackFrame frame)
		{
			//ToyVMObject obj = new ToyVMObject(classRef);
			Heap.HeapReference heapRef = Heap.GetInstance().newInstance(ToyVMClassLoader.loadClass(classRef.getClassName()));
			
			if (log.IsDebugEnabled) log.DebugFormat("executing new from {0}",frame);
			frame.pushOperand(heapRef);
		}

	}
}
