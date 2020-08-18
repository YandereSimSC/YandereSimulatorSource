using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class BloodCleanerScript : MonoBehaviour
{
	// Token: 0x06000A4D RID: 2637 RVA: 0x00054A70 File Offset: 0x00052C70
	private void Start()
	{
		Physics.IgnoreLayerCollision(11, 15, true);
		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x00054A94 File Offset: 0x00052C94
	private void Update()
	{
		if (this.Blood < 100f)
		{
			if (this.BloodParent.childCount > 0)
			{
				this.Pathfinding.target = this.BloodParent.GetChild(0);
				this.Pathfinding.speed = 4f;
				if (this.Pathfinding.target.position.y < 4f)
				{
					this.Label.text = "1";
				}
				else if (this.Pathfinding.target.position.y < 8f)
				{
					this.Label.text = "2";
				}
				else if (this.Pathfinding.target.position.y < 12f)
				{
					this.Label.text = "3";
				}
				else
				{
					this.Label.text = "R";
				}
				if (this.Pathfinding.target != null)
				{
					this.Distance = Vector3.Distance(base.transform.position, this.Pathfinding.target.position);
					if (this.Distance >= 1f)
					{
						this.Pathfinding.speed = 4f;
						return;
					}
					this.Pathfinding.speed = 0f;
					Transform child = this.BloodParent.GetChild(0);
					if (!(child.GetComponent("BloodPoolScript") != null))
					{
						UnityEngine.Object.Destroy(child.gameObject);
						return;
					}
					child.localScale = new Vector3(child.localScale.x - Time.deltaTime, child.localScale.y - Time.deltaTime, child.localScale.z);
					this.Blood += Time.deltaTime;
					if (this.Blood >= 100f)
					{
						this.Lens.SetActive(true);
					}
					if (child.transform.localScale.x < 0.1f)
					{
						UnityEngine.Object.Destroy(child.gameObject);
						return;
					}
				}
			}
			else if (this.Super)
			{
				this.Pathfinding.target = this.Prompt.Yandere.transform;
				this.Pathfinding.speed = 4f;
			}
		}
	}

	// Token: 0x04000A8A RID: 2698
	public Transform BloodParent;

	// Token: 0x04000A8B RID: 2699
	public PromptScript Prompt;

	// Token: 0x04000A8C RID: 2700
	public AIPath Pathfinding;

	// Token: 0x04000A8D RID: 2701
	public GameObject Lens;

	// Token: 0x04000A8E RID: 2702
	public UILabel Label;

	// Token: 0x04000A8F RID: 2703
	public float Distance;

	// Token: 0x04000A90 RID: 2704
	public float Blood;

	// Token: 0x04000A91 RID: 2705
	public bool Super;
}
