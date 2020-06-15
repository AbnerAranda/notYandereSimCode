using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Pathfinding.Ionic.Zip;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x020005CF RID: 1487
	public class AstarSerializer
	{
		// Token: 0x0600283A RID: 10298 RVA: 0x001BB1DC File Offset: 0x001B93DC
		private static StringBuilder GetStringBuilder()
		{
			AstarSerializer._stringBuilder.Length = 0;
			return AstarSerializer._stringBuilder;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x001BB1EE File Offset: 0x001B93EE
		public AstarSerializer(AstarData data)
		{
			this.data = data;
			this.settings = SerializeSettings.Settings;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x001BB21A File Offset: 0x001B941A
		public AstarSerializer(AstarData data, SerializeSettings settings)
		{
			this.data = data;
			this.settings = settings;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x001BB242 File Offset: 0x001B9442
		public void SetGraphIndexOffset(int offset)
		{
			this.graphIndexOffset = offset;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x001BB24B File Offset: 0x001B944B
		private void AddChecksum(byte[] bytes)
		{
			this.checksum = Checksum.GetChecksum(bytes, this.checksum);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x001BB25F File Offset: 0x001B945F
		private void AddEntry(string name, byte[] bytes)
		{
			this.zip.AddEntry(name, bytes);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x001BB26F File Offset: 0x001B946F
		public uint GetChecksum()
		{
			return this.checksum;
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x001BB277 File Offset: 0x001B9477
		public void OpenSerialize()
		{
			this.zipStream = new MemoryStream();
			this.zip = new ZipFile();
			this.zip.AlternateEncoding = Encoding.UTF8;
			this.zip.AlternateEncodingUsage = ZipOption.Always;
			this.meta = new GraphMeta();
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x001BB2B8 File Offset: 0x001B94B8
		public byte[] CloseSerialize()
		{
			byte[] array = this.SerializeMeta();
			this.AddChecksum(array);
			this.AddEntry("meta.json", array);
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			foreach (ZipEntry zipEntry in this.zip.Entries)
			{
				zipEntry.AccessedTime = dateTime;
				zipEntry.CreationTime = dateTime;
				zipEntry.LastModified = dateTime;
				zipEntry.ModifiedTime = dateTime;
			}
			this.zip.Save(this.zipStream);
			this.zip.Dispose();
			array = this.zipStream.ToArray();
			this.zip = null;
			this.zipStream = null;
			return array;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x001BB380 File Offset: 0x001B9580
		public void SerializeGraphs(NavGraph[] _graphs)
		{
			if (this.graphs != null)
			{
				throw new InvalidOperationException("Cannot serialize graphs multiple times.");
			}
			this.graphs = _graphs;
			if (this.zip == null)
			{
				throw new NullReferenceException("You must not call CloseSerialize before a call to this function");
			}
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					byte[] bytes = this.Serialize(this.graphs[i]);
					this.AddChecksum(bytes);
					this.AddEntry("graph" + i + ".json", bytes);
				}
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x001BB420 File Offset: 0x001B9620
		private byte[] SerializeMeta()
		{
			if (this.graphs == null)
			{
				throw new Exception("No call to SerializeGraphs has been done");
			}
			this.meta.version = AstarPath.Version;
			this.meta.graphs = this.graphs.Length;
			this.meta.guids = new List<string>();
			this.meta.typeNames = new List<string>();
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.meta.guids.Add(this.graphs[i].guid.ToString());
					this.meta.typeNames.Add(this.graphs[i].GetType().FullName);
				}
				else
				{
					this.meta.guids.Add(null);
					this.meta.typeNames.Add(null);
				}
			}
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			TinyJsonSerializer.Serialize(this.meta, stringBuilder);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x001BB534 File Offset: 0x001B9734
		public byte[] Serialize(NavGraph graph)
		{
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			TinyJsonSerializer.Serialize(graph, stringBuilder);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x00002ACE File Offset: 0x00000CCE
		[Obsolete("Not used anymore. You can safely remove the call to this function.")]
		public void SerializeNodes()
		{
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x001BB560 File Offset: 0x001B9760
		private static int GetMaxNodeIndexInAllGraphs(NavGraph[] graphs)
		{
			int maxIndex = 0;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < graphs.Length; i++)
			{
				if (graphs[i] != null)
				{
					NavGraph navGraph = graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							maxIndex = Math.Max(node.NodeIndex, maxIndex);
							if (node.NodeIndex == -1)
							{
								Debug.LogError("Graph contains destroyed nodes. This is a bug.");
							}
						});
					}
					navGraph.GetNodes(action);
				}
			}
			return maxIndex;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x001BB5BC File Offset: 0x001B97BC
		private static byte[] SerializeNodeIndices(NavGraph[] graphs)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(memoryStream);
			int maxNodeIndexInAllGraphs = AstarSerializer.GetMaxNodeIndexInAllGraphs(graphs);
			writer.Write(maxNodeIndexInAllGraphs);
			int maxNodeIndex2 = 0;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < graphs.Length; i++)
			{
				if (graphs[i] != null)
				{
					NavGraph navGraph = graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							maxNodeIndex2 = Math.Max(node.NodeIndex, maxNodeIndex2);
							writer.Write(node.NodeIndex);
						});
					}
					navGraph.GetNodes(action);
				}
			}
			if (maxNodeIndex2 != maxNodeIndexInAllGraphs)
			{
				throw new Exception("Some graphs are not consistent in their GetNodes calls, sequential calls give different results.");
			}
			byte[] result = memoryStream.ToArray();
			writer.Close();
			return result;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x001BB65C File Offset: 0x001B985C
		private static byte[] SerializeGraphExtraInfo(NavGraph graph)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryWriter);
			((IGraphInternals)graph).SerializeExtraInfo(ctx);
			byte[] result = memoryStream.ToArray();
			binaryWriter.Close();
			return result;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x001BB690 File Offset: 0x001B9890
		private static byte[] SerializeGraphNodeReferences(NavGraph graph)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryWriter);
			graph.GetNodes(delegate(GraphNode node)
			{
				node.SerializeReferences(ctx);
			});
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x001BB6D8 File Offset: 0x001B98D8
		public void SerializeExtraInfo()
		{
			if (!this.settings.nodes)
			{
				return;
			}
			if (this.graphs == null)
			{
				throw new InvalidOperationException("Cannot serialize extra info with no serialized graphs (call SerializeGraphs first)");
			}
			byte[] bytes = AstarSerializer.SerializeNodeIndices(this.graphs);
			this.AddChecksum(bytes);
			this.AddEntry("graph_references.binary", bytes);
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					bytes = AstarSerializer.SerializeGraphExtraInfo(this.graphs[i]);
					this.AddChecksum(bytes);
					this.AddEntry("graph" + i + "_extra.binary", bytes);
					bytes = AstarSerializer.SerializeGraphNodeReferences(this.graphs[i]);
					this.AddChecksum(bytes);
					this.AddEntry("graph" + i + "_references.binary", bytes);
				}
			}
			bytes = this.SerializeNodeLinks();
			this.AddChecksum(bytes);
			this.AddEntry("node_link2.binary", bytes);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x001BB7BE File Offset: 0x001B99BE
		private byte[] SerializeNodeLinks()
		{
			MemoryStream memoryStream = new MemoryStream();
			NodeLink2.SerializeReferences(new GraphSerializationContext(new BinaryWriter(memoryStream)));
			return memoryStream.ToArray();
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x001BB7DA File Offset: 0x001B99DA
		private ZipEntry GetEntry(string name)
		{
			return this.zip[name];
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x001BB7E8 File Offset: 0x001B99E8
		private bool ContainsEntry(string name)
		{
			return this.GetEntry(name) != null;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x001BB7F4 File Offset: 0x001B99F4
		public bool OpenDeserialize(byte[] bytes)
		{
			this.zipStream = new MemoryStream();
			this.zipStream.Write(bytes, 0, bytes.Length);
			this.zipStream.Position = 0L;
			try
			{
				this.zip = ZipFile.Read(this.zipStream);
			}
			catch (Exception arg)
			{
				Debug.LogError("Caught exception when loading from zip\n" + arg);
				this.zipStream.Dispose();
				return false;
			}
			if (this.ContainsEntry("meta.json"))
			{
				this.meta = this.DeserializeMeta(this.GetEntry("meta.json"));
			}
			else
			{
				if (!this.ContainsEntry("meta.binary"))
				{
					throw new Exception("No metadata found in serialized data.");
				}
				this.meta = this.DeserializeBinaryMeta(this.GetEntry("meta.binary"));
			}
			if (AstarSerializer.FullyDefinedVersion(this.meta.version) > AstarSerializer.FullyDefinedVersion(AstarPath.Version))
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Trying to load data from a newer version of the A* Pathfinding Project\nCurrent version: ",
					AstarPath.Version,
					" Data version: ",
					this.meta.version,
					"\nThis is usually fine as the stored data is usually backwards and forwards compatible.\nHowever node data (not settings) can get corrupted between versions (even though I try my best to keep compatibility), so it is recommended to recalculate any caches (those for faster startup) and resave any files. Even if it seems to load fine, it might cause subtle bugs.\n"
				}));
			}
			else if (AstarSerializer.FullyDefinedVersion(this.meta.version) < AstarSerializer.FullyDefinedVersion(AstarPath.Version))
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Upgrading serialized pathfinding data from version ",
					this.meta.version,
					" to ",
					AstarPath.Version,
					"\nThis is usually fine, it just means you have upgraded to a new version.\nHowever node data (not settings) can get corrupted between versions (even though I try my best to keep compatibility), so it is recommended to recalculate any caches (those for faster startup) and resave any files. Even if it seems to load fine, it might cause subtle bugs.\n"
				}));
			}
			return true;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x001BB98C File Offset: 0x001B9B8C
		private static Version FullyDefinedVersion(Version v)
		{
			return new Version(Mathf.Max(v.Major, 0), Mathf.Max(v.Minor, 0), Mathf.Max(v.Build, 0), Mathf.Max(v.Revision, 0));
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x001BB9C3 File Offset: 0x001B9BC3
		public void CloseDeserialize()
		{
			this.zipStream.Dispose();
			this.zip.Dispose();
			this.zip = null;
			this.zipStream = null;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x001BB9EC File Offset: 0x001B9BEC
		private NavGraph DeserializeGraph(int zipIndex, int graphIndex)
		{
			Type graphType = this.meta.GetGraphType(zipIndex);
			if (object.Equals(graphType, null))
			{
				return null;
			}
			NavGraph navGraph = this.data.CreateGraph(graphType);
			navGraph.graphIndex = (uint)graphIndex;
			string name = "graph" + zipIndex + ".json";
			string name2 = "graph" + zipIndex + ".binary";
			if (this.ContainsEntry(name))
			{
				TinyJsonDeserializer.Deserialize(AstarSerializer.GetString(this.GetEntry(name)), graphType, navGraph);
			}
			else
			{
				if (!this.ContainsEntry(name2))
				{
					throw new FileNotFoundException(string.Concat(new object[]
					{
						"Could not find data for graph ",
						zipIndex,
						" in zip. Entry 'graph",
						zipIndex,
						".json' does not exist"
					}));
				}
				GraphSerializationContext ctx = new GraphSerializationContext(AstarSerializer.GetBinaryReader(this.GetEntry(name2)), null, navGraph.graphIndex, this.meta);
				((IGraphInternals)navGraph).DeserializeSettingsCompatibility(ctx);
			}
			if (navGraph.guid.ToString() != this.meta.guids[zipIndex])
			{
				throw new Exception(string.Concat(new object[]
				{
					"Guid in graph file not equal to guid defined in meta file. Have you edited the data manually?\n",
					navGraph.guid,
					" != ",
					this.meta.guids[zipIndex]
				}));
			}
			return navGraph;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x001BBB4C File Offset: 0x001B9D4C
		public NavGraph[] DeserializeGraphs()
		{
			List<NavGraph> list = new List<NavGraph>();
			this.graphIndexInZip = new Dictionary<NavGraph, int>();
			for (int i = 0; i < this.meta.graphs; i++)
			{
				int graphIndex = list.Count + this.graphIndexOffset;
				NavGraph navGraph = this.DeserializeGraph(i, graphIndex);
				if (navGraph != null)
				{
					list.Add(navGraph);
					this.graphIndexInZip[navGraph] = i;
				}
			}
			this.graphs = list.ToArray();
			return this.graphs;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x001BBBC0 File Offset: 0x001B9DC0
		private bool DeserializeExtraInfo(NavGraph graph)
		{
			int num = this.graphIndexInZip[graph];
			ZipEntry entry = this.GetEntry("graph" + num + "_extra.binary");
			if (entry == null)
			{
				return false;
			}
			GraphSerializationContext ctx = new GraphSerializationContext(AstarSerializer.GetBinaryReader(entry), null, graph.graphIndex, this.meta);
			((IGraphInternals)graph).DeserializeExtraInfo(ctx);
			return true;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x001BBC1C File Offset: 0x001B9E1C
		private bool AnyDestroyedNodesInGraphs()
		{
			bool result = false;
			Action<GraphNode> <>9__0;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				NavGraph navGraph = this.graphs[i];
				Action<GraphNode> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GraphNode node)
					{
						if (node.Destroyed)
						{
							result = true;
						}
					});
				}
				navGraph.GetNodes(action);
			}
			return result;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x001BBC7C File Offset: 0x001B9E7C
		private GraphNode[] DeserializeNodeReferenceMap()
		{
			ZipEntry entry = this.GetEntry("graph_references.binary");
			if (entry == null)
			{
				throw new Exception("Node references not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
			}
			BinaryReader reader = AstarSerializer.GetBinaryReader(entry);
			int num = reader.ReadInt32();
			GraphNode[] int2Node = new GraphNode[num + 1];
			try
			{
				Action<GraphNode> <>9__0;
				for (int i = 0; i < this.graphs.Length; i++)
				{
					NavGraph navGraph = this.graphs[i];
					Action<GraphNode> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(GraphNode node)
						{
							int num2 = reader.ReadInt32();
							int2Node[num2] = node;
						});
					}
					navGraph.GetNodes(action);
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("Some graph(s) has thrown an exception during GetNodes, or some graph(s) have deserialized more or fewer nodes than were serialized", innerException);
			}
			if (reader.BaseStream.Position != reader.BaseStream.Length)
			{
				throw new Exception(string.Concat(new object[]
				{
					reader.BaseStream.Length / 4L,
					" nodes were serialized, but only data for ",
					reader.BaseStream.Position / 4L,
					" nodes was found. The data looks corrupt."
				}));
			}
			reader.Close();
			return int2Node;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x001BBDBC File Offset: 0x001B9FBC
		private void DeserializeNodeReferences(NavGraph graph, GraphNode[] int2Node)
		{
			int num = this.graphIndexInZip[graph];
			ZipEntry entry = this.GetEntry("graph" + num + "_references.binary");
			if (entry == null)
			{
				throw new Exception("Node references for graph " + num + " not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
			}
			BinaryReader binaryReader = AstarSerializer.GetBinaryReader(entry);
			GraphSerializationContext ctx = new GraphSerializationContext(binaryReader, int2Node, graph.graphIndex, this.meta);
			graph.GetNodes(delegate(GraphNode node)
			{
				node.DeserializeReferences(ctx);
			});
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x001BBE48 File Offset: 0x001BA048
		public void DeserializeExtraInfo()
		{
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				flag |= this.DeserializeExtraInfo(this.graphs[i]);
			}
			if (!flag)
			{
				return;
			}
			if (this.AnyDestroyedNodesInGraphs())
			{
				Debug.LogError("Graph contains destroyed nodes. This is a bug.");
			}
			GraphNode[] int2Node = this.DeserializeNodeReferenceMap();
			for (int j = 0; j < this.graphs.Length; j++)
			{
				this.DeserializeNodeReferences(this.graphs[j], int2Node);
			}
			this.DeserializeNodeLinks(int2Node);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x001BBEC4 File Offset: 0x001BA0C4
		private void DeserializeNodeLinks(GraphNode[] int2Node)
		{
			ZipEntry entry = this.GetEntry("node_link2.binary");
			if (entry == null)
			{
				return;
			}
			NodeLink2.DeserializeReferences(new GraphSerializationContext(AstarSerializer.GetBinaryReader(entry), int2Node, 0U, this.meta));
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x001BBEFC File Offset: 0x001BA0FC
		public void PostDeserialization()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				GraphSerializationContext ctx = new GraphSerializationContext(null, null, 0U, this.meta);
				((IGraphInternals)this.graphs[i]).PostDeserialization(ctx);
			}
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x001BBF3C File Offset: 0x001BA13C
		public void DeserializeEditorSettingsCompatibility()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				int num = this.graphIndexInZip[this.graphs[i]];
				ZipEntry entry = this.GetEntry("graph" + num + "_editor.json");
				if (entry != null)
				{
					((IGraphInternals)this.graphs[i]).SerializedEditorSettings = AstarSerializer.GetString(entry);
				}
			}
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x001BBFA4 File Offset: 0x001BA1A4
		private static BinaryReader GetBinaryReader(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			return new BinaryReader(memoryStream);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x001BBFCC File Offset: 0x001BA1CC
		private static string GetString(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			StreamReader streamReader = new StreamReader(memoryStream);
			string result = streamReader.ReadToEnd();
			streamReader.Dispose();
			return result;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x001BC001 File Offset: 0x001BA201
		private GraphMeta DeserializeMeta(ZipEntry entry)
		{
			return TinyJsonDeserializer.Deserialize(AstarSerializer.GetString(entry), typeof(GraphMeta), null) as GraphMeta;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x001BC020 File Offset: 0x001BA220
		private GraphMeta DeserializeBinaryMeta(ZipEntry entry)
		{
			GraphMeta graphMeta = new GraphMeta();
			BinaryReader binaryReader = AstarSerializer.GetBinaryReader(entry);
			if (binaryReader.ReadString() != "A*")
			{
				throw new Exception("Invalid magic number in saved data");
			}
			int num = binaryReader.ReadInt32();
			int num2 = binaryReader.ReadInt32();
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			if (num < 0)
			{
				graphMeta.version = new Version(0, 0);
			}
			else if (num2 < 0)
			{
				graphMeta.version = new Version(num, 0);
			}
			else if (num3 < 0)
			{
				graphMeta.version = new Version(num, num2);
			}
			else if (num4 < 0)
			{
				graphMeta.version = new Version(num, num2, num3);
			}
			else
			{
				graphMeta.version = new Version(num, num2, num3, num4);
			}
			graphMeta.graphs = binaryReader.ReadInt32();
			graphMeta.guids = new List<string>();
			int num5 = binaryReader.ReadInt32();
			for (int i = 0; i < num5; i++)
			{
				graphMeta.guids.Add(binaryReader.ReadString());
			}
			graphMeta.typeNames = new List<string>();
			num5 = binaryReader.ReadInt32();
			for (int j = 0; j < num5; j++)
			{
				graphMeta.typeNames.Add(binaryReader.ReadString());
			}
			return graphMeta;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x001BC150 File Offset: 0x001BA350
		public static void SaveToFile(string path, byte[] data)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				fileStream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x001BC18C File Offset: 0x001BA38C
		public static byte[] LoadFromFile(string path)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				byte[] array = new byte[(int)fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x04004320 RID: 17184
		private AstarData data;

		// Token: 0x04004321 RID: 17185
		private ZipFile zip;

		// Token: 0x04004322 RID: 17186
		private MemoryStream zipStream;

		// Token: 0x04004323 RID: 17187
		private GraphMeta meta;

		// Token: 0x04004324 RID: 17188
		private SerializeSettings settings;

		// Token: 0x04004325 RID: 17189
		private NavGraph[] graphs;

		// Token: 0x04004326 RID: 17190
		private Dictionary<NavGraph, int> graphIndexInZip;

		// Token: 0x04004327 RID: 17191
		private int graphIndexOffset;

		// Token: 0x04004328 RID: 17192
		private const string binaryExt = ".binary";

		// Token: 0x04004329 RID: 17193
		private const string jsonExt = ".json";

		// Token: 0x0400432A RID: 17194
		private uint checksum = uint.MaxValue;

		// Token: 0x0400432B RID: 17195
		private UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x0400432C RID: 17196
		private static StringBuilder _stringBuilder = new StringBuilder();

		// Token: 0x0400432D RID: 17197
		public static readonly Version V3_8_3 = new Version(3, 8, 3);

		// Token: 0x0400432E RID: 17198
		public static readonly Version V3_9_0 = new Version(3, 9, 0);

		// Token: 0x0400432F RID: 17199
		public static readonly Version V4_1_0 = new Version(4, 1, 0);
	}
}
