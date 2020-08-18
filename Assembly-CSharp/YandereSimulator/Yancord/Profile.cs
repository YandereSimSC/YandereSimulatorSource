using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004A4 RID: 1188
	[CreateAssetMenu(fileName = "ChatProfile", menuName = "Yancord/Profile", order = 1)]
	public class Profile : ScriptableObject
	{
		// Token: 0x06001E33 RID: 7731 RVA: 0x0017929C File Offset: 0x0017749C
		public string GetTag(bool WithHashtag)
		{
			string text = this.Tag;
			if (text.Length > 4)
			{
				text = text.Substring(0, 4);
			}
			return WithHashtag ? ("#" + text) : text;
		}

		// Token: 0x04003C16 RID: 15382
		[Header("Personal Information")]
		public string FirstName;

		// Token: 0x04003C17 RID: 15383
		public string LastName;

		// Token: 0x04003C18 RID: 15384
		[Space(20f)]
		[Header("Profile Information")]
		public Texture2D ProfilePicture;

		// Token: 0x04003C19 RID: 15385
		public string Tag = "XXXX";

		// Token: 0x04003C1A RID: 15386
		[Space(20f)]
		[Header("Profile Settings")]
		public Status CurrentStatus;
	}
}
