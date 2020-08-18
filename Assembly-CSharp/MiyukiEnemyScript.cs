using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class MiyukiEnemyScript : MonoBehaviour
{
	// Token: 0x06001832 RID: 6194 RVA: 0x000D8436 File Offset: 0x000D6636
	private void Start()
	{
		base.transform.position = this.SpawnPoints[this.ID].position;
		base.transform.rotation = this.SpawnPoints[this.ID].rotation;
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x000D8474 File Offset: 0x000D6674
	private void Update()
	{
		if (this.Enemy.activeInHierarchy)
		{
			if (!this.Down)
			{
				this.Float += Time.deltaTime * this.Speed;
				if (this.Float > this.Limit)
				{
					this.Down = true;
				}
			}
			else
			{
				this.Float -= Time.deltaTime * this.Speed;
				if (this.Float < -1f * this.Limit)
				{
					this.Down = false;
				}
			}
			this.Enemy.transform.position += new Vector3(0f, this.Float * Time.deltaTime, 0f);
			if (this.Enemy.transform.position.y > this.SpawnPoints[this.ID].position.y + 1.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 1.5f, this.Enemy.transform.position.z);
			}
			if (this.Enemy.transform.position.y < this.SpawnPoints[this.ID].position.y + 0.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 0.5f, this.Enemy.transform.position.z);
				return;
			}
		}
		else
		{
			this.RespawnTimer += Time.deltaTime;
			if (this.RespawnTimer > 5f)
			{
				base.transform.position = this.SpawnPoints[this.ID].position;
				base.transform.rotation = this.SpawnPoints[this.ID].rotation;
				this.Enemy.SetActive(true);
				this.RespawnTimer = 0f;
			}
		}
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x000D86C8 File Offset: 0x000D68C8
	private void OnTriggerEnter(Collider other)
	{
		if (this.Enemy.activeInHierarchy && other.gameObject.tag == "missile")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(other.gameObject);
			this.Health -= 1f;
			if (this.Health == 0f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathEffect, other.transform.position, Quaternion.identity);
				this.Enemy.SetActive(false);
				this.Health = 50f;
				this.ID++;
				if (this.ID >= this.SpawnPoints.Length)
				{
					this.ID = 0;
				}
			}
		}
	}

	// Token: 0x0400230D RID: 8973
	public float Float;

	// Token: 0x0400230E RID: 8974
	public float Limit;

	// Token: 0x0400230F RID: 8975
	public float Speed;

	// Token: 0x04002310 RID: 8976
	public bool Dead;

	// Token: 0x04002311 RID: 8977
	public bool Down;

	// Token: 0x04002312 RID: 8978
	public GameObject DeathEffect;

	// Token: 0x04002313 RID: 8979
	public GameObject HitEffect;

	// Token: 0x04002314 RID: 8980
	public GameObject Enemy;

	// Token: 0x04002315 RID: 8981
	public Transform[] SpawnPoints;

	// Token: 0x04002316 RID: 8982
	public float RespawnTimer;

	// Token: 0x04002317 RID: 8983
	public float Health;

	// Token: 0x04002318 RID: 8984
	public int ID;
}
