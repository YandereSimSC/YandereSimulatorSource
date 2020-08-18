using System;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class MirrorScript : MonoBehaviour
{
	// Token: 0x0600182B RID: 6187 RVA: 0x000D8047 File Offset: 0x000D6247
	private void Start()
	{
		this.Limit = this.Idles.Length - 1;
		if (ClubGlobals.Club == ClubType.Delinquent)
		{
			this.ID = 10;
			if (this.Prompt.Yandere.Persona != YanderePersonaType.Tough)
			{
				this.UpdatePersona();
			}
		}
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x000D8084 File Offset: 0x000D6284
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Health > 0)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.ID++;
				if (this.ID == this.Limit)
				{
					this.ID = 0;
				}
				this.UpdatePersona();
				return;
			}
		}
		else if (this.Prompt.Circle[1].fillAmount == 0f && this.Prompt.Yandere.Health > 0)
		{
			this.Prompt.Circle[1].fillAmount = 1f;
			this.ID--;
			if (this.ID < 0)
			{
				this.ID = this.Limit - 1;
			}
			this.UpdatePersona();
		}
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x000D8170 File Offset: 0x000D6370
	private void UpdatePersona()
	{
		if (!this.Prompt.Yandere.Carrying)
		{
			this.Prompt.Yandere.NotificationManager.PersonaName = this.Personas[this.ID];
			this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Persona);
			this.Prompt.Yandere.IdleAnim = this.Idles[this.ID];
			this.Prompt.Yandere.WalkAnim = this.Walks[this.ID];
			this.Prompt.Yandere.UpdatePersona(this.ID);
		}
		this.Prompt.Yandere.OriginalIdleAnim = this.Idles[this.ID];
		this.Prompt.Yandere.OriginalWalkAnim = this.Walks[this.ID];
		this.Prompt.Yandere.StudentManager.UpdatePerception();
	}

	// Token: 0x04002303 RID: 8963
	public PromptScript Prompt;

	// Token: 0x04002304 RID: 8964
	public string[] Personas;

	// Token: 0x04002305 RID: 8965
	public string[] Idles;

	// Token: 0x04002306 RID: 8966
	public string[] Walks;

	// Token: 0x04002307 RID: 8967
	public int ID;

	// Token: 0x04002308 RID: 8968
	public int Limit;
}
