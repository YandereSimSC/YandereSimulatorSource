using System;
using UnityEngine;

// Token: 0x020003E0 RID: 992
public class SlowMoScript : MonoBehaviour
{
	// Token: 0x06001A9F RID: 6815 RVA: 0x001098AC File Offset: 0x00107AAC
	private void Update()
	{
		if (Input.GetKeyDown("s"))
		{
			this.Spinning = !this.Spinning;
		}
		if (Input.GetKeyDown("a"))
		{
			Time.timeScale = 0.1f;
		}
		if (Input.GetKeyDown("-"))
		{
			Time.timeScale -= 1f;
		}
		if (Input.GetKeyDown("="))
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown("z"))
		{
			this.Speed += Time.deltaTime;
		}
		if (this.Speed > 0f)
		{
			base.transform.position += new Vector3(Time.deltaTime * 0.1f, 0f, Time.deltaTime * 0.1f);
		}
		if (this.Spinning)
		{
			base.transform.parent.transform.localEulerAngles += new Vector3(0f, Time.deltaTime * 36f, 0f);
		}
	}

	// Token: 0x04002ACA RID: 10954
	public bool Spinning;

	// Token: 0x04002ACB RID: 10955
	public float Speed;
}
