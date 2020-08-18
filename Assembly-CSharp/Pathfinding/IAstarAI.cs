using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000513 RID: 1299
	public interface IAstarAI
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060020F1 RID: 8433
		Vector3 position { get; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060020F2 RID: 8434
		Quaternion rotation { get; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060020F3 RID: 8435
		// (set) Token: 0x060020F4 RID: 8436
		float maxSpeed { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060020F5 RID: 8437
		Vector3 velocity { get; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060020F6 RID: 8438
		Vector3 desiredVelocity { get; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060020F7 RID: 8439
		float remainingDistance { get; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060020F8 RID: 8440
		bool reachedEndOfPath { get; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060020F9 RID: 8441
		// (set) Token: 0x060020FA RID: 8442
		Vector3 destination { get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060020FB RID: 8443
		// (set) Token: 0x060020FC RID: 8444
		bool canSearch { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060020FD RID: 8445
		// (set) Token: 0x060020FE RID: 8446
		bool canMove { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060020FF RID: 8447
		bool hasPath { get; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06002100 RID: 8448
		bool pathPending { get; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002101 RID: 8449
		// (set) Token: 0x06002102 RID: 8450
		bool isStopped { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002103 RID: 8451
		Vector3 steeringTarget { get; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002104 RID: 8452
		// (set) Token: 0x06002105 RID: 8453
		Action onSearchPath { get; set; }

		// Token: 0x06002106 RID: 8454
		void SearchPath();

		// Token: 0x06002107 RID: 8455
		void SetPath(Path path);

		// Token: 0x06002108 RID: 8456
		void Teleport(Vector3 newPosition, bool clearPath = true);

		// Token: 0x06002109 RID: 8457
		void Move(Vector3 deltaPosition);

		// Token: 0x0600210A RID: 8458
		void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation);

		// Token: 0x0600210B RID: 8459
		void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation);
	}
}
