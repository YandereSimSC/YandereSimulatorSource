using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class AspectUtility : MonoBehaviour
{
	// Token: 0x06000A20 RID: 2592 RVA: 0x000513EC File Offset: 0x0004F5EC
	private void Start()
	{
		AspectUtility.cam = base.GetComponent<Camera>();
		if (!AspectUtility.cam)
		{
			AspectUtility.cam = Camera.main;
		}
		if (!AspectUtility.cam)
		{
			Debug.LogError("No camera available");
			return;
		}
		AspectUtility.wantedAspectRatio = this._wantedAspectRatio;
		AspectUtility.SetCamera();
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00051444 File Offset: 0x0004F644
	public static void SetCamera()
	{
		float num = (float)Screen.width / (float)Screen.height;
		if ((float)((int)(num * 100f)) / 100f == (float)((int)(AspectUtility.wantedAspectRatio * 100f)) / 100f)
		{
			AspectUtility.cam.rect = new Rect(0f, 0f, 1f, 1f);
			if (AspectUtility.backgroundCam)
			{
				UnityEngine.Object.Destroy(AspectUtility.backgroundCam.gameObject);
			}
			return;
		}
		if (num > AspectUtility.wantedAspectRatio)
		{
			float num2 = 1f - AspectUtility.wantedAspectRatio / num;
			AspectUtility.cam.rect = new Rect(num2 / 2f, 0f, 1f - num2, 1f);
		}
		else
		{
			float num3 = 1f - num / AspectUtility.wantedAspectRatio;
			AspectUtility.cam.rect = new Rect(0f, num3 / 2f, 1f, 1f - num3);
		}
		if (!AspectUtility.backgroundCam)
		{
			AspectUtility.backgroundCam = new GameObject("BackgroundCam", new Type[]
			{
				typeof(Camera)
			}).GetComponent<Camera>();
			AspectUtility.backgroundCam.depth = -2.1474836E+09f;
			AspectUtility.backgroundCam.clearFlags = CameraClearFlags.Color;
			AspectUtility.backgroundCam.backgroundColor = Color.black;
			AspectUtility.backgroundCam.cullingMask = 0;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000515A0 File Offset: 0x0004F7A0
	public static int screenHeight
	{
		get
		{
			return (int)((float)Screen.height * AspectUtility.cam.rect.height);
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000515C8 File Offset: 0x0004F7C8
	public static int screenWidth
	{
		get
		{
			return (int)((float)Screen.width * AspectUtility.cam.rect.width);
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000515F0 File Offset: 0x0004F7F0
	public static int xOffset
	{
		get
		{
			return (int)((float)Screen.width * AspectUtility.cam.rect.x);
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00051618 File Offset: 0x0004F818
	public static int yOffset
	{
		get
		{
			return (int)((float)Screen.height * AspectUtility.cam.rect.y);
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00051640 File Offset: 0x0004F840
	public static Rect screenRect
	{
		get
		{
			return new Rect(AspectUtility.cam.rect.x * (float)Screen.width, AspectUtility.cam.rect.y * (float)Screen.height, AspectUtility.cam.rect.width * (float)Screen.width, AspectUtility.cam.rect.height * (float)Screen.height);
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000516B8 File Offset: 0x0004F8B8
	public static Vector3 mousePosition
	{
		get
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.y -= (float)((int)(AspectUtility.cam.rect.y * (float)Screen.height));
			mousePosition.x -= (float)((int)(AspectUtility.cam.rect.x * (float)Screen.width));
			return mousePosition;
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000A28 RID: 2600 RVA: 0x00051718 File Offset: 0x0004F918
	public static Vector2 guiMousePosition
	{
		get
		{
			Vector2 mousePosition = Event.current.mousePosition;
			mousePosition.y = Mathf.Clamp(mousePosition.y, AspectUtility.cam.rect.y * (float)Screen.height, AspectUtility.cam.rect.y * (float)Screen.height + AspectUtility.cam.rect.height * (float)Screen.height);
			mousePosition.x = Mathf.Clamp(mousePosition.x, AspectUtility.cam.rect.x * (float)Screen.width, AspectUtility.cam.rect.x * (float)Screen.width + AspectUtility.cam.rect.width * (float)Screen.width);
			return mousePosition;
		}
	}

	// Token: 0x04000A3C RID: 2620
	public float _wantedAspectRatio = 1.777778f;

	// Token: 0x04000A3D RID: 2621
	private static float wantedAspectRatio;

	// Token: 0x04000A3E RID: 2622
	private static Camera cam;

	// Token: 0x04000A3F RID: 2623
	private static Camera backgroundCam;
}
