using System.Collections;
using System.Collections.Generic;

namespace FileExplorer
{
	internal class CopyQueue<T> : IEnumerable
	{
		private Queue<T> history = new Queue<T>();
		public uint Size { get; private set; }

		public CopyQueue(uint size)
		{
			this.Size = size;
		}

		public void Enqueue(T item)
		{
			history.Enqueue(item);

			if (history.Count > Size)
			{
				history.Dequeue();
			}
		}

		public void Dequeue()
		{
			if (history.Count > 0)
			{
				history.Dequeue();
			}
		}

		public IEnumerator GetEnumerator()
		{
			return history.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}