// VMFile.cs created with MonoDevelop
// User: jdewald at 8:27 AM 10/18/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.IO;
using log4net;
/**
 * VMFile, note that this "should" be coming through from libjavaio.so
 */
namespace ToyVM
{
	
	
	public class VMFile
	{
		
		static readonly ILog log = LogManager.GetLogger(typeof(VMFile));
		public VMFile()
		{

		}
		
		/*
		 * Determines if the the string passed in is a directory or not
		 * It also determines if the path even exists
		 */
		public static void isDirectory(StackFrame frame){
			Heap.HeapReference fileRef = (Heap.HeapReference) frame.getLocalVariables()[0];
			
			ToyVMObject fileObj = (ToyVMObject)fileRef.obj;
			
			Heap.HeapReference filenameCharRef = (Heap.HeapReference)fileObj.getFieldValue("value",ConstantPoolInfo_FieldRef.TYPE_REF);
			
			System.Char[] filenameChars = (System.Char[]) filenameCharRef.obj;
			
			string filename = new string(filenameChars);
			
			if (log.IsDebugEnabled) log.DebugFormat("Determining if {0} is a directory or not",filename);
			
			FileInfo file = new FileInfo(filename);
			
			frame.getPrev().pushOperand(((file.Exists && (file.Attributes & FileAttributes.Directory) != 0) ? 1 : 0));
			
		}
		
		public static void isFile(StackFrame frame){
			Heap.HeapReference fileRef = (Heap.HeapReference) frame.getLocalVariables()[0];
			
			ToyVMObject fileObj = (ToyVMObject)fileRef.obj;
			
			Heap.HeapReference filenameCharRef = (Heap.HeapReference)fileObj.getFieldValue("value",ConstantPoolInfo_FieldRef.TYPE_REF);
			
			System.Char[] filenameChars = (System.Char[]) filenameCharRef.obj;
			
			string filename = new string(filenameChars);
			
			if (log.IsDebugEnabled) log.DebugFormat("Determining if {0} is a file or not",filename);
			
			FileInfo file = new FileInfo(filename);
			
			frame.getPrev().pushOperand(((file.Exists && (file.Attributes & FileAttributes.Directory) == 0) ? 1 : 0));
			
		}
	}
}
