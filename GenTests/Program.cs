using System;

namespace GenTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DaemonLink.DaemonStdOut stdOut = new DaemonLink.DaemonStdOut();
            stdOut.EnableRedirect();
            Console.WriteLine("test");
			Console.Write('a');
			Console.Write('b');
			Console.Write('c');
            Console.WriteLine("wrote a,b,c");
            stdOut.DisableRedirect();
            Console.WriteLine("autoflush: {0}", Console.Out.GetType());
            try
			{
				throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
