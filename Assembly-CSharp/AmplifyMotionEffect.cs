using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Amplify Motion")]
public class AmplifyMotionEffect : AmplifyMotionEffectBase
{
	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000997 RID: 2455 RVA: 0x0004B258 File Offset: 0x00049458
	public new static AmplifyMotionEffect FirstInstance
	{
		get
		{
			return (AmplifyMotionEffect)AmplifyMotionEffectBase.FirstInstance;
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000998 RID: 2456 RVA: 0x0004B264 File Offset: 0x00049464
	public new static AmplifyMotionEffect Instance
	{
		get
		{
			return (AmplifyMotionEffect)AmplifyMotionEffectBase.Instance;
		}
	}
}
