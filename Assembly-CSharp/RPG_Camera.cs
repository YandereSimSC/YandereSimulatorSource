using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class RPG_Camera : MonoBehaviour
{
	// Token: 0x060009C6 RID: 2502 RVA: 0x0004BB0E File Offset: 0x00049D0E
	private void Awake()
	{
		RPG_Camera.instance = this;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0004BB18 File Offset: 0x00049D18
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		this.invertAxis = OptionGlobals.InvertAxis;
		this.sensitivity = (float)OptionGlobals.Sensitivity;
		RPG_Camera.MainCamera = base.GetComponent<Camera>();
		this.distance = Mathf.Clamp(this.distance, 0.05f, this.distanceMax);
		this.desiredDistance = this.distance;
		RPG_Camera.halfFieldOfView = RPG_Camera.MainCamera.fieldOfView / 2f * 0.017453292f;
		RPG_Camera.planeAspect = RPG_Camera.MainCamera.aspect;
		RPG_Camera.halfPlaneHeight = RPG_Camera.MainCamera.nearClipPlane * Mathf.Tan(RPG_Camera.halfFieldOfView);
		RPG_Camera.halfPlaneWidth = RPG_Camera.halfPlaneHeight * RPG_Camera.planeAspect;
		this.UpdateRotation();
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0004BBD5 File Offset: 0x00049DD5
	public void UpdateRotation()
	{
		this.mouseX = this.cameraPivot.transform.eulerAngles.y;
		this.mouseY = 15f;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0004BC00 File Offset: 0x00049E00
	public static void CameraSetup()
	{
		GameObject gameObject;
		if (RPG_Camera.MainCamera != null)
		{
			gameObject = RPG_Camera.MainCamera.gameObject;
		}
		else
		{
			gameObject = new GameObject("Main Camera");
			gameObject.AddComponent<Camera>();
			gameObject.tag = "MainCamera";
		}
		if (!gameObject.GetComponent("RPG_Camera"))
		{
			gameObject.AddComponent<RPG_Camera>();
		}
		RPG_Camera rpg_Camera = gameObject.GetComponent("RPG_Camera") as RPG_Camera;
		GameObject gameObject2 = GameObject.Find("cameraPivot");
		rpg_Camera.cameraPivot = gameObject2.transform;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0004BC84 File Offset: 0x00049E84
	private void LateUpdate()
	{
		if (Time.deltaTime > 0f)
		{
			if (this.cameraPivot == null)
			{
				this.cameraPivot = GameObject.Find("CameraPivot").transform;
				return;
			}
			this.GetInput();
			this.GetDesiredPosition();
			this.PositionUpdate();
			this.CharacterFade();
		}
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x0004BCDC File Offset: 0x00049EDC
	public void GetInput()
	{
		if ((double)this.distance > 0.1)
		{
			this.camBottom = Physics.Linecast(base.transform.position, base.transform.position - Vector3.up * this.camBottomDistance);
		}
		object obj = this.camBottom && base.transform.position.y - this.cameraPivot.transform.position.y <= 0f;
		this.mouseX += Input.GetAxis("Mouse X") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * this.sensitivity * 10f;
		object obj2 = obj;
		if (obj2 != null)
		{
			if (Input.GetAxis("Mouse Y") < 0f)
			{
				if (!this.invertAxis)
				{
					this.mouseY -= Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * this.sensitivity * 10f;
				}
				else
				{
					this.mouseY += Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * this.sensitivity * 10f;
				}
			}
			else if (!this.invertAxis)
			{
				this.mouseY -= Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * this.sensitivity * 10f;
			}
			else
			{
				this.mouseY += Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * this.sensitivity * 10f;
			}
		}
		else if (!this.invertAxis)
		{
			this.mouseY -= Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * this.sensitivity * 10f;
		}
		else
		{
			this.mouseY += Input.GetAxis("Mouse Y") * this.mouseSpeed * (Time.deltaTime / Mathf.Clamp(Time.timeScale, 1E-10f, 1E+10f)) * this.sensitivity * 10f;
		}
		this.mouseY = this.ClampAngle(this.mouseY, -89.5f, 89.5f);
		this.mouseXSmooth = Mathf.SmoothDamp(this.mouseXSmooth, this.mouseX, ref this.mouseXVel, this.mouseSmoothingFactor);
		this.mouseYSmooth = Mathf.SmoothDamp(this.mouseYSmooth, this.mouseY, ref this.mouseYVel, this.mouseSmoothingFactor);
		if (obj2 != null)
		{
			this.mouseYMin = this.mouseY;
		}
		else
		{
			this.mouseYMin = -89.5f;
		}
		this.mouseYSmooth = this.ClampAngle(this.mouseYSmooth, this.mouseYMin, this.mouseYMax);
		this.desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * this.mouseScroll;
		if (this.desiredDistance > this.distanceMax)
		{
			this.desiredDistance = this.distanceMax;
		}
		if (this.desiredDistance < this.distanceMin)
		{
			this.desiredDistance = this.distanceMin;
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0004C08C File Offset: 0x0004A28C
	public void GetDesiredPosition()
	{
		this.distance = this.desiredDistance;
		this.desiredPosition = this.GetCameraPosition(this.mouseYSmooth, this.mouseXSmooth, this.distance);
		this.constraint = false;
		float num = this.CheckCameraClipPlane(this.cameraPivot.position, this.desiredPosition);
		if (num != -1f)
		{
			this.distance = num;
			this.desiredPosition = this.GetCameraPosition(this.mouseYSmooth, this.mouseXSmooth, this.distance);
			this.constraint = true;
		}
		if (RPG_Camera.MainCamera == null)
		{
			RPG_Camera.MainCamera = base.GetComponent<Camera>();
		}
		this.distance -= RPG_Camera.MainCamera.nearClipPlane;
		if (this.lastDistance < this.distance || !this.constraint)
		{
			this.distance = Mathf.SmoothDamp(this.lastDistance, this.distance, ref this.distanceVel, this.camDistanceSpeed);
		}
		if ((double)this.distance < 0.05)
		{
			this.distance = 0.05f;
		}
		this.lastDistance = this.distance;
		this.desiredPosition = this.GetCameraPosition(this.mouseYSmooth, this.mouseXSmooth, this.distance);
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0004C1C5 File Offset: 0x0004A3C5
	public void PositionUpdate()
	{
		base.transform.position = this.desiredPosition;
		if ((double)this.distance > 0.05)
		{
			base.transform.LookAt(this.cameraPivot);
		}
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0004C1FC File Offset: 0x0004A3FC
	private void CharacterFade()
	{
		if (RPG_Animation.instance == null)
		{
			return;
		}
		if (this.distance < this.firstPersonThreshold)
		{
			RPG_Animation.instance.GetComponent<Renderer>().enabled = false;
			return;
		}
		if (this.distance < this.characterFadeThreshold)
		{
			RPG_Animation.instance.GetComponent<Renderer>().enabled = true;
			float num = 1f - (this.characterFadeThreshold - this.distance) / (this.characterFadeThreshold - this.firstPersonThreshold);
			if (RPG_Animation.instance.GetComponent<Renderer>().material.color.a != num)
			{
				RPG_Animation.instance.GetComponent<Renderer>().material.color = new Color(RPG_Animation.instance.GetComponent<Renderer>().material.color.r, RPG_Animation.instance.GetComponent<Renderer>().material.color.g, RPG_Animation.instance.GetComponent<Renderer>().material.color.b, num);
				return;
			}
		}
		else
		{
			RPG_Animation.instance.GetComponent<Renderer>().enabled = true;
			if (RPG_Animation.instance.GetComponent<Renderer>().material.color.a != 1f)
			{
				RPG_Animation.instance.GetComponent<Renderer>().material.color = new Color(RPG_Animation.instance.GetComponent<Renderer>().material.color.r, RPG_Animation.instance.GetComponent<Renderer>().material.color.g, RPG_Animation.instance.GetComponent<Renderer>().material.color.b, 1f);
			}
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0004C398 File Offset: 0x0004A598
	private Vector3 GetCameraPosition(float xAxis, float yAxis, float distance)
	{
		Vector3 point = new Vector3(0f, 0f, -distance);
		Quaternion rotation = Quaternion.Euler(xAxis, yAxis, 0f);
		return this.cameraPivot.position + rotation * point;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0004C3DC File Offset: 0x0004A5DC
	private float CheckCameraClipPlane(Vector3 from, Vector3 to)
	{
		float num = -1f;
		RPG_Camera.ClipPlaneVertexes clipPlaneAt = RPG_Camera.GetClipPlaneAt(to);
		int layerMask = 257;
		if (RPG_Camera.MainCamera != null)
		{
			RaycastHit raycastHit;
			if (Physics.Linecast(from, to, out raycastHit, layerMask))
			{
				num = raycastHit.distance - RPG_Camera.MainCamera.nearClipPlane;
			}
			if (Physics.Linecast(from - base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.UpperLeft, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point + base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
			if (Physics.Linecast(from + base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.UpperRight, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point - base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
			if (Physics.Linecast(from - base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.LowerLeft, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point + base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
			if (Physics.Linecast(from + base.transform.right * RPG_Camera.halfPlaneWidth - base.transform.up * RPG_Camera.halfPlaneHeight, clipPlaneAt.LowerRight, out raycastHit, layerMask) && (raycastHit.distance < num || num == -1f))
			{
				num = Vector3.Distance(raycastHit.point - base.transform.right * RPG_Camera.halfPlaneWidth + base.transform.up * RPG_Camera.halfPlaneHeight, from);
			}
		}
		return num;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0004C690 File Offset: 0x0004A890
	private float ClampAngle(float angle, float min, float max)
	{
		while (angle < -360f || angle > 360f)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0004C6D0 File Offset: 0x0004A8D0
	public static RPG_Camera.ClipPlaneVertexes GetClipPlaneAt(Vector3 pos)
	{
		RPG_Camera.ClipPlaneVertexes result = default(RPG_Camera.ClipPlaneVertexes);
		if (RPG_Camera.MainCamera == null)
		{
			return result;
		}
		Transform transform = RPG_Camera.MainCamera.transform;
		float nearClipPlane = RPG_Camera.MainCamera.nearClipPlane;
		result.UpperLeft = pos - transform.right * RPG_Camera.halfPlaneWidth;
		result.UpperLeft += transform.up * RPG_Camera.halfPlaneHeight;
		result.UpperLeft += transform.forward * nearClipPlane;
		result.UpperRight = pos + transform.right * RPG_Camera.halfPlaneWidth;
		result.UpperRight += transform.up * RPG_Camera.halfPlaneHeight;
		result.UpperRight += transform.forward * nearClipPlane;
		result.LowerLeft = pos - transform.right * RPG_Camera.halfPlaneWidth;
		result.LowerLeft -= transform.up * RPG_Camera.halfPlaneHeight;
		result.LowerLeft += transform.forward * nearClipPlane;
		result.LowerRight = pos + transform.right * RPG_Camera.halfPlaneWidth;
		result.LowerRight -= transform.up * RPG_Camera.halfPlaneHeight;
		result.LowerRight += transform.forward * nearClipPlane;
		return result;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0004C8A8 File Offset: 0x0004AAA8
	public void RotateWithCharacter()
	{
		float num = Input.GetAxis("Horizontal") * RPG_Controller.instance.turnSpeed;
		this.mouseX += num;
	}

	// Token: 0x04000820 RID: 2080
	public static RPG_Camera instance;

	// Token: 0x04000821 RID: 2081
	public static Camera MainCamera;

	// Token: 0x04000822 RID: 2082
	public Transform cameraPivot;

	// Token: 0x04000823 RID: 2083
	public float distance = 5f;

	// Token: 0x04000824 RID: 2084
	public float distanceMax = 30f;

	// Token: 0x04000825 RID: 2085
	public float distanceMin = 2f;

	// Token: 0x04000826 RID: 2086
	public float mouseSpeed = 8f;

	// Token: 0x04000827 RID: 2087
	public float mouseScroll = 15f;

	// Token: 0x04000828 RID: 2088
	public float mouseSmoothingFactor = 0.08f;

	// Token: 0x04000829 RID: 2089
	public float camDistanceSpeed = 0.7f;

	// Token: 0x0400082A RID: 2090
	public float camBottomDistance = 1f;

	// Token: 0x0400082B RID: 2091
	public float firstPersonThreshold = 0.8f;

	// Token: 0x0400082C RID: 2092
	public float characterFadeThreshold = 1.8f;

	// Token: 0x0400082D RID: 2093
	public Vector3 desiredPosition;

	// Token: 0x0400082E RID: 2094
	public float desiredDistance;

	// Token: 0x0400082F RID: 2095
	private float lastDistance;

	// Token: 0x04000830 RID: 2096
	public float mouseX;

	// Token: 0x04000831 RID: 2097
	public float mouseXSmooth;

	// Token: 0x04000832 RID: 2098
	private float mouseXVel;

	// Token: 0x04000833 RID: 2099
	public float mouseY;

	// Token: 0x04000834 RID: 2100
	public float mouseYSmooth;

	// Token: 0x04000835 RID: 2101
	private float mouseYVel;

	// Token: 0x04000836 RID: 2102
	private float mouseYMin = -89.5f;

	// Token: 0x04000837 RID: 2103
	private float mouseYMax = 89.5f;

	// Token: 0x04000838 RID: 2104
	private float distanceVel;

	// Token: 0x04000839 RID: 2105
	private bool camBottom;

	// Token: 0x0400083A RID: 2106
	private bool constraint;

	// Token: 0x0400083B RID: 2107
	public bool invertAxis;

	// Token: 0x0400083C RID: 2108
	public float sensitivity;

	// Token: 0x0400083D RID: 2109
	private static float halfFieldOfView;

	// Token: 0x0400083E RID: 2110
	private static float planeAspect;

	// Token: 0x0400083F RID: 2111
	private static float halfPlaneHeight;

	// Token: 0x04000840 RID: 2112
	private static float halfPlaneWidth;

	// Token: 0x02000695 RID: 1685
	public struct ClipPlaneVertexes
	{
		// Token: 0x04004612 RID: 17938
		public Vector3 UpperLeft;

		// Token: 0x04004613 RID: 17939
		public Vector3 UpperRight;

		// Token: 0x04004614 RID: 17940
		public Vector3 LowerLeft;

		// Token: 0x04004615 RID: 17941
		public Vector3 LowerRight;
	}
}
