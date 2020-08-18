using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002FB RID: 763
public class HomeYandereScript : MonoBehaviour
{
	// Token: 0x06001756 RID: 5974 RVA: 0x000C8CA0 File Offset: 0x000C6EA0
	public void Start()
	{
		if (this.CutsceneYandere != null)
		{
			this.CutsceneYandere.GetComponent<Animation>()["f02_midoriTexting_00"].speed = 0.1f;
		}
		if (SceneManager.GetActiveScene().name == "HomeScene")
		{
			if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
			{
				base.transform.position = Vector3.zero;
				base.transform.eulerAngles = Vector3.zero;
				if (!HomeGlobals.Night)
				{
					this.ChangeSchoolwear();
					base.StartCoroutine(this.ApplyCustomCostume());
				}
				else
				{
					this.WearPajamas();
				}
				if (DateGlobals.Weekday == DayOfWeek.Sunday)
				{
					this.Nude();
				}
			}
			else if (HomeGlobals.StartInBasement)
			{
				HomeGlobals.StartInBasement = false;
				base.transform.position = new Vector3(0f, -135f, 0f);
				base.transform.eulerAngles = Vector3.zero;
			}
			else if (HomeGlobals.MiyukiDefeated)
			{
				base.transform.position = new Vector3(1f, 0f, 0f);
				base.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.Character.GetComponent<Animation>().Play("f02_discScratch_00");
				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0f, 0f, -180f);
				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];
				this.Disc.SetActive(true);
				this.WearPajamas();
				this.MyAudio.clip = this.MiyukiReaction;
			}
			else
			{
				base.transform.position = new Vector3(1f, 0f, 0f);
				base.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.Character.GetComponent<Animation>().Play("f02_discScratch_00");
				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0f, 0f, -180f);
				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];
				this.Disc.SetActive(true);
				this.WearPajamas();
			}
			if (GameGlobals.BlondeHair)
			{
				this.PonytailRenderer.material.mainTexture = this.BlondePony;
			}
		}
		Time.timeScale = 1f;
		this.UpdateHair();
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000C8FA8 File Offset: 0x000C71A8
	private void Update()
	{
		if (!this.Disc.activeInHierarchy)
		{
			Animation component = this.Character.GetComponent<Animation>();
			if (this.CanMove)
			{
				if (!OptionGlobals.ToggleRun)
				{
					this.Running = false;
					if (Input.GetButton("LB"))
					{
						this.Running = true;
					}
				}
				else if (Input.GetButtonDown("LB"))
				{
					this.Running = !this.Running;
				}
				this.MyController.Move(Physics.gravity * 0.01f);
				float axis = Input.GetAxis("Vertical");
				float axis2 = Input.GetAxis("Horizontal");
				Vector3 vector = Camera.main.transform.TransformDirection(Vector3.forward);
				vector.y = 0f;
				vector = vector.normalized;
				Vector3 a = new Vector3(vector.z, 0f, -vector.x);
				Vector3 vector2 = axis2 * a + axis * vector;
				if (vector2 != Vector3.zero)
				{
					Quaternion b = Quaternion.LookRotation(vector2);
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 10f);
				}
				if (axis != 0f || axis2 != 0f)
				{
					if (this.Running)
					{
						component.CrossFade("f02_run_00");
						this.MyController.Move(base.transform.forward * this.RunSpeed * Time.deltaTime);
					}
					else
					{
						component.CrossFade("f02_newWalk_00");
						this.MyController.Move(base.transform.forward * this.WalkSpeed * Time.deltaTime);
					}
				}
				else
				{
					component.CrossFade("f02_idleShort_00");
				}
			}
			else
			{
				component.CrossFade("f02_idleShort_00");
			}
		}
		else if (this.HomeDarkness.color.a == 0f)
		{
			if (this.Timer == 0f)
			{
				this.MyAudio.Play();
			}
			else if (this.Timer > this.MyAudio.clip.length + 1f)
			{
				YanvaniaGlobals.DraculaDefeated = false;
				HomeGlobals.MiyukiDefeated = false;
				this.Disc.SetActive(false);
				this.HomeVideoGames.Quit();
			}
			this.Timer += Time.deltaTime;
		}
		Rigidbody component2 = base.GetComponent<Rigidbody>();
		if (component2 != null)
		{
			component2.velocity = Vector3.zero;
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			this.UpdateHair();
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			SchoolGlobals.KidnapVictim = this.VictimID;
			StudentGlobals.SetStudentSanity(this.VictimID, 100f);
			SchemeGlobals.SetSchemeStage(6, 5);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.F1))
		{
			StudentGlobals.MaleUniform = 1;
			StudentGlobals.FemaleUniform = 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			StudentGlobals.MaleUniform = 2;
			StudentGlobals.FemaleUniform = 2;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			StudentGlobals.MaleUniform = 3;
			StudentGlobals.FemaleUniform = 3;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F4))
		{
			StudentGlobals.MaleUniform = 4;
			StudentGlobals.FemaleUniform = 4;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F5))
		{
			StudentGlobals.MaleUniform = 5;
			StudentGlobals.FemaleUniform = 5;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F6))
		{
			StudentGlobals.MaleUniform = 6;
			StudentGlobals.FemaleUniform = 6;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (base.transform.position.y < -10f)
		{
			base.transform.position = new Vector3(base.transform.position.x, -10f, base.transform.position.z);
		}
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x000C93E8 File Offset: 0x000C75E8
	private void LateUpdate()
	{
		if (this.HidePony)
		{
			this.Ponytail.parent.transform.localScale = new Vector3(1f, 1f, 0.93f);
			this.Ponytail.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
			this.HairR.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
			this.HairL.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
		}
		if (Input.GetKeyDown(this.Letter[this.AlphabetID]))
		{
			this.AlphabetID++;
			if (this.AlphabetID == this.Letter.Length)
			{
				GameGlobals.AlphabetMode = true;
				SceneManager.LoadScene("LoadingScene");
			}
		}
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000C94C8 File Offset: 0x000C76C8
	private void UpdateHair()
	{
		this.PigtailR.transform.parent.transform.parent.transform.localScale = new Vector3(1f, 0.75f, 1f);
		this.PigtailL.transform.parent.transform.parent.transform.localScale = new Vector3(1f, 0.75f, 1f);
		this.PigtailR.gameObject.SetActive(false);
		this.PigtailL.gameObject.SetActive(false);
		this.Drills.gameObject.SetActive(false);
		this.HidePony = true;
		this.Hairstyle++;
		if (this.Hairstyle > 7)
		{
			this.Hairstyle = 1;
		}
		if (this.Hairstyle == 1)
		{
			this.HidePony = false;
			this.Ponytail.localScale = new Vector3(1f, 1f, 1f);
			this.HairR.localScale = new Vector3(1f, 1f, 1f);
			this.HairL.localScale = new Vector3(1f, 1f, 1f);
			return;
		}
		if (this.Hairstyle == 2)
		{
			this.PigtailR.gameObject.SetActive(true);
			return;
		}
		if (this.Hairstyle == 3)
		{
			this.PigtailL.gameObject.SetActive(true);
			return;
		}
		if (this.Hairstyle == 4)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
			return;
		}
		if (this.Hairstyle == 5)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
			this.HidePony = false;
			this.Ponytail.localScale = new Vector3(1f, 1f, 1f);
			this.HairR.localScale = new Vector3(1f, 1f, 1f);
			this.HairL.localScale = new Vector3(1f, 1f, 1f);
			return;
		}
		if (this.Hairstyle == 6)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
			this.PigtailR.transform.parent.transform.parent.transform.localScale = new Vector3(2f, 2f, 2f);
			this.PigtailL.transform.parent.transform.parent.transform.localScale = new Vector3(2f, 2f, 2f);
			return;
		}
		if (this.Hairstyle == 7)
		{
			this.Drills.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000C97BC File Offset: 0x000C79BC
	private void ChangeSchoolwear()
	{
		this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[0].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[1].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		base.StartCoroutine(this.ApplyCustomCostume());
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000C9844 File Offset: 0x000C7A44
	private void WearPajamas()
	{
		this.MyRenderer.sharedMesh = this.PajamaMesh;
		this.MyRenderer.materials[0].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[1].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		base.StartCoroutine(this.ApplyCustomFace());
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000C98B8 File Offset: 0x000C7AB8
	private void Nude()
	{
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000C991E File Offset: 0x000C7B1E
	private IEnumerator ApplyCustomCostume()
	{
		if (StudentGlobals.FemaleUniform == 1)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomUniform.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 2)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomLong.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 3)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomSweater.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 4 || StudentGlobals.FemaleUniform == 5)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomBlazer.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		base.StartCoroutine(this.ApplyCustomFace());
		yield break;
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x000C992D File Offset: 0x000C7B2D
	private IEnumerator ApplyCustomFace()
	{
		WWW CustomFace = new WWW("file:///" + Application.streamingAssetsPath + "/CustomFace.png");
		yield return CustomFace;
		if (CustomFace.error == null)
		{
			this.MyRenderer.materials[2].mainTexture = CustomFace.texture;
			this.FaceTexture = CustomFace.texture;
		}
		WWW CustomHair = new WWW("file:///" + Application.streamingAssetsPath + "/CustomHair.png");
		yield return CustomHair;
		if (CustomHair.error == null)
		{
			this.PonytailRenderer.material.mainTexture = CustomHair.texture;
			this.PigtailR.material.mainTexture = CustomHair.texture;
			this.PigtailL.material.mainTexture = CustomHair.texture;
		}
		WWW CustomDrills = new WWW("file:///" + Application.streamingAssetsPath + "/CustomDrills.png");
		yield return CustomDrills;
		if (CustomDrills.error == null)
		{
			this.Drills.materials[0].mainTexture = CustomDrills.texture;
			this.Drills.materials[1].mainTexture = CustomDrills.texture;
			this.Drills.materials[2].mainTexture = CustomDrills.texture;
		}
		yield break;
	}

	// Token: 0x0400204A RID: 8266
	public CharacterController MyController;

	// Token: 0x0400204B RID: 8267
	public AudioSource MyAudio;

	// Token: 0x0400204C RID: 8268
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x0400204D RID: 8269
	public HomeCameraScript HomeCamera;

	// Token: 0x0400204E RID: 8270
	public UISprite HomeDarkness;

	// Token: 0x0400204F RID: 8271
	public GameObject CutsceneYandere;

	// Token: 0x04002050 RID: 8272
	public GameObject Controller;

	// Token: 0x04002051 RID: 8273
	public GameObject Character;

	// Token: 0x04002052 RID: 8274
	public GameObject Disc;

	// Token: 0x04002053 RID: 8275
	public float WalkSpeed;

	// Token: 0x04002054 RID: 8276
	public float RunSpeed;

	// Token: 0x04002055 RID: 8277
	public bool CanMove;

	// Token: 0x04002056 RID: 8278
	public bool Running;

	// Token: 0x04002057 RID: 8279
	public AudioClip MiyukiReaction;

	// Token: 0x04002058 RID: 8280
	public AudioClip DiscScratch;

	// Token: 0x04002059 RID: 8281
	public Renderer PonytailRenderer;

	// Token: 0x0400205A RID: 8282
	public Renderer PigtailR;

	// Token: 0x0400205B RID: 8283
	public Renderer PigtailL;

	// Token: 0x0400205C RID: 8284
	public Renderer Drills;

	// Token: 0x0400205D RID: 8285
	public Transform Ponytail;

	// Token: 0x0400205E RID: 8286
	public Transform HairR;

	// Token: 0x0400205F RID: 8287
	public Transform HairL;

	// Token: 0x04002060 RID: 8288
	public bool HidePony;

	// Token: 0x04002061 RID: 8289
	public int Hairstyle;

	// Token: 0x04002062 RID: 8290
	public int VictimID;

	// Token: 0x04002063 RID: 8291
	public float Timer;

	// Token: 0x04002064 RID: 8292
	public Texture BlondePony;

	// Token: 0x04002065 RID: 8293
	public int AlphabetID;

	// Token: 0x04002066 RID: 8294
	public string[] Letter;

	// Token: 0x04002067 RID: 8295
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04002068 RID: 8296
	public Texture[] UniformTextures;

	// Token: 0x04002069 RID: 8297
	public Texture FaceTexture;

	// Token: 0x0400206A RID: 8298
	public Mesh[] Uniforms;

	// Token: 0x0400206B RID: 8299
	public Texture PajamaTexture;

	// Token: 0x0400206C RID: 8300
	public Mesh PajamaMesh;

	// Token: 0x0400206D RID: 8301
	public Texture NudeTexture;

	// Token: 0x0400206E RID: 8302
	public Mesh NudeMesh;
}
