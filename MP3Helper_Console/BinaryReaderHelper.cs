using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3Helper_Console
{
	using System.IO;
	using System.Runtime.Remoting.Channels;

	public static class BinaryReaderHelper
	{
		/// <summary>
		/// Retrieve a string of a specified length and encoding from a given BinaryReader stream
		/// </summary>
		/// <param name="br">The <see cref="BinaryReader"/> to use for reading</param>
		/// <param name="length">The total number of bytes the string occupies</param>
		/// <param name="encoding">The <see cref="Encoding"/> to use for encoding the retrieved byte array to a string</param>
		/// <returns>A <see cref="string"/> of the specified lenth and encoding</returns>
		public static string GetString(BinaryReader br, int length, Encoding encoding)
		{
			try
			{
				// Read the string as a byte array
				// Be wary of UTF-16 encoding and other non-standard encoding platforms as they may have string characters in any number of 1 to 4 bytes per character.
				byte[] output = br.ReadBytes(length);
				// Use the specified encoding platform (class) to retrieve a string from a given byte array
				string result = encoding.GetString(output);
				// return the result
				return result;
				//return encoding.GetString(output); // Gets rid of one line of code
				//return encoding.GetString(br.ReadBytes(length)); // Would be the only line of code in the method
				// Lots of ways we can return the data. This way is pretty simple and doesn't do much in terms of error handling or validation
			}
			// Standard way of catching exceptions to keep the application running when something gets borked
			// You can have multiple catches. Check out the ID3Helper class and/or the ID3File class for examples
			catch (Exception e)
			{
				Console.WriteLine($"\r\nAN ERROR HAS OCCURRED WHILE RETRIEVING A STRING WITH A LENGTH OF {length} FROM THE BINARY STREAM!!\r\n\n{e.Message}\r\n\n{e}\r\n\n");
				throw;
			}
		}

		/// <summary>Convert an integer's byte order</summary>
		/// <param name="source">The integer to reverse</param>
		/// <returns>The reversed integer (Small if source was big and visa-versa)</returns>
		public static int ReverseInt(int source)
		{
			byte[] fileCodeBytes = BitConverter.GetBytes(source);
			Array.Reverse(fileCodeBytes);
			//string fileCodeByteString = fileCodeBytes.ToString();
			return BitConverter.ToInt32(fileCodeBytes, 0);
		}

		/// <summary>
		/// Retrieve a Big-Endian (Reversed) Integer
		/// </summary>
		/// <param name="br">The <see cref="BinaryReader"/> to retrieve the <see cref="int"/> from</param>
		/// <returns>The next four bytes of the <see cref="BinaryReader"/> as an integer reversed to Little-Endian</returns>
		public static int GetMSBInt(BinaryReader br)
		{
			return ReverseInt(br.ReadInt32());
		}

		/// <summary>
		/// Retrieve a Litte-Endian (Normal) Integer
		/// </summary>
		/// <param name="br">The <see cref="BinaryReader"/> to retrieve the <see cref="int"/> from</param>
		/// <returns>The next four bytes of the <see cref="BinaryReader"/> as an integer</returns>
		public static int GetLSBInt(BinaryReader br)
		{
			return br.ReadInt32();
		}
	}
}
