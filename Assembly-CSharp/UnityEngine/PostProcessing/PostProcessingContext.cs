using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004DF RID: 1247
	public class PostProcessingContext
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0018463C File Offset: 0x0018283C
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x00184644 File Offset: 0x00182844
		public bool interrupted { get; private set; }

		// Token: 0x06001F65 RID: 8037 RVA: 0x0018464D File Offset: 0x0018284D
		public void Interrupt()
		{
			this.interrupted = true;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00184656 File Offset: 0x00182856
		public PostProcessingContext Reset()
		{
			this.profile = null;
			this.camera = null;
			this.materialFactory = null;
			this.renderTextureFactory = null;
			this.interrupted = false;
			return this;
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0018467C File Offset: 0x0018287C
		public bool isGBufferAvailable
		{
			get
			{
				return this.camera.actualRenderingPath == RenderingPath.DeferredShading;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0018468C File Offset: 0x0018288C
		public bool isHdr
		{
			get
			{
				return this.camera.allowHDR;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x00184699 File Offset: 0x00182899
		public int width
		{
			get
			{
				return this.camera.pixelWidth;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x001846A6 File Offset: 0x001828A6
		public int height
		{
			get
			{
				return this.camera.pixelHeight;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x001846B3 File Offset: 0x001828B3
		public Rect viewport
		{
			get
			{
				return this.camera.rect;
			}
		}

		// Token: 0x04003D58 RID: 15704
		public PostProcessingProfile profile;

		// Token: 0x04003D59 RID: 15705
		public Camera camera;

		// Token: 0x04003D5A RID: 15706
		public MaterialFactory materialFactory;

		// Token: 0x04003D5B RID: 15707
		public RenderTextureFactory renderTextureFactory;
	}
}
