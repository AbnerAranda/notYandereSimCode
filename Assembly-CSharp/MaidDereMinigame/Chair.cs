using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004FA RID: 1274
	public class Chair : MonoBehaviour
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00189640 File Offset: 0x00187840
		public static Chairs AllChairs
		{
			get
			{
				if (Chair.chairs == null || Chair.chairs.Count == 0)
				{
					Chair.chairs = new Chairs();
					foreach (Chair item in UnityEngine.Object.FindObjectsOfType<Chair>())
					{
						Chair.chairs.Add(item);
					}
				}
				return Chair.chairs;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00189694 File Offset: 0x00187894
		public static Chair RandomChair
		{
			get
			{
				Chairs chairs = new Chairs();
				foreach (Chair chair in Chair.AllChairs)
				{
					if (chair.available)
					{
						chairs.Add(chair);
					}
				}
				if (chairs.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, chairs.Count);
					chairs[index].available = false;
					return chairs[index];
				}
				return null;
			}
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0018971C File Offset: 0x0018791C
		private void OnDestroy()
		{
			Chair.chairs = null;
		}

		// Token: 0x04003E31 RID: 15921
		private static Chairs chairs;

		// Token: 0x04003E32 RID: 15922
		public bool available = true;
	}
}
