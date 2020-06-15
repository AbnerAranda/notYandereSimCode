using System;

// Token: 0x020003CF RID: 975
public static class SchoolAtmosphere
{
	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00103F14 File Offset: 0x00102114
	public static SchoolAtmosphereType Type
	{
		get
		{
			float schoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
			if (schoolAtmosphere > 0.6666667f)
			{
				return SchoolAtmosphereType.High;
			}
			if (schoolAtmosphere > 0.333333343f)
			{
				return SchoolAtmosphereType.Medium;
			}
			return SchoolAtmosphereType.Low;
		}
	}
}
