﻿using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C8 RID: 1224
	public sealed class TaaComponent : PostProcessingComponentRenderTexture<AntialiasingModel>
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00182CA4 File Offset: 0x00180EA4
		public override bool active
		{
			get
			{
				return base.model.enabled && base.model.settings.method == AntialiasingModel.Method.Taa && SystemInfo.supportsMotionVectors && SystemInfo.supportedRenderTargetCount >= 2 && !this.context.interrupted;
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0009778C File Offset: 0x0009598C
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00182CF0 File Offset: 0x00180EF0
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x00182CF8 File Offset: 0x00180EF8
		public Vector2 jitterVector { get; private set; }

		// Token: 0x06001EEE RID: 7918 RVA: 0x00182D01 File Offset: 0x00180F01
		public void ResetHistory()
		{
			this.m_ResetHistory = true;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00182D0C File Offset: 0x00180F0C
		public void SetProjectionMatrix(Func<Vector2, Matrix4x4> jitteredFunc)
		{
			AntialiasingModel.TaaSettings taaSettings = base.model.settings.taaSettings;
			Vector2 vector = this.GenerateRandomOffset();
			vector *= taaSettings.jitterSpread;
			this.context.camera.nonJitteredProjectionMatrix = this.context.camera.projectionMatrix;
			if (jitteredFunc != null)
			{
				this.context.camera.projectionMatrix = jitteredFunc(vector);
			}
			else
			{
				this.context.camera.projectionMatrix = (this.context.camera.orthographic ? this.GetOrthographicProjectionMatrix(vector) : this.GetPerspectiveProjectionMatrix(vector));
			}
			this.context.camera.useJitteredProjectionMatrixForTransparentRendering = false;
			vector.x /= (float)this.context.width;
			vector.y /= (float)this.context.height;
			this.context.materialFactory.Get("Hidden/Post FX/Temporal Anti-aliasing").SetVector(TaaComponent.Uniforms._Jitter, vector);
			this.jitterVector = vector;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00182E18 File Offset: 0x00181018
		public void Render(RenderTexture source, RenderTexture destination)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Temporal Anti-aliasing");
			material.shaderKeywords = null;
			AntialiasingModel.TaaSettings taaSettings = base.model.settings.taaSettings;
			if (this.m_ResetHistory || this.m_HistoryTexture == null || this.m_HistoryTexture.width != source.width || this.m_HistoryTexture.height != source.height)
			{
				if (this.m_HistoryTexture)
				{
					RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
				}
				this.m_HistoryTexture = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
				this.m_HistoryTexture.name = "TAA History";
				Graphics.Blit(source, this.m_HistoryTexture, material, 2);
			}
			material.SetVector(TaaComponent.Uniforms._SharpenParameters, new Vector4(taaSettings.sharpen, 0f, 0f, 0f));
			material.SetVector(TaaComponent.Uniforms._FinalBlendParameters, new Vector4(taaSettings.stationaryBlending, taaSettings.motionBlending, 6000f, 0f));
			material.SetTexture(TaaComponent.Uniforms._MainTex, source);
			material.SetTexture(TaaComponent.Uniforms._HistoryTex, this.m_HistoryTexture);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			temporary.name = "TAA History";
			this.m_MRT[0] = destination.colorBuffer;
			this.m_MRT[1] = temporary.colorBuffer;
			Graphics.SetRenderTarget(this.m_MRT, source.depthBuffer);
			GraphicsUtils.Blit(material, this.context.camera.orthographic ? 1 : 0);
			RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
			this.m_HistoryTexture = temporary;
			this.m_ResetHistory = false;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00182FDC File Offset: 0x001811DC
		private float GetHaltonValue(int index, int radix)
		{
			float num = 0f;
			float num2 = 1f / (float)radix;
			while (index > 0)
			{
				num += (float)(index % radix) * num2;
				index /= radix;
				num2 /= (float)radix;
			}
			return num;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00183014 File Offset: 0x00181214
		private Vector2 GenerateRandomOffset()
		{
			Vector2 result = new Vector2(this.GetHaltonValue(this.m_SampleIndex & 1023, 2), this.GetHaltonValue(this.m_SampleIndex & 1023, 3));
			int num = this.m_SampleIndex + 1;
			this.m_SampleIndex = num;
			if (num >= 8)
			{
				this.m_SampleIndex = 0;
			}
			return result;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00183068 File Offset: 0x00181268
		private Matrix4x4 GetPerspectiveProjectionMatrix(Vector2 offset)
		{
			float num = Mathf.Tan(0.008726646f * this.context.camera.fieldOfView);
			float num2 = num * this.context.camera.aspect;
			offset.x *= num2 / (0.5f * (float)this.context.width);
			offset.y *= num / (0.5f * (float)this.context.height);
			float num3 = (offset.x - num2) * this.context.camera.nearClipPlane;
			float num4 = (offset.x + num2) * this.context.camera.nearClipPlane;
			float num5 = (offset.y + num) * this.context.camera.nearClipPlane;
			float num6 = (offset.y - num) * this.context.camera.nearClipPlane;
			Matrix4x4 result = default(Matrix4x4);
			result[0, 0] = 2f * this.context.camera.nearClipPlane / (num4 - num3);
			result[0, 1] = 0f;
			result[0, 2] = (num4 + num3) / (num4 - num3);
			result[0, 3] = 0f;
			result[1, 0] = 0f;
			result[1, 1] = 2f * this.context.camera.nearClipPlane / (num5 - num6);
			result[1, 2] = (num5 + num6) / (num5 - num6);
			result[1, 3] = 0f;
			result[2, 0] = 0f;
			result[2, 1] = 0f;
			result[2, 2] = -(this.context.camera.farClipPlane + this.context.camera.nearClipPlane) / (this.context.camera.farClipPlane - this.context.camera.nearClipPlane);
			result[2, 3] = -(2f * this.context.camera.farClipPlane * this.context.camera.nearClipPlane) / (this.context.camera.farClipPlane - this.context.camera.nearClipPlane);
			result[3, 0] = 0f;
			result[3, 1] = 0f;
			result[3, 2] = -1f;
			result[3, 3] = 0f;
			return result;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x001832F0 File Offset: 0x001814F0
		private Matrix4x4 GetOrthographicProjectionMatrix(Vector2 offset)
		{
			float orthographicSize = this.context.camera.orthographicSize;
			float num = orthographicSize * this.context.camera.aspect;
			offset.x *= num / (0.5f * (float)this.context.width);
			offset.y *= orthographicSize / (0.5f * (float)this.context.height);
			float left = offset.x - num;
			float right = offset.x + num;
			float top = offset.y + orthographicSize;
			float bottom = offset.y - orthographicSize;
			return Matrix4x4.Ortho(left, right, bottom, top, this.context.camera.nearClipPlane, this.context.camera.farClipPlane);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x001833AC File Offset: 0x001815AC
		public override void OnDisable()
		{
			if (this.m_HistoryTexture != null)
			{
				RenderTexture.ReleaseTemporary(this.m_HistoryTexture);
			}
			this.m_HistoryTexture = null;
			this.m_SampleIndex = 0;
			this.ResetHistory();
		}

		// Token: 0x04003D21 RID: 15649
		private const string k_ShaderString = "Hidden/Post FX/Temporal Anti-aliasing";

		// Token: 0x04003D22 RID: 15650
		private const int k_SampleCount = 8;

		// Token: 0x04003D23 RID: 15651
		private readonly RenderBuffer[] m_MRT = new RenderBuffer[2];

		// Token: 0x04003D24 RID: 15652
		private int m_SampleIndex;

		// Token: 0x04003D25 RID: 15653
		private bool m_ResetHistory = true;

		// Token: 0x04003D26 RID: 15654
		private RenderTexture m_HistoryTexture;

		// Token: 0x020006E2 RID: 1762
		private static class Uniforms
		{
			// Token: 0x0400486C RID: 18540
			internal static int _Jitter = Shader.PropertyToID("_Jitter");

			// Token: 0x0400486D RID: 18541
			internal static int _SharpenParameters = Shader.PropertyToID("_SharpenParameters");

			// Token: 0x0400486E RID: 18542
			internal static int _FinalBlendParameters = Shader.PropertyToID("_FinalBlendParameters");

			// Token: 0x0400486F RID: 18543
			internal static int _HistoryTex = Shader.PropertyToID("_HistoryTex");

			// Token: 0x04004870 RID: 18544
			internal static int _MainTex = Shader.PropertyToID("_MainTex");
		}
	}
}
