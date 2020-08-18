using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000005 RID: 5
public class DDRMinigame : MonoBehaviour
{
	// Token: 0x06000061 RID: 97 RVA: 0x00003978 File Offset: 0x00001B78
	public void LoadLevel(DDRLevel level)
	{
		this.gameplayUiParent.anchoredPosition = Vector2.zero;
		this.gameplayUiParent.rotation = Quaternion.identity;
		this.trackCache = new Dictionary<float, RectTransform>[4];
		for (int i = 0; i < this.trackCache.Length; i++)
		{
			this.trackCache[i] = new Dictionary<float, RectTransform>();
			foreach (float key in level.Tracks[i].Nodes)
			{
				RectTransform component = UnityEngine.Object.Instantiate<GameObject>(this.arrowPrefab, this.uiTracks[i]).GetComponent<RectTransform>();
				switch (i)
				{
				case 0:
					component.rotation = Quaternion.Euler(0f, 0f, 90f);
					break;
				case 1:
					component.rotation = Quaternion.Euler(0f, 0f, 180f);
					break;
				case 2:
					component.rotation = Quaternion.Euler(0f, 0f, 0f);
					break;
				case 3:
					component.rotation = Quaternion.Euler(0f, 0f, -90f);
					break;
				}
				this.trackCache[i].Add(key, component);
			}
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00003AD4 File Offset: 0x00001CD4
	public void LoadLevelSelect(DDRLevel[] levels)
	{
		this.levelSelectCache = new Dictionary<RectTransform, DDRLevel>();
		this.levels = levels;
		for (int i = 0; i < levels.Length; i++)
		{
			RectTransform component = UnityEngine.Object.Instantiate<GameObject>(this.levelIconPrefab, this.levelSelectParent).GetComponent<RectTransform>();
			component.GetComponent<Image>().sprite = levels[i].LevelIcon;
			this.levelSelectCache.Add(component, levels[i]);
		}
		this.positionLevels(true);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00003B44 File Offset: 0x00001D44
	public void UnloadLevelSelect()
	{
		foreach (KeyValuePair<RectTransform, DDRLevel> keyValuePair in this.levelSelectCache)
		{
			UnityEngine.Object.Destroy(keyValuePair.Key.gameObject);
		}
		this.levelSelectCache = new Dictionary<RectTransform, DDRLevel>();
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00003BAC File Offset: 0x00001DAC
	public void UpdateLevelSelect()
	{
		if (this.inputManager.TappedLeft)
		{
			this.levelSelectScroll -= 1f;
		}
		else if (this.inputManager.TappedRight)
		{
			this.levelSelectScroll += 1f;
		}
		this.levelSelectScroll = Mathf.Clamp(this.levelSelectScroll, 0f, (float)(this.levels.Length - 1));
		this.selectedLevel = (int)Mathf.Round(this.levelSelectScroll);
		this.positionLevels(false);
		if (Input.GetButtonDown("A"))
		{
			this.manager.LoadedLevel = this.levels[this.selectedLevel];
		}
		if (Input.GetButtonDown("B"))
		{
			this.manager.BootOut();
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00003C70 File Offset: 0x00001E70
	private void positionLevels(bool instant = false)
	{
		for (int i = 0; i < this.levelSelectCache.Keys.Count; i++)
		{
			RectTransform key = this.levelSelectCache.ElementAt(i).Key;
			Vector2 vector = new Vector2((float)(-(float)this.selectedLevel * 400 + i * 400), 0f);
			key.anchoredPosition = (instant ? vector : Vector2.Lerp(key.anchoredPosition, vector, 10f * Time.deltaTime));
			this.levelNameLabel.text = this.levels[this.selectedLevel].LevelName;
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00003D14 File Offset: 0x00001F14
	public void UpdateGame(float time)
	{
		if (this.trackCache == null)
		{
			return;
		}
		bool flag = this.manager.GameState.FinishStatus == DDRFinishStatus.Failed;
		if (!flag)
		{
			this.pollInput(time);
			this.gameplayUiParent.anchoredPosition = Vector2.Lerp(this.gameplayUiParent.anchoredPosition, Vector2.zero, 10f * Time.deltaTime);
			this.gameplayUiParent.rotation = Quaternion.identity;
		}
		else
		{
			this.gameplayUiParent.anchoredPosition += Vector2.down * 50f * Time.deltaTime;
			this.gameplayUiParent.Rotate(Vector3.forward * -2f * Time.deltaTime);
			this.shakeUi(0.5f);
		}
		this.healthImage.fillAmount = Mathf.Lerp(this.healthImage.fillAmount, this.manager.GameState.Health / 100f, 10f * Time.deltaTime);
		for (int i = 0; i < this.trackCache.Length; i++)
		{
			Dictionary<float, RectTransform> dictionary = this.trackCache[i];
			foreach (float num in dictionary.Keys)
			{
				float num2 = num - time;
				if (num2 < -0.05f)
				{
					if (!flag)
					{
						this.displayHitRating(i, DDRRating.Miss);
					}
					this.assignPoints(DDRRating.Miss);
					this.updateCombo(DDRRating.Miss);
					this.removeNodeAt(this.trackCache.ToList<Dictionary<float, RectTransform>>().IndexOf(dictionary), 0f);
					return;
				}
				dictionary[num].anchoredPosition = new Vector2(0f, -num2 * this.speed) + this.offset;
			}
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00003EF8 File Offset: 0x000020F8
	public void UpdateEndcard(GameState state)
	{
		this.scoreText.text = string.Format("Score: {0}", state.Score);
		Color color;
		this.rankText.text = this.getRank(state, out color);
		this.rankText.color = color;
		this.longestComboText.text = string.Format("Biggest combo: {0}", state.LongestCombo.ToString());
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00003F68 File Offset: 0x00002168
	private DDRRating getRating(int track, float time)
	{
		float num;
		RectTransform rectTransform;
		this.getFirstNodeOn(track, out num, out rectTransform);
		DDRRating result = DDRRating.Miss;
		float num2 = this.offset.y - rectTransform.localPosition.y;
		if (num2 < 130f)
		{
			result = DDRRating.Early;
			if (num2 < 75f)
			{
				result = DDRRating.Ok;
			}
			if (num2 < 65f)
			{
				result = DDRRating.Good;
			}
			if (num2 < 50f)
			{
				result = DDRRating.Great;
			}
			if (num2 < 30f)
			{
				result = DDRRating.Perfect;
			}
			if (num2 < -30f)
			{
				result = DDRRating.Ok;
			}
			if (num2 < -130f)
			{
				result = DDRRating.Miss;
			}
		}
		return result;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003FE4 File Offset: 0x000021E4
	private string getRank(GameState state, out Color resultColor)
	{
		string result = "F";
		int num = 0;
		int perfectScorePoints = this.manager.LoadedLevel.PerfectScorePoints;
		foreach (DDRTrack ddrtrack in this.manager.LoadedLevel.Tracks)
		{
			num += ddrtrack.Nodes.Count * perfectScorePoints;
		}
		float num2 = (float)state.Score / (float)num * 100f;
		if (num2 >= 30f)
		{
			result = "D";
		}
		if (num2 >= 50f)
		{
			result = "C";
		}
		if (num2 >= 75f)
		{
			result = "B";
		}
		if (num2 >= 80f)
		{
			result = "A";
		}
		if (num2 >= 95f)
		{
			result = "S";
		}
		if (num2 >= 100f)
		{
			result = "S+";
		}
		resultColor = Color.Lerp(Color.red, Color.green, num2 / 100f);
		return result;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x000040CC File Offset: 0x000022CC
	private void pollInput(float time)
	{
		if (this.inputManager.TappedLeft)
		{
			this.registerKeypress(0, time);
		}
		if (this.inputManager.TappedDown)
		{
			this.registerKeypress(1, time);
		}
		if (this.inputManager.TappedUp)
		{
			this.registerKeypress(2, time);
		}
		if (this.inputManager.TappedRight)
		{
			this.registerKeypress(3, time);
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004130 File Offset: 0x00002330
	private void registerKeypress(int track, float time)
	{
		DDRRating rating = this.getRating(track, time);
		this.displayHitRating(track, rating);
		this.assignPoints(rating);
		this.registerRating(rating);
		this.updateCombo(rating);
		if (rating != DDRRating.Miss)
		{
			this.removeNodeAt(track, 0f);
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00004174 File Offset: 0x00002374
	private void registerRating(DDRRating rating)
	{
		Dictionary<DDRRating, int> ratings = this.manager.GameState.Ratings;
		ratings[rating]++;
		from x in this.manager.GameState.Ratings
		orderby x.Value
		select x;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000041DC File Offset: 0x000023DC
	private void updateCombo(DDRRating rating)
	{
		this.comboText.text = "";
		this.comboText.color = Color.white;
		this.comboText.GetComponent<Animation>().Play();
		if (rating != DDRRating.Miss && rating != DDRRating.Early)
		{
			this.manager.GameState.Combo++;
			if (this.manager.GameState.Combo > this.manager.GameState.LongestCombo)
			{
				this.manager.GameState.LongestCombo = this.manager.GameState.Combo;
				this.comboText.color = Color.yellow;
			}
			if (this.manager.GameState.Combo >= 2)
			{
				this.comboText.text = string.Format("x{0} combo", this.manager.GameState.Combo);
				this.comboText.color = Color.white;
				return;
			}
		}
		else
		{
			this.manager.GameState.Combo = 0;
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000042F4 File Offset: 0x000024F4
	private void removeNodeAt(int trackId, float delay = 0f)
	{
		Dictionary<float, RectTransform> dictionary = this.trackCache[trackId];
		float[] array = dictionary.Keys.ToArray<float>();
		Array.Sort<float>(array, (float a, float b) => a.CompareTo(b));
		UnityEngine.Object.Destroy(dictionary[array[0]].gameObject, delay);
		dictionary.Remove(array[0]);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00004358 File Offset: 0x00002558
	private void getFirstNodeOn(int track, out float time, out RectTransform rect)
	{
		Dictionary<float, RectTransform> dictionary = this.trackCache[track];
		float[] array = dictionary.Keys.ToArray<float>();
		Array.Sort<float>(array, (float a, float b) => a.CompareTo(b));
		time = array[0];
		rect = dictionary[time];
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000043B0 File Offset: 0x000025B0
	private void displayHitRating(int track, DDRRating rating)
	{
		Text component = UnityEngine.Object.Instantiate<GameObject>(this.ratingTextPrefab, this.uiTracks[track]).GetComponent<Text>();
		component.rectTransform.anchoredPosition = new Vector2(0f, 280f);
		switch (rating)
		{
		case DDRRating.Perfect:
			component.text = "Perfect";
			component.color = this.perfectColor;
			break;
		case DDRRating.Great:
			component.text = "Great";
			component.color = this.greatColor;
			break;
		case DDRRating.Good:
			component.text = "Good";
			component.color = this.goodColor;
			break;
		case DDRRating.Ok:
			component.text = "Ok";
			component.color = this.okColor;
			break;
		case DDRRating.Miss:
			component.text = "Miss";
			component.color = Color.red;
			this.shakeUi(5f);
			break;
		case DDRRating.Early:
			component.text = "Early";
			component.color = this.earlyColor;
			break;
		}
		UnityEngine.Object.Destroy(component, 1f);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000044BC File Offset: 0x000026BC
	private void assignPoints(DDRRating rating)
	{
		int num = 0;
		switch (rating)
		{
		case DDRRating.Perfect:
			num = this.manager.LoadedLevel.PerfectScorePoints;
			break;
		case DDRRating.Great:
			num = this.manager.LoadedLevel.GreatScorePoints;
			break;
		case DDRRating.Good:
			num = this.manager.LoadedLevel.GoodScorePoints;
			break;
		case DDRRating.Ok:
			num = this.manager.LoadedLevel.OkScorePoints;
			break;
		case DDRRating.Miss:
			num = this.manager.LoadedLevel.MissScorePoints;
			break;
		case DDRRating.Early:
			num = this.manager.LoadedLevel.EarlyScorePoints;
			break;
		}
		if (rating != DDRRating.Miss)
		{
			this.manager.GameState.Score += num;
		}
		this.manager.GameState.Health += (float)num;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00004590 File Offset: 0x00002790
	private void shakeUi(float factor)
	{
		Vector2 b = new Vector2(UnityEngine.Random.Range(-factor, factor), UnityEngine.Random.Range(-factor, factor));
		this.gameplayUiParent.anchoredPosition += b;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000045CB File Offset: 0x000027CB
	public void Unload()
	{
		this.UnloadLevelSelect();
	}

	// Token: 0x04000064 RID: 100
	[Header("General")]
	[SerializeField]
	private DDRManager manager;

	// Token: 0x04000065 RID: 101
	[SerializeField]
	private InputManagerScript inputManager;

	// Token: 0x04000066 RID: 102
	[Header("Level select")]
	[SerializeField]
	private GameObject levelIconPrefab;

	// Token: 0x04000067 RID: 103
	[SerializeField]
	private RectTransform levelSelectParent;

	// Token: 0x04000068 RID: 104
	[SerializeField]
	private Text levelNameLabel;

	// Token: 0x04000069 RID: 105
	private DDRLevel[] levels;

	// Token: 0x0400006A RID: 106
	[Header("Gameplay")]
	[SerializeField]
	private Text comboText;

	// Token: 0x0400006B RID: 107
	[SerializeField]
	private Text longestComboText;

	// Token: 0x0400006C RID: 108
	[SerializeField]
	private Text rankText;

	// Token: 0x0400006D RID: 109
	[SerializeField]
	private Text scoreText;

	// Token: 0x0400006E RID: 110
	[SerializeField]
	private Image healthImage;

	// Token: 0x0400006F RID: 111
	[SerializeField]
	private GameObject arrowPrefab;

	// Token: 0x04000070 RID: 112
	[SerializeField]
	private GameObject ratingTextPrefab;

	// Token: 0x04000071 RID: 113
	[SerializeField]
	private RectTransform gameplayUiParent;

	// Token: 0x04000072 RID: 114
	[SerializeField]
	private RectTransform[] uiTracks;

	// Token: 0x04000073 RID: 115
	[SerializeField]
	private Vector2 offset;

	// Token: 0x04000074 RID: 116
	[SerializeField]
	private float speed;

	// Token: 0x04000075 RID: 117
	[Header("Colors")]
	[SerializeField]
	private Color perfectColor;

	// Token: 0x04000076 RID: 118
	[SerializeField]
	private Color greatColor;

	// Token: 0x04000077 RID: 119
	[SerializeField]
	private Color goodColor;

	// Token: 0x04000078 RID: 120
	[SerializeField]
	private Color okColor;

	// Token: 0x04000079 RID: 121
	[SerializeField]
	private Color earlyColor;

	// Token: 0x0400007A RID: 122
	private float levelSelectScroll;

	// Token: 0x0400007B RID: 123
	private int selectedLevel;

	// Token: 0x0400007C RID: 124
	private Dictionary<RectTransform, DDRLevel> levelSelectCache;

	// Token: 0x0400007D RID: 125
	private Dictionary<float, RectTransform>[] trackCache;
}
