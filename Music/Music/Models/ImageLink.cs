using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
	public class ImageNode
	{
		public string ImageSource { get; set; }

		public string Tag { get; set; }

		public int Num { get; set; }
	}

	public class Node<T>
	{
		public T data { get; set; }

		public Node<T> previous
		{
			get;
			set;
		}

		public Node<T> next
		{
			get;
			set;
		}

		public Node(T data)
		{
			this.data = data;
		}
	}

	public class CircularLinkedList<T>
	{
		public Node<T> head = null;

		public Node<T> tail = null;

		public int count { get; private set; } =0;

		public void Add(T element)
		{
			if (this.head == null)
			{
				this.AddFirstItem(element);
			}
			else
			{
				Node<T> newNode = new Node<T>(element);
				this.tail.next = newNode;
				newNode.previous = this.tail;
				newNode.next = this.head;

				this.tail = newNode;

				// important logic
				this.head.previous = this.tail;
			}

			count++;
		}

		public void Remove(T element)
		{
			Node<T> targetNode = this.Find(element);
			if (targetNode != null)
			{
				if (count == 1)
				{
					this.head = null;
					this.tail = null;
				}
				else if (targetNode == this.head)
				{
					this.head = targetNode.next;
					this.head.previous = targetNode.previous;

					targetNode.next = null;
					targetNode.previous = null;

					// important logic
					this.tail.next = this.head;
				}
				else if (targetNode == this.tail)
				{
					this.tail = targetNode.previous;
					this.tail.next = targetNode.next;
					targetNode.next = null;
					targetNode.previous = null;

					this.head.previous = this.tail;
				}
				else
				{
					targetNode.previous.next = targetNode.next;
					targetNode.next.previous = targetNode.previous;
					targetNode.previous = null;
					targetNode.next = null;

				}

				count--;
			}
		}

		public Node<T> Find(T element)
		{
			Node<T> result = null;
			Node<T> current = this.head;

			while (current != null)
			{
				if (Comparer<T>.Equals(current.data, element))
				{
					result = current;
					break;
				}
				else
				{
					current = current.next;
					if (current == this.head)
					{
						break;
					}
				}
			}

			return result;
		}

		private void AddFirstItem(T element)
		{
			Node<T> newNode = new Node<T>(element);

			if (this.head == null)
			{
				this.head = newNode;
				this.head.next = this.head;

				this.tail = this.head;
				this.tail.next = this.head;
				this.tail.previous = this.head;

				this.head.previous = this.tail;
			}
		}

		public void PrintNodes()
		{
			Node<T> temp = this.head;
			while (temp != null)
			{
				Console.WriteLine(temp.data);
				temp = temp.next;
				if (temp == this.head)
				{
					break;
				}
			}
		}

		public void Clear()
		{
			Node<T> temp = this.head;
			while (temp != null)
			{
				var next = temp.next;
				temp.next = null;
				temp.previous = null;
				temp = next;
				if (temp == this.head)
				{
					temp.next = null;
					temp.previous = null;
					count = 0;
					break;
				}
			}
		}
	}
}
