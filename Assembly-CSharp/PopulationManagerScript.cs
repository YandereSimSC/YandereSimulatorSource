using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class PopulationManagerScript : MonoBehaviour
{
	// Token: 0x06001912 RID: 6418 RVA: 0x000EC49C File Offset: 0x000EA69C
	public Vector3 GetCrowdedLocation()
	{
		AreaScript crowdedArea = this.GetCrowdedArea();
		Vector3 position = crowdedArea.transform.position;
		Vector3 vector = new Vector3(0f, 0f, 0f);
		float num = 0f;
		foreach (StudentScript studentScript in crowdedArea.Students)
		{
			vector += new Vector3(studentScript.transform.position.x, 0f, studentScript.transform.position.z);
			num += 1f;
		}
		vector /= num;
		int num2;
		if (position.y >= 0f && position.y < 4f)
		{
			num2 = 0;
		}
		else if (position.y >= 4f && position.y < 8f)
		{
			num2 = 4;
		}
		else if (position.y >= 8f && position.y < 12f)
		{
			num2 = 8;
		}
		else
		{
			num2 = 12;
		}
		return new Vector3(vector.x, (float)num2, vector.z);
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x000EC5CC File Offset: 0x000EA7CC
	public AreaScript GetCrowdedArea()
	{
		AreaScript result = null;
		float num = 0f;
		foreach (AreaScript areaScript in this._definedAreas)
		{
			int population = areaScript.Population;
			if ((float)population > num)
			{
				num = (float)population;
				result = areaScript;
			}
		}
		return result;
	}

	// Token: 0x040025E7 RID: 9703
	[Tooltip("All defined areas should go in here. If your area is not in here, it will not count as an actual area.")]
	[SerializeField]
	private List<AreaScript> _definedAreas;

	// Token: 0x040025E8 RID: 9704
	public Transform Cube;
}
