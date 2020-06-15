using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AmplifyMotion
{
	// Token: 0x020004E8 RID: 1256
	[Serializable]
	internal abstract class MotionState
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x00184E2B File Offset: 0x0018302B
		public AmplifyMotionCamera Owner
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x00184E33 File Offset: 0x00183033
		public bool Initialized
		{
			get
			{
				return this.m_initialized;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x00184E3B File Offset: 0x0018303B
		public bool Error
		{
			get
			{
				return this.m_error;
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00184E43 File Offset: 0x00183043
		public MotionState(AmplifyMotionCamera owner, AmplifyMotionObjectBase obj)
		{
			this.m_error = false;
			this.m_initialized = false;
			this.m_owner = owner;
			this.m_obj = obj;
			this.m_transform = obj.transform;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00184E73 File Offset: 0x00183073
		internal virtual void Initialize()
		{
			this.m_initialized = true;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void Shutdown()
		{
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void AsyncUpdate()
		{
		}

		// Token: 0x06001F8E RID: 8078
		internal abstract void UpdateTransform(CommandBuffer updateCB, bool starting);

		// Token: 0x06001F8F RID: 8079 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void RenderVectors(Camera camera, CommandBuffer renderCB, float scale, Quality quality)
		{
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00002ACE File Offset: 0x00000CCE
		internal virtual void RenderDebugHUD()
		{
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00184E7C File Offset: 0x0018307C
		protected MotionState.MaterialDesc[] ProcessSharedMaterials(Material[] mats)
		{
			MotionState.MaterialDesc[] array = new MotionState.MaterialDesc[mats.Length];
			for (int i = 0; i < mats.Length; i++)
			{
				array[i].material = mats[i];
				bool flag = mats[i].GetTag("RenderType", false) == "TransparentCutout" || mats[i].IsKeywordEnabled("_ALPHATEST_ON");
				array[i].propertyBlock = new MaterialPropertyBlock();
				array[i].coverage = (mats[i].HasProperty("_MainTex") && flag);
				array[i].cutoff = mats[i].HasProperty("_Cutoff");
				if (flag && !array[i].coverage && !MotionState.m_materialWarnings.Contains(array[i].material))
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"[AmplifyMotion] TransparentCutout material \"",
						array[i].material.name,
						"\" {",
						array[i].material.shader.name,
						"} not using _MainTex standard property."
					}));
					MotionState.m_materialWarnings.Add(array[i].material);
				}
			}
			return array;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00184FC4 File Offset: 0x001831C4
		protected static bool MatrixChanged(MotionState.Matrix3x4 a, MotionState.Matrix3x4 b)
		{
			return Vector4.SqrMagnitude(new Vector4(a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02, a.m03 - b.m03)) > 0f || Vector4.SqrMagnitude(new Vector4(a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12, a.m13 - b.m13)) > 0f || Vector4.SqrMagnitude(new Vector4(a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22, a.m23 - b.m23)) > 0f;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x001850A8 File Offset: 0x001832A8
		protected static void MulPoint3x4_XYZ(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x = mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03;
			result.y = mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13;
			result.z = mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00185158 File Offset: 0x00183358
		protected static void MulPoint3x4_XYZW(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x = mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03 * vec.w;
			result.y = mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13 * vec.w;
			result.z = mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23 * vec.w;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0018521C File Offset: 0x0018341C
		protected static void MulAddPoint3x4_XYZW(ref Vector3 result, ref MotionState.Matrix3x4 mat, Vector4 vec)
		{
			result.x += mat.m00 * vec.x + mat.m01 * vec.y + mat.m02 * vec.z + mat.m03 * vec.w;
			result.y += mat.m10 * vec.x + mat.m11 * vec.y + mat.m12 * vec.z + mat.m13 * vec.w;
			result.z += mat.m20 * vec.x + mat.m21 * vec.y + mat.m22 * vec.z + mat.m23 * vec.w;
		}

		// Token: 0x04003D81 RID: 15745
		public const int AsyncUpdateTimeout = 100;

		// Token: 0x04003D82 RID: 15746
		protected bool m_error;

		// Token: 0x04003D83 RID: 15747
		protected bool m_initialized;

		// Token: 0x04003D84 RID: 15748
		protected Transform m_transform;

		// Token: 0x04003D85 RID: 15749
		protected AmplifyMotionCamera m_owner;

		// Token: 0x04003D86 RID: 15750
		protected AmplifyMotionObjectBase m_obj;

		// Token: 0x04003D87 RID: 15751
		private static HashSet<Material> m_materialWarnings = new HashSet<Material>();

		// Token: 0x02000711 RID: 1809
		protected struct MaterialDesc
		{
			// Token: 0x04004935 RID: 18741
			public Material material;

			// Token: 0x04004936 RID: 18742
			public MaterialPropertyBlock propertyBlock;

			// Token: 0x04004937 RID: 18743
			public bool coverage;

			// Token: 0x04004938 RID: 18744
			public bool cutoff;
		}

		// Token: 0x02000712 RID: 1810
		protected struct Matrix3x4
		{
			// Token: 0x06002C7F RID: 11391 RVA: 0x001CE8EC File Offset: 0x001CCAEC
			public Vector4 GetRow(int i)
			{
				if (i == 0)
				{
					return new Vector4(this.m00, this.m01, this.m02, this.m03);
				}
				if (i == 1)
				{
					return new Vector4(this.m10, this.m11, this.m12, this.m13);
				}
				if (i == 2)
				{
					return new Vector4(this.m20, this.m21, this.m22, this.m23);
				}
				return new Vector4(0f, 0f, 0f, 1f);
			}

			// Token: 0x06002C80 RID: 11392 RVA: 0x001CE978 File Offset: 0x001CCB78
			public static implicit operator MotionState.Matrix3x4(Matrix4x4 from)
			{
				return new MotionState.Matrix3x4
				{
					m00 = from.m00,
					m01 = from.m01,
					m02 = from.m02,
					m03 = from.m03,
					m10 = from.m10,
					m11 = from.m11,
					m12 = from.m12,
					m13 = from.m13,
					m20 = from.m20,
					m21 = from.m21,
					m22 = from.m22,
					m23 = from.m23
				};
			}

			// Token: 0x06002C81 RID: 11393 RVA: 0x001CEA2C File Offset: 0x001CCC2C
			public static implicit operator Matrix4x4(MotionState.Matrix3x4 from)
			{
				Matrix4x4 result = default(Matrix4x4);
				result.m00 = from.m00;
				result.m01 = from.m01;
				result.m02 = from.m02;
				result.m03 = from.m03;
				result.m10 = from.m10;
				result.m11 = from.m11;
				result.m12 = from.m12;
				result.m13 = from.m13;
				result.m20 = from.m20;
				result.m21 = from.m21;
				result.m22 = from.m22;
				result.m23 = from.m23;
				result.m30 = (result.m31 = (result.m32 = 0f));
				result.m33 = 1f;
				return result;
			}

			// Token: 0x06002C82 RID: 11394 RVA: 0x001CEB0C File Offset: 0x001CCD0C
			public static MotionState.Matrix3x4 operator *(MotionState.Matrix3x4 a, MotionState.Matrix3x4 b)
			{
				return new MotionState.Matrix3x4
				{
					m00 = a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20,
					m01 = a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21,
					m02 = a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22,
					m03 = a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03,
					m10 = a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20,
					m11 = a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21,
					m12 = a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22,
					m13 = a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13,
					m20 = a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20,
					m21 = a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21,
					m22 = a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22,
					m23 = a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23
				};
			}

			// Token: 0x04004939 RID: 18745
			public float m00;

			// Token: 0x0400493A RID: 18746
			public float m01;

			// Token: 0x0400493B RID: 18747
			public float m02;

			// Token: 0x0400493C RID: 18748
			public float m03;

			// Token: 0x0400493D RID: 18749
			public float m10;

			// Token: 0x0400493E RID: 18750
			public float m11;

			// Token: 0x0400493F RID: 18751
			public float m12;

			// Token: 0x04004940 RID: 18752
			public float m13;

			// Token: 0x04004941 RID: 18753
			public float m20;

			// Token: 0x04004942 RID: 18754
			public float m21;

			// Token: 0x04004943 RID: 18755
			public float m22;

			// Token: 0x04004944 RID: 18756
			public float m23;
		}
	}
}
