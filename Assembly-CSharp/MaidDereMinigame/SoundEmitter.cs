using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000509 RID: 1289
	[Serializable]
	public class SoundEmitter
	{
		// Token: 0x0600204A RID: 8266 RVA: 0x0018A798 File Offset: 0x00188998
		public AudioSource GetSource()
		{
			for (int i = 0; i < this.sources.Count; i++)
			{
				if (!this.sources[i].isPlaying)
				{
					return this.sources[i];
				}
			}
			return this.sources[0];
		}

		// Token: 0x04003E78 RID: 15992
		public SFXController.Sounds sound;

		// Token: 0x04003E79 RID: 15993
		public bool interupt;

		// Token: 0x04003E7A RID: 15994
		[Reorderable]
		public AudioSources sources;

		// Token: 0x04003E7B RID: 15995
		[Reorderable]
		public AudioClips clips;
	}
}
