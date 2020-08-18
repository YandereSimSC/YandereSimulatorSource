using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004E5 RID: 1253
	public class CustomerSpawner : MonoBehaviour
	{
		// Token: 0x06001F9F RID: 8095 RVA: 0x001831F4 File Offset: 0x001813F4
		private void Start()
		{
			this.spawnRate = GameController.Instance.activeDifficultyVariables.customerSpawnRate;
			this.spawnVariance = GameController.Instance.activeDifficultyVariables.customerSpawnVariance;
			this.isPaused = true;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00183227 File Offset: 0x00181427
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00183249 File Offset: 0x00181449
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0018326B File Offset: 0x0018146B
		public void Pause(bool toPause)
		{
			this.isPaused = toPause;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00183274 File Offset: 0x00181474
		private void Update()
		{
			if (this.isPaused)
			{
				return;
			}
			if (this.timeTillSpawn <= 0f)
			{
				this.timeTillSpawn = this.spawnRate + UnityEngine.Random.Range(-this.spawnVariance, this.spawnVariance);
				this.SpawnCustomer();
				return;
			}
			this.timeTillSpawn -= Time.deltaTime;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x001832D0 File Offset: 0x001814D0
		private void SpawnCustomer()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.customerPrefabs[UnityEngine.Random.Range(0, this.customerPrefabs.Length)]);
			gameObject.transform.position = base.transform.position;
			AIController component = gameObject.GetComponent<AIController>();
			component.Init();
			component.leaveTarget = base.transform;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00183323 File Offset: 0x00181523
		public void OpenDoor()
		{
			base.transform.parent.GetComponent<Animator>().SetTrigger("DoorOpen");
		}

		// Token: 0x04003D57 RID: 15703
		public GameObject[] customerPrefabs;

		// Token: 0x04003D58 RID: 15704
		private float spawnRate = 10f;

		// Token: 0x04003D59 RID: 15705
		private float spawnVariance = 5f;

		// Token: 0x04003D5A RID: 15706
		private float timeTillSpawn;

		// Token: 0x04003D5B RID: 15707
		private int spawnedCustomers;

		// Token: 0x04003D5C RID: 15708
		private bool isPaused;
	}
}
