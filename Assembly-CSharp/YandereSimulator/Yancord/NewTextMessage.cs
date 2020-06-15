using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004B3 RID: 1203
	[Serializable]
	public class NewTextMessage
	{
		// Token: 0x04003CBC RID: 15548
		public string Message;

		// Token: 0x04003CBD RID: 15549
		public bool isQuestion;

		// Token: 0x04003CBE RID: 15550
		public bool sentByPlayer;

		// Token: 0x04003CBF RID: 15551
		public bool isSystemMessage;

		// Token: 0x04003CC0 RID: 15552
		[Header("== Question Related ==")]
		public string OptionQ;

		// Token: 0x04003CC1 RID: 15553
		public string OptionR;

		// Token: 0x04003CC2 RID: 15554
		public string OptionF;

		// Token: 0x04003CC3 RID: 15555
		[Space(20f)]
		public string ReactionQ;

		// Token: 0x04003CC4 RID: 15556
		public string ReactionR;

		// Token: 0x04003CC5 RID: 15557
		public string ReactionF;
	}
}
