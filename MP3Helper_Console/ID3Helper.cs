using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3Helper_Console
{
	using System.Diagnostics;
	using System.IO;

	public static class ID3Helper
	{
		public const string V2_4MainStructureURL = @"http://id3.org/id3v2.4.0-structure";
		public const string V2_4NativeFramesURL = @"http://id3.org/id3v2.4.0-frames";
		public const string V2_3InformalStandardURL = @"http://id3.org/id3v2.3.0";
		public static readonly Encoding DefaultEncoding = Encoding.Default;

		public static Encoding CheckEncoding(this Encoding encoding)
		{
			return encoding ?? DefaultEncoding ?? Encoding.Default;
		}
		public static void ProcessDirectory(string directoryPath, SearchOption searchOption = SearchOption.AllDirectories)
		{
			ProcessDirectory(new DirectoryInfo(directoryPath), searchOption);
		}

		public static void ProcessDirectory(DirectoryInfo directory, SearchOption searchOption = SearchOption.AllDirectories, Encoding encoding = null)
		{
			encoding = encoding.CheckEncoding();

			IReadOnlyCollection<FileInfo> files = directory.GetFiles("*", searchOption);

			foreach (FileInfo file in files)
			{
				try
				{
					ID3File id3File = new ID3File(file);
				}
				catch (ID3Exception e)
				{
					Console.WriteLine($"\r{file.Name} did not process properly.!\r\n\nERROR: {e.Message}\r\n\n{e}");
				}
				catch (Exception e)
				{
					Console.WriteLine($"{file.Name} has failed to process!\r\n\nERROR: {e.Message}\r\n\n{e}");
					throw;
				}
			}
		}
	}
}
