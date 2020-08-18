using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000DF RID: 223
public class BloodPoolSpawnerScript : MonoBehaviour
{
	// Token: 0x06000A54 RID: 2644 RVA: 0x00054EA4 File Offset: 0x000530A4
	public void Start()
	{
		if (SceneManager.GetActiveScene().name == "SchoolScene")
		{
			this.GardenArea = GameObject.Find("GardenArea").GetComponent<Collider>();
			this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
			this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
			this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
			this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
		}
		this.BloodParent = GameObject.Find("BloodParent").transform;
		this.Positions = new Vector3[5];
		this.Positions[0] = Vector3.zero;
		this.Positions[1] = new Vector3(0.5f, 0.012f, 0f);
		this.Positions[2] = new Vector3(-0.5f, 0.012f, 0f);
		this.Positions[3] = new Vector3(0f, 0.012f, 0.5f);
		this.Positions[4] = new Vector3(0f, 0.012f, -0.5f);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00054FE5 File Offset: 0x000531E5
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.LastBloodPool = other.gameObject;
			this.NearbyBlood++;
		}
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00055018 File Offset: 0x00053218
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "BloodPool(Clone)")
		{
			this.NearbyBlood--;
		}
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00055040 File Offset: 0x00053240
	private void Update()
	{
		if (!this.Falling)
		{
			if (this.MyCollider.enabled)
			{
				if (this.Timer > 0f)
				{
					this.Timer -= Time.deltaTime;
				}
				this.SetHeight();
				Vector3 position = base.transform.position;
				if (SceneManager.GetActiveScene().name == "SchoolScene")
				{
					this.CanSpawn = (!this.GardenArea.bounds.Contains(position) && !this.NEStairs.bounds.Contains(position) && !this.NWStairs.bounds.Contains(position) && !this.SEStairs.bounds.Contains(position) && !this.SWStairs.bounds.Contains(position));
				}
				if (this.CanSpawn && position.y < this.Height + 0.33333334f)
				{
					if (this.NearbyBlood > 0 && this.LastBloodPool == null)
					{
						this.NearbyBlood--;
					}
					if (this.NearbyBlood < 1 && this.Timer <= 0f)
					{
						this.Timer = 0.1f;
						if (this.PoolsSpawned < 10)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, new Vector3(position.x, this.Height + 0.012f, position.z), Quaternion.identity);
							gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
							gameObject.transform.parent = this.BloodParent;
							this.PoolsSpawned++;
							return;
						}
						if (this.PoolsSpawned < 20)
						{
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, new Vector3(position.x, this.Height + 0.012f, position.z), Quaternion.identity);
							gameObject2.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
							gameObject2.transform.parent = this.BloodParent;
							this.PoolsSpawned++;
							gameObject2.GetComponent<BloodPoolScript>().TargetSize = 1f - (float)(this.PoolsSpawned - 10) * 0.1f;
							if (this.PoolsSpawned == 20)
							{
								base.gameObject.SetActive(false);
								return;
							}
						}
					}
				}
			}
		}
		else
		{
			this.FallTimer += Time.deltaTime;
			if (this.FallTimer > 10f)
			{
				this.Falling = false;
			}
		}
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x000552F8 File Offset: 0x000534F8
	public void SpawnBigPool()
	{
		this.SetHeight();
		Vector3 a = new Vector3(this.Hips.position.x, this.Height + 0.012f, this.Hips.position.z);
		for (int i = 0; i < 5; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, a + this.Positions[i], Quaternion.identity);
			gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
			gameObject.transform.parent = this.BloodParent;
		}
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x000553A8 File Offset: 0x000535A8
	private void SpawnRow(Transform Location)
	{
		Vector3 position = Location.position;
		Vector3 forward = Location.forward;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 2f, Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 2.5f, Quaternion.identity);
		gameObject2.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject2.transform.parent = this.BloodParent;
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, position + forward * 3f, Quaternion.identity);
		gameObject3.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject3.transform.parent = this.BloodParent;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x000554D4 File Offset: 0x000536D4
	public void SpawnPool(Transform Location)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BloodPool, Location.position + Location.forward + new Vector3(0f, 0.0001f, 0f), Quaternion.identity);
		gameObject.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
		gameObject.transform.parent = this.BloodParent;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00055554 File Offset: 0x00053754
	private void SetHeight()
	{
		float y = base.transform.position.y;
		if (y < 4f)
		{
			this.Height = 0f;
			return;
		}
		if (y < 8f)
		{
			this.Height = 4f;
			return;
		}
		if (y < 12f)
		{
			this.Height = 8f;
			return;
		}
		this.Height = 12f;
	}

	// Token: 0x04000A97 RID: 2711
	public RagdollScript Ragdoll;

	// Token: 0x04000A98 RID: 2712
	public GameObject LastBloodPool;

	// Token: 0x04000A99 RID: 2713
	public GameObject BloodPool;

	// Token: 0x04000A9A RID: 2714
	public Transform BloodParent;

	// Token: 0x04000A9B RID: 2715
	public Transform Hips;

	// Token: 0x04000A9C RID: 2716
	public Collider MyCollider;

	// Token: 0x04000A9D RID: 2717
	public Collider GardenArea;

	// Token: 0x04000A9E RID: 2718
	public Collider NEStairs;

	// Token: 0x04000A9F RID: 2719
	public Collider NWStairs;

	// Token: 0x04000AA0 RID: 2720
	public Collider SEStairs;

	// Token: 0x04000AA1 RID: 2721
	public Collider SWStairs;

	// Token: 0x04000AA2 RID: 2722
	public Vector3[] Positions;

	// Token: 0x04000AA3 RID: 2723
	public bool CanSpawn;

	// Token: 0x04000AA4 RID: 2724
	public bool Falling;

	// Token: 0x04000AA5 RID: 2725
	public int PoolsSpawned;

	// Token: 0x04000AA6 RID: 2726
	public int NearbyBlood;

	// Token: 0x04000AA7 RID: 2727
	public float FallTimer;

	// Token: 0x04000AA8 RID: 2728
	public float Height;

	// Token: 0x04000AA9 RID: 2729
	public float Timer;

	// Token: 0x04000AAA RID: 2730
	public LayerMask Mask;
}
