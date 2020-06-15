using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200059F RID: 1439
	public abstract class NavmeshClipper : VersionedMonoBehaviour
	{
		// Token: 0x060026D5 RID: 9941 RVA: 0x001ADB4A File Offset: 0x001ABD4A
		public NavmeshClipper()
		{
			this.node = new LinkedListNode<NavmeshClipper>(this);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x001ADB60 File Offset: 0x001ABD60
		public static void AddEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Combine(NavmeshClipper.OnDisableCallback, onDisable);
			for (LinkedListNode<NavmeshClipper> linkedListNode = NavmeshClipper.all.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				onEnable(linkedListNode.Value);
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x001ADBBC File Offset: 0x001ABDBC
		public static void RemoveEnableCallback(Action<NavmeshClipper> onEnable, Action<NavmeshClipper> onDisable)
		{
			NavmeshClipper.OnEnableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnEnableCallback, onEnable);
			NavmeshClipper.OnDisableCallback = (Action<NavmeshClipper>)Delegate.Remove(NavmeshClipper.OnDisableCallback, onDisable);
			for (LinkedListNode<NavmeshClipper> linkedListNode = NavmeshClipper.all.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				onDisable(linkedListNode.Value);
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x001ADC16 File Offset: 0x001ABE16
		public static bool AnyEnableListeners
		{
			get
			{
				return NavmeshClipper.OnEnableCallback != null;
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x001ADC20 File Offset: 0x001ABE20
		protected virtual void OnEnable()
		{
			NavmeshClipper.all.AddFirst(this.node);
			if (NavmeshClipper.OnEnableCallback != null)
			{
				NavmeshClipper.OnEnableCallback(this);
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x001ADC44 File Offset: 0x001ABE44
		protected virtual void OnDisable()
		{
			if (NavmeshClipper.OnDisableCallback != null)
			{
				NavmeshClipper.OnDisableCallback(this);
			}
			NavmeshClipper.all.Remove(this.node);
		}

		// Token: 0x060026DB RID: 9947
		internal abstract void NotifyUpdated();

		// Token: 0x060026DC RID: 9948
		internal abstract Rect GetBounds(GraphTransform transform);

		// Token: 0x060026DD RID: 9949
		public abstract bool RequiresUpdate();

		// Token: 0x060026DE RID: 9950
		public abstract void ForceUpdate();

		// Token: 0x04004242 RID: 16962
		private static Action<NavmeshClipper> OnEnableCallback;

		// Token: 0x04004243 RID: 16963
		private static Action<NavmeshClipper> OnDisableCallback;

		// Token: 0x04004244 RID: 16964
		private static readonly LinkedList<NavmeshClipper> all = new LinkedList<NavmeshClipper>();

		// Token: 0x04004245 RID: 16965
		private readonly LinkedListNode<NavmeshClipper> node;
	}
}
