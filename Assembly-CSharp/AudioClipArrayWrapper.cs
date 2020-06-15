using System;
using UnityEngine;

// Token: 0x0200043D RID: 1085
[Serializable]
public class AudioClipArrayWrapper : ArrayWrapper<AudioClip>
{
	// Token: 0x06001CAD RID: 7341 RVA: 0x0015851B File Offset: 0x0015671B
	public AudioClipArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x00158524 File Offset: 0x00156724
	public AudioClipArrayWrapper(AudioClip[] elements) : base(elements)
	{
	}
}
