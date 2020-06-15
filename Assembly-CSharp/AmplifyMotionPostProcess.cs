using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[AddComponentMenu("")]
[RequireComponent(typeof(Camera))]
public sealed class AmplifyMotionPostProcess : MonoBehaviour
{
	// Token: 0x17000201 RID: 513
	// (get) Token: 0x060009AE RID: 2478 RVA: 0x0004B751 File Offset: 0x00049951
	// (set) Token: 0x060009AF RID: 2479 RVA: 0x0004B759 File Offset: 0x00049959
	public AmplifyMotionEffectBase Instance
	{
		get
		{
			return this.m_instance;
		}
		set
		{
			this.m_instance = value;
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0004B762 File Offset: 0x00049962
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_instance != null)
		{
			this.m_instance.PostProcess(source, destination);
		}
	}

	// Token: 0x0400081A RID: 2074
	private AmplifyMotionEffectBase m_instance;
}
