using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;

namespace FileExplorer
{
	internal enum ArrowAction
	{
		Back,
		Forward,
		None
	}

	// Enum vázaný na nápad, viz konec skriptu
	internal enum StackAction
	{
		Remove,
		Update
	}

	public partial class MainWindow : Window
	{
		public static string NewName { get; set; }
		public static string NewDestination { get; set; }
		private static string SystemError { get; } = "System Error";
		
		private string CurrentPath { get; set; }
		private string Backslash { get; } = "\\";

		private Item copy = null;
		private Stack<string> backHistory = new Stack<string>();
		private Stack<string> forwardHistory = new Stack<string>();

		public MainWindow()
		{
			InitializeComponent();
			InitializeDisks();
		}

		private void InitializeDisks()
		{
			DriveInfo[] disks = null;

			try
			{
				disks = DriveInfo.GetDrives();
			}
			catch (IOException)
			{
				ShowErrorMB("Unable to load drives, application will close.");
				Application.Current.Shutdown();
			}

			foreach (DriveInfo disk in disks)
			{
				if (disk.DriveType == DriveType.Fixed || disk.DriveType == DriveType.Removable)
				{
					Disks_ListBox.Items.Add(disk.Name);
				}
			}
		}

		private DirectoryItems InitializeDI(string path)
		{
			DirectoryItems directoryItems;

			try
			{
				directoryItems = new DirectoryItems(path);
			}
			catch (Exception)
			{
				directoryItems = null;
			}

			return directoryItems;
		}

		private FileItem InitializeFI(string path)
		{
			FileItem fileItem;

			try
			{
				fileItem = new FileItem(path);
			}
			catch (Exception)
			{
				fileItem = null;
			}

			return fileItem;
		}

		private void UpdatePath(string path)
		{
			CurrentPath = path;
			Path_TextBox.Text = path;
		}

		private void UpdateItemsCount(DirectoryItems directoryItems)
		{
			ItemsCount_TextBlock.Text = directoryItems.ItemCount == 1 ? directoryItems.ItemCount + " item" : directoryItems.ItemCount + " items";
		}

		private void UpdateFilesLB(string path, ArrowAction arrowAction = ArrowAction.None, bool addToHistory = true)
		{
			DirectoryItems currentDirectoryItems = InitializeDI(path);

			if (currentDirectoryItems != null)
			{
				if (addToHistory)
				{
					if (arrowAction == ArrowAction.None || arrowAction == ArrowAction.Forward)
					{
						backHistory.Push(CurrentPath);
					}
					else if (arrowAction == ArrowAction.Back)
					{
						forwardHistory.Push(CurrentPath);
					}
				}


				UpdatePath(path);

				Files_ListBox.Items.Clear();

				AddItemsToLB(currentDirectoryItems, Files_ListBox);
				UpdateItemsCount(currentDirectoryItems);
			}
		}

		private void AddItemsToLB(DirectoryItems directoryItems, ListBox listBox)
		{
			foreach (string directory in directoryItems.Directories)
			{
				listBox.Items.Add(directory.Substring(directoryItems.Path.Length));
			}

			foreach (string file in directoryItems.Files)
			{
				listBox.Items.Add(file.Substring(directoryItems.Path.Length));
			}
		}

		private void RemoveFromHistory()
		{
			DeleteNullsFromStack(backHistory);
			DeleteNullsFromStack(forwardHistory);
		}

		private void DeleteNullsFromStack(Stack<string> historyStack)
		{
			if (historyStack.Count == 0) { return; }

			Stack<string> temp = new Stack<string>();

			while (historyStack.Count != 0)
			{
				string current = historyStack.Pop();

				if (string.IsNullOrEmpty(current))
				{
					continue;
				}
				else
				{
					temp.Push(current);
				}
			}

			while (temp.Count != 0)
			{
				historyStack.Push(temp.Pop());
			}
		}

		public static void ShowErrorMB(string errorMessage)
		{
			MessageBox.Show(errorMessage, SystemError);
		}

		private bool IsItemSelected(ListBox listBox)
		{
			return listBox.SelectedItem != null;
		}

