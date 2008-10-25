// Heap.cs created with MonoDevelop
// User: jdewald at 11:47 PM 8/31/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headersc
//

using System;
using System.Collections;
using System.Threading;
namespace ToyVM
{

	
	// TODO: This isn't really "safe" in the sense that if a single .NET
	// runtime had multiple instances of the JVM going there would
	// actually only be a single heap!
	public class Heap
	{
		static Heap instance;
		
		int count;
		
		// could change..
		ArrayList theHeap = new ArrayList();
		Hashtable stringMap = new Hashtable(); // stringVal->instance
		Hashtable threadMap = new Hashtable(); // C# Thread->instance
		public Hashtable reverseStringMap = new Hashtable();
		Hashtable reverseThreadMap = new Hashtable(); // instance->C# 
		protected Heap()
		{
			
	
		}
		
		public static Heap GetInstance(){
			if (instance == null){
				instance = new Heap();
			}
			
			return instance;
		}

		public HeapReference createThread(Thread thread){
			ClassFile threadClass = ToyVMClassLoader.loadClass("java/lang/Thread");
			
			if (threadMap[thread] == null){
				HeapReference heapRef = newInstance(threadClass);
				
				
				threadMap[thread] = heapRef;
				reverseThreadMap[heapRef.obj] = thread;
			}
			
			return (Heap.HeapReference) threadMap[thread];
		}
		// Create or return HeapRef corresponding to this string
		public HeapReference createString(ClassFile stringType, ConstantPoolInfo_String stringConst){
			
			// the reason this method exists is because I suck and can't seem
			// to properly output unicode characters without breaking everything
			string stringVal = stringConst.getValue();
			if (stringMap[stringVal] == null){
				
				HeapReference stringRef = newInstance(stringType);
				
				StackFrame stringFrame = new StackFrame(stringType.getConstantPool());
			
				// Attempt to initialize using the character array
				MethodInfo stringInit = stringType.getMethod("<init>","([C)V");
				
				if (stringInit == null){
					throw new Exception("Unable to find init method for string");
				}
				stringFrame.setMethod(stringType,stringInit);
				stringFrame.getLocalVariables()[0] = stringRef;
				stringFrame.getLocalVariables()[1] = stringConst;
			
				Console.Write("Stringval1:");
				//stringRef.isUnicode = true; // I suck
				char[] chars = stringVal.ToCharArray();
				for (int i = 0; i < chars.Length; i++){
					if (chars[i] > 13 && chars[i] < 127){
						Console.Write(chars[i]);
					}
				}
				Console.WriteLine("");
				//Console.WriteLine(stringVal);
			//	Console.WriteLine("U:" + stringRef.isUnicode);
				stringType.execute("<init>","([C)V",stringFrame);
				stringMap.Add(stringVal,stringRef);
				reverseStringMap.Add(stringRef,stringVal);
			}
			return (HeapReference) stringMap[stringVal];
		}
		
		public HeapReference createString(ClassFile stringType, string stringVal){
			if (stringMap[stringVal] == null){
				
				HeapReference stringRef = newInstance(stringType);
				
				StackFrame stringFrame = new StackFrame(stringType.getConstantPool());
			
				// Attempt to initialize using the character array
				MethodInfo stringInit = stringType.getMethod("<init>","([C)V");
				
				if (stringInit == null){
					throw new Exception("Unable to find init method for string");
				}
				stringFrame.setMethod(stringType,stringInit);
				stringFrame.getLocalVariables()[0] = stringRef;
				stringFrame.getLocalVariables()[1] = stringVal;
			//	stringRef.isUnicode = containsUnicode(stringVal);
				char[] chars = stringVal.ToCharArray();
				Console.Write("StringVal2:");
				for (int i = 0; i < chars.Length; i++){
					if (chars[i] > 13 && chars[i] < 127){
						Console.Write(chars[i]);
					}
				}
				Console.WriteLine("");
				
			 	stringType.execute("<init>","([C)V",stringFrame);
				stringMap.Add(stringVal,stringRef);
				reverseStringMap.Add(stringRef,stringVal);
			}
			return (HeapReference) stringMap[stringVal];
			
		}
		
