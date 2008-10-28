using System;
using System.IO;
using System.Collections;
using log4net;
using ToyVM.handlers;
[assembly: log4net.Config.XmlConfigurator(ConfigFile="/home/jdewald/ToyVM/log4net.config",Watch=true)]
namespace ToyVM
{
	/// <summary>
	/// A "toy" Java VM
	/// </summary>
	class ToyVM
	{
		
		static readonly ILog log = LogManager.GetLogger(typeof(ToyVM));	

		public void start(string className)
		{
			try 
			{
				EventLogger logger = new EventLogger("/home/jdewald/ToyVMEvents.log");
				CountingHandler counter = new CountingHandler("/home/jdewald/ToyVMCounter");
				
				ToyVMClassLoader.ClassLoadedEvent += logger.handleClassLoaded;
				MethodInfo.EnterEvent += counter.HandleMethodInfoEnterEvent;
				MethodInfo.ByteCodeEnterEvent += counter.HandleByteCodeEnterEvent;
				
				ClassFile mainClass = ToyVMClassLoader.loadClass(className);
				if (log.IsDebugEnabled) log.DebugFormat("Loaded");
				if (log.IsDebugEnabled) log.DebugFormat(mainClass.GetName() + " was loaded");
				//classCache[mainClass.GetName()] = mainClass;

				if (log.IsDebugEnabled) log.DebugFormat(mainClass.GetName() + " stored in cache");
				// initialize from the root of the class tree
				string superClassName = mainClass.GetSuperClassName();
	
				if (log.IsDebugEnabled) log.DebugFormat("Super class is {0}",superClassName);
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
				counter.Complete();
				
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
