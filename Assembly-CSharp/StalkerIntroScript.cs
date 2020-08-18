using System;
using UnityEngine;
using UnityEngine.PostProcessing;

// Token: 0x020003EF RID: 1007
public class StalkerIntroScript : MonoBehaviour
{
	// Token: 0x06001AD0 RID: 6864 RVA: 0x0010CBA4 File Offset: 0x0010ADA4
	private void Start()
	{
		RenderSettings.ambientIntensity = 4f;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		base.transform.position = new Vector3(12.5f, 5f, 13f);
		base.transform.LookAt(this.Moon);
		this.CameraFocus.parent = base.transform;
		this.CameraFocus.localPosition = new Vector3(0f, 0f, 100f);
		this.CameraFocus.parent = null;
		this.UpdateDOF(3f);
		this.DOF = 4f;
		this.Alpha = 1f;
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x0010CC54 File Offset: 0x0010AE54
	private void Update()
	{
		this.Moon.LookAt(base.transform);
		if (this.Phase == 0)
		{
			if (Input.GetKeyDown("space"))
			{
				this.Timer = 2f;
				this.Alpha = 0f;
			}
			this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.5f);
			this.Darkness.material.color = new Color(0f, 0f, 0f, this.Alpha);
			if (this.Alpha == 0f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 2f)
				{
					this.Phase++;
				}
			}
		}
		else if (this.Phase == 1)
		{
			if (this.Speed == 0f)
			{
				this.Yandere.MyAnimation.Play();
			}
			if (Input.GetKeyDown("space") && this.Yandere.MyAnimation["f02_jumpOverWall_00"].time < 12f)
			{
				this.Yandere.MyAnimation["f02_jumpOverWall_00"].time = 12f;
				this.Speed = 100f;
			}
			this.Speed += Time.deltaTime;
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(11.5f, 1f, 13f), Time.deltaTime * this.Speed);
			this.CameraFocus.position = Vector3.Lerp(this.CameraFocus.position, new Vector3(13.62132f, 1f, 15.12132f), Time.deltaTime * this.Speed);
			this.DOF = Mathf.Lerp(this.DOF, 2f, Time.deltaTime * this.Speed);
			this.UpdateDOF(this.DOF);
			base.transform.LookAt(this.CameraFocus);
			if (this.Yandere.MyAnimation["f02_jumpOverWall_00"].time > 13f)
			{
				this.Yandere.transform.position = new Vector3(13.15f, 0f, 13f);
				base.transform.position = new Vector3(12.9f, 1.35f, 12.5f);
				base.transform.eulerAngles = new Vector3(0f, 45f, 0f);
				this.UpdateDOF(0.3f);
				this.Speed = -1f;
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (Input.GetKeyDown("space"))
			{
				this.Speed = 100f;
			}
			this.Speed += Time.deltaTime;
			if (this.Speed > 0f)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(13.15f, 1.51515f, 14.92272f), Time.deltaTime * this.Speed);
				base.transform.eulerAngles = Vector3.Lerp(base.transform.eulerAngles, new Vector3(15f, 180f, 0f), Time.deltaTime * this.Speed);
				this.DOF = Mathf.Lerp(this.DOF, 2f, Time.deltaTime * this.Speed);
				this.UpdateDOF(this.DOF);
				if (this.Speed > 4f)
				{
					this.RPGCamera.enabled = true;
					this.Yandere.enabled = true;
					this.Phase++;
				}
			}
		}
		if (Input.GetKeyDown("space"))
		{
			if (this.Neighborhood[0].activeInHierarchy)
			{
				this.Neighborhood[0].SetActive(false);
				this.Neighborhood[1].SetActive(true);
				return;
			}
			this.Neighborhood[0].SetActive(true);
			this.Neighborhood[1].SetActive(false);
		}
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x0010D098 File Offset: 0x0010B298
	private void UpdateDOF(float Value)
	{
		DepthOfFieldModel.Settings settings = this.Profile.depthOfField.settings;
		settings.focusDistance = Value;
		settings.aperture = 5.6f;
		this.Profile.depthOfField.settings = settings;
	}

	// Token: 0x04002B5D RID: 11101
	public PostProcessingProfile Profile;

	// Token: 0x04002B5E RID: 11102
	public StalkerYandereScript Yandere;

	// Token: 0x04002B5F RID: 11103
	public RPG_Camera RPGCamera;

	// Token: 0x04002B60 RID: 11104
	public Transform CameraFocus;

	// Token: 0x04002B61 RID: 11105
	public Transform Moon;

	// Token: 0x04002B62 RID: 11106
	public Renderer Darkness;

	// Token: 0x04002B63 RID: 11107
	public float Alpha;

	// Token: 0x04002B64 RID: 11108
	public float Speed;

	// Token: 0x04002B65 RID: 11109
	public float Timer;

	// Token: 0x04002B66 RID: 11110
	public float DOF;

	// Token: 0x04002B67 RID: 11111
	public int Phase;

	// Token: 0x04002B68 RID: 11112
	public GameObject[] Neighborhood;
}
