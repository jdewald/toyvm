using System;
using System.IO;
using System.Collections;

namespace ToyVM
{
	/// <summary>
	/// A "toy" Java VM
	/// </summary>
	class ToyVM
	{
		
		public void start(string className)
		{
			try 
			{
				ClassFile mainClass = ToyVMClassLoader.loadClass(className);
				Console.WriteLine("Loaded");
				Console.WriteLine(mainClass.GetName() + " was loaded");
				//classCache[mainClass.GetName()] = mainClass;

				Console.WriteLine(mainClass.GetName() + " stored in cache");
				// initialize from the root of the class tree
				string superClassName = mainClass.GetSuperClassName();
	
				Console.WriteLine("Super class is {0}",superClassName);
				ClassFile superClass = ToyVMClassLoader.loadClass(superClassName);
				if (superClass == null)
				{
					throw new Exception("ClassLoader did not load super class!");
					//superClass = loadClass(superClassName);
				}
				
				// Execute main(String[])
				//MethodInfo info = mainClass.getMethod("main","[Ljava/lang/String;V");
				StackFrame frame = new StackFrame(mainClass.getConstantPool());
				//frame.pushOperand(mainClass);
//				info.execute(frame);
				frame.setMethod(mainClass,mainClass.getMethod("main","([Ljava/lang/String;)V"));
				mainClass.execute("main","([Ljava/lang/String;)V",frame);
				/*if (info == null) {
					throw new Exception("Unable to find 'main' method of class " + className);
				}
				else {
					StackFrame frame = new StackFrame(mainClass.getConstantPool());
					frame.pushOperand(mainClass);
					info.execute(frame);
				}
				*/
			}
			catch (ToyVMException te){
				if (te.InnerException == null){
					Console.WriteLine("{0}\n{1}",te.Message,te.getStackFrame());
				}
				else {
					Console.WriteLine("{0}:{1}\n{2}",te.InnerException,te.Message,te.getStackFrame());
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message + ":" + e.StackTrace);
				//throw e;
			}
		}
	
		
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ToyVM vm = new ToyVM();
			vm.start(args[0]);
			//vm.start("HelloWorlds");
		}
	}
}
