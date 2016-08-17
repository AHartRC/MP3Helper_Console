using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3Helper_Console
{
	using System.IO;

	// Our custom object for holding the ID3 information from/for the file
	public class ID3File
	{
		// Basic constructor (mainly included usually for compatability as many things require a parameterless constructor
		public ID3File()
		{
			// Empty method body. We aren't doing anything but we still need to open/close it.
		}

		// Using a file path, we create a constructor that calls a different constructor of the same class
		public ID3File(string filePath)
			// This constructor call is using a new FileInfo object using the filePath string we send to the constructor we're in the line above
			: this(new FileInfo(filePath))
		{
		}

		// The "real" constructor that gets called when we instantiate a new object directly from a file using new Object(ourFile) (IE: new ID3File(new FileInfo("some/Amazing/Path.exe")))
		public ID3File(FileInfo file)
		{
			// Call ReadFile() method that returns a boolean and immediately check it's value
			if (!ReadFile(file))
				// If ReadFile() method returns false, reading the file failed... Throw an exception
				throw new ID3Exception(file, "Attempting to read the file during construction has failed. Unable to scaffold the ID3File model object.");
		}

		// The comments below are XML code comments that can be used to automatically generate code documentation similar to the MSDN documentation.

		/// <summary>
		/// Read the ID3 header information directly from a file
		/// </summary>
		/// <param name="file">The FileInfo object relating to the file being read</param>
		/// <param name="encoding">The <see cref="Encoding"/> to utilize when converting the byte array to a string</param>
		/// <returns>A <see cref="bool"/> of True or False indicating whether or not reading the file was successful</returns>
		private bool ReadFile(FileInfo file, Encoding encoding = null)
		{
			if (file == null
				|| !file.Exists)
				return false;

			encoding = encoding.CheckEncoding();

			using (BinaryReader br = new BinaryReader(file.OpenRead()))
			{
				string id3Header = BinaryReaderHelper.GetString(br, 3, encoding);

				if (!id3Header.Equals("ID3"))
					throw new ID3Exception(file, $"{file.Name} is not a valid media file or does not have a valid ID3 header");
			}
			return true;
		}
	}
}
