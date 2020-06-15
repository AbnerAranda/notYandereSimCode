using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004BD RID: 1213
	public sealed class BuiltinDebugViewsComponent : PostProcessingComponentCommandBuffer<BuiltinDebugViewsModel>
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0017FFDC File Offset: 0x0017E1DC
		public override bool active
		{
			get
			{
				return base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Depth) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Normals) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.MotionVectors);
			}
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00180008 File Offset: 0x0017E208
		public override DepthTextureMode GetCameraFlags()
		{
			BuiltinDebugViewsModel.Mode mode = base.model.settings.mode;
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			switch (mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				depthTextureMode |= DepthTextureMode.Depth;
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				depthTextureMode |= DepthTextureMode.DepthNormals;
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				depthTextureMode |= (DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
				break;
			}
			return depthTextureMode;
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0018004F File Offset: 0x0017E24F
		public override CameraEvent GetCameraEvent()
		{
			if (base.model.settings.mode != BuiltinDebugViewsModel.Mode.MotionVectors)
			{
				return CameraEvent.BeforeImageEffectsOpaque;
			}
			return CameraEvent.BeforeImageEffects;
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00180069 File Offset: 0x0017E269
		public override string GetName()
		{
			return "Builtin Debug Views";
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00180070 File Offset: 0x0017E270
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			ref BuiltinDebugViewsModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			material.shaderKeywords = null;
			if (this.context.isGBufferAvailable)
			{
				material.EnableKeyword("SOURCE_GBUFFER");
			}
			switch (settings.mode)
			{
			case BuiltinDebugViewsModel.Mode.Depth:
				this.DepthPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.Normals:
				this.DepthNormalsPass(cb);
				break;
			case BuiltinDebugViewsModel.Mode.MotionVectors:
				this.MotionVectorsPass(cb);
				break;
			}
			this.context.Interrupt();
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00180100 File Offset: 0x0017E300
		private void DepthPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.DepthSettings depth = base.model.settings.depth;
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._DepthScale, 1f / depth.scale);
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 0);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0018015C File Offset: 0x0017E35C
		private void DepthNormalsPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 1);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00180190 File Offset: 0x0017E390
		private void MotionVectorsPass(CommandBuffer cb)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.MotionVectorsSettings motionVectors = base.model.settings.motionVectors;
			int nameID = BuiltinDebugViewsComponent.Uniforms._TempRT;
			cb.GetTemporaryRT(nameID, this.context.width, this.context.height, 0, FilterMode.Bilinear);
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.sourceOpacity);
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, nameID, material, 2);
			if (motionVectors.motionImageOpacity > 0f && motionVectors.motionImageAmplitude > 0f)
			{
				int tempRT = BuiltinDebugViewsComponent.Uniforms._TempRT2;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionImageOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionImageAmplitude);
				cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
				cb.Blit(nameID, tempRT, material, 3);
				cb.ReleaseTemporaryRT(nameID);
				nameID = tempRT;
			}
			if (motionVectors.motionVectorsOpacity > 0f && motionVectors.motionVectorsAmplitude > 0f)
			{
				this.PrepareArrows();
				float num = 1f / (float)motionVectors.motionVectorsResolution;
				float x = num * (float)this.context.height / (float)this.context.width;
				cb.SetGlobalVector(BuiltinDebugViewsComponent.Uniforms._Scale, new Vector2(x, num));
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionVectorsOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionVectorsAmplitude);
				cb.DrawMesh(this.m_Arrows.mesh, Matrix4x4.identity, material, 0, 4);
			}
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
			cb.Blit(nameID, BuiltinRenderTextureType.CameraTarget);
			cb.ReleaseTemporaryRT(nameID);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00180384 File Offset: 0x0017E584
		private void PrepareArrows()
		{
			int motionVectorsResolution = base.model.settings.motionVectors.motionVectorsResolution;
			int num = motionVectorsResolution * Screen.width / Screen.height;
			if (this.m_Arrows == null)
			{
				this.m_Arrows = new BuiltinDebugViewsComponent.ArrowArray();
			}
			if (this.m_Arrows.columnCount != num || this.m_Arrows.rowCount != motionVectorsResolution)
			{
				this.m_Arrows.Release();
				this.m_Arrows.BuildMesh(num, motionVectorsResolution);
			}
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x001803FC File Offset: 0x0017E5FC
		public override void OnDisable()
		{
			if (this.m_Arrows != null)
			{
				this.m_Arrows.Release();
			}
			this.m_Arrows = null;
		}

		// Token: 0x04003CFE RID: 15614
		private const string k_ShaderString = "Hidden/Post FX/Builtin Debug Views";

		// Token: 0x04003CFF RID: 15615
		private BuiltinDebugViewsComponent.ArrowArray m_Arrows;

		// Token: 0x020006D1 RID: 1745
		private static class Uniforms
		{
			// Token: 0x040047D1 RID: 18385
			internal static readonly int _DepthScale = Shader.PropertyToID("_DepthScale");

			// Token: 0x040047D2 RID: 18386
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");

			// Token: 0x040047D3 RID: 18387
			internal static readonly int _Opacity = Shader.PropertyToID("_Opacity");

			// Token: 0x040047D4 RID: 18388
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x040047D5 RID: 18389
			internal static readonly int _TempRT2 = Shader.PropertyToID("_TempRT2");

			// Token: 0x040047D6 RID: 18390
			internal static readonly int _Amplitude = Shader.PropertyToID("_Amplitude");

			// Token: 0x040047D7 RID: 18391
			internal static readonly int _Scale = Shader.PropertyToID("_Scale");
		}

		// Token: 0x020006D2 RID: 1746
		private enum Pass
		{
			// Token: 0x040047D9 RID: 18393
			Depth,
			// Token: 0x040047DA RID: 18394
			Normals,
			// Token: 0x040047DB RID: 18395
			MovecOpacity,
			// Token: 0x040047DC RID: 18396
			MovecImaging,
			// Token: 0x040047DD RID: 18397
			MovecArrows
		}

		// Token: 0x020006D3 RID: 1747
		private class ArrowArray
		{
			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06002C3E RID: 11326 RVA: 0x001CCC62 File Offset: 0x001CAE62
			// (set) Token: 0x06002C3F RID: 11327 RVA: 0x001CCC6A File Offset: 0x001CAE6A
			public Mesh mesh { get; private set; }

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06002C40 RID: 11328 RVA: 0x001CCC73 File Offset: 0x001CAE73
			// (set) Token: 0x06002C41 RID: 11329 RVA: 0x001CCC7B File Offset: 0x001CAE7B
			public int columnCount { get; private set; }

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06002C42 RID: 11330 RVA: 0x001CCC84 File Offset: 0x001CAE84
			// (set) Token: 0x06002C43 RID: 11331 RVA: 0x001CCC8C File Offset: 0x001CAE8C
			public int rowCount { get; private set; }

			// Token: 0x06002C44 RID: 11332 RVA: 0x001CCC98 File Offset: 0x001CAE98
			public void BuildMesh(int columns, int rows)
			{
				Vector3[] array = new Vector3[]
				{
					new Vector3(0f, 0f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(1f, 1f, 0f)
				};
				int num = 6 * columns * rows;
				List<Vector3> list = new List<Vector3>(num);
				List<Vector2> list2 = new List<Vector2>(num);
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < columns; j++)
					{
						Vector2 item = new Vector2((0.5f + (float)j) / (float)columns, (0.5f + (float)i) / (float)rows);
						for (int k = 0; k < 6; k++)
						{
							list.Add(array[k]);
							list2.Add(item);
						}
					}
				}
				int[] array2 = new int[num];
				for (int l = 0; l < num; l++)
				{
					array2[l] = l;
				}
				this.mesh = new Mesh
				{
					hideFlags = HideFlags.DontSave
				};
				this.mesh.SetVertices(list);
				this.mesh.SetUVs(0, list2);
				this.mesh.SetIndices(array2, MeshTopology.Lines, 0);
				this.mesh.UploadMeshData(true);
				this.columnCount = columns;
				this.rowCount = rows;
			}

			// Token: 0x06002C45 RID: 11333 RVA: 0x001CCE3B File Offset: 0x001CB03B
			public void Release()
			{
				GraphicsUtils.Destroy(this.mesh);
				this.mesh = null;
			}
		}
	}
}
