using System;
using UnityEngine;

// Token: 0x0200048B RID: 1163
public class YanvaniaZombieSpawnerScript : MonoBehaviour
{
	// Token: 0x06001DEE RID: 7662 RVA: 0x00176B94 File Offset: 0x00174D94
	private void Update()
	{
		if (this.Yanmont.transform.position.y > 0f)
		{
			this.ID = 0;
			this.SpawnTimer += Time.deltaTime;
			if (this.SpawnTimer > 1f)
			{
				while (this.ID < 4)
				{
					if (this.Zombies[this.ID] == null)
					{
						this.SpawnSide = UnityEngine.Random.Range(1, 3);
						if (this.Yanmont.transform.position.x < this.LeftBoundary + 5f)
						{
							this.SpawnSide = 2;
						}
						if (this.Yanmont.transform.position.x > this.RightBoundary - 5f)
						{
							this.SpawnSide = 1;
						}
						if (this.Yanmont.transform.position.x < this.LeftBoundary)
						{
							this.RelativePoint = this.LeftBoundary;
						}
						else if (this.Yanmont.transform.position.x > this.RightBoundary)
						{
							this.RelativePoint = this.RightBoundary;
						}
						else
						{
							this.RelativePoint = this.Yanmont.transform.position.x;
						}
						if (this.SpawnSide == 1)
						{
							this.SpawnPoints[0].x = this.RelativePoint - 2.5f;
							this.SpawnPoints[1].x = this.RelativePoint - 3.5f;
							this.SpawnPoints[2].x = this.RelativePoint - 4.5f;
							this.SpawnPoints[3].x = this.RelativePoint - 5.5f;
						}
						else
						{
							this.SpawnPoints[0].x = this.RelativePoint + 2.5f;
							this.SpawnPoints[1].x = this.RelativePoint + 3.5f;
							this.SpawnPoints[2].x = this.RelativePoint + 4.5f;
							this.SpawnPoints[3].x = this.RelativePoint + 5.5f;
						}
						this.Zombies[this.ID] = UnityEngine.Object.Instantiate<GameObject>(this.Zombie, this.SpawnPoints[this.ID], Quaternion.identity);
						this.NewZombieScript = this.Zombies[this.ID].GetComponent<YanvaniaZombieScript>();
						this.NewZombieScript.LeftBoundary = this.LeftBoundary;
						this.NewZombieScript.RightBoundary = this.RightBoundary;
						this.NewZombieScript.Yanmont = this.Yanmont;
						break;
					}
					this.ID++;
				}
				this.SpawnTimer = 0f;
			}
		}
	}

	// Token: 0x04003BA0 RID: 15264
	public YanvaniaZombieScript NewZombieScript;

	// Token: 0x04003BA1 RID: 15265
	public GameObject Zombie;

	// Token: 0x04003BA2 RID: 15266
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x04003BA3 RID: 15267
	public float SpawnTimer;

	// Token: 0x04003BA4 RID: 15268
	public float RelativePoint;

	// Token: 0x04003BA5 RID: 15269
	public float RightBoundary;

	// Token: 0x04003BA6 RID: 15270
	public float LeftBoundary;

	// Token: 0x04003BA7 RID: 15271
	public int SpawnSide;

	// Token: 0x04003BA8 RID: 15272
	public int ID;

	// Token: 0x04003BA9 RID: 15273
	public GameObject[] Zombies;

	// Token: 0x04003BAA RID: 15274
	public Vector3[] SpawnPoints;
}
