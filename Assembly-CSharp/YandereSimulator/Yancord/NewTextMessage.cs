using System;
using UnityEngine;

namespace YandereSimulator.Yancord
{
	// Token: 0x020004A3 RID: 1187
	[Serializable]
	public class NewTextMessage
	{
		// Token: 0x04003C0C RID: 15372
		public string Message;

		// Token: 0x04003C0D RID: 15373
		public bool isQuestion;

		// Token: 0x04003C0E RID: 15374
		public bool sentByPlayer;

		// Token: 0x04003C0F RID: 15375
		public bool isSystemMessage;

		// Token: 0x04003C10 RID: 15376
		[Header("== Question Related ==")]
		public string OptionQ;

		// Token: 0x04003C11 RID: 15377
		public string OptionR;

		// Token: 0x04003C12 RID: 15378
		public string OptionF;

		// Token: 0x04003C13 RID: 15379
		[Space(20f)]
		public string ReactionQ;

		// Token: 0x04003C14 RID: 15380
		public string ReactionR;

		// Token: 0x04003C15 RID: 15381
		public string ReactionF;
	}
}