		public HeapReference newInstance(ClassFile type){
			int addr = theHeap.Count;
			
			HeapReference href = new HeapReference();
			href.address = addr;
			href.obj = new ToyVMObject(type);
			href.type = type;
			theHeap.Add(href);
			((ToyVMObject)href.obj).setHeapReference(href);
			return href;
		}
		
		public HeapReference newArray(ClassFile type, int length){
			int addr = theHeap.Count;
			HeapReference href = new HeapReference();
			ArrayList arr = new ArrayList(length);
			for (int i = 0; i < length; i++) { arr.Add(NullValue.INSTANCE); }
			href.obj = arr;
			href.address = addr;
			href.type = type;
			href.isArray = true;
			href.length = length;
			theHeap.Add(href);
			return href;
		}
		
		/*
		 * Create array of arrays
		 * Will basically have the actual array entries be HeapRefs
		 */
		public HeapReference new2DArray(string arrayType, int length){
			if ("C".Equals(arrayType)){
				//Heap.HeapReference arrayObj = newPrimitiveArray(Type.GetType("System.Char[]"),length);
				int addr = theHeap.Count;
				HeapReference href = new HeapReference();
				href.isPrimitive = true;
				href.primitiveType = Type.GetType("System.Char[]");
				ArrayList arr = new ArrayList(length);
				for (int i = 0; i < length; i++) { arr.Add(NullValue.INSTANCE); }
				href.obj = arr;
				href.address = addr;
				
				//href.type = type;
				
				href.isArray = true;
				href.length = length;
				theHeap.Add(href);
				return href;
				//return arrayObj;
			}
			else {
				throw new Exception("Can only create 2D char arrays, not " + arrayType);
			}				
		}
		
		public HeapReference newPrimitiveArray(Type type, int length){
			Console.WriteLine("Creatring new Primitive Array of type {0}",type);
			int addr = theHeap.Count;
			HeapReference href = new HeapReference();
			Array arr = Array.CreateInstance(type,length);
			
			//for (int i = 0; i < length; i++) { arr.Add(0); }
			
			href.obj = arr;
			href.address = addr;
			href.primitiveType = type;
			href.isPrimitive = true;
			href.isArray = true;
			href.length = length;
			theHeap.Add(href);
			return href;
		}
		
		public string GetStringVal(HeapReference heapRef){
			return (string) stringMap[heapRef];
		}
		
		protected bool containsUnicode(string theString){
			return new System.Globalization.StringInfo(theString).LengthInTextElements < theString.Length;
		}
		
		public class HeapReference
		{
			public int address;
			//public ConstantPoolInfo type;
			public ClassFile type;
			public Type primitiveType;
			//public ToyVMObject obj;
			public Object obj;
			public bool isArray;
			public bool isPrimitive;
			public bool isUnicode;
			public int length;
			
			/**
			 * Returns shallow copy of whatever is referenced here
			 */
			public HeapReference copy(){
				if (isPrimitive || isArray){
					throw new Exception("Cannot perform copy of " + this);
				}
				
				ToyVMObject vmObj = (ToyVMObject)obj;
				// note this uses assumption that nothing really happens
				HeapReference newRef = Heap.GetInstance().newInstance(type);
				
				ToyVMObject newObj = (ToyVMObject) newRef.obj;
				
				vmObj.copyValuesInto(newObj);
			
				return newRef;
			}
			public override string ToString(){
				if ((isArray == false && type.GetName().Equals("java/lang/String")) ||
				    (isPrimitive && primitiveType.FullName.Equals("System.Char"))){
					string printableString = "";
					
					char[] chars = null;
					if (! isPrimitive){
						if (Heap.GetInstance().reverseStringMap[this] != null){
							chars = ((string)Heap.GetInstance().reverseStringMap[this]).ToCharArray();
						}
					}
					else {
						chars = (System.Char[])obj;
					}
					if (chars != null){
						for (int i = 0; i < chars.Length; i++){
							if (chars[i] > 13 && chars[i] < 128){
								printableString += chars[i];
							}
						}
					}
					return String.Format("{0}/{0:X},{1},{2},{3},{4}",address, (! isPrimitive)? type.GetName() : primitiveType.ToString(),isArray,length,printableString);
				}                                                             
				else {
					return String.Format("{0}/{0:X},{1},{2},{3}",address, (! isPrimitive)? type.GetName() : primitiveType.ToString(),isArray,length);
				}
			}
			
			
	}
	}

	
	
}
