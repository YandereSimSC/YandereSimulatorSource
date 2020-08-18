using System;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class KittenScript : MonoBehaviour
{
	// Token: 0x060017CF RID: 6095 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Start()
	{
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void PickRandomAnim()
	{
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x000D1304 File Offset: 0x000CF504
	private void LateUpdate()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			if (!this.Yandere.Aiming)
			{
				Vector3 b = (this.Yandere.Head.transform.position.x < base.transform.position.x) ? this.Yandere.Head.transform.position : (base.transform.position + base.transform.forward + base.transform.up * 0.139854f);
				this.Target.position = Vector3.Lerp(this.Target.position, b, Time.deltaTime * 5f);
				this.Head.transform.LookAt(this.Target);
				return;
			}
			this.Head.transform.LookAt(this.Yandere.transform.position + Vector3.up * this.Head.position.y);
		}
	}

	// Token: 0x040021FF RID: 8703
	public YandereScript Yandere;

	// Token: 0x04002200 RID: 8704
	public GameObject Character;

	// Token: 0x04002201 RID: 8705
	public string[] AnimationNames;

	// Token: 0x04002202 RID: 8706
	public Transform Target;

	// Token: 0x04002203 RID: 8707
	public Transform Head;

	// Token: 0x04002204 RID: 8708
	public string CurrentAnim = string.Empty;

	// Token: 0x04002205 RID: 8709
	public string IdleAnim = string.Empty;

	// Token: 0x04002206 RID: 8710
	public bool Wait;

	// Token: 0x04002207 RID: 8711
	public float Timer;
}
