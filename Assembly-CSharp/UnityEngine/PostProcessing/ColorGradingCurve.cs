using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020004E2 RID: 1250
	[Serializable]
	public sealed class ColorGradingCurve
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x00184794 File Offset: 0x00182994
		public ColorGradingCurve(AnimationCurve curve, float zeroValue, bool loop, Vector2 bounds)
		{
			this.curve = curve;
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			this.m_Range = bounds.magnitude;
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x001847C0 File Offset: 0x001829C0
		public void Cache()
		{
			if (!this.m_Loop)
			{
				return;
			}
			int length = this.curve.length;
			if (length < 2)
			{
				return;
			}
			if (this.m_InternalLoopingCurve == null)
			{
				this.m_InternalLoopingCurve = new AnimationCurve();
			}
			Keyframe key = this.curve[length - 1];
			key.time -= this.m_Range;
			Keyframe key2 = this.curve[0];
			key2.time += this.m_Range;
			this.m_InternalLoopingCurve.keys = this.curve.keys;
			this.m_InternalLoopingCurve.AddKey(key);
			this.m_InternalLoopingCurve.AddKey(key2);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00184870 File Offset: 0x00182A70
		public float Evaluate(float t)
		{
			if (this.curve.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.curve.length == 1)
			{
				return this.curve.Evaluate(t);
			}
			return this.m_InternalLoopingCurve.Evaluate(t);
		}

		// Token: 0x04003D6D RID: 15725
		public AnimationCurve curve;

		// Token: 0x04003D6E RID: 15726
		[SerializeField]
		private bool m_Loop;

		// Token: 0x04003D6F RID: 15727
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x04003D70 RID: 15728
		[SerializeField]
		private float m_Range;

		// Token: 0x04003D71 RID: 15729
		private AnimationCurve m_InternalLoopingCurve;
	}
}
