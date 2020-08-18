using System;
using System.Collections.Generic;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004A0 RID: 1184
	public class ChatPartnerScript : MonoBehaviour
	{
		// Token: 0x06001E2B RID: 7723 RVA: 0x00179088 File Offset: 0x00177288
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

		// Token: 0x06001E2C RID: 7724 RVA: 0x00179188 File Offset: 0x00177388
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

		// Token: 0x04003C02 RID: 15362
		[Header("== Partner Informations ==")]
		public Profile MyProfile;

		// Token: 0x04003C03 RID: 15363
		[Space(20f)]
		public UILabel NameLabel;

		// Token: 0x04003C04 RID: 15364
		public UILabel TagLabel;

		// Token: 0x04003C05 RID: 15365
		public UITexture ProfilPictureTexture;

		// Token: 0x04003C06 RID: 15366
		public UITexture StatusTexture;

		// Token: 0x04003C07 RID: 15367
		[Space(20f)]
		public List<Texture2D> StatusTextures = new List<Texture2D>();
	}
}
