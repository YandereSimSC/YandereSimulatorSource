using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020005D2 RID: 1490
	public static class MovementUtilities
	{
		// Token: 0x0600290F RID: 10511 RVA: 0x001BB0F0 File Offset: 0x001B92F0
		public static Vector2 ClampVelocity(Vector2 velocity, float maxSpeed, float slowdownFactor, bool slowWhenNotFacingTarget, Vector2 forward)
		{
			float num = maxSpeed * slowdownFactor;
			if (slowWhenNotFacingTarget && (forward.x != 0f || forward.y != 0f))
			{
				float num2;
				Vector2 vector = VectorMath.Normalize(velocity, out num2);
				float num3 = Vector2.Dot(vector, forward);
				float num4 = Mathf.Clamp(num3 + 0.707f, 0.2f, 1f);
				num *= num4;
				num2 = Mathf.Min(num2, num);
				float f = Mathf.Min(Mathf.Acos(Mathf.Clamp(num3, -1f, 1f)), (20f + 180f * (1f - slowdownFactor * slowdownFactor)) * 0.017453292f);
				float num5 = Mathf.Sin(f);
				float num6 = Mathf.Cos(f);
				num5 *= Mathf.Sign(vector.x * forward.y - vector.y * forward.x);
				return new Vector2(forward.x * num6 + forward.y * num5, forward.y * num6 - forward.x * num5) * num2;
			}
			return Vector2.ClampMagnitude(velocity, num);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x001BB204 File Offset: 0x001B9404
		public static Vector2 CalculateAccelerationToReachPoint(Vector2 deltaPosition, Vector2 targetVelocity, Vector2 currentVelocity, float forwardsAcceleration, float rotationSpeed, float maxSpeed, Vector2 forwardsVector)
		{
			if (forwardsAcceleration <= 0f)
			{
				return Vector2.zero;
			}
			float magnitude = currentVelocity.magnitude;
			float a = magnitude * rotationSpeed * 0.017453292f;
			a = Mathf.Max(a, forwardsAcceleration);
			deltaPosition = VectorMath.ComplexMultiplyConjugate(deltaPosition, forwardsVector);
			targetVelocity = VectorMath.ComplexMultiplyConjugate(targetVelocity, forwardsVector);
			currentVelocity = VectorMath.ComplexMultiplyConjugate(currentVelocity, forwardsVector);
			float num = 1f / (forwardsAcceleration * forwardsAcceleration);
			float num2 = 1f / (forwardsAcceleration * forwardsAcceleration);
			if (targetVelocity == Vector2.zero)
			{
				float num3 = 0.01f;
				float num4 = 10f;
				while (num4 - num3 > 0.01f)
				{
					float num5 = (num4 + num3) * 0.5f;
					Vector2 vector = (6f * deltaPosition - 4f * num5 * currentVelocity) / (num5 * num5);
					Vector2 a2 = 6f * (num5 * currentVelocity - 2f * deltaPosition) / (num5 * num5 * num5);
					Vector2 vector2 = vector + a2 * num5;
					if (vector.x * vector.x * num + vector.y * vector.y * num2 > 1f || vector2.x * vector2.x * num + vector2.y * vector2.y * num2 > 1f)
					{
						num3 = num5;
					}
					else
					{
						num4 = num5;
					}
				}
				Vector2 vector3 = (6f * deltaPosition - 4f * num4 * currentVelocity) / (num4 * num4);
				vector3.y *= 2f;
				float num6 = vector3.x * vector3.x * num + vector3.y * vector3.y * num2;
				if (num6 > 1f)
				{
					vector3 /= Mathf.Sqrt(num6);
				}
				return VectorMath.ComplexMultiply(vector3, forwardsVector);
			}
			float num7;
			Vector2 a3 = VectorMath.Normalize(targetVelocity, out num7);
			float magnitude2 = deltaPosition.magnitude;
			Vector2 vector4 = ((deltaPosition - a3 * Math.Min(0.5f * magnitude2 * num7 / (magnitude + num7), maxSpeed * 1.5f)).normalized * maxSpeed - currentVelocity) * 10f;
			float num8 = vector4.x * vector4.x * num + vector4.y * vector4.y * num2;
			if (num8 > 1f)
			{
				vector4 /= Mathf.Sqrt(num8);
			}
			return VectorMath.ComplexMultiply(vector4, forwardsVector);
		}
	}
}
