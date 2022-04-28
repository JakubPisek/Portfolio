using System;
using System.IO;

namespace FileExplorer
{
	internal enum ItemType
	{
		Directory,
		File
	}

	internal class DirectoryItems : Item
	{
		public string[] Files { get; private set; }
		public string[] Directories { get; private set; }
		public int ItemCount { get; private set; }

		public DirectoryItems(string path) : base(path)
		{
			this.Directories = GetDirectoriesOrFiles(path, ItemType.Directory);
			this.Files = GetDirectoriesOrFiles(path, ItemType.File);

			if (this.Directories == null || this.Files == null)
			{
				throw new FileLoadException("Couldn't load directory items or directory simply doesn't exist");
			}

			this.ItemCount = this.Directories.Length + this.Files.Length;
		}

		private bool DoesDirectoryExist()
		{
			if (!Directory.Exists(Path))
			{
				MainWindow.ShowErrorMB("Directory no longer exists");
				return false;
			}
			return true;
		}

		private string[] GetDirectoriesOrFiles(string path, ItemType itemType)
		{
			string[] files = null;

			try
			{
				files = itemType == ItemType.Directory
					? Array.FindAll(Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly), name => !name.StartsWith(path + "$"))
					: Directory.GetFiles(path);
			}
			catch (Exception ex)
			{
				MainWindow.ShowErrorMB(ex.Message);
			}

			return files;
		}

		public override bool Copy(string destination, string path = null)
		{
			if (!DoesDirectoryExist()) { return false; }
			if (string.IsNullOrEmpty(path)) { path = Path; }

			DirectoryItems currentDirectory = new DirectoryItems(path);
			string currentPath = destination + currentDirectory.Name + "\\";

			try
			{
				Directory.CreateDirectory(currentPath);
			}
			catch (Exception ex)
			{
				MainWindow.ShowErrorMB(ex.Message);
				return false;
			}

			foreach (string subDirPath in currentDirectory.Directories)
			{
				DirectoryItems directory = new DirectoryItems(subDirPath);
				Copy(currentPath, directory.Path);
			}

			foreach (string filePath in currentDirectory.Files)
			{
				FileItem file = new FileItem(filePath);
				string destinationPath = currentPath + file.Name;
				file.Copy(destinationPath);
			}

			return true;
		}

		public override bool Delete(string path = null)
		{
			if (path == null) { path = Path; }

			string[] directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
			string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

			while (directories.Length != 0)
			{
				foreach (string directory in directories)
				{
					Delete(directory);
				}
				directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
			}

			foreach (string file in files)
			{
				FileItem item = new FileItem(file);
				item.Delete();
			}

			try
			{
				Directory.Delete(path);
			}
			catch (Exception ex)
			{
				MainWindow.ShowErrorMB(ex.Message);
				return false;
			}
			
			return true;
		}

		public override bool Move(string destination)
		{
			if (!DoesDirectoryExist()) { return false; }

			try
			{
				Directory.Move(Path, destination);
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