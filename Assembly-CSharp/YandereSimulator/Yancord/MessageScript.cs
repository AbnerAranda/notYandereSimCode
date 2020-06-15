using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004B2 RID: 1202
	public class MessageScript : MonoBehaviour
	{
		// Token: 0x06001E72 RID: 7794 RVA: 0x0017E6D0 File Offset: 0x0017C8D0
		public void Awake()
		{
			if (this.MyProfile != null)
			{
				if (this.NameLabel != null)
				{
					this.NameLabel.text = this.MyProfile.FirstName + " " + this.MyProfile.LastName;
				}
				if (this.ProfilPictureTexture != null)
				{
					this.ProfilPictureTexture.mainTexture = this.MyProfile.ProfilePicture;
				}
				base.gameObject.name = this.MyProfile.FirstName + "_Message";
			}
		}

		// Token: 0x04003CB8 RID: 15544
		[Header("== Partner Informations ==")]
		public Profile MyProfile;

		// Token: 0x04003CB9 RID: 15545
		[Space(20f)]
		public UILabel NameLabel;

		// Token: 0x04003CBA RID: 15546
		public UILabel MessageLabel;

		// Token: 0x04003CBB RID: 15547
		public UITexture ProfilPictureTexture;
	}
}
