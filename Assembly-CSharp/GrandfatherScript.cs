using System;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class GrandfatherScript : MonoBehaviour
{
	// Token: 0x060016CD RID: 5837 RVA: 0x000BCAAC File Offset: 0x000BACAC
	private void Update()
	{
		if (!this.Flip)
		{
			if ((double)this.Force < 0.1)
			{
				this.Force += Time.deltaTime * 0.1f * this.Speed;
			}
		}
		else if ((double)this.Force > -0.1)
		{
			this.Force -= Time.deltaTime * 0.1f * this.Speed;
		}
		this.Rotation += this.Force;
		if (this.Rotation > 1f)
		{
			this.Flip = true;
		}
		else if (this.Rotation < -1f)
		{
			this.Flip = false;
		}
		if (this.Rotation > 5f)
		{
			this.Rotation = 5f;
		}
		else if (this.Rotation < -5f)
		{
			this.Rotation = -5f;
		}
		this.Pendulum.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		this.MinuteHand.localEulerAngles = new Vector3(this.MinuteHand.localEulerAngles.x, this.MinuteHand.localEulerAngles.y, this.Clock.Minute * 6f);
		this.HourHand.localEulerAngles = new Vector3(this.HourHand.localEulerAngles.x, this.HourHand.localEulerAngles.y, this.Clock.Hour * 30f);
	}

	// Token: 0x04001E0C RID: 7692
	public ClockScript Clock;

	// Token: 0x04001E0D RID: 7693
	public Transform MinuteHand;

	// Token: 0x04001E0E RID: 7694
	public Transform HourHand;

	// Token: 0x04001E0F RID: 7695
	public Transform Pendulum;

	// Token: 0x04001E10 RID: 7696
	public float Rotation;

	// Token: 0x04001E11 RID: 7697
	public float Force;

	// Token: 0x04001E12 RID: 7698
	public float Speed;

	// Token: 0x04001E13 RID: 7699
	public bool Flip;
}
