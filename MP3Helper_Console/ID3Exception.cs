using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3Helper_Console
{
	using System.IO;

	// ID3Exception inherits (:) Exception
	public class ID3Exception : Exception
	{
		// The constructor...
		public ID3Exception(FileInfo file, string message)
			// ...That calls the constructor of the base inherited class
			: base($"An ID3Exception has been thrown while handling {file.Name}", new Exception(message))
		{
		}
	}
}
