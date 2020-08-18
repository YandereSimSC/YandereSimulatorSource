using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F9 RID: 1273
	[Serializable]
	public class SoundEmitter
	{
		// Token: 0x06002008 RID: 8200 RVA: 0x001852A8 File Offset: 0x001834A8
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

		// Token: 0x04003DC8 RID: 15816
		public SFXController.Sounds sound;

		// Token: 0x04003DC9 RID: 15817
		public bool interupt;

		// Token: 0x04003DCA RID: 15818
		[Reorderable]
		public AudioSources sources;

		// Token: 0x04003DCB RID: 15819
		[Reorderable]
		public AudioClips clips;
	}
}
