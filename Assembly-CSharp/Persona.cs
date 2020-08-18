using System;
using UnityEngine;

// Token: 0x02000289 RID: 649
[Serializable]
public class Persona
{
	// Token: 0x060013C0 RID: 5056 RVA: 0x000AC409 File Offset: 0x000AA609
	public Persona(PersonaType type)
	{
		this.type = type;
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000AC418 File Offset: 0x000AA618
	public PersonaType Type
	{
		get
		{
			return this.type;
		}
	}

	// Token: 0x04001B80 RID: 7040
	[SerializeField]
	private PersonaType type;

	// Token: 0x04001B81 RID: 7041
	public static readonly PersonaTypeAndStringDictionary PersonaNames = new PersonaTypeAndStringDictionary
	{
		{
			PersonaType.None,
			"None"
		},
		{
			PersonaType.Loner,
			"Loner"
		},
		{
			PersonaType.TeachersPet,
			"Teacher's Pet"
		},
		{
			PersonaType.Heroic,
			"Heroic"
		},
		{
			PersonaType.Coward,
			"Coward"
		},
		{
			PersonaType.Evil,
			"Evil"
		},
		{
			PersonaType.SocialButterfly,
			"Social Butterfly"
		},
		{
			PersonaType.Lovestruck,
			"Lovestruck"
		},
		{
			PersonaType.Dangerous,
			"Dangerous"
		},
		{
			PersonaType.Strict,
			"Strict"
		},
		{
			PersonaType.PhoneAddict,
			"Phone Addict"
		},
		{
			PersonaType.Fragile,
			"Fragile"
		},
		{
			PersonaType.Spiteful,
			"Spiteful"
		},
		{
			PersonaType.Sleuth,
			"Sleuth"
		},
		{
			PersonaType.Vengeful,
			"Vengeful"
		},
		{
			PersonaType.Protective,
			"Protective"
		},
		{
			PersonaType.Violent,
			"Violent"
		},
		{
			PersonaType.Nemesis,
			"?????"
		}
	};
}
