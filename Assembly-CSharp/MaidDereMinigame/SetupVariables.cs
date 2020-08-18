using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x020004F5 RID: 1269
	[Serializable]
	public class SetupVariables
	{
		// Token: 0x04003DB2 RID: 15794
		[Tooltip("Transition time for white fade between scenes")]
		public float transitionTime = 1f;

		// Token: 0x04003DB3 RID: 15795
		[Tooltip("Base rate at which customers spawn.")]
		public float customerSpawnRate = 5f;

		// Token: 0x04003DB4 RID: 15796
		[Tooltip("Amount of variance on the customer spawn rate. A random value between this and negative this is added to the base spawn rate.")]
		public float customerSpawnVariance = 5f;

		// Token: 0x04003DB5 RID: 15797
		[Tooltip("Speed the player can move.")]
		public float playerMoveSpeed = 2f;

		// Token: 0x04003DB6 RID: 15798
		[Tooltip("Speed the customers can move.")]
		public float customerMoveSpeed = 2f;

		// Token: 0x04003DB7 RID: 15799
		[Tooltip("Patience degradation multiplier. Patience degrades at 1 point per second times this value.")]
		public float customerPatienceDegradation = 5f;

		// Token: 0x04003DB8 RID: 15800
		[Tooltip("Time the customer will show their order. Patience resets after this amount of time.")]
		public float timeSpentOrdering = 2f;

		// Token: 0x04003DB9 RID: 15801
		[Tooltip("Time the customer will stay in their chair eating after receiving their food.")]
		public float timeSpentEatingFood = 5f;

		// Token: 0x04003DBA RID: 15802
		[Tooltip("Base cooking time for food. This value is multiplied by the dish's individual cooking time multiplier. By default, they are all set to 1.")]
		public float baseCookingTime = 3f;

		// Token: 0x04003DBB RID: 15803
		[Tooltip("Base pay for the minigame.")]
		public float basePay = 100f;

		// Token: 0x04003DBC RID: 15804
		[Tooltip("Base tip multiplier. Tip is customer's remaining patience multiplied by this value.")]
		public float baseTip = 0.1f;

		// Token: 0x04003DBD RID: 15805
		[Tooltip("Time in seconds for the game to last.")]
		public float gameTime = 60f;

		// Token: 0x04003DBE RID: 15806
		[Tooltip("Game will fail if this many customers leave without being served.")]
		public int failQuantity = 5;
	}
}
