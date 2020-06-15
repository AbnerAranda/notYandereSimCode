using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004C9 RID: 1225
	public sealed class UserLutComponent : PostProcessingComponentRenderTexture<UserLutModel>
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x001833F8 File Offset: 0x001815F8
		public override bool active
		{
			get
			{
				UserLutModel.Settings settings = base.model.settings;
				return base.model.enabled && settings.lut != null && settings.contribution > 0f && settings.lut.height == (int)Mathf.Sqrt((float)settings.lut.width) && !this.context.interrupted;
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00183468 File Offset: 0x00181668
		public override void Prepare(Material uberMaterial)
		{
			UserLutModel.Settings settings = base.model.settings;
			uberMaterial.EnableKeyword("USER_LUT");
			uberMaterial.SetTexture(UserLutComponent.Uniforms._UserLut, settings.lut);
			uberMaterial.SetVector(UserLutComponent.Uniforms._UserLut_Params, new Vector4(1f / (float)settings.lut.width, 1f / (float)settings.lut.height, (float)settings.lut.height - 1f, settings.contribution));
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x001834EC File Offset: 0x001816EC
		public void OnGUI()
		{
			UserLutModel.Settings settings = base.model.settings;
			GUI.DrawTexture(new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)settings.lut.width, (float)settings.lut.height), settings.lut);
		}

		// Token: 0x020006E3 RID: 1763
		private static class Uniforms
		{
			// Token: 0x04004871 RID: 18545
			internal static readonly int _UserLut = Shader.PropertyToID("_UserLut");

			// Token: 0x04004872 RID: 18546
			internal static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");
		}
	}
}
