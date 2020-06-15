using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004B4 RID: 1204
	[CreateAssetMenu(fileName = "ChatProfile", menuName = "Yancord/Profile", order = 1)]
	public class Profile : ScriptableObject
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x0017E768 File Offset: 0x0017C968
		public string GetTag(bool WithHashtag)
		{
			string text = this.Tag;
			if (text.Length > 4)
			{
				text = text.Substring(0, 4);
			}
			return WithHashtag ? ("#" + text) : text;
		}

		// Token: 0x04003CC6 RID: 15558
		[Header("Personal Information")]
		public string FirstName;

		// Token: 0x04003CC7 RID: 15559
		public string LastName;

		// Token: 0x04003CC8 RID: 15560
		[Space(20f)]
		[Header("Profile Information")]
		public Texture2D ProfilePicture;

		// Token: 0x04003CC9 RID: 15561
		public string Tag = "XXXX";

		// Token: 0x04003CCA RID: 15562
		[Space(20f)]
		[Header("Profile Settings")]
		public Status CurrentStatus;
	}
}
