using System;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000538 RID: 1336
	[AddComponentMenu("Pathfinding/Pathfinding Debugger")]
	[ExecuteInEditMode]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_astar_debugger.php")]
	public class AstarDebugger : VersionedMonoBehaviour
	{
		// Token: 0x060022B9 RID: 8889 RVA: 0x001949A4 File Offset: 0x00192BA4
		public void Start()
		{
			base.useGUILayout = false;
			this.fpsDrops = new float[this.fpsDropCounterSize];
			this.cam = base.GetComponent<Camera>();
			if (this.cam == null)
			{
				this.cam = Camera.main;
			}
			this.graph = new AstarDebugger.GraphPoint[this.graphBufferSize];
			if (Time.unscaledDeltaTime > 0f)
			{
				for (int i = 0; i < this.fpsDrops.Length; i++)
				{
					this.fpsDrops[i] = 1f / Time.unscaledDeltaTime;
				}
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00194A34 File Offset: 0x00192C34
		public void LateUpdate()
		{
			if (!this.show || (!Application.isPlaying && !this.showInEditor))
			{
				return;
			}
			if (Time.unscaledDeltaTime <= 0.0001f)
			{
				return;
			}
			int num = GC.CollectionCount(0);
			if (this.lastCollectNum != (float)num)
			{
				this.lastCollectNum = (float)num;
				this.delta = Time.realtimeSinceStartup - this.lastCollect;
				this.lastCollect = Time.realtimeSinceStartup;
				this.lastDeltaTime = Time.unscaledDeltaTime;
				this.collectAlloc = this.allocMem;
			}
			this.allocMem = (int)GC.GetTotalMemory(false);
			bool flag = this.allocMem < this.peakAlloc;
			this.peakAlloc = ((!flag) ? this.allocMem : this.peakAlloc);
			if (Time.realtimeSinceStartup - this.lastAllocSet > 0.3f || !Application.isPlaying)
			{
				int num2 = this.allocMem - this.lastAllocMemory;
				this.lastAllocMemory = this.allocMem;
				this.lastAllocSet = Time.realtimeSinceStartup;
				this.delayedDeltaTime = Time.unscaledDeltaTime;
				if (num2 >= 0)
				{
					this.allocRate = num2;
				}
			}
			if (Application.isPlaying)
			{
				this.fpsDrops[Time.frameCount % this.fpsDrops.Length] = ((Time.unscaledDeltaTime > 1E-05f) ? (1f / Time.unscaledDeltaTime) : 0f);
				int num3 = Time.frameCount % this.graph.Length;
				this.graph[num3].fps = ((Time.unscaledDeltaTime < 1E-05f) ? (1f / Time.unscaledDeltaTime) : 0f);
				this.graph[num3].collectEvent = flag;
				this.graph[num3].memory = (float)this.allocMem;
			}
			if (Application.isPlaying && this.cam != null && this.showGraph)
			{
				this.graphWidth = (float)this.cam.pixelWidth * 0.8f;
				float num4 = float.PositiveInfinity;
				float b = 0f;
				float num5 = float.PositiveInfinity;
				float b2 = 0f;
				for (int i = 0; i < this.graph.Length; i++)
				{
					num4 = Mathf.Min(this.graph[i].memory, num4);
					b = Mathf.Max(this.graph[i].memory, b);
					num5 = Mathf.Min(this.graph[i].fps, num5);
					b2 = Mathf.Max(this.graph[i].fps, b2);
				}
				int num6 = Time.frameCount % this.graph.Length;
				Matrix4x4 m = Matrix4x4.TRS(new Vector3(((float)this.cam.pixelWidth - this.graphWidth) / 2f, this.graphOffset, 1f), Quaternion.identity, new Vector3(this.graphWidth, this.graphHeight, 1f));
				for (int j = 0; j < this.graph.Length - 1; j++)
				{
					if (j != num6)
					{
						this.DrawGraphLine(j, m, (float)j / (float)this.graph.Length, (float)(j + 1) / (float)this.graph.Length, Mathf.InverseLerp(num4, b, this.graph[j].memory), Mathf.InverseLerp(num4, b, this.graph[j + 1].memory), Color.blue);
						this.DrawGraphLine(j, m, (float)j / (float)this.graph.Length, (float)(j + 1) / (float)this.graph.Length, Mathf.InverseLerp(num5, b2, this.graph[j].fps), Mathf.InverseLerp(num5, b2, this.graph[j + 1].fps), Color.green);
					}
				}
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00194E06 File Offset: 0x00193006
		private void DrawGraphLine(int index, Matrix4x4 m, float x1, float x2, float y1, float y2, Color color)
		{
			Debug.DrawLine(this.cam.ScreenToWorldPoint(m.MultiplyPoint3x4(new Vector3(x1, y1))), this.cam.ScreenToWorldPoint(m.MultiplyPoint3x4(new Vector3(x2, y2))), color);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00194E44 File Offset: 0x00193044
		public void OnGUI()
		{
			if (!this.show || (!Application.isPlaying && !this.showInEditor))
			{
				return;
			}
			if (this.style == null)
			{
				this.style = new GUIStyle();
				this.style.normal.textColor = Color.white;
				this.style.padding = new RectOffset(5, 5, 5, 5);
			}
			if (Time.realtimeSinceStartup - this.lastUpdate > 0.5f || this.cachedText == null || !Application.isPlaying)
			{
				this.lastUpdate = Time.realtimeSinceStartup;
				this.boxRect = new Rect(5f, (float)this.yOffset, 310f, 40f);
				this.text.Length = 0;
				this.text.AppendLine("A* Pathfinding Project Debugger");
				this.text.Append("A* Version: ").Append(AstarPath.Version.ToString());
				if (this.showMemProfile)
				{
					this.boxRect.height = this.boxRect.height + 200f;
					this.text.AppendLine();
					this.text.AppendLine();
					this.text.Append("Currently allocated".PadRight(25));
					this.text.Append(((float)this.allocMem / 1000000f).ToString("0.0 MB"));
					this.text.AppendLine();
					this.text.Append("Peak allocated".PadRight(25));
					this.text.Append(((float)this.peakAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
					this.text.Append("Last collect peak".PadRight(25));
					this.text.Append(((float)this.collectAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
					this.text.Append("Allocation rate".PadRight(25));
					this.text.Append(((float)this.allocRate / 1000000f).ToString("0.0 MB")).AppendLine();
					this.text.Append("Collection frequency".PadRight(25));
					this.text.Append(this.delta.ToString("0.00"));
					this.text.Append("s\n");
					this.text.Append("Last collect fps".PadRight(25));
					this.text.Append((1f / this.lastDeltaTime).ToString("0.0 fps"));
					this.text.Append(" (");
					this.text.Append(this.lastDeltaTime.ToString("0.000 s"));
					this.text.Append(")");
				}
				if (this.showFPS)
				{
					this.text.AppendLine();
					this.text.AppendLine();
					float num = (this.delayedDeltaTime > 1E-05f) ? (1f / this.delayedDeltaTime) : 0f;
					this.text.Append("FPS".PadRight(25)).Append(num.ToString("0.0 fps"));
					float num2 = float.PositiveInfinity;
					for (int i = 0; i < this.fpsDrops.Length; i++)
					{
						if (this.fpsDrops[i] < num2)
						{
							num2 = this.fpsDrops[i];
						}
					}
					this.text.AppendLine();
					this.text.Append(("Lowest fps (last " + this.fpsDrops.Length + ")").PadRight(25)).Append(num2.ToString("0.0"));
				}
				if (this.showPathProfile)
				{
					UnityEngine.Object active = AstarPath.active;
					this.text.AppendLine();
					if (active == null)
					{
						this.text.Append("\nNo AstarPath Object In The Scene");
					}
					else
					{
						if (ListPool<Vector3>.GetSize() > this.maxVecPool)
						{
							this.maxVecPool = ListPool<Vector3>.GetSize();
						}
						if (ListPool<GraphNode>.GetSize() > this.maxNodePool)
						{
							this.maxNodePool = ListPool<GraphNode>.GetSize();
						}
						this.text.Append("\nPool Sizes (size/total created)");
						for (int j = 0; j < this.debugTypes.Length; j++)
						{
							this.debugTypes[j].Print(this.text);
						}
					}
				}
				this.cachedText = this.text.ToString();
			}
			if (this.font != null)
			{
				this.style.font = this.font;
				this.style.fontSize = this.fontSize;
			}
			this.boxRect.height = this.style.CalcHeight(new GUIContent(this.cachedText), this.boxRect.width);
			GUI.Box(this.boxRect, "");
			GUI.Label(this.boxRect, this.cachedText, this.style);
			if (this.showGraph)
			{
				float num3 = float.PositiveInfinity;
				float num4 = 0f;
				float num5 = float.PositiveInfinity;
				float num6 = 0f;
				for (int k = 0; k < this.graph.Length; k++)
				{
					num3 = Mathf.Min(this.graph[k].memory, num3);
					num4 = Mathf.Max(this.graph[k].memory, num4);
					num5 = Mathf.Min(this.graph[k].fps, num5);
					num6 = Mathf.Max(this.graph[k].fps, num6);
				}
				GUI.color = Color.blue;
				float num7 = (float)Mathf.RoundToInt(num4 / 100000f);
				GUI.Label(new Rect(5f, (float)Screen.height - AstarMath.MapTo(num3, num4, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7 * 1000f * 100f) - 10f, 100f, 20f), (num7 / 10f).ToString("0.0 MB"));
				num7 = Mathf.Round(num3 / 100000f);
				GUI.Label(new Rect(5f, (float)Screen.height - AstarMath.MapTo(num3, num4, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7 * 1000f * 100f) - 10f, 100f, 20f), (num7 / 10f).ToString("0.0 MB"));
				GUI.color = Color.green;
				num7 = Mathf.Round(num6);
				GUI.Label(new Rect(55f, (float)Screen.height - AstarMath.MapTo(num5, num6, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7) - 10f, 100f, 20f), num7.ToString("0 FPS"));
				num7 = Mathf.Round(num5);
				GUI.Label(new Rect(55f, (float)Screen.height - AstarMath.MapTo(num5, num6, 0f + this.graphOffset, this.graphHeight + this.graphOffset, num7) - 10f, 100f, 20f), num7.ToString("0 FPS"));
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x001955D4 File Offset: 0x001937D4
		public AstarDebugger()
		{
			AstarDebugger.PathTypeDebug[] array = new AstarDebugger.PathTypeDebug[7];
			array[0] = new AstarDebugger.PathTypeDebug("ABPath", () => PathPool.GetSize(typeof(ABPath)), () => PathPool.GetTotalCreated(typeof(ABPath)));
			array[1] = new AstarDebugger.PathTypeDebug("MultiTargetPath", () => PathPool.GetSize(typeof(MultiTargetPath)), () => PathPool.GetTotalCreated(typeof(MultiTargetPath)));
			array[2] = new AstarDebugger.PathTypeDebug("RandomPath", () => PathPool.GetSize(typeof(RandomPath)), () => PathPool.GetTotalCreated(typeof(RandomPath)));
			array[3] = new AstarDebugger.PathTypeDebug("FleePath", () => PathPool.GetSize(typeof(FleePath)), () => PathPool.GetTotalCreated(typeof(FleePath)));
			array[4] = new AstarDebugger.PathTypeDebug("ConstantPath", () => PathPool.GetSize(typeof(ConstantPath)), () => PathPool.GetTotalCreated(typeof(ConstantPath)));
			array[5] = new AstarDebugger.PathTypeDebug("FloodPath", () => PathPool.GetSize(typeof(FloodPath)), () => PathPool.GetTotalCreated(typeof(FloodPath)));
			array[6] = new AstarDebugger.PathTypeDebug("FloodPathTracer", () => PathPool.GetSize(typeof(FloodPathTracer)), () => PathPool.GetTotalCreated(typeof(FloodPathTracer)));
			this.debugTypes = array;
			base..ctor();
		}

		// Token: 0x04003FB1 RID: 16305
		public int yOffset = 5;

		// Token: 0x04003FB2 RID: 16306
		public bool show = true;

		// Token: 0x04003FB3 RID: 16307
		public bool showInEditor;

		// Token: 0x04003FB4 RID: 16308
		public bool showFPS;

		// Token: 0x04003FB5 RID: 16309
		public bool showPathProfile;

		// Token: 0x04003FB6 RID: 16310
		public bool showMemProfile;

		// Token: 0x04003FB7 RID: 16311
		public bool showGraph;

		// Token: 0x04003FB8 RID: 16312
		public int graphBufferSize = 200;

		// Token: 0x04003FB9 RID: 16313
		public Font font;

		// Token: 0x04003FBA RID: 16314
		public int fontSize = 12;

		// Token: 0x04003FBB RID: 16315
		private StringBuilder text = new StringBuilder();

		// Token: 0x04003FBC RID: 16316
		private string cachedText;

		// Token: 0x04003FBD RID: 16317
		private float lastUpdate = -999f;

		// Token: 0x04003FBE RID: 16318
		private AstarDebugger.GraphPoint[] graph;

		// Token: 0x04003FBF RID: 16319
		private float delayedDeltaTime = 1f;

		// Token: 0x04003FC0 RID: 16320
		private float lastCollect;

		// Token: 0x04003FC1 RID: 16321
		private float lastCollectNum;

		// Token: 0x04003FC2 RID: 16322
		private float delta;

		// Token: 0x04003FC3 RID: 16323
		private float lastDeltaTime;

		// Token: 0x04003FC4 RID: 16324
		private int allocRate;

		// Token: 0x04003FC5 RID: 16325
		private int lastAllocMemory;

		// Token: 0x04003FC6 RID: 16326
		private float lastAllocSet = -9999f;

		// Token: 0x04003FC7 RID: 16327
		private int allocMem;

		// Token: 0x04003FC8 RID: 16328
		private int collectAlloc;

		// Token: 0x04003FC9 RID: 16329
		private int peakAlloc;

		// Token: 0x04003FCA RID: 16330
		private int fpsDropCounterSize = 200;

		// Token: 0x04003FCB RID: 16331
		private float[] fpsDrops;

		// Token: 0x04003FCC RID: 16332
		private Rect boxRect;

		// Token: 0x04003FCD RID: 16333
		private GUIStyle style;

		// Token: 0x04003FCE RID: 16334
		private Camera cam;

		// Token: 0x04003FCF RID: 16335
		private float graphWidth = 100f;

		// Token: 0x04003FD0 RID: 16336
		private float graphHeight = 100f;

		// Token: 0x04003FD1 RID: 16337
		private float graphOffset = 50f;

		// Token: 0x04003FD2 RID: 16338
		private int maxVecPool;

		// Token: 0x04003FD3 RID: 16339
		private int maxNodePool;

		// Token: 0x04003FD4 RID: 16340
		private AstarDebugger.PathTypeDebug[] debugTypes;

		// Token: 0x0200072E RID: 1838
		private struct GraphPoint
		{
			// Token: 0x040049C2 RID: 18882
			public float fps;

			// Token: 0x040049C3 RID: 18883
			public float memory;

			// Token: 0x040049C4 RID: 18884
			public bool collectEvent;
		}

		// Token: 0x0200072F RID: 1839
		private struct PathTypeDebug
		{
			// Token: 0x06002CE2 RID: 11490 RVA: 0x001CFB7B File Offset: 0x001CDD7B
			public PathTypeDebug(string name, Func<int> getSize, Func<int> getTotalCreated)
			{
				this.name = name;
				this.getSize = getSize;
				this.getTotalCreated = getTotalCreated;
			}

			// Token: 0x06002CE3 RID: 11491 RVA: 0x001CFB94 File Offset: 0x001CDD94
			public void Print(StringBuilder text)
			{
				int num = this.getTotalCreated();
				if (num > 0)
				{
					text.Append("\n").Append(("  " + this.name).PadRight(25)).Append(this.getSize()).Append("/").Append(num);
				}
			}

			// Token: 0x040049C5 RID: 18885
			private string name;

			// Token: 0x040049C6 RID: 18886
			private Func<int> getSize;

			// Token: 0x040049C7 RID: 18887
			private Func<int> getTotalCreated;
		}
	}
}
