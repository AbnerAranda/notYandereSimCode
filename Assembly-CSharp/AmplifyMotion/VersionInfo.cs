using System;
using UnityEngine;

namespace AmplifyMotion
{
	// Token: 0x020004EB RID: 1259
	[Serializable]
	public class VersionInfo
	{
		// Token: 0x06001FA7 RID: 8103 RVA: 0x001862B1 File Offset: 0x001844B1
		public static string StaticToString()
		{
			return string.Format("{0}.{1}.{2}", 1, 8, 3) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x001862DE File Offset: 0x001844DE
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.m_major, this.m_minor, this.m_release) + VersionInfo.StageSuffix + VersionInfo.TrialSuffix;
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0018631A File Offset: 0x0018451A
		public int Number
		{
			get
			{
				return this.m_major * 100 + this.m_minor * 10 + this.m_release;
			}
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00186336 File Offset: 0x00184536
		private VersionInfo()
		{
			this.m_major = 1;
			this.m_minor = 8;
			this.m_release = 3;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00186353 File Offset: 0x00184553
		private VersionInfo(byte major, byte minor, byte release)
		{
			this.m_major = (int)major;
			this.m_minor = (int)minor;
			this.m_release = (int)release;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00186370 File Offset: 0x00184570
		public static VersionInfo Current()
		{
			return new VersionInfo(1, 8, 3);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0018637A File Offset: 0x0018457A
		public static bool Matches(VersionInfo version)
		{
			return 1 == version.m_major && 8 == version.m_minor && 3 == version.m_release;
		}

		// Token: 0x04003DA3 RID: 15779
		public const byte Major = 1;

		// Token: 0x04003DA4 RID: 15780
		public const byte Minor = 8;

		// Token: 0x04003DA5 RID: 15781
		public const byte Release = 3;

		// Token: 0x04003DA6 RID: 15782
		private static string StageSuffix = "_dev001";

		// Token: 0x04003DA7 RID: 15783
		private static string TrialSuffix = "";

		// Token: 0x04003DA8 RID: 15784
		[SerializeField]
		private int m_major;

		// Token: 0x04003DA9 RID: 15785
		[SerializeField]
		private int m_minor;

		// Token: 0x04003DAA RID: 15786
		[SerializeField]
		private int m_release;
	}
}
