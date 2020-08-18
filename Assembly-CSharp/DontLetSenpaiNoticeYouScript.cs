using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class DontLetSenpaiNoticeYouScript : MonoBehaviour
{
	// Token: 0x06000A08 RID: 2568 RVA: 0x0004F6C4 File Offset: 0x0004D8C4
	private void Start()
	{
		while (this.ID < this.Letters.Length)
		{
			UILabel uilabel = this.Letters[this.ID];
			uilabel.transform.localScale = new Vector3(10f, 10f, 1f);
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0f);
			this.Origins[this.ID] = uilabel.transform.localPosition;
			this.ID++;
		}
		this.ID = 0;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0004F778 File Offset: 0x0004D978
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.Proceed = true;
		}
		if (this.Proceed)
		{
			if (this.ID < this.Letters.Length)
			{
				UILabel uilabel = this.Letters[this.ID];
				uilabel.transform.localScale = Vector3.MoveTowards(uilabel.transform.localScale, Vector3.one, Time.deltaTime * 100f);
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, uilabel.color.a + Time.deltaTime * 10f);
				if (uilabel.transform.localScale == Vector3.one)
				{
					base.GetComponent<AudioSource>().PlayOneShot(this.Slam);
					this.ID++;
				}
			}
			this.ShakeID = 0;
			while (this.ShakeID < this.Letters.Length)
			{
				UILabel uilabel2 = this.Letters[this.ShakeID];
				Vector3 vector = this.Origins[this.ShakeID];
				uilabel2.transform.localPosition = new Vector3(vector.x + UnityEngine.Random.Range(-5f, 5f), vector.y + UnityEngine.Random.Range(-5f, 5f), uilabel2.transform.localPosition.z);
				this.ShakeID++;
			}
		}
	}

	// Token: 0x040009EA RID: 2538
	public UILabel[] Letters;

	// Token: 0x040009EB RID: 2539
	public Vector3[] Origins;

	// Token: 0x040009EC RID: 2540
	public AudioClip Slam;

	// Token: 0x040009ED RID: 2541
	public bool Proceed;

	// Token: 0x040009EE RID: 2542
	public int ShakeID;

	// Token: 0x040009EF RID: 2543
	public int ID;
}
