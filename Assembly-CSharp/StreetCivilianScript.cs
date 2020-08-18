using System;
using Pathfinding;
using UnityEngine;

// Token: 0x020003F9 RID: 1017
public class StreetCivilianScript : MonoBehaviour
{
	// Token: 0x06001AF1 RID: 6897 RVA: 0x0010F00D File Offset: 0x0010D20D
	private void Start()
	{
		this.Pathfinding.target = this.Destinations[0];
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x0010F024 File Offset: 0x0010D224
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Destinations[this.ID].position) < 0.55f)
		{
			this.MoveTowardsTarget(this.Destinations[this.ID].position);
			this.MyAnimation.CrossFade("f02_idle_00");
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
			this.Timer += Time.deltaTime;
			if (this.Timer > 13.5f)
			{
				this.MyAnimation.CrossFade("f02_newWalk_00");
				this.ID++;
				if (this.ID == this.Destinations.Length)
				{
					this.ID = 0;
				}
				this.Pathfinding.target = this.Destinations[this.ID];
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x0010F12C File Offset: 0x0010D32C
	public void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		if (a.sqrMagnitude > 1E-06f)
		{
			this.MyController.Move(a * (Time.deltaTime * 1f / Time.timeScale));
		}
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Destinations[this.ID].rotation, 10f * Time.deltaTime);
	}

	// Token: 0x04002BB4 RID: 11188
	public CharacterController MyController;

	// Token: 0x04002BB5 RID: 11189
	public Animation MyAnimation;

	// Token: 0x04002BB6 RID: 11190
	public AIPath Pathfinding;

	// Token: 0x04002BB7 RID: 11191
	public Transform[] Destinations;

	// Token: 0x04002BB8 RID: 11192
	public float Timer;

	// Token: 0x04002BB9 RID: 11193
	public int ID;
}
