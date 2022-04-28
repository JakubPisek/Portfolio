using System;
using System.IO;

namespace FileExplorer
{
	internal class FileItem : Item
	{
		public FileItem(string path) : base(path)
		{
		}

		private bool DoesFileExist()
		{
			if (!File.Exists(Path))
			{
				MainWindow.ShowErrorMB("File no longer exists");
				return false;
			}
			return true;
		}

		public override bool Copy(string destination, string path = null)
		{
			if (!DoesFileExist()) { return false; }

			try
			{
				File.Copy(Path, destination);
				return true;
			}
			catch (Exception ex)
			{
				MainWindow.ShowErrorMB(ex.Message);
				return false;
			}
		}

		public override bool Delete(string path = null)
		{
			if (!DoesFileExist()) { return false; }

			try
			{
				File.Delete(Path);
				return true;
			}
			catch (Exception ex)
			{
				MainWindow.ShowErrorMB(ex.Message);
				return false;
			}
		}

		public override bool Move(string destination)
		{
			if (!DoesFileExist()) { return false; }

			try
			{
				File.Move(Path, destination);
				return true;
			}
			catch (Exception ex)
			{
				MainWindow.ShowErrorMB(ex.Message);
				return false;
			}
		}
	}
}
