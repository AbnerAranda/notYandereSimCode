using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200053B RID: 1339
	[ExecuteInEditMode]
	public abstract class GraphModifier : VersionedMonoBehaviour
	{
		// Token: 0x060022CB RID: 8907 RVA: 0x00195F28 File Offset: 0x00194128
		protected static List<T> GetModifiersOfType<T>() where T : GraphModifier
		{
			GraphModifier graphModifier = GraphModifier.root;
			List<T> list = new List<T>();
			while (graphModifier != null)
			{
				T t = graphModifier as T;
				if (t != null)
				{
					list.Add(t);
				}
				graphModifier = graphModifier.next;
			}
			return list;
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x00195F78 File Offset: 0x00194178
		public static void FindAllModifiers()
		{
			GraphModifier[] array = UnityEngine.Object.FindObjectsOfType(typeof(GraphModifier)) as GraphModifier[];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].enabled)
				{
					array[i].OnEnable();
				}
			}
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x00195FBC File Offset: 0x001941BC
		public static void TriggerEvent(GraphModifier.EventType type)
		{
			if (!Application.isPlaying)
			{
				GraphModifier.FindAllModifiers();
			}
			GraphModifier graphModifier = GraphModifier.root;
			if (type <= GraphModifier.EventType.PreUpdate)
			{
				switch (type)
				{
				case GraphModifier.EventType.PostScan:
					while (graphModifier != null)
					{
						graphModifier.OnPostScan();
						graphModifier = graphModifier.next;
					}
					return;
				case GraphModifier.EventType.PreScan:
					while (graphModifier != null)
					{
						graphModifier.OnPreScan();
						graphModifier = graphModifier.next;
					}
					return;
				case (GraphModifier.EventType)3:
					break;
				case GraphModifier.EventType.LatePostScan:
					while (graphModifier != null)
					{
						graphModifier.OnLatePostScan();
						graphModifier = graphModifier.next;
					}
					return;
				default:
					if (type != GraphModifier.EventType.PreUpdate)
					{
						return;
					}
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPreUpdate();
						graphModifier = graphModifier.next;
					}
					return;
				}
			}
			else
			{
				if (type == GraphModifier.EventType.PostUpdate)
				{
					while (graphModifier != null)
					{
						graphModifier.OnGraphsPostUpdate();
						graphModifier = graphModifier.next;
					}
					return;
				}
				if (type != GraphModifier.EventType.PostCacheLoad)
				{
					return;
				}
				while (graphModifier != null)
				{
					graphModifier.OnPostCacheLoad();
					graphModifier = graphModifier.next;
				}
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x00196093 File Offset: 0x00194293
		protected virtual void OnEnable()
		{
			this.RemoveFromLinkedList();
			this.AddToLinkedList();
			this.ConfigureUniqueID();
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x001960A7 File Offset: 0x001942A7
		protected virtual void OnDisable()
		{
			this.RemoveFromLinkedList();
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x001960AF File Offset: 0x001942AF
		protected override void Awake()
		{
			base.Awake();
			this.ConfigureUniqueID();
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x001960C0 File Offset: 0x001942C0
		private void ConfigureUniqueID()
		{
			GraphModifier x;
			if (GraphModifier.usedIDs.TryGetValue(this.uniqueID, out x) && x != this)
			{
				this.Reset();
			}
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x00196101 File Offset: 0x00194301
		private void AddToLinkedList()
		{
			if (GraphModifier.root == null)
			{
				GraphModifier.root = this;
				return;
			}
			this.next = GraphModifier.root;
			GraphModifier.root.prev = this;
			GraphModifier.root = this;
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00196134 File Offset: 0x00194334
		private void RemoveFromLinkedList()
		{
			if (GraphModifier.root == this)
			{
				GraphModifier.root = this.next;
				if (GraphModifier.root != null)
				{
					GraphModifier.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x001961BF File Offset: 0x001943BF
		protected virtual void OnDestroy()
		{
			GraphModifier.usedIDs.Remove(this.uniqueID);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnPostScan()
		{
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnPreScan()
		{
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnLatePostScan()
		{
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnPostCacheLoad()
		{
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnGraphsPreUpdate()
		{
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00002ACE File Offset: 0x00000CCE
		public virtual void OnGraphsPostUpdate()
		{
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x001961D4 File Offset: 0x001943D4
		private void Reset()
		{
			ulong num = (ulong)((long)UnityEngine.Random.Range(0, int.MaxValue));
			ulong num2 = (ulong)((ulong)((long)UnityEngine.Random.Range(0, int.MaxValue)) << 32);
			this.uniqueID = (num | num2);
			GraphModifier.usedIDs[this.uniqueID] = this;
		}

		// Token: 0x04003FDC RID: 16348
		private static GraphModifier root;

		// Token: 0x04003FDD RID: 16349
		private GraphModifier prev;

		// Token: 0x04003FDE RID: 16350
		private GraphModifier next;

		// Token: 0x04003FDF RID: 16351
		[SerializeField]
		[HideInInspector]
		protected ulong uniqueID;

		// Token: 0x04003FE0 RID: 16352
		protected static Dictionary<ulong, GraphModifier> usedIDs = new Dictionary<ulong, GraphModifier>();

		// Token: 0x02000732 RID: 1842
		public enum EventType
		{
			// Token: 0x040049DA RID: 18906
			PostScan = 1,
			// Token: 0x040049DB RID: 18907
			PreScan,
			// Token: 0x040049DC RID: 18908
			LatePostScan = 4,
			// Token: 0x040049DD RID: 18909
			PreUpdate = 8,
			// Token: 0x040049DE RID: 18910
			PostUpdate = 16,
			// Token: 0x040049DF RID: 18911
			PostCacheLoad = 32
		}
	}
}
