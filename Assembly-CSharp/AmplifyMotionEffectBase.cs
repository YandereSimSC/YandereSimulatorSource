using System;
using System.Collections.Generic;
using System.Linq;
using AmplifyMotion;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

// Token: 0x020000B2 RID: 178
[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class AmplifyMotionEffectBase : MonoBehaviour
{
	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000933 RID: 2355 RVA: 0x000490CB File Offset: 0x000472CB
	// (set) Token: 0x06000934 RID: 2356 RVA: 0x000490D3 File Offset: 0x000472D3
	[Obsolete("workerThreads is deprecated, please use WorkerThreads instead.")]
	public int workerThreads
	{
		get
		{
			return this.WorkerThreads;
		}
		set
		{
			this.WorkerThreads = value;
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000935 RID: 2357 RVA: 0x000490DC File Offset: 0x000472DC
	internal Material ReprojectionMaterial
	{
		get
		{
			return this.m_reprojectionMaterial;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000936 RID: 2358 RVA: 0x000490E4 File Offset: 0x000472E4
	internal Material SolidVectorsMaterial
	{
		get
		{
			return this.m_solidVectorsMaterial;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000937 RID: 2359 RVA: 0x000490EC File Offset: 0x000472EC
	internal Material SkinnedVectorsMaterial
	{
		get
		{
			return this.m_skinnedVectorsMaterial;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000938 RID: 2360 RVA: 0x000490F4 File Offset: 0x000472F4
	internal Material ClothVectorsMaterial
	{
		get
		{
			return this.m_clothVectorsMaterial;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000939 RID: 2361 RVA: 0x000490FC File Offset: 0x000472FC
	internal RenderTexture MotionRenderTexture
	{
		get
		{
			return this.m_motionRT;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x0600093A RID: 2362 RVA: 0x00049104 File Offset: 0x00047304
	public Dictionary<Camera, AmplifyMotionCamera> LinkedCameras
	{
		get
		{
			return this.m_linkedCameras;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x0600093B RID: 2363 RVA: 0x0004910C File Offset: 0x0004730C
	internal float MotionScaleNorm
	{
		get
		{
			return this.m_motionScaleNorm;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x0600093C RID: 2364 RVA: 0x00049114 File Offset: 0x00047314
	internal float FixedMotionScaleNorm
	{
		get
		{
			return this.m_fixedMotionScaleNorm;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x0600093D RID: 2365 RVA: 0x0004911C File Offset: 0x0004731C
	public AmplifyMotionCamera BaseCamera
	{
		get
		{
			return this.m_baseCamera;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x0600093E RID: 2366 RVA: 0x00049124 File Offset: 0x00047324
	internal WorkerThreadPool WorkerPool
	{
		get
		{
			return this.m_workerThreadPool;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x0600093F RID: 2367 RVA: 0x0004912C File Offset: 0x0004732C
	public static bool IsD3D
	{
		get
		{
			return AmplifyMotionEffectBase.m_isD3D;
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000940 RID: 2368 RVA: 0x00049133 File Offset: 0x00047333
	public bool CanUseGPU
	{
		get
		{
			return this.m_canUseGPU;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000941 RID: 2369 RVA: 0x0004913B File Offset: 0x0004733B
	public static bool IgnoreMotionScaleWarning
	{
		get
		{
			return AmplifyMotionEffectBase.m_ignoreMotionScaleWarning;
		}
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000942 RID: 2370 RVA: 0x00049142 File Offset: 0x00047342
	public static AmplifyMotionEffectBase FirstInstance
	{
		get
		{
			return AmplifyMotionEffectBase.m_firstInstance;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000943 RID: 2371 RVA: 0x00049142 File Offset: 0x00047342
	public static AmplifyMotionEffectBase Instance
	{
		get
		{
			return AmplifyMotionEffectBase.m_firstInstance;
		}
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0004914C File Offset: 0x0004734C
	private void Awake()
	{
		if (AmplifyMotionEffectBase.m_firstInstance == null)
		{
			AmplifyMotionEffectBase.m_firstInstance = this;
		}
		AmplifyMotionEffectBase.m_isD3D = SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D");
		this.m_globalObjectId = 1;
		this.m_width = (this.m_height = 0);
		if (this.ForceCPUOnly)
		{
			this.m_canUseGPU = false;
			return;
		}
		bool flag = SystemInfo.graphicsShaderLevel >= 30;
		bool flag2 = SystemInfo.SupportsTextureFormat(TextureFormat.RHalf);
		bool flag3 = SystemInfo.SupportsTextureFormat(TextureFormat.RGHalf);
		bool flag4 = SystemInfo.SupportsTextureFormat(TextureFormat.RGBAHalf);
		bool flag5 = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBFloat);
		this.m_canUseGPU = (flag && flag2 && flag3 && flag4 && flag5);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x000491E7 File Offset: 0x000473E7
	internal void ResetObjectId()
	{
		this.m_globalObjectId = 1;
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x000491F0 File Offset: 0x000473F0
	internal int GenerateObjectId(GameObject obj)
	{
		if (obj.isStatic)
		{
			return 0;
		}
		this.m_globalObjectId++;
		if (this.m_globalObjectId > 254)
		{
			this.m_globalObjectId = 1;
		}
		return this.m_globalObjectId;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00049224 File Offset: 0x00047424
	private void SafeDestroyMaterial(ref Material mat)
	{
		if (mat != null)
		{
			UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0004923C File Offset: 0x0004743C
	private bool CheckMaterialAndShader(Material material, string name)
	{
		bool result = true;
		if (material == null || material.shader == null)
		{
			Debug.LogWarning("[AmplifyMotion] Error creating " + name + " material");
			result = false;
		}
		else if (!material.shader.isSupported)
		{
			Debug.LogWarning("[AmplifyMotion] " + name + " shader not supported on this platform");
			result = false;
		}
		return result;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x000492A0 File Offset: 0x000474A0
	private void DestroyMaterials()
	{
		this.SafeDestroyMaterial(ref this.m_blurMaterial);
		this.SafeDestroyMaterial(ref this.m_solidVectorsMaterial);
		this.SafeDestroyMaterial(ref this.m_skinnedVectorsMaterial);
		this.SafeDestroyMaterial(ref this.m_clothVectorsMaterial);
		this.SafeDestroyMaterial(ref this.m_reprojectionMaterial);
		this.SafeDestroyMaterial(ref this.m_combineMaterial);
		this.SafeDestroyMaterial(ref this.m_dilationMaterial);
		this.SafeDestroyMaterial(ref this.m_depthMaterial);
		this.SafeDestroyMaterial(ref this.m_debugMaterial);
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0004931C File Offset: 0x0004751C
	private bool CreateMaterials()
	{
		this.DestroyMaterials();
		int num = (SystemInfo.graphicsShaderLevel >= 30) ? 3 : 2;
		string name = "Hidden/Amplify Motion/MotionBlurSM" + num;
		string name2 = "Hidden/Amplify Motion/SolidVectors";
		string name3 = "Hidden/Amplify Motion/SkinnedVectors";
		string name4 = "Hidden/Amplify Motion/ClothVectors";
		string name5 = "Hidden/Amplify Motion/ReprojectionVectors";
		string name6 = "Hidden/Amplify Motion/Combine";
		string name7 = "Hidden/Amplify Motion/Dilation";
		string name8 = "Hidden/Amplify Motion/Depth";
		string name9 = "Hidden/Amplify Motion/Debug";
		try
		{
			this.m_blurMaterial = new Material(Shader.Find(name))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_solidVectorsMaterial = new Material(Shader.Find(name2))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_skinnedVectorsMaterial = new Material(Shader.Find(name3))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_clothVectorsMaterial = new Material(Shader.Find(name4))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_reprojectionMaterial = new Material(Shader.Find(name5))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_combineMaterial = new Material(Shader.Find(name6))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_dilationMaterial = new Material(Shader.Find(name7))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_depthMaterial = new Material(Shader.Find(name8))
			{
				hideFlags = HideFlags.DontSave
			};
			this.m_debugMaterial = new Material(Shader.Find(name9))
			{
				hideFlags = HideFlags.DontSave
			};
		}
		catch (Exception)
		{
		}
		return this.CheckMaterialAndShader(this.m_blurMaterial, name) && this.CheckMaterialAndShader(this.m_solidVectorsMaterial, name2) && this.CheckMaterialAndShader(this.m_skinnedVectorsMaterial, name3) && this.CheckMaterialAndShader(this.m_clothVectorsMaterial, name4) && this.CheckMaterialAndShader(this.m_reprojectionMaterial, name5) && this.CheckMaterialAndShader(this.m_combineMaterial, name6) && this.CheckMaterialAndShader(this.m_dilationMaterial, name7) && this.CheckMaterialAndShader(this.m_depthMaterial, name8) && this.CheckMaterialAndShader(this.m_debugMaterial, name9);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x00049524 File Offset: 0x00047724
	private RenderTexture CreateRenderTexture(string name, int depth, RenderTextureFormat fmt, RenderTextureReadWrite rw, FilterMode fm)
	{
		RenderTexture renderTexture = new RenderTexture(this.m_width, this.m_height, depth, fmt, rw);
		renderTexture.hideFlags = HideFlags.DontSave;
		renderTexture.name = name;
		renderTexture.wrapMode = TextureWrapMode.Clamp;
		renderTexture.filterMode = fm;
		renderTexture.Create();
		return renderTexture;
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00049560 File Offset: 0x00047760
	private void SafeDestroyRenderTexture(ref RenderTexture rt)
	{
		if (rt != null)
		{
			RenderTexture.active = null;
			rt.Release();
			UnityEngine.Object.DestroyImmediate(rt);
			rt = null;
		}
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x00049224 File Offset: 0x00047424
	private void SafeDestroyTexture(ref Texture tex)
	{
		if (tex != null)
		{
			UnityEngine.Object.DestroyImmediate(tex);
			tex = null;
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x00049583 File Offset: 0x00047783
	private void DestroyRenderTextures()
	{
		RenderTexture.active = null;
		this.SafeDestroyRenderTexture(ref this.m_motionRT);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00049598 File Offset: 0x00047798
	private void UpdateRenderTextures(bool qualityChanged)
	{
		int num = Mathf.Max(Mathf.FloorToInt((float)this.m_camera.pixelWidth + 0.5f), 1);
		int num2 = Mathf.Max(Mathf.FloorToInt((float)this.m_camera.pixelHeight + 0.5f), 1);
		if (this.QualityLevel == Quality.Mobile)
		{
			num /= 2;
			num2 /= 2;
		}
		if (this.m_width != num || this.m_height != num2)
		{
			this.m_width = num;
			this.m_height = num2;
			this.DestroyRenderTextures();
		}
		if (this.m_motionRT == null)
		{
			this.m_motionRT = this.CreateRenderTexture("AM-MotionVectors", 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, FilterMode.Point);
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0004963B File Offset: 0x0004783B
	public bool CheckSupport()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			Debug.LogError("[AmplifyMotion] Initialization failed. This plugin requires support for Image Effects and Render Textures.");
			return false;
		}
		return true;
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00049651 File Offset: 0x00047851
	private void InitializeThreadPool()
	{
		if (this.WorkerThreads <= 0)
		{
			this.WorkerThreads = Mathf.Max(Environment.ProcessorCount / 2, 1);
		}
		this.m_workerThreadPool = new WorkerThreadPool();
		this.m_workerThreadPool.InitializeAsyncUpdateThreads(this.WorkerThreads, this.SystemThreadPool);
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00049691 File Offset: 0x00047891
	private void ShutdownThreadPool()
	{
		if (this.m_workerThreadPool != null)
		{
			this.m_workerThreadPool.FinalizeAsyncUpdateThreads();
			this.m_workerThreadPool = null;
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x000496B0 File Offset: 0x000478B0
	private void InitializeCommandBuffers()
	{
		this.ShutdownCommandBuffers();
		this.m_updateCB = new CommandBuffer();
		this.m_updateCB.name = "AmplifyMotion.Update";
		this.m_camera.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_updateCB);
		this.m_fixedUpdateCB = new CommandBuffer();
		this.m_fixedUpdateCB.name = "AmplifyMotion.FixedUpdate";
		this.m_camera.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_fixedUpdateCB);
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00049720 File Offset: 0x00047920
	private void ShutdownCommandBuffers()
	{
		if (this.m_updateCB != null)
		{
			this.m_camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_updateCB);
			this.m_updateCB.Release();
			this.m_updateCB = null;
		}
		if (this.m_fixedUpdateCB != null)
		{
			this.m_camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_fixedUpdateCB);
			this.m_fixedUpdateCB.Release();
			this.m_fixedUpdateCB = null;
		}
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00049788 File Offset: 0x00047988
	private void OnEnable()
	{
		this.m_camera = base.GetComponent<Camera>();
		if (!this.CheckSupport())
		{
			base.enabled = false;
			return;
		}
		this.InitializeThreadPool();
		this.m_starting = true;
		if (!this.CreateMaterials())
		{
			Debug.LogError("[AmplifyMotion] Failed loading or compiling necessary shaders. Please try reinstalling Amplify Motion or contact support@amplify.pt");
			base.enabled = false;
			return;
		}
		if (this.AutoRegisterObjs)
		{
			this.UpdateActiveObjects();
		}
		this.InitializeCameras();
		this.InitializeCommandBuffers();
		this.UpdateRenderTextures(true);
		this.m_linkedCameras.TryGetValue(this.m_camera, out this.m_baseCamera);
		if (this.m_baseCamera == null)
		{
			Debug.LogError("[AmplifyMotion] Failed setting up Base Camera. Please contact support@amplify.pt");
			base.enabled = false;
			return;
		}
		if (this.m_currentPostProcess != null)
		{
			this.m_currentPostProcess.enabled = true;
		}
		this.m_qualityLevel = this.QualityLevel;
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00049857 File Offset: 0x00047A57
	private void OnDisable()
	{
		if (this.m_currentPostProcess != null)
		{
			this.m_currentPostProcess.enabled = false;
		}
		this.ShutdownCommandBuffers();
		this.ShutdownThreadPool();
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0004987F File Offset: 0x00047A7F
	private void Start()
	{
		this.UpdatePostProcess();
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00049887 File Offset: 0x00047A87
	internal void RemoveCamera(Camera reference)
	{
		this.m_linkedCameras.Remove(reference);
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x00049898 File Offset: 0x00047A98
	private void OnDestroy()
	{
		foreach (AmplifyMotionCamera amplifyMotionCamera in this.m_linkedCameras.Values.ToArray<AmplifyMotionCamera>())
		{
			if (amplifyMotionCamera != null && amplifyMotionCamera.gameObject != base.gameObject)
			{
				Camera component = amplifyMotionCamera.GetComponent<Camera>();
				if (component != null)
				{
					component.targetTexture = null;
				}
				UnityEngine.Object.DestroyImmediate(amplifyMotionCamera);
			}
		}
		this.DestroyRenderTextures();
		this.DestroyMaterials();
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x00049910 File Offset: 0x00047B10
	private GameObject RecursiveFindCamera(GameObject obj, string auxCameraName)
	{
		GameObject gameObject = null;
		if (obj.name == auxCameraName)
		{
			gameObject = obj;
		}
		else
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				gameObject = this.RecursiveFindCamera(transform.gameObject, auxCameraName);
				if (gameObject != null)
				{
					break;
				}
			}
		}
		return gameObject;
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00049990 File Offset: 0x00047B90
	private void InitializeCameras()
	{
		List<Camera> list = new List<Camera>(this.OverlayCameras.Length);
		for (int i = 0; i < this.OverlayCameras.Length; i++)
		{
			if (this.OverlayCameras[i] != null)
			{
				list.Add(this.OverlayCameras[i]);
			}
		}
		Camera[] array = new Camera[list.Count + 1];
		array[0] = this.m_camera;
		for (int j = 0; j < list.Count; j++)
		{
			array[j + 1] = list[j];
		}
		this.m_linkedCameras.Clear();
		for (int k = 0; k < array.Length; k++)
		{
			Camera camera = array[k];
			if (!this.m_linkedCameras.ContainsKey(camera))
			{
				AmplifyMotionCamera amplifyMotionCamera = camera.gameObject.GetComponent<AmplifyMotionCamera>();
				if (amplifyMotionCamera != null)
				{
					amplifyMotionCamera.enabled = false;
					amplifyMotionCamera.enabled = true;
				}
				else
				{
					amplifyMotionCamera = camera.gameObject.AddComponent<AmplifyMotionCamera>();
				}
				amplifyMotionCamera.LinkTo(this, k > 0);
				this.m_linkedCameras.Add(camera, amplifyMotionCamera);
				this.m_linkedCamerasChanged = true;
			}
		}
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00049A9D File Offset: 0x00047C9D
	public void UpdateActiveCameras()
	{
		this.InitializeCameras();
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x00049AA8 File Offset: 0x00047CA8
	internal static void RegisterCamera(AmplifyMotionCamera cam)
	{
		if (!AmplifyMotionEffectBase.m_activeCameras.ContainsValue(cam))
		{
			AmplifyMotionEffectBase.m_activeCameras.Add(cam.GetComponent<Camera>(), cam);
		}
		foreach (AmplifyMotionObjectBase amplifyMotionObjectBase in AmplifyMotionEffectBase.m_activeObjects.Values)
		{
			amplifyMotionObjectBase.RegisterCamera(cam);
		}
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x00049B1C File Offset: 0x00047D1C
	internal static void UnregisterCamera(AmplifyMotionCamera cam)
	{
		foreach (AmplifyMotionObjectBase amplifyMotionObjectBase in AmplifyMotionEffectBase.m_activeObjects.Values)
		{
			amplifyMotionObjectBase.UnregisterCamera(cam);
		}
		AmplifyMotionEffectBase.m_activeCameras.Remove(cam.GetComponent<Camera>());
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x00049B84 File Offset: 0x00047D84
	public void UpdateActiveObjects()
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		for (int i = 0; i < array.Length; i++)
		{
			if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(array[i]))
			{
				AmplifyMotionEffectBase.TryRegister(array[i], true);
			}
		}
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x00049BCC File Offset: 0x00047DCC
	internal static void RegisterObject(AmplifyMotionObjectBase obj)
	{
		AmplifyMotionEffectBase.m_activeObjects.Add(obj.gameObject, obj);
		foreach (AmplifyMotionCamera camera in AmplifyMotionEffectBase.m_activeCameras.Values)
		{
			obj.RegisterCamera(camera);
		}
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00049C34 File Offset: 0x00047E34
	internal static void UnregisterObject(AmplifyMotionObjectBase obj)
	{
		foreach (AmplifyMotionCamera camera in AmplifyMotionEffectBase.m_activeCameras.Values)
		{
			obj.UnregisterCamera(camera);
		}
		AmplifyMotionEffectBase.m_activeObjects.Remove(obj.gameObject);
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x00049C9C File Offset: 0x00047E9C
	internal static bool FindValidTag(Material[] materials)
	{
		foreach (Material material in materials)
		{
			if (material != null)
			{
				string tag = material.GetTag("RenderType", false);
				if (tag == "Opaque" || tag == "TransparentCutout")
				{
					return !material.IsKeywordEnabled("_ALPHABLEND_ON") && !material.IsKeywordEnabled("_ALPHAPREMULTIPLY_ON");
				}
			}
		}
		return false;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00049D0C File Offset: 0x00047F0C
	internal static bool CanRegister(GameObject gameObj, bool autoReg)
	{
		if (gameObj.isStatic)
		{
			return false;
		}
		Renderer component = gameObj.GetComponent<Renderer>();
		if (component == null || component.sharedMaterials == null || component.isPartOfStaticBatch)
		{
			return false;
		}
		if (!component.enabled)
		{
			return false;
		}
		if (component.shadowCastingMode == ShadowCastingMode.ShadowsOnly)
		{
			return false;
		}
		if (component.GetType() == typeof(SpriteRenderer))
		{
			return false;
		}
		if (!AmplifyMotionEffectBase.FindValidTag(component.sharedMaterials))
		{
			return false;
		}
		Type type = component.GetType();
		if (type == typeof(MeshRenderer) || type == typeof(SkinnedMeshRenderer))
		{
			return true;
		}
		if (type == typeof(ParticleSystemRenderer) && !autoReg)
		{
			ParticleSystemRenderMode renderMode = (component as ParticleSystemRenderer).renderMode;
			return renderMode == ParticleSystemRenderMode.Mesh || renderMode == ParticleSystemRenderMode.Billboard;
		}
		return false;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00049DDC File Offset: 0x00047FDC
	internal static void TryRegister(GameObject gameObj, bool autoReg)
	{
		if (AmplifyMotionEffectBase.CanRegister(gameObj, autoReg) && gameObj.GetComponent<AmplifyMotionObjectBase>() == null)
		{
			AmplifyMotionObjectBase.ApplyToChildren = false;
			gameObj.AddComponent<AmplifyMotionObjectBase>();
			AmplifyMotionObjectBase.ApplyToChildren = true;
		}
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00049E08 File Offset: 0x00048008
	internal static void TryUnregister(GameObject gameObj)
	{
		AmplifyMotionObjectBase component = gameObj.GetComponent<AmplifyMotionObjectBase>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component);
		}
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00049E2B File Offset: 0x0004802B
	public void Register(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00049E41 File Offset: 0x00048041
	public static void RegisterS(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00049E58 File Offset: 0x00048058
	public void RegisterRecursively(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
		foreach (object obj in gameObj.transform)
		{
			Transform transform = (Transform)obj;
			this.RegisterRecursively(transform.gameObject);
		}
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00049ECC File Offset: 0x000480CC
	public static void RegisterRecursivelyS(GameObject gameObj)
	{
		if (!AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryRegister(gameObj, false);
		}
		foreach (object obj in gameObj.transform)
		{
			AmplifyMotionEffectBase.RegisterRecursivelyS(((Transform)obj).gameObject);
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00049F3C File Offset: 0x0004813C
	public void Unregister(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00049F51 File Offset: 0x00048151
	public static void UnregisterS(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x00049F68 File Offset: 0x00048168
	public void UnregisterRecursively(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
		foreach (object obj in gameObj.transform)
		{
			Transform transform = (Transform)obj;
			this.UnregisterRecursively(transform.gameObject);
		}
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00049FDC File Offset: 0x000481DC
	public static void UnregisterRecursivelyS(GameObject gameObj)
	{
		if (AmplifyMotionEffectBase.m_activeObjects.ContainsKey(gameObj))
		{
			AmplifyMotionEffectBase.TryUnregister(gameObj);
		}
		foreach (object obj in gameObj.transform)
		{
			AmplifyMotionEffectBase.UnregisterRecursivelyS(((Transform)obj).gameObject);
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0004A04C File Offset: 0x0004824C
	private void UpdatePostProcess()
	{
		Camera camera = null;
		float num = float.MinValue;
		if (this.m_linkedCamerasChanged)
		{
			this.UpdateLinkedCameras();
		}
		for (int i = 0; i < this.m_linkedCameraKeys.Length; i++)
		{
			if (this.m_linkedCameraKeys[i] != null && this.m_linkedCameraKeys[i].isActiveAndEnabled && this.m_linkedCameraKeys[i].depth > num)
			{
				camera = this.m_linkedCameraKeys[i];
				num = this.m_linkedCameraKeys[i].depth;
			}
		}
		if (this.m_currentPostProcess != null && this.m_currentPostProcess.gameObject != camera.gameObject)
		{
			UnityEngine.Object.DestroyImmediate(this.m_currentPostProcess);
			this.m_currentPostProcess = null;
		}
		if (this.m_currentPostProcess == null && camera != null && camera != this.m_camera)
		{
			AmplifyMotionPostProcess[] components = base.gameObject.GetComponents<AmplifyMotionPostProcess>();
			if (components != null && components.Length != 0)
			{
				for (int j = 0; j < components.Length; j++)
				{
					UnityEngine.Object.DestroyImmediate(components[j]);
				}
			}
			this.m_currentPostProcess = camera.gameObject.AddComponent<AmplifyMotionPostProcess>();
			this.m_currentPostProcess.Instance = this;
		}
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0004A170 File Offset: 0x00048370
	private void LateUpdate()
	{
		if (this.m_baseCamera.AutoStep)
		{
			float num = Application.isPlaying ? Time.unscaledDeltaTime : Time.fixedDeltaTime;
			float fixedDeltaTime = Time.fixedDeltaTime;
			this.m_deltaTime = ((num > float.Epsilon) ? num : this.m_deltaTime);
			this.m_fixedDeltaTime = ((num > float.Epsilon) ? fixedDeltaTime : this.m_fixedDeltaTime);
		}
		this.QualitySteps = Mathf.Clamp(this.QualitySteps, 0, 16);
		this.MotionScale = Mathf.Max(this.MotionScale, 0f);
		this.MinVelocity = Mathf.Min(this.MinVelocity, this.MaxVelocity);
		this.DepthThreshold = Mathf.Max(this.DepthThreshold, 0f);
		this.MinResetDeltaDist = Mathf.Max(this.MinResetDeltaDist, 0f);
		this.MinResetDeltaDistSqr = this.MinResetDeltaDist * this.MinResetDeltaDist;
		this.ResetFrameDelay = Mathf.Max(this.ResetFrameDelay, 0);
		this.UpdatePostProcess();
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0004A26C File Offset: 0x0004846C
	public void StopAutoStep()
	{
		foreach (AmplifyMotionCamera amplifyMotionCamera in this.m_linkedCameras.Values)
		{
			amplifyMotionCamera.StopAutoStep();
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0004A2C4 File Offset: 0x000484C4
	public void StartAutoStep()
	{
		foreach (AmplifyMotionCamera amplifyMotionCamera in this.m_linkedCameras.Values)
		{
			amplifyMotionCamera.StartAutoStep();
		}
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0004A31C File Offset: 0x0004851C
	public void Step(float delta)
	{
		this.m_deltaTime = delta;
		this.m_fixedDeltaTime = delta;
		foreach (AmplifyMotionCamera amplifyMotionCamera in this.m_linkedCameras.Values)
		{
			amplifyMotionCamera.Step();
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x0004A380 File Offset: 0x00048580
	private void UpdateLinkedCameras()
	{
		Dictionary<Camera, AmplifyMotionCamera>.KeyCollection keys = this.m_linkedCameras.Keys;
		Dictionary<Camera, AmplifyMotionCamera>.ValueCollection values = this.m_linkedCameras.Values;
		if (this.m_linkedCameraKeys == null || keys.Count != this.m_linkedCameraKeys.Length)
		{
			this.m_linkedCameraKeys = new Camera[keys.Count];
		}
		if (this.m_linkedCameraValues == null || values.Count != this.m_linkedCameraValues.Length)
		{
			this.m_linkedCameraValues = new AmplifyMotionCamera[values.Count];
		}
		keys.CopyTo(this.m_linkedCameraKeys, 0);
		values.CopyTo(this.m_linkedCameraValues, 0);
		this.m_linkedCamerasChanged = false;
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0004A418 File Offset: 0x00048618
	private void FixedUpdate()
	{
		if (this.m_camera.enabled)
		{
			if (this.m_linkedCamerasChanged)
			{
				this.UpdateLinkedCameras();
			}
			this.m_fixedUpdateCB.Clear();
			for (int i = 0; i < this.m_linkedCameraValues.Length; i++)
			{
				if (this.m_linkedCameraValues[i] != null && this.m_linkedCameraValues[i].isActiveAndEnabled)
				{
					this.m_linkedCameraValues[i].FixedUpdateTransform(this, this.m_fixedUpdateCB);
				}
			}
		}
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0004A494 File Offset: 0x00048694
	private void OnPreRender()
	{
		if (this.m_camera.enabled && (Time.frameCount == 1 || Mathf.Abs(Time.unscaledDeltaTime) > 1E-45f))
		{
			if (this.m_linkedCamerasChanged)
			{
				this.UpdateLinkedCameras();
			}
			this.m_updateCB.Clear();
			for (int i = 0; i < this.m_linkedCameraValues.Length; i++)
			{
				if (this.m_linkedCameraValues[i] != null && this.m_linkedCameraValues[i].isActiveAndEnabled)
				{
					this.m_linkedCameraValues[i].UpdateTransform(this, this.m_updateCB);
				}
			}
		}
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0004A528 File Offset: 0x00048728
	private void OnPostRender()
	{
		bool qualityChanged = this.QualityLevel != this.m_qualityLevel;
		this.m_qualityLevel = this.QualityLevel;
		this.UpdateRenderTextures(qualityChanged);
		this.ResetObjectId();
		bool flag = this.CameraMotionMult > float.Epsilon;
		bool clearColor = !flag || this.m_starting;
		float num = (this.DepthThreshold > float.Epsilon) ? (1f / this.DepthThreshold) : float.MaxValue;
		this.m_motionScaleNorm = ((this.m_deltaTime >= float.Epsilon) ? (this.MotionScale * (1f / this.m_deltaTime)) : 0f);
		this.m_fixedMotionScaleNorm = ((this.m_fixedDeltaTime >= float.Epsilon) ? (this.MotionScale * (1f / this.m_fixedDeltaTime)) : 0f);
		float scale = (!this.m_starting) ? this.m_motionScaleNorm : 0f;
		float fixedScale = (!this.m_starting) ? this.m_fixedMotionScaleNorm : 0f;
		Shader.SetGlobalFloat("_AM_MIN_VELOCITY", this.MinVelocity);
		Shader.SetGlobalFloat("_AM_MAX_VELOCITY", this.MaxVelocity);
		Shader.SetGlobalFloat("_AM_RCP_TOTAL_VELOCITY", 1f / (this.MaxVelocity - this.MinVelocity));
		Shader.SetGlobalVector("_AM_DEPTH_THRESHOLD", new Vector2(this.DepthThreshold, num));
		this.m_motionRT.DiscardContents();
		this.m_baseCamera.PreRenderVectors(this.m_motionRT, clearColor, num);
		for (int i = 0; i < this.m_linkedCameraValues.Length; i++)
		{
			AmplifyMotionCamera amplifyMotionCamera = this.m_linkedCameraValues[i];
			if (amplifyMotionCamera != null && amplifyMotionCamera.Overlay && amplifyMotionCamera.isActiveAndEnabled)
			{
				amplifyMotionCamera.PreRenderVectors(this.m_motionRT, clearColor, num);
				amplifyMotionCamera.RenderVectors(scale, fixedScale, this.QualityLevel);
			}
		}
		if (flag)
		{
			float num2 = (this.m_deltaTime >= float.Epsilon) ? (this.MotionScale * this.CameraMotionMult * (1f / this.m_deltaTime)) : 0f;
			float scale2 = (!this.m_starting) ? num2 : 0f;
			this.m_motionRT.DiscardContents();
			this.m_baseCamera.RenderReprojectionVectors(this.m_motionRT, scale2);
		}
		this.m_baseCamera.RenderVectors(scale, fixedScale, this.QualityLevel);
		for (int j = 0; j < this.m_linkedCameraValues.Length; j++)
		{
			AmplifyMotionCamera amplifyMotionCamera2 = this.m_linkedCameraValues[j];
			if (amplifyMotionCamera2 != null && amplifyMotionCamera2.Overlay && amplifyMotionCamera2.isActiveAndEnabled)
			{
				amplifyMotionCamera2.RenderVectors(scale, fixedScale, this.QualityLevel);
			}
		}
		this.m_starting = false;
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0004A7C8 File Offset: 0x000489C8
	private void ApplyMotionBlur(RenderTexture source, RenderTexture destination, Vector4 blurStep)
	{
		bool flag = this.QualityLevel == Quality.Mobile;
		int pass = (int)(this.QualityLevel + (this.Noise ? 4 : 0));
		RenderTexture renderTexture = null;
		if (flag)
		{
			renderTexture = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, RenderTextureFormat.ARGB32);
			renderTexture.name = "AM-DepthTemp";
			renderTexture.wrapMode = TextureWrapMode.Clamp;
			renderTexture.filterMode = FilterMode.Point;
		}
		RenderTexture temporary = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, source.format);
		temporary.name = "AM-CombinedTemp";
		temporary.wrapMode = TextureWrapMode.Clamp;
		temporary.filterMode = FilterMode.Point;
		temporary.DiscardContents();
		this.m_combineMaterial.SetTexture("_MotionTex", this.m_motionRT);
		source.filterMode = FilterMode.Point;
		Graphics.Blit(source, temporary, this.m_combineMaterial, 0);
		this.m_blurMaterial.SetTexture("_MotionTex", this.m_motionRT);
		if (flag)
		{
			Graphics.Blit(null, renderTexture, this.m_depthMaterial, 0);
			this.m_blurMaterial.SetTexture("_DepthTex", renderTexture);
		}
		if (this.QualitySteps > 1)
		{
			RenderTexture temporary2 = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, source.format);
			temporary2.name = "AM-CombinedTemp2";
			temporary2.filterMode = FilterMode.Point;
			float num = 1f / (float)this.QualitySteps;
			float num2 = 1f;
			RenderTexture renderTexture2 = temporary;
			RenderTexture renderTexture3 = temporary2;
			for (int i = 0; i < this.QualitySteps; i++)
			{
				if (renderTexture3 != destination)
				{
					renderTexture3.DiscardContents();
				}
				this.m_blurMaterial.SetVector("_AM_BLUR_STEP", blurStep * num2);
				Graphics.Blit(renderTexture2, renderTexture3, this.m_blurMaterial, pass);
				if (i < this.QualitySteps - 2)
				{
					RenderTexture renderTexture4 = renderTexture3;
					renderTexture3 = renderTexture2;
					renderTexture2 = renderTexture4;
				}
				else
				{
					renderTexture2 = renderTexture3;
					renderTexture3 = destination;
				}
				num2 -= num;
			}
			RenderTexture.ReleaseTemporary(temporary2);
		}
		else
		{
			this.m_blurMaterial.SetVector("_AM_BLUR_STEP", blurStep);
			Graphics.Blit(temporary, destination, this.m_blurMaterial, pass);
		}
		if (flag)
		{
			this.m_combineMaterial.SetTexture("_MotionTex", this.m_motionRT);
			Graphics.Blit(source, destination, this.m_combineMaterial, 1);
		}
		RenderTexture.ReleaseTemporary(temporary);
		if (renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0004A9ED File Offset: 0x00048BED
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.m_currentPostProcess == null)
		{
			this.PostProcess(source, destination);
			return;
		}
		Graphics.Blit(source, destination);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0004AA10 File Offset: 0x00048C10
	public void PostProcess(RenderTexture source, RenderTexture destination)
	{
		Vector4 zero = Vector4.zero;
		zero.x = this.MaxVelocity / 1000f;
		zero.y = this.MaxVelocity / 1000f;
		RenderTexture renderTexture = null;
		if (QualitySettings.antiAliasing > 1)
		{
			renderTexture = RenderTexture.GetTemporary(this.m_width, this.m_height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			renderTexture.name = "AM-DilatedTemp";
			renderTexture.filterMode = FilterMode.Point;
			this.m_dilationMaterial.SetTexture("_MotionTex", this.m_motionRT);
			Graphics.Blit(this.m_motionRT, renderTexture, this.m_dilationMaterial, 0);
			this.m_dilationMaterial.SetTexture("_MotionTex", renderTexture);
			Graphics.Blit(renderTexture, this.m_motionRT, this.m_dilationMaterial, 1);
		}
		if (this.DebugMode)
		{
			this.m_debugMaterial.SetTexture("_MotionTex", this.m_motionRT);
			Graphics.Blit(source, destination, this.m_debugMaterial);
		}
		else
		{
			this.ApplyMotionBlur(source, destination, zero);
		}
		if (renderTexture != null)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	// Token: 0x040007C5 RID: 1989
	[Header("Motion Blur")]
	public Quality QualityLevel = Quality.Standard;

	// Token: 0x040007C6 RID: 1990
	public int QualitySteps = 1;

	// Token: 0x040007C7 RID: 1991
	public float MotionScale = 3f;

	// Token: 0x040007C8 RID: 1992
	public float CameraMotionMult = 1f;

	// Token: 0x040007C9 RID: 1993
	public float MinVelocity = 1f;

	// Token: 0x040007CA RID: 1994
	public float MaxVelocity = 10f;

	// Token: 0x040007CB RID: 1995
	public float DepthThreshold = 0.01f;

	// Token: 0x040007CC RID: 1996
	public bool Noise;

	// Token: 0x040007CD RID: 1997
	[Header("Camera")]
	public Camera[] OverlayCameras = new Camera[0];

	// Token: 0x040007CE RID: 1998
	public LayerMask CullingMask = -1;

	// Token: 0x040007CF RID: 1999
	[Header("Objects")]
	public bool AutoRegisterObjs = true;

	// Token: 0x040007D0 RID: 2000
	public float MinResetDeltaDist = 1000f;

	// Token: 0x040007D1 RID: 2001
	[NonSerialized]
	public float MinResetDeltaDistSqr;

	// Token: 0x040007D2 RID: 2002
	public int ResetFrameDelay = 1;

	// Token: 0x040007D3 RID: 2003
	[Header("Low-Level")]
	[FormerlySerializedAs("workerThreads")]
	public int WorkerThreads;

	// Token: 0x040007D4 RID: 2004
	public bool SystemThreadPool;

	// Token: 0x040007D5 RID: 2005
	public bool ForceCPUOnly;

	// Token: 0x040007D6 RID: 2006
	public bool DebugMode;

	// Token: 0x040007D7 RID: 2007
	private Camera m_camera;

	// Token: 0x040007D8 RID: 2008
	private bool m_starting = true;

	// Token: 0x040007D9 RID: 2009
	private int m_width;

	// Token: 0x040007DA RID: 2010
	private int m_height;

	// Token: 0x040007DB RID: 2011
	private RenderTexture m_motionRT;

	// Token: 0x040007DC RID: 2012
	private Material m_blurMaterial;

	// Token: 0x040007DD RID: 2013
	private Material m_solidVectorsMaterial;

	// Token: 0x040007DE RID: 2014
	private Material m_skinnedVectorsMaterial;

	// Token: 0x040007DF RID: 2015
	private Material m_clothVectorsMaterial;

	// Token: 0x040007E0 RID: 2016
	private Material m_reprojectionMaterial;

	// Token: 0x040007E1 RID: 2017
	private Material m_combineMaterial;

	// Token: 0x040007E2 RID: 2018
	private Material m_dilationMaterial;

	// Token: 0x040007E3 RID: 2019
	private Material m_depthMaterial;

	// Token: 0x040007E4 RID: 2020
	private Material m_debugMaterial;

	// Token: 0x040007E5 RID: 2021
	private Dictionary<Camera, AmplifyMotionCamera> m_linkedCameras = new Dictionary<Camera, AmplifyMotionCamera>();

	// Token: 0x040007E6 RID: 2022
	internal Camera[] m_linkedCameraKeys;

	// Token: 0x040007E7 RID: 2023
	internal AmplifyMotionCamera[] m_linkedCameraValues;

	// Token: 0x040007E8 RID: 2024
	internal bool m_linkedCamerasChanged = true;

	// Token: 0x040007E9 RID: 2025
	private AmplifyMotionPostProcess m_currentPostProcess;

	// Token: 0x040007EA RID: 2026
	private int m_globalObjectId = 1;

	// Token: 0x040007EB RID: 2027
	private float m_deltaTime;

	// Token: 0x040007EC RID: 2028
	private float m_fixedDeltaTime;

	// Token: 0x040007ED RID: 2029
	private float m_motionScaleNorm;

	// Token: 0x040007EE RID: 2030
	private float m_fixedMotionScaleNorm;

	// Token: 0x040007EF RID: 2031
	private Quality m_qualityLevel;

	// Token: 0x040007F0 RID: 2032
	private AmplifyMotionCamera m_baseCamera;

	// Token: 0x040007F1 RID: 2033
	private WorkerThreadPool m_workerThreadPool;

	// Token: 0x040007F2 RID: 2034
	public static Dictionary<GameObject, AmplifyMotionObjectBase> m_activeObjects = new Dictionary<GameObject, AmplifyMotionObjectBase>();

	// Token: 0x040007F3 RID: 2035
	public static Dictionary<Camera, AmplifyMotionCamera> m_activeCameras = new Dictionary<Camera, AmplifyMotionCamera>();

	// Token: 0x040007F4 RID: 2036
	private static bool m_isD3D = false;

	// Token: 0x040007F5 RID: 2037
	private bool m_canUseGPU;

	// Token: 0x040007F6 RID: 2038
	private const CameraEvent m_updateCBEvent = CameraEvent.BeforeImageEffectsOpaque;

	// Token: 0x040007F7 RID: 2039
	private CommandBuffer m_updateCB;

	// Token: 0x040007F8 RID: 2040
	private const CameraEvent m_fixedUpdateCBEvent = CameraEvent.BeforeImageEffectsOpaque;

	// Token: 0x040007F9 RID: 2041
	private CommandBuffer m_fixedUpdateCB;

	// Token: 0x040007FA RID: 2042
	private static bool m_ignoreMotionScaleWarning = false;

	// Token: 0x040007FB RID: 2043
	private static AmplifyMotionEffectBase m_firstInstance = null;
}
