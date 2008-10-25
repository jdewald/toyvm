// ToyVMObject.cs created with MonoDevelop
// User: jdewald at 12:07 AM 9/1/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections;

namespace ToyVM
{
	
	
	public class ToyVMObject
	{
		public readonly int TYPE_PRIMITIVE = 0;
		public readonly int TYPE_REFERENCE = 1;

		protected Object ownerThread;
		protected int refCount;
		
//		ConstantPoolInfo type;
		ClassFile type;
		Hashtable fieldValues = new Hashtable();
		Heap.HeapReference heapRef;
		
		public ToyVMObject(ClassFile type)
		{
			this.type = type;
			
			//if (type is ConstantPoolInfo_Class){
				Console.WriteLine("Pretending to create new instance of {0}",type.GetName());
			//}
			//else {
				//throw new Exception("Don't know how to handle " + type.getName());
			//}
		}
		
		public void monitorEnter(){
			
			refCount++;
			
		}
		
		public void monitorExit(){
			refCount--;
			Console.WriteLine("New refCount: {0}",refCount);
		}
		
		public void setHeapReference(Heap.HeapReference reference){
			this.heapRef = reference;
		}
		
		public Object getFieldValue(ConstantPoolInfo_FieldRef field){
			string fieldName = field.getNameAndType().getRefName();
			return getFieldValue(fieldName,field.getFieldType());
		}
		
		public Object getFieldValue(string fieldName,int type){
			if (fieldValues[fieldName] == null){
				switch(type){
				case ConstantPoolInfo_FieldRef.TYPE_INT:{
					fieldValues.Add(fieldName,0);
					break;
				}
				case ConstantPoolInfo_FieldRef.TYPE_FLOAT:{
					fieldValues.Add(fieldName,0.0);
					break;
				}
				case ConstantPoolInfo_FieldRef.TYPE_BOOLEAN:{
					fieldValues.Add(fieldName,0); // false
					break;
				}
				case ConstantPoolInfo_FieldRef.TYPE_REF:{
					fieldValues.Add(fieldName,NullValue.INSTANCE);
					break;
				}
				default: {
					throw new Exception("getfield no default for " + type);
				}
				}		
			}
			Console.WriteLine("GET{0}:{1} = {2}",this.heapRef,fieldName,fieldValues[fieldName]);
			return fieldValues[fieldName];
		}
		
		public void setFieldValue(ConstantPoolInfo_FieldRef field,Object val){
			Console.WriteLine("SET{0}:{1} = {2}",this.heapRef,field,val);
			fieldValues[field.getNameAndType().getRefName()] = val;
		}
		
		public string ToString(){
			return heapRef.ToString();
		}
		
		public void copyValuesInto(ToyVMObject other){
			foreach (Object key in fieldValues.Keys){
				other.fieldValues.Add(key,fieldValues[key]);
			}
		}
	}
}
