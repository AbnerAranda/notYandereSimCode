using System;
using UnityEngine;

// Token: 0x020002CA RID: 714
public static class PoseModeGlobals
{
	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000B9E71 File Offset: 0x000B8071
	// (set) Token: 0x060015C1 RID: 5569 RVA: 0x000B9E91 File Offset: 0x000B8091
	public static Vector3 PosePosition
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PosePosition");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PosePosition", value);
		}
	}

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x060015C2 RID: 5570 RVA: 0x000B9EB2 File Offset: 0x000B80B2
	// (set) Token: 0x060015C3 RID: 5571 RVA: 0x000B9ED2 File Offset: 0x000B80D2
	public static Vector3 PoseRotation
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PoseRotation");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PoseRotation", value);
		}
	}

	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000B9EF3 File Offset: 0x000B80F3
	// (set) Token: 0x060015C5 RID: 5573 RVA: 0x000B9F13 File Offset: 0x000B8113
	public static Vector3 PoseScale
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PoseScale");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PoseScale", value);
		}
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x000B9F34 File Offset: 0x000B8134
	public static void DeleteAll()
	{
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PosePosition");
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PoseRotation");
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PoseScale");
	}

	// Token: 0x04001DC4 RID: 7620
	private const string Str_PosePosition = "PosePosition";

	// Token: 0x04001DC5 RID: 7621
	private const string Str_PoseRotation = "PoseRotation";

	// Token: 0x04001DC6 RID: 7622
	private const string Str_PoseScale = "PoseScale";
}