		private static bool IsItemFile(string item)
		{
			return item.Contains(".");
		}

		private bool IsPathValid(string path, ItemType itemType)
		{
			if (itemType == ItemType.Directory)
			{
				return Directory.Exists(path);
			}
			else if (itemType == ItemType.File)
			{
				return File.Exists(path);
			}

			return false;
		}

		private void Files_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SelectedItem_TextBlock.Text = Files_ListBox.SelectedItem != null ? "item selected: " + Files_ListBox.SelectedItem.ToString() : "item selected: ";
		}

		private void Disks_ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (!IsItemSelected(Disks_ListBox)) { return; }

			string disk = Disks_ListBox.SelectedItem.ToString();

			UpdateFilesLB(disk);
		}

		private void Files_ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs mouse)
		{
			if (!IsItemSelected(Files_ListBox)) { return; }

			string item = Files_ListBox.SelectedItem.ToString();

			if (!IsItemFile(item))
			{
				string path = CurrentPath + item + Backslash;
				UpdateFilesLB(path);
			}
		}

		private void Search_Button_Click(object sender, RoutedEventArgs e)
		{
			string path = Path_TextBox.Text.Trim();

			if (IsItemFile(path))
			{
				ShowErrorMB("Path cannot contain a file");
				return;
			}

			if (!IsPathValid(path, ItemType.Directory))
			{
				ShowErrorMB("Directory doesn't exist");
				return;
			}

			path = path[path.Length - 1].ToString() == Backslash ? path : path + Backslash;
			UpdateFilesLB(path);
		}

		private void Back_Button_Click(object sender, RoutedEventArgs e)
		{
			if (backHistory.Count != 0) 
			{
				string path = backHistory.Pop();
				UpdateFilesLB(path, ArrowAction.Back);
			}
		}

		private void Forward_Button_Click(object sender, RoutedEventArgs e)
		{
			if (forwardHistory.Count != 0)
			{
				string path = forwardHistory.Pop();
				UpdateFilesLB(path, ArrowAction.Forward);
			}
		}

		private void Copy_Click(object sender, RoutedEventArgs e)
		{
			if (!IsItemSelected(Files_ListBox)) { return; }

			string item = Files_ListBox.SelectedItem.ToString();

			if (IsItemFile(item))
			{
				FileItem fileCopy = InitializeFI(CurrentPath + item);

				if (fileCopy != null)
				{
					copy = fileCopy;
				}
			}
			else
			{
				DirectoryItems directoryCopy = InitializeDI(CurrentPath + item);

				if (directoryCopy != null)
				{
					copy = directoryCopy;
				}
			}
		}

		private void Paste_Click(object sender, RoutedEventArgs e)
		{
			if (copy == null) { return; }

			if (IsItemSelected(Files_ListBox))
			{
				string item = Files_ListBox.SelectedItem.ToString();
				if (IsItemFile(item))
				{
					ShowErrorMB("Cannot paste into a file");
					return;
				}

				DirectoryItems folder = InitializeDI(CurrentPath + item);

				if (folder != null)
				{
					copy.Copy(folder.Path + Backslash);
					UpdateFilesLB(CurrentPath, addToHistory: false);
				}

				copy = null; // Tohle jsem nemusel vlastně dělat :D
			}
			else
			{
				DirectoryItems folder = InitializeDI(CurrentPath);

				if (folder != null)
				{
					copy.Copy(folder.Path);
					UpdateFilesLB(CurrentPath, addToHistory: false);
				}

				copy = null; // Tohle jsem nemusel vlastně dělat :D
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (!IsItemSelected(Files_ListBox)) { return; }

			string item = Files_ListBox.SelectedItem.ToString();
			DeleteItem(item);
		}

		private void DeleteItem(string item)
		{
			if (IsItemFile(item))
			{
				FileItem fileDelete = InitializeFI(CurrentPath + item);

				if (fileDelete != null)
				{
					fileDelete.Delete();
					UpdateFilesLB(CurrentPath, addToHistory: false);
				}

			}
			else
			{
				DirectoryItems directory = InitializeDI(CurrentPath + item);

				if (directory != null)
				{
					try
					{
						string path = directory.Path;
						directory.Delete();

						RemoveFromHistory();
						UpdateFilesLB(CurrentPath, addToHistory: false);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
		}

		private void Rename_Click(object sender, RoutedEventArgs e)
		{
			if (!IsItemSelected(Files_ListBox)) { return; }

			string item = Files_ListBox.SelectedItem.ToString();

			InputBox rename = new InputBox(InputType.Rename);
			rename.ShowDialog();

			RenameItem(item);
			UpdateFilesLB(CurrentPath, addToHistory: false);
		}

		private void RenameItem(string item)
		{
			if (NewName == null) { return; }

			if ((IsItemFile(item) && !IsItemFile(NewName)) || (!IsItemFile(item) && IsItemFile(NewName)))
			{
				ShowErrorMB("Invalid Name");
				return;
			}
			else
			{
				if (IsItemFile(item))
				{
					FileItem file = InitializeFI(CurrentPath + item);

					if (file != null)
					{
						string newPath = file.Path.Substring(0, file.Path.LastIndexOf("\\") + 1) + NewName;
						file.Move(newPath);
					}
				}
				else
				{
					DirectoryItems directory = InitializeDI(CurrentPath + item);

					if (directory != null)
					{
						string newPath = directory.Path.Substring(0, directory.Path.LastIndexOf("\\") + 1) + NewName;

						directory.Move(newPath);
						RemoveFromHistory();
					}
				}
			}
		}

		private void Move_Click(object sender, RoutedEventArgs e)
		{
			if (!IsItemSelected(Files_ListBox)) { return; }

			string item = Files_ListBox.SelectedItem.ToString();

			InputBox rename = new InputBox(InputType.Move);
			rename.ShowDialog();

			MoveItem(item);
			UpdateFilesLB(CurrentPath, addToHistory: false);
		}

		private void MoveItem(string item)
		{
			if (NewDestination == null) { return; }

			if (!IsPathValid(NewDestination, ItemType.Directory))
			{
				ShowErrorMB("Invalid path");
				return;
			}

			if (IsItemFile(item) && IsItemFile(NewDestination))
			{
				ShowErrorMB("Invalid path");
				return;
			}

			if (IsItemFile(item))
			{
				FileItem file = InitializeFI(CurrentPath + item);

				if (file != null)
				{
					file.Move(NewDestination + file.Name);
				}
			}
			else
			{
				DirectoryItems directory = InitializeDI(CurrentPath + item);

				if (directory != null)
				{
					directory.Move(NewDestination + directory.Name);
					RemoveFromHistory();
				}
			}
		}


		// Nápady na které už nezbyl čas

		/* Nápad: implementování do budoucna
		private CopyQueue<Item> copy = new CopyQueue<Item>(5); 
		private void RefreshPasteMenu()
		{
			Paste.Items.Clear();
			foreach (Item item in copy)
			{
				Paste.Items.Add(item.Name);
			}
		}

		private void UpdateInHistory(string newFolder, string oldFolder)
		{
			ModifyStackItems(newFolder, backHistory, StackAction.Update, oldFolder);
			ModifyStackItems(newFolder, forwardHistory, StackAction.Update, oldFolder);
		}

		// Nápad: nahrazení cesty v historii, pokud dojde k přejmenování adresáře, dá se předělat na obecnou i pro smazání adresáře
		private void ModifyStackItems(string folder, Stack<string> historyStack, StackAction stackAction, string oldFolder = null)
		{
			if (historyStack.Count == 0) { return; }

			Stack<string> temp = new Stack<string>();

			while (historyStack.Count != 0)
			{
				string current = historyStack.Pop();

				if (string.IsNullOrEmpty(current)) { continue; }

				if (current.Equals(folder) && stackAction == StackAction.Update)
				{
					if (!string.IsNullOrEmpty(oldFolder) && oldFolder.Equals(current))
					{
						temp.Push(current);
					}
				}
				else if (!current.Equals(folder) && stackAction == StackAction.Remove)
				{
					temp.Push(current);
				}
			}

			while (temp.Count != 0)
			{
				historyStack.Push(temp.Pop());
			}
		}*/
	}
}
