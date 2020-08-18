using System;
using MaidDereMinigame.Malee;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidDereMinigame
{
	// Token: 0x02000501 RID: 1281
	[CreateAssetMenu(fileName = "New Scene Wrapper", menuName = "Scenes/New Scene Wrapper")]
	public class SceneWrapper : ScriptableObject
	{
		// Token: 0x06002023 RID: 8227 RVA: 0x001857A8 File Offset: 0x001839A8
		public SceneObject GetSceneByBuildIndex(int buildIndex)
		{
			foreach (SceneObject sceneObject in this.m_Scenes)
			{
				if (sceneObject.sceneBuildNumber == buildIndex)
				{
					return sceneObject;
				}
			}
			return null;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00185800 File Offset: 0x00183A00
		public SceneObject GetSceneByName(string name)
		{
			foreach (SceneObject sceneObject in this.m_Scenes)
			{
				if (sceneObject.name == name)
				{
					return sceneObject;
				}
			}
			return null;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0018585C File Offset: 0x00183A5C
		public static void LoadScene(SceneObject sceneObject)
		{
			GameController.Scenes.LoadLevel(sceneObject);
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x0018586C File Offset: 0x00183A6C
		public void LoadLevel(SceneObject sceneObject)
		{
			int num = -1;
			for (int i = 0; i < this.m_Scenes.Length; i++)
			{
				if (this.m_Scenes[i] == sceneObject)
				{
					num = this.m_Scenes[i].sceneBuildNumber;
				}
			}
			if (num == -1)
			{
				Debug.LogError("Scene could not be found. Is it in the Scene Wrapper?");
				return;
			}
			SceneManager.LoadScene(num);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x001858CC File Offset: 0x00183ACC
		public int GetSceneID(SceneObject scene)
		{
			for (int i = 0; i < this.m_Scenes.Count; i++)
			{
				if (this.m_Scenes[i] == scene)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00185906 File Offset: 0x00183B06
		public SceneObject GetSceneByIndex(int scene)
		{
			return this.m_Scenes[scene];
		}

		// Token: 0x04003DE0 RID: 15840
		[Reorderable]
		public SceneObjectMetaData m_Scenes;
	}
}
