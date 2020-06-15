using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C1 RID: 1217
	public sealed class DitheringComponent : PostProcessingComponentRenderTexture<DitheringModel>
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0018177E File Offset: 0x0017F97E
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0018179D File Offset: 0x0017F99D
		public override void OnDisable()
		{
			this.noiseTextures = null;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x001817A8 File Offset: 0x0017F9A8
		private void LoadNoiseTextures()
		{
			this.noiseTextures = new Texture2D[64];
			for (int i = 0; i < 64; i++)
			{
				this.noiseTextures[i] = Resources.Load<Texture2D>("Bluenoise64/LDR_LLL1_" + i);
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x001817EC File Offset: 0x0017F9EC
		public override void Prepare(Material uberMaterial)
		{
			int num = this.textureIndex + 1;
			this.textureIndex = num;
			if (num >= 64)
			{
				this.textureIndex = 0;
			}
			float value = Random.value;
			float value2 = Random.value;
			if (this.noiseTextures == null)
			{
				this.LoadNoiseTextures();
			}
			Texture2D texture2D = this.noiseTextures[this.textureIndex];
			uberMaterial.EnableKeyword("DITHERING");
			uberMaterial.SetTexture(DitheringComponent.Uniforms._DitheringTex, texture2D);
			uberMaterial.SetVector(DitheringComponent.Uniforms._DitheringCoords, new Vector4((float)this.context.width / (float)texture2D.width, (float)this.context.height / (float)texture2D.height, value, value2));
		}

		// Token: 0x04003D09 RID: 15625
		private Texture2D[] noiseTextures;

		// Token: 0x04003D0A RID: 15626
		private int textureIndex;

		// Token: 0x04003D0B RID: 15627
		private const int k_TextureCount = 64;

		// Token: 0x020006D7 RID: 1751
		private static class Uniforms
		{
			// Token: 0x04004802 RID: 18434
			internal static readonly int _DitheringTex = Shader.PropertyToID("_DitheringTex");

			// Token: 0x04004803 RID: 18435
			internal static readonly int _DitheringCoords = Shader.PropertyToID("_DitheringCoords");
		}
	}
}
