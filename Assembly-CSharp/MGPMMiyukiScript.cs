using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class MGPMMiyukiScript : MonoBehaviour
{
	// Token: 0x0600008A RID: 138 RVA: 0x000068BB File Offset: 0x00004ABB
	private void Start()
	{
		Time.timeScale = 1f;
		if (!GameGlobals.EasyMode)
		{
			this.MagicBar.parent.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x000068E4 File Offset: 0x00004AE4
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.Timer = 0f;
			this.Frame++;
			if (this.Frame == 3)
			{
				this.Frame = 0;
				if (this.RightPhase == 1)
				{
					this.RightPhase = 2;
				}
				else if (this.RightPhase == 3)
				{
					this.RightPhase = 0;
				}
				if (this.LeftPhase == 1)
				{
					this.LeftPhase = 2;
				}
				else if (this.LeftPhase == 3)
				{
					this.LeftPhase = 0;
				}
			}
			if (this.RightPhase == 0 && this.LeftPhase == 0)
			{
				this.MyRenderer.material.mainTexture = this.ForwardSprite[this.Frame];
			}
			else if (this.RightPhase == 1)
			{
				this.MyRenderer.material.mainTexture = this.TurnRightSprite[this.Frame];
			}
			else if (this.RightPhase == 2)
			{
				this.MyRenderer.material.mainTexture = this.RightSprite[this.Frame];
			}
			else if (this.RightPhase == 3)
			{
				this.MyRenderer.material.mainTexture = this.ReverseRightSprite[this.Frame];
			}
			else if (this.LeftPhase == 1)
			{
				this.MyRenderer.material.mainTexture = this.TurnLeftSprite[this.Frame];
			}
			else if (this.LeftPhase == 2)
			{
				this.MyRenderer.material.mainTexture = this.LeftSprite[this.Frame];
			}
			else if (this.LeftPhase == 3)
			{
				this.MyRenderer.material.mainTexture = this.ReverseLeftSprite[this.Frame];
			}
		}
		float num;
		if (Input.GetButton("LB"))
		{
			num = this.Speed * 0.5f;
		}
		else
		{
			num = this.Speed;
		}
		if (this.Gameplay)
		{
			if (Input.GetKey("right") || this.InputManager.DPadRight || Input.GetAxis("Horizontal") > 0.5f)
			{
				if (this.RightPhase < 1)
				{
					this.RightPhase = 1;
					this.LeftPhase = 0;
					this.Frame = 0;
				}
				this.PositionX += num * Time.deltaTime;
			}
			else if (this.RightPhase == 1 || this.RightPhase == 2)
			{
				this.RightPhase = 3;
				this.Frame = 0;
			}
			if (Input.GetKey("left") || this.InputManager.DPadLeft || Input.GetAxis("Horizontal") < -0.5f)
			{
				if (this.LeftPhase < 1)
				{
					this.RightPhase = 0;
					this.LeftPhase = 1;
					this.Frame = 0;
				}
				this.PositionX -= num * Time.deltaTime;
			}
			else if (this.LeftPhase == 1 || this.LeftPhase == 2)
			{
				this.LeftPhase = 3;
				this.Frame = 0;
			}
			if (Input.GetKey("up") || this.InputManager.DPadUp || Input.GetAxis("Vertical") > 0.5f)
			{
				this.PositionY += num * Time.deltaTime;
			}
			if (Input.GetKey("down") || this.InputManager.DPadDown || Input.GetAxis("Vertical") < -0.5f)
			{
				this.PositionY -= num * Time.deltaTime;
			}
			if (this.PositionX > 108f)
			{
				this.PositionX = 108f;
			}
			if (this.PositionX < -110f)
			{
				this.PositionX = -110f;
			}
			if (this.PositionY > 224f)
			{
				this.PositionY = 224f;
			}
			if (this.PositionY < -224f)
			{
				this.PositionY = -224f;
			}
			base.transform.localPosition = new Vector3(this.PositionX, this.PositionY, 0f);
			if (Input.GetKey("z") || Input.GetKey("y") || Input.GetButton("A"))
			{
				if (this.ShootTimer == 0f)
				{
					if (this.MagicLevel == 0)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
					}
					else if (this.MagicLevel == 1)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.1f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.1f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
					}
					else if (this.MagicLevel == 2)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
					}
					else
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position, Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.2f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(0.4f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject.GetComponent<MGPMProjectileScript>().Angle = 1;
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Projectile, this.SpawnPoint.position + new Vector3(-0.4f, 0f, 0f), Quaternion.identity);
						gameObject.transform.parent = base.transform.parent;
						gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 1f);
						gameObject.transform.localScale = new Vector3(16f, 16f, 1f);
						gameObject.GetComponent<MGPMProjectileScript>().Angle = -1;
					}
					this.ShootTimer = 0f;
				}
				this.ShootTimer += Time.deltaTime;
				if (this.ShootTimer >= 0.075f)
				{
					this.ShootTimer = 0f;
				}
			}
			if (Input.GetKeyUp("z") || Input.GetKeyUp("y") || Input.GetButtonUp("A"))
			{
				this.ShootTimer = 0f;
			}
			if (Input.GetKeyDown("r"))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if (this.Invincibility > 0f)
		{
			this.Invincibility = Mathf.MoveTowards(this.Invincibility, 0f, Time.deltaTime);
			if (this.Invincibility == 0f)
			{
				this.MyRenderer.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 0f));
			}
		}
	}

	// Token: 0x0600008C RID: 140 RVA: 0x000074BC File Offset: 0x000056BC
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 9)
		{
			if (this.Invincibility == 0f)
			{
				this.Health--;
				if (GameGlobals.EasyMode)
				{
					this.MyRenderer.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f, 1f));
					this.Invincibility = 1f;
				}
				if (this.Health > 0)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Explosion, base.transform.position, Quaternion.identity);
					gameObject.transform.parent = base.transform.parent;
					gameObject.transform.localScale = new Vector3(64f, 64f, 1f);
					AudioSource.PlayClipAtPoint(this.DamageSound, base.transform.position);
				}
				else
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.DeathExplosion, base.transform.position, Quaternion.identity);
					gameObject2.transform.parent = base.transform.parent;
					gameObject2.transform.localScale = new Vector3(128f, 128f, 1f);
					AudioSource.PlayClipAtPoint(this.DeathSound, base.transform.position);
					this.GameplayManager.BeginGameOver();
					base.gameObject.SetActive(false);
				}
			}
			this.UpdateHearts();
			return;
		}
		if (collision.gameObject.layer == 15)
		{
			AudioSource.PlayClipAtPoint(this.PickUpSound, base.transform.position);
			this.GameplayManager.Score += 10;
			this.Magic += 1f;
			if (this.Magic == 20f)
			{
				this.MagicLevel++;
				if (this.MagicLevel > 3 && this.Health < 3)
				{
					this.Health++;
					this.UpdateHearts();
				}
				this.Magic = 0f;
			}
			this.MagicBar.localScale = new Vector3(this.Magic / 20f, 1f, 1f);
			UnityEngine.Object.Destroy(collision.gameObject);
		}
	}

	// Token: 0x0600008D RID: 141 RVA: 0x000076F8 File Offset: 0x000058F8
	private void UpdateHearts()
	{
		this.Hearts[1].SetActive(false);
		this.Hearts[2].SetActive(false);
		this.Hearts[3].SetActive(false);
		for (int i = 1; i < this.Health + 1; i++)
		{
			this.Hearts[i].SetActive(true);
		}
	}

	// Token: 0x040000F3 RID: 243
	public MGPMManagerScript GameplayManager;

	// Token: 0x040000F4 RID: 244
	public InputManagerScript InputManager;

	// Token: 0x040000F5 RID: 245
	public AudioClip DamageSound;

	// Token: 0x040000F6 RID: 246
	public AudioClip PickUpSound;

	// Token: 0x040000F7 RID: 247
	public AudioClip DeathSound;

	// Token: 0x040000F8 RID: 248
	public GameObject Projectile;

	// Token: 0x040000F9 RID: 249
	public GameObject DeathExplosion;

	// Token: 0x040000FA RID: 250
	public GameObject Explosion;

	// Token: 0x040000FB RID: 251
	public Transform SpawnPoint;

	// Token: 0x040000FC RID: 252
	public Transform MagicBar;

	// Token: 0x040000FD RID: 253
	public Renderer MyRenderer;

	// Token: 0x040000FE RID: 254
	public Texture[] ForwardSprite;

	// Token: 0x040000FF RID: 255
	public Texture[] ReverseRightSprite;

	// Token: 0x04000100 RID: 256
	public Texture[] TurnRightSprite;

	// Token: 0x04000101 RID: 257
	public Texture[] RightSprite;

	// Token: 0x04000102 RID: 258
	public Texture[] ReverseLeftSprite;

	// Token: 0x04000103 RID: 259
	public Texture[] TurnLeftSprite;

	// Token: 0x04000104 RID: 260
	public Texture[] LeftSprite;

	// Token: 0x04000105 RID: 261
	public GameObject[] Hearts;

	// Token: 0x04000106 RID: 262
	public int MagicLevel;

	// Token: 0x04000107 RID: 263
	public int Frame;

	// Token: 0x04000108 RID: 264
	public int RightPhase;

	// Token: 0x04000109 RID: 265
	public int LeftPhase;

	// Token: 0x0400010A RID: 266
	public int Health;

	// Token: 0x0400010B RID: 267
	public float Invincibility;

	// Token: 0x0400010C RID: 268
	public float ShootTimer;

	// Token: 0x0400010D RID: 269
	public float Magic;

	// Token: 0x0400010E RID: 270
	public float Speed;

	// Token: 0x0400010F RID: 271
	public float Timer;

	// Token: 0x04000110 RID: 272
	public float FPS;

	// Token: 0x04000111 RID: 273
	public float PositionX;

	// Token: 0x04000112 RID: 274
	public float PositionY;

	// Token: 0x04000113 RID: 275
	public bool Gameplay;
}
