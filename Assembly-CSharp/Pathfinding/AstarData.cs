using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000530 RID: 1328
	[Serializable]
	public class AstarData
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x00190640 File Offset: 0x0018E840
		public static AstarPath active
		{
			get
			{
				return AstarPath.active;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x00190647 File Offset: 0x0018E847
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x0019064F File Offset: 0x0018E84F
		public NavMeshGraph navmesh { get; private set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x00190658 File Offset: 0x0018E858
		// (set) Token: 0x060021D0 RID: 8656 RVA: 0x00190660 File Offset: 0x0018E860
		public GridGraph gridGraph { get; private set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x00190669 File Offset: 0x0018E869
		// (set) Token: 0x060021D2 RID: 8658 RVA: 0x00190671 File Offset: 0x0018E871
		public LayerGridGraph layerGridGraph { get; private set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0019067A File Offset: 0x0018E87A
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x00190682 File Offset: 0x0018E882
		public PointGraph pointGraph { get; private set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0019068B File Offset: 0x0018E88B
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x00190693 File Offset: 0x0018E893
		public RecastGraph recastGraph { get; private set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0019069C File Offset: 0x0018E89C
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x001906A4 File Offset: 0x0018E8A4
		public Type[] graphTypes { get; private set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x001906AD File Offset: 0x0018E8AD
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x001906E8 File Offset: 0x0018E8E8
		private byte[] data
		{
			get
			{
				if (this.upgradeData != null && this.upgradeData.Length != 0)
				{
					this.data = this.upgradeData;
					this.upgradeData = null;
				}
				if (this.dataString == null)
				{
					return null;
				}
				return Convert.FromBase64String(this.dataString);
			}
			set
			{
				this.dataString = ((value != null) ? Convert.ToBase64String(value) : null);
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x001906FC File Offset: 0x0018E8FC
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00190704 File Offset: 0x0018E904
		public void SetData(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0019070D File Offset: 0x0018E90D
		public void Awake()
		{
			this.graphs = new NavGraph[0];
			if (this.cacheStartup && this.file_cachedStartup != null)
			{
				this.LoadFromCache();
				return;
			}
			this.DeserializeGraphs();
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0019073E File Offset: 0x0018E93E
		internal void LockGraphStructure(bool allowAddingGraphs = false)
		{
			this.graphStructureLocked.Add(allowAddingGraphs);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0019074C File Offset: 0x0018E94C
		internal void UnlockGraphStructure()
		{
			if (this.graphStructureLocked.Count == 0)
			{
				throw new InvalidOperationException();
			}
			this.graphStructureLocked.RemoveAt(this.graphStructureLocked.Count - 1);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0019077C File Offset: 0x0018E97C
		private PathProcessor.GraphUpdateLock AssertSafe(bool onlyAddingGraph = false)
		{
			if (this.graphStructureLocked.Count > 0)
			{
				bool flag = true;
				for (int i = 0; i < this.graphStructureLocked.Count; i++)
				{
					flag &= this.graphStructureLocked[i];
				}
				if (!onlyAddingGraph || !flag)
				{
					throw new InvalidOperationException("Graphs cannot be added, removed or serialized while the graph structure is locked. This is the case when a graph is currently being scanned and when executing graph updates and work items.\nHowever as a special case, graphs can be added inside work items.");
				}
			}
			PathProcessor.GraphUpdateLock result = AstarData.active.PausePathfinding();
			if (!AstarData.active.IsInsideWorkItem)
			{
				AstarData.active.FlushWorkItems();
				AstarData.active.pathReturnQueue.ReturnPaths(false);
			}
			return result;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00190800 File Offset: 0x0018EA00
		public void UpdateShortcuts()
		{
			this.navmesh = (NavMeshGraph)this.FindGraphOfType(typeof(NavMeshGraph));
			this.gridGraph = (GridGraph)this.FindGraphOfType(typeof(GridGraph));
			this.layerGridGraph = (LayerGridGraph)this.FindGraphOfType(typeof(LayerGridGraph));
			this.pointGraph = (PointGraph)this.FindGraphOfType(typeof(PointGraph));
			this.recastGraph = (RecastGraph)this.FindGraphOfType(typeof(RecastGraph));
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00190894 File Offset: 0x0018EA94
		public void LoadFromCache()
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			if (this.file_cachedStartup != null)
			{
				byte[] bytes = this.file_cachedStartup.bytes;
				this.DeserializeGraphs(bytes);
				GraphModifier.TriggerEvent(GraphModifier.EventType.PostCacheLoad);
			}
			else
			{
				Debug.LogError("Can't load from cache since the cache is empty");
			}
			graphUpdateLock.Release();
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x001908E4 File Offset: 0x0018EAE4
		public byte[] SerializeGraphs()
		{
			return this.SerializeGraphs(SerializeSettings.Settings);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x001908F4 File Offset: 0x0018EAF4
		public byte[] SerializeGraphs(SerializeSettings settings)
		{
			uint num;
			return this.SerializeGraphs(settings, out num);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0019090C File Offset: 0x0018EB0C
		public byte[] SerializeGraphs(SerializeSettings settings, out uint checksum)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			AstarSerializer astarSerializer = new AstarSerializer(this, settings);
			astarSerializer.OpenSerialize();
			astarSerializer.SerializeGraphs(this.graphs);
			astarSerializer.SerializeExtraInfo();
			byte[] result = astarSerializer.CloseSerialize();
			checksum = astarSerializer.GetChecksum();
			graphUpdateLock.Release();
			return result;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00190956 File Offset: 0x0018EB56
		public void DeserializeGraphs()
		{
			if (this.data != null)
			{
				this.DeserializeGraphs(this.data);
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0019096C File Offset: 0x0018EB6C
		private void ClearGraphs()
		{
			if (this.graphs == null)
			{
				return;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					((IGraphInternals)this.graphs[i]).OnDestroy();
					this.graphs[i].active = null;
				}
			}
			this.graphs = null;
			this.UpdateShortcuts();
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x001909C7 File Offset: 0x0018EBC7
		public void OnDestroy()
		{
			this.ClearGraphs();
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x001909D0 File Offset: 0x0018EBD0
		public void DeserializeGraphs(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			this.ClearGraphs();
			this.DeserializeGraphsAdditive(bytes);
			graphUpdateLock.Release();
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x001909FC File Offset: 0x0018EBFC
		public void DeserializeGraphsAdditive(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPartAdditive(astarSerializer);
					astarSerializer.CloseDeserialize();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip).\nThe data is either corrupt or it was saved using a 3.0.x or earlier version of the system");
				}
				AstarData.active.VerifyIntegrity();
			}
			catch (Exception arg)
			{
				Debug.LogError("Caught exception while deserializing data.\n" + arg);
				this.graphs = new NavGraph[0];
			}
			this.UpdateShortcuts();
			graphUpdateLock.Release();
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00190A94 File Offset: 0x0018EC94
		private void DeserializeGraphsPartAdditive(AstarSerializer sr)
		{
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			List<NavGraph> list = new List<NavGraph>(this.graphs);
			sr.SetGraphIndexOffset(list.Count);
			list.AddRange(sr.DeserializeGraphs());
			this.graphs = list.ToArray();
			sr.DeserializeEditorSettingsCompatibility();
			sr.DeserializeExtraInfo();
			int i;
			int l;
			for (i = 0; i < this.graphs.Length; i = l + 1)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(delegate(GraphNode node)
					{
						node.GraphIndex = (uint)i;
					});
				}
				l = i;
			}
			for (int j = 0; j < this.graphs.Length; j++)
			{
				for (int k = j + 1; k < this.graphs.Length; k++)
				{
					if (this.graphs[j] != null && this.graphs[k] != null && this.graphs[j].guid == this.graphs[k].guid)
					{
						Debug.LogWarning("Guid Conflict when importing graphs additively. Imported graph will get a new Guid.\nThis message is (relatively) harmless.");
						this.graphs[j].guid = Pathfinding.Util.Guid.NewGuid();
						break;
					}
				}
			}
			sr.PostDeserialization();
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00190BD4 File Offset: 0x0018EDD4
		public void FindGraphTypes()
		{
			Type[] types = WindowsStoreCompatibility.GetTypeInfo(typeof(AstarPath)).Assembly.GetTypes();
			List<Type> list = new List<Type>();
			foreach (Type type in types)
			{
				Type baseType = type.BaseType;
				while (baseType != null)
				{
					if (object.Equals(baseType, typeof(NavGraph)))
					{
						list.Add(type);
						break;
					}
					baseType = baseType.BaseType;
				}
			}
			this.graphTypes = list.ToArray();
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00190C58 File Offset: 0x0018EE58
		[Obsolete("If really necessary. Use System.Type.GetType instead.")]
		public Type GetGraphType(string type)
		{
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.graphTypes[i];
				}
			}
			return null;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00190C98 File Offset: 0x0018EE98
		[Obsolete("Use CreateGraph(System.Type) instead")]
		public NavGraph CreateGraph(string type)
		{
			Debug.Log("Creating Graph of type '" + type + "'");
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.CreateGraph(this.graphTypes[i]);
				}
			}
			Debug.LogError("Graph type (" + type + ") wasn't found");
			return null;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00190D07 File Offset: 0x0018EF07
		internal NavGraph CreateGraph(Type type)
		{
			NavGraph navGraph = Activator.CreateInstance(type) as NavGraph;
			navGraph.active = AstarData.active;
			return navGraph;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00190D20 File Offset: 0x0018EF20
		[Obsolete("Use AddGraph(System.Type) instead")]
		public NavGraph AddGraph(string type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError("No NavGraph of type '" + type + "' could be found");
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00190D88 File Offset: 0x0018EF88
		public NavGraph AddGraph(Type type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (object.Equals(this.graphTypes[i], type))
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"No NavGraph of type '",
					type,
					"' could be found, ",
					this.graphTypes.Length,
					" graph types are avaliable"
				}));
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00190E14 File Offset: 0x0018F014
		private void AddGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(true);
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == null)
				{
					this.graphs[i] = graph;
					graph.graphIndex = (uint)i;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (this.graphs != null && (long)this.graphs.Length >= 255L)
				{
					throw new Exception("Graph Count Limit Reached. You cannot have more than " + 255U + " graphs.");
				}
				this.graphs = new List<NavGraph>(this.graphs ?? new NavGraph[0])
				{
					graph
				}.ToArray();
				graph.graphIndex = (uint)(this.graphs.Length - 1);
			}
			this.UpdateShortcuts();
			graph.active = AstarData.active;
			graphUpdateLock.Release();
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x00190EE8 File Offset: 0x0018F0E8
		public bool RemoveGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			((IGraphInternals)graph).OnDestroy();
			graph.active = null;
			int num = Array.IndexOf<NavGraph>(this.graphs, graph);
			if (num != -1)
			{
				this.graphs[num] = null;
			}
			this.UpdateShortcuts();
			graphUpdateLock.Release();
			return num != -1;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00190F38 File Offset: 0x0018F138
		public static NavGraph GetGraph(GraphNode node)
		{
			if (node == null)
			{
				return null;
			}
			AstarPath active = AstarPath.active;
			if (active == null)
			{
				return null;
			}
			AstarData data = active.data;
			if (data == null || data.graphs == null)
			{
				return null;
			}
			uint graphIndex = node.GraphIndex;
			if ((ulong)graphIndex >= (ulong)((long)data.graphs.Length))
			{
				return null;
			}
			return data.graphs[(int)graphIndex];
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00190F90 File Offset: 0x0018F190
		public NavGraph FindGraph(Func<NavGraph, bool> predicate)
		{
			if (this.graphs != null)
			{
				for (int i = 0; i < this.graphs.Length; i++)
				{
					if (this.graphs[i] != null && predicate(this.graphs[i]))
					{
						return this.graphs[i];
					}
				}
			}
			return null;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00190FDC File Offset: 0x0018F1DC
		public NavGraph FindGraphOfType(Type type)
		{
			return this.FindGraph((NavGraph graph) => object.Equals(graph.GetType(), type));
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00191008 File Offset: 0x0018F208
		public NavGraph FindGraphWhichInheritsFrom(Type type)
		{
			return this.FindGraph((NavGraph graph) => WindowsStoreCompatibility.GetTypeInfo(type).IsAssignableFrom(WindowsStoreCompatibility.GetTypeInfo(graph.GetType())));
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00191034 File Offset: 0x0018F234
		public IEnumerable FindGraphsOfType(Type type)
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] != null && object.Equals(this.graphs[i].GetType(), type))
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0019104B File Offset: 0x0018F24B
		public IEnumerable GetUpdateableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] is IUpdatableGraph)
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0019105B File Offset: 0x0018F25B
		[Obsolete("Obsolete because it is not used by the package internally and the use cases are few. Iterate through the graphs array instead.")]
		public IEnumerable GetRaycastableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.graphs.Length; i = num + 1)
			{
				if (this.graphs[i] is IRaycastableGraph)
				{
					yield return this.graphs[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0019106C File Offset: 0x0018F26C
		public int GetGraphIndex(NavGraph graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			int num = -1;
			if (this.graphs != null)
			{
				num = Array.IndexOf<NavGraph>(this.graphs, graph);
				if (num == -1)
				{
					Debug.LogError("Graph doesn't exist");
				}
			}
			return num;
		}

		// Token: 0x04003F88 RID: 16264
		[NonSerialized]
		public NavGraph[] graphs = new NavGraph[0];

		// Token: 0x04003F89 RID: 16265
		[SerializeField]
		private string dataString;

		// Token: 0x04003F8A RID: 16266
		[SerializeField]
		[FormerlySerializedAs("data")]
		private byte[] upgradeData;

		// Token: 0x04003F8B RID: 16267
		public TextAsset file_cachedStartup;

		// Token: 0x04003F8C RID: 16268
		public byte[] data_cachedStartup;

		// Token: 0x04003F8D RID: 16269
		[SerializeField]
		public bool cacheStartup;

		// Token: 0x04003F8E RID: 16270
		private List<bool> graphStructureLocked = new List<bool>();
	}
}
