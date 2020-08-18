using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020005C1 RID: 1473
	public interface IAgent
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600282E RID: 10286
		// (set) Token: 0x0600282F RID: 10287
		Vector2 Position { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002830 RID: 10288
		// (set) Token: 0x06002831 RID: 10289
		float ElevationCoordinate { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002832 RID: 10290
		Vector2 CalculatedTargetPoint { get; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002833 RID: 10291
		float CalculatedSpeed { get; }

		// Token: 0x06002834 RID: 10292
		void SetTarget(Vector2 targetPoint, float desiredSpeed, float maxSpeed);

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002835 RID: 10293
		// (set) Token: 0x06002836 RID: 10294
		bool Locked { get; set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002837 RID: 10295
		// (set) Token: 0x06002838 RID: 10296
		float Radius { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002839 RID: 10297
		// (set) Token: 0x0600283A RID: 10298
		float Height { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600283B RID: 10299
		// (set) Token: 0x0600283C RID: 10300
		float AgentTimeHorizon { get; set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600283D RID: 10301
		// (set) Token: 0x0600283E RID: 10302
		float ObstacleTimeHorizon { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600283F RID: 10303
		// (set) Token: 0x06002840 RID: 10304
		int MaxNeighbours { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002841 RID: 10305
		int NeighbourCount { get; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002842 RID: 10306
		// (set) Token: 0x06002843 RID: 10307
		RVOLayer Layer { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002844 RID: 10308
		// (set) Token: 0x06002845 RID: 10309
		RVOLayer CollidesWith { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002846 RID: 10310
		// (set) Token: 0x06002847 RID: 10311
		bool DebugDraw { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002848 RID: 10312
		[Obsolete]
		List<ObstacleVertex> NeighbourObstacles { get; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002849 RID: 10313
		// (set) Token: 0x0600284A RID: 10314
		float Priority { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (set) Token: 0x0600284B RID: 10315
		Action PreCalculationCallback { set; }

		// Token: 0x0600284C RID: 10316
		void SetCollisionNormal(Vector2 normal);

		// Token: 0x0600284D RID: 10317
		void ForceSetVelocity(Vector2 velocity);
	}
}
