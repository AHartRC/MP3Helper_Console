using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3Helper_Console
{
	using System.Diagnostics;

	// Properties/Fields/Variables don't go here... only struct/class/interface/etc declarations go here
	// Everything else goes inside of those
	class Program
	{
		// This is the starting point of the application
		// Console applications always start with an entry point similar to this
		// It doesn't always have to be MyNamespace.Program.Main(new string[]{arg1,arg2,arg3})
		// In our case it's MP3Helper_Console.Program.Main(...)
		// Entry point of the application is usually specified in the project properties.
		// In the solution explorer, the project is the second item from the very top (just below the solution item)
		// Right click that and click properties and you can set the entry point in the Application tab's "Startup Object" dropdown
		// This is the default configuration for a console application
		// the args method parameter is simply a collection of strings that represent the arguments that were passed along with the application.
		// For debugging purposes, in that very same project properties window under the debug tab, you can specify arguments to be passed while running the application in debug mode.
		// We are passing in the path of my music directory via this method to test the functionality of this application
		// (That's how we're passing it in when we hit the run at the top instead of running it like a real application)
		static void Main(string[] args)
		{
			// Strings in C# are "escaped" by a backslash.
			// Uncomment the next two lines 
			//Console.WriteLine(
			//	"This \"String\" contains \"double-quotes\",\r\n...a carriage return (\\r),\r\n...and a line feed (\\n)");
			// You can "escape" an "escape" by using double backslashes as I've done above.
			// Notice how the real escapes are single backslashes where-as the examples i give are double-escaped so I can output the value I want
			// Also check out the Console class by typing Console. and then pressing CTRL+Space to bring up the intellisense that will show you usable objects and methods from that given object


			// This is the standard method for iterating through an object array (strings in this case)
			// foreach(SomethingType something in collectionOfSomething)
			// This is a standard OOP loop and will repeat the code inside of the braces for each item in the collection (regardless of whether or not it's null as null is still an object)
			foreach (string arg in args)
			{
				// ID3Helper is a static class I created that houses a number of "static" methods that I can call without instantiating an object.
				// Instantiating an object simply means that I don't create a new instance of that object in memory . . IE: var myObject = new Object();
				// Instead, I can just utilize the class directly with MyStaticClass.SomeStaticMethod()
				// Keep in mind that console applications are always static applications (due to the Console class being a static class)
				// Put the text cursor anywhere on the "ProcessDirectory" below by clicking on it and then press F12 on your keyboard to navigate to that code's declaration
				// This is the best way to crawl through a .NET/Visual Studio application to determine the process flow and chain of events.
				ID3Helper.ProcessDirectory(arg);
			}


			// Here's some fun console play...
			// Dump out a string indicating the end of the application...
			// Leave a gap for us to replace text in a very basic fashion (without using cursor location/setting and stuff like that)
			Console.WriteLine($"\r{DateTime.Now} | END OF APPLICATION");

			// Stopwatches are uber easy to use for creating timers and benchmarking performance
			Stopwatch sw = Stopwatch.StartNew();

			// This section simply replaces the date section above with the new date (as fast as it can)
			// The reason why the Console.Write after the while declaration isn't wrapped in braces ({}) is because they are not required on single-line code groups
			int maxSeconds = 10;
			int maxMilliseconds = maxSeconds*1000;
			Console.Write($"\r{DateTime.Now} | 10.00 seconds remaining until I'll let you exit...");

			while (sw.Elapsed.Seconds < maxSeconds)
			{
				long remainingMilliseconds = maxMilliseconds - sw.ElapsedMilliseconds;
				decimal remainingSeconds = remainingMilliseconds/1000m;
				Console.Write($"\r{DateTime.Now} | {remainingSeconds}");
			}

			// TODO: Always make sure that if you start or open something to ALWAYS AND WITHOUT FAIL stop and or close it. Open connections or things that never stop running are the devil and will have no problem fucking your face should you ever forget
			sw.Stop();
			Console.Write($"\r{DateTime.Now} | END OF APPLICATION - PRESS H TO EXIT\t\t\t\t\t");

			// ReadLine() reads the whole line until the user presses enter or the exit combintation (CTRL+C)
			// ReadKey() reads a single keystroke (for Y/N selection, 0-9, modifiers, etc.)
			ConsoleKeyInfo pressedKey = Console.ReadKey();

			int counter = 0;
			while (pressedKey.Key != ConsoleKey.H)
			{
				// ++ after the value means to add 1 AFTER execution of that line of code...  
				// SomeMethod(counter++) will use the current value
				// SomeMethod(++counter) will use the current value PLUS ONE... meaning that it adds it BEFORE execution
				counter++;
				if (counter >= 13)
					// Alternatively: if(++counter >= 13)
					// This writeline has to be escaped by a CRLF due to the fact that Console.Write does not append a new line to the end so you're still on the same line we just wrote to
					// This makes sure that it's on it's own line and at the beginning instead of at the end of the current (last written) line
					Console.WriteLine("\r\nYou are in a dark place... You are likely to be eaten by a grue...");
				
				// We don't need to worry about this line replacing our previous WriteLine as that method appends the carriage return and line feed for you
				Console.Write($"\r{DateTime.Now} | END OF APPLICATION - PRESS H TO EXIT");
				// We pass a true to this ReadKey to suppress the pressed key from being output to the console window
				// True to hide, false to show (false by default/if null)
				pressedKey = Console.ReadKey(true);
			}

		}
	}
}
