using System;

namespace FileExplorer
{
	internal abstract class Item : IEquatable<Item>
	{
		public string Name { get; private set; }
		public string Path { get; private set; }

		public Item(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path cannot be null or empty");
			}

			this.Name = path.Substring(path.LastIndexOf("\\") + 1);
			this.Path = path.Trim();
		}

		public bool Equals(Item other)
		{
			return this.Path.Equals(other.Path);
		}

		public abstract bool Copy(string destination, string path = null);

		public abstract bool Delete(string path);

		public abstract bool Move(string destination);
	}
}
