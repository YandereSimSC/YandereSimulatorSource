using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class NyanDroidScript : MonoBehaviour
{
	// Token: 0x0600187F RID: 6271 RVA: 0x000DFD80 File Offset: 0x000DDF80
	private void Start()
	{
		this.OriginalPosition = base.transform.position;
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x000DFD94 File Offset: 0x000DDF94
	private void Update()
	{
		if (!this.Pathfinding.canSearch)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Stop";
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				return;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.Timer = 0f;
				base.transform.position += new Vector3(0f, 0.0001f, 0f);
				if (base.transform.position.y < 0f)
				{
					base.transform.position = new Vector3(base.transform.position.x, 0.001f, base.transform.position.z);
				}
				Physics.SyncTransforms();
			}
			if (Input.GetButtonDown("RB"))
			{
				base.transform.position = this.OriginalPosition;
			}
			if (Vector3.Distance(base.transform.position, this.Pathfinding.target.position) <= 1f)
			{
				this.Character.CrossFade(this.Prefix + "_Idle");
				this.Pathfinding.speed = 0f;
			}
			else if (Vector3.Distance(base.transform.position, this.Pathfinding.target.position) <= 2f)
			{
				this.Character.CrossFade(this.Prefix + "_Walk");
				this.Pathfinding.speed = 0.5f;
			}
			else
			{
				this.Character.CrossFade(this.Prefix + "_Run");
				this.Pathfinding.speed = 5f;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Label[0].text = "     Follow";
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Character.CrossFade(this.Prefix + "_Idle");
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
			}
		}
	}

	// Token: 0x040023F7 RID: 9207
	public Animation Character;

	// Token: 0x040023F8 RID: 9208
	public PromptScript Prompt;

	// Token: 0x040023F9 RID: 9209
	public AIPath Pathfinding;

	// Token: 0x040023FA RID: 9210
	public Vector3 OriginalPosition;

	// Token: 0x040023FB RID: 9211
	public string Prefix;

	// Token: 0x040023FC RID: 9212
	public float Timer;
}
