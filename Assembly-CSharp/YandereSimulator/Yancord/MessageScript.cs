using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004A2 RID: 1186
	public class MessageScript : MonoBehaviour
	{
		// Token: 0x06001E30 RID: 7728 RVA: 0x00179204 File Offset: 0x00177404
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

		// Token: 0x04003C08 RID: 15368
		[Header("== Partner Informations ==")]
		public Profile MyProfile;

		// Token: 0x04003C09 RID: 15369
		[Space(20f)]
		public UILabel NameLabel;

		// Token: 0x04003C0A RID: 15370
		public UILabel MessageLabel;

		// Token: 0x04003C0B RID: 15371
		public UITexture ProfilPictureTexture;
	}
}
