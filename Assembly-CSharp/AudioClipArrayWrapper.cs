using System;
using UnityEngine;

// Token: 0x02000438 RID: 1080
[Serializable]
public class AudioClipArrayWrapper : ArrayWrapper<AudioClip>
{
	// Token: 0x06001C88 RID: 7304 RVA: 0x00155017 File Offset: 0x00153217
	public AudioClipArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x06001C89 RID: 7305 RVA: 0x00155020 File Offset: 0x00153220
	public AudioClipArrayWrapper(AudioClip[] elements) : base(elements)
	{
	}
}
