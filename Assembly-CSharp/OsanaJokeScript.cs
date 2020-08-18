using System;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class OsanaJokeScript : MonoBehaviour
{
	// Token: 0x06001889 RID: 6281 RVA: 0x000E0940 File Offset: 0x000DEB40
	private void Update()
	{
		if (this.Advance)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 3f)
			{
				this.Label.text = "Congratulations, you eliminated Osana!";
				if (!this.Jukebox.isPlaying)
				{
					this.Jukebox.clip = this.VictoryMusic;
					this.Jukebox.Play();
					return;
				}
			}
			else if (this.Timer > 14f)
			{
				Application.Quit();
				return;
			}
		}
		else if (Input.GetKeyDown("f"))
		{
			this.Rotation[0].enabled = false;
			this.Rotation[1].enabled = false;
			this.Rotation[2].enabled = false;
			this.Rotation[3].enabled = false;
			this.Rotation[4].enabled = false;
			this.Rotation[5].enabled = false;
			this.Rotation[6].enabled = false;
			this.Rotation[7].enabled = false;
			UnityEngine.Object.Instantiate<GameObject>(this.BloodSplatterEffect, this.Head.position, Quaternion.identity);
			this.Head.localScale = new Vector3(0f, 0f, 0f);
			this.Jukebox.clip = this.BloodSplatterSFX;
			this.Jukebox.Play();
			this.Label.text = "";
			this.Advance = true;
		}
	}

	// Token: 0x04002415 RID: 9237
	public ConstantRandomRotation[] Rotation;

	// Token: 0x04002416 RID: 9238
	public GameObject BloodSplatterEffect;

	// Token: 0x04002417 RID: 9239
	public AudioClip BloodSplatterSFX;

	// Token: 0x04002418 RID: 9240
	public AudioClip VictoryMusic;

	// Token: 0x04002419 RID: 9241
	public AudioSource Jukebox;

	// Token: 0x0400241A RID: 9242
	public Transform Head;

	// Token: 0x0400241B RID: 9243
	public UILabel Label;

	// Token: 0x0400241C RID: 9244
	public bool Advance;

	// Token: 0x0400241D RID: 9245
	public float Timer;

	// Token: 0x0400241E RID: 9246
	public int ID;
}
