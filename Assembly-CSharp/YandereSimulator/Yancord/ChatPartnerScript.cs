using System;
using System.Collections.Generic;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004B0 RID: 1200
	public class ChatPartnerScript : MonoBehaviour
	{
		// Token: 0x06001E6D RID: 7789 RVA: 0x0017E554 File Offset: 0x0017C754
		private void Awake()
		{
			if (this.MyProfile != null)
			{
				if (this.NameLabel != null)
				{
					this.NameLabel.text = this.MyProfile.FirstName + " " + this.MyProfile.LastName;
				}
				if (this.TagLabel != null)
				{
					this.TagLabel.text = this.MyProfile.GetTag(true);
				}
				if (this.ProfilPictureTexture != null)
				{
					this.ProfilPictureTexture.mainTexture = this.MyProfile.ProfilePicture;
				}
				if (this.StatusTexture != null)
				{
					this.StatusTexture.mainTexture = this.GetStatusTexture(this.MyProfile.CurrentStatus);
				}
				base.gameObject.name = this.MyProfile.FirstName + "_Profile";
				return;
			}
			Debug.LogError("[ChatPartnerScript] MyProfile wasn't assgined!");
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0017E654 File Offset: 0x0017C854
		private Texture2D GetStatusTexture(Status currentStatus)
		{
			switch (currentStatus)
			{
			case Status.Online:
				return this.StatusTextures[1];
			case Status.Idle:
				return this.StatusTextures[2];
			case Status.DontDisturb:
				return this.StatusTextures[3];
			case Status.Invisible:
				return this.StatusTextures[4];
			default:
				return null;
			}
		}

		// Token: 0x04003CB2 RID: 15538
		[Header("== Partner Informations ==")]
		public Profile MyProfile;

		// Token: 0x04003CB3 RID: 15539
		[Space(20f)]
		public UILabel NameLabel;

		// Token: 0x04003CB4 RID: 15540
		public UILabel TagLabel;

		// Token: 0x04003CB5 RID: 15541
		public UITexture ProfilPictureTexture;

		// Token: 0x04003CB6 RID: 15542
		public UITexture StatusTexture;

		// Token: 0x04003CB7 RID: 15543
		[Space(20f)]
		public List<Texture2D> StatusTextures = new List<Texture2D>();
	}
}
