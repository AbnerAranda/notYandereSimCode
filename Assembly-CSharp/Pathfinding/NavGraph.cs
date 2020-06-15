using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000577 RID: 1399
	public abstract class NavGraph : IGraphInternals
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x0019CF11 File Offset: 0x0019B111
		internal bool exists
		{
			get
			{
				return this.active != null;
			}
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x0019CF20 File Offset: 0x0019B120
		public virtual int CountNodes()
		{
			int count = 0;
			this.GetNodes(delegate(GraphNode node)
			{
				int count = count;
				count++;
			});
			return count;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x0019CF54 File Offset: 0x0019B154
		public void GetNodes(Func<GraphNode, bool> action)
		{
			bool cont = true;
			this.GetNodes(delegate(GraphNode node)
			{
				if (cont)
				{
					cont &= action(node);
				}
			});
		}

		// Token: 0x060024B6 RID: 9398
		public abstract void GetNodes(Action<GraphNode> action);

		// Token: 0x060024B7 RID: 9399 RVA: 0x0019CF87 File Offset: 0x0019B187
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public void SetMatrix(Matrix4x4 m)
		{
			this.matrix = m;
			this.inverseMatrix = m.inverse;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x0019CF9D File Offset: 0x0019B19D
		[Obsolete("Use RelocateNodes(Matrix4x4) instead. To keep the same behavior you can call RelocateNodes(newMatrix * oldMatrix.inverse).")]
		public void RelocateNodes(Matrix4x4 oldMatrix, Matrix4x4 newMatrix)
		{
			this.RelocateNodes(newMatrix * oldMatrix.inverse);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0019CFB4 File Offset: 0x0019B1B4
		public virtual void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.position = (Int3)deltaMatrix.MultiplyPoint((Vector3)node.position);
			});
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0019CFE0 File Offset: 0x0019B1E0
		public NNInfoInternal GetNearest(Vector3 position)
		{
			return this.GetNearest(position, NNConstraint.None);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0019CFEE File Offset: 0x0019B1EE
		public NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint, null);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0019CFFC File Offset: 0x0019B1FC
		public virtual NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			float maxDistSqr = (constraint == null || constraint.constrainDistance) ? AstarPath.active.maxNearestNodeDistanceSqr : float.PositiveInfinity;
			float minDist = float.PositiveInfinity;
			GraphNode minNode = null;
			float minConstDist = float.PositiveInfinity;
			GraphNode minConstNode = null;
			this.GetNodes(delegate(GraphNode node)
			{
				float sqrMagnitude = (position - (Vector3)node.position).sqrMagnitude;
				if (sqrMagnitude < minDist)
				{
					minDist = sqrMagnitude;
					minNode = node;
				}
				if (sqrMagnitude < minConstDist && sqrMagnitude < maxDistSqr && (constraint == null || constraint.Suitable(node)))
				{
					minConstDist = sqrMagnitude;
					minConstNode = node;
				}
			});
			NNInfoInternal result = new NNInfoInternal(minNode);
			result.constrainedNode = minConstNode;
			if (minConstNode != null)
			{
				result.constClampedPosition = (Vector3)minConstNode.position;
			}
			else if (minNode != null)
			{
				result.constrainedNode = minNode;
				result.constClampedPosition = (Vector3)minNode.position;
			}
			return result;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0019D0E7 File Offset: 0x0019B2E7
		public virtual NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0019D0F1 File Offset: 0x0019B2F1
		protected virtual void OnDestroy()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0019D0F9 File Offset: 0x0019B2F9
		protected virtual void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.Destroy();
			});
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0019D120 File Offset: 0x0019B320
		[Obsolete("Use AstarPath.Scan instead")]
		public void ScanGraph()
		{
			this.Scan();
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0019D128 File Offset: 0x0019B328
		public void Scan()
		{
			this.active.Scan(this);
		}

		// Token: 0x060024C2 RID: 9410
		protected abstract IEnumerable<Progress> ScanInternal();

		// Token: 0x060024C3 RID: 9411 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void SerializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00002ACE File Offset: 0x00000CCE
		protected virtual void PostDeserialization(GraphSerializationContext ctx)
		{
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0019D138 File Offset: 0x0019B338
		protected virtual void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.guid = new Pathfinding.Util.Guid(ctx.reader.ReadBytes(16));
			this.initialPenalty = ctx.reader.ReadUInt32();
			this.open = ctx.reader.ReadBoolean();
			this.name = ctx.reader.ReadString();
			this.drawGizmos = ctx.reader.ReadBoolean();
			this.infoScreenOpen = ctx.reader.ReadBoolean();
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0019D1B4 File Offset: 0x0019B3B4
		public virtual void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
			this.GetNodes(delegate(GraphNode node)
			{
				hasher.HashNode(node);
			});
			if (!gizmos.Draw(hasher))
			{
				using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
				{
					this.GetNodes(new Action<GraphNode>(gizmoHelper.DrawConnections));
				}
			}
			if (this.active.showUnwalkableNodes)
			{
				this.DrawUnwalkableNodes(this.active.unwalkableNodeDebugSize);
			}
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0019D25C File Offset: 0x0019B45C
		protected void DrawUnwalkableNodes(float size)
		{
			Gizmos.color = AstarColor.UnwalkableNode;
			this.GetNodes(delegate(GraphNode node)
			{
				if (!node.Walkable)
				{
					Gizmos.DrawCube((Vector3)node.position, Vector3.one * size);
				}
			});
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x0019D292 File Offset: 0x0019B492
		// (set) Token: 0x060024CA RID: 9418 RVA: 0x0019D29A File Offset: 0x0019B49A
		string IGraphInternals.SerializedEditorSettings
		{
			get
			{
				return this.serializedEditorSettings;
			}
			set
			{
				this.serializedEditorSettings = value;
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0019D2A3 File Offset: 0x0019B4A3
		void IGraphInternals.OnDestroy()
		{
			this.OnDestroy();
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0019D0F1 File Offset: 0x0019B2F1
		void IGraphInternals.DestroyAllNodes()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0019D2AB File Offset: 0x0019B4AB
		IEnumerable<Progress> IGraphInternals.ScanInternal()
		{
			return this.ScanInternal();
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0019D2B3 File Offset: 0x0019B4B3
		void IGraphInternals.SerializeExtraInfo(GraphSerializationContext ctx)
		{
			this.SerializeExtraInfo(ctx);
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0019D2BC File Offset: 0x0019B4BC
		void IGraphInternals.DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			this.DeserializeExtraInfo(ctx);
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0019D2C5 File Offset: 0x0019B4C5
		void IGraphInternals.PostDeserialization(GraphSerializationContext ctx)
		{
			this.PostDeserialization(ctx);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0019D2CE File Offset: 0x0019B4CE
		void IGraphInternals.DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.DeserializeSettingsCompatibility(ctx);
		}

		// Token: 0x04004112 RID: 16658
		public AstarPath active;

		// Token: 0x04004113 RID: 16659
		[JsonMember]
		public Pathfinding.Util.Guid guid;

		// Token: 0x04004114 RID: 16660
		[JsonMember]
		public uint initialPenalty;

		// Token: 0x04004115 RID: 16661
		[JsonMember]
		public bool open;

		// Token: 0x04004116 RID: 16662
		public uint graphIndex;

		// Token: 0x04004117 RID: 16663
		[JsonMember]
		public string name;

		// Token: 0x04004118 RID: 16664
		[JsonMember]
		public bool drawGizmos = true;

		// Token: 0x04004119 RID: 16665
		[JsonMember]
		public bool infoScreenOpen;

		// Token: 0x0400411A RID: 16666
		[JsonMember]
		private string serializedEditorSettings;

		// Token: 0x0400411B RID: 16667
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 matrix = Matrix4x4.identity;

		// Token: 0x0400411C RID: 16668
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 inverseMatrix = Matrix4x4.identity;
	}
}
