using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class YandereKunScript : MonoBehaviour
{
	// Token: 0x06001D29 RID: 7465 RVA: 0x0015C8A4 File Offset: 0x0015AAA4
	private void Start()
	{
		if (!this.Kizuna)
		{
			if (this.KunHips != null)
			{
				this.KunHips.parent = this.ChanHips;
			}
			if (this.KunSpine != null)
			{
				this.KunSpine.parent = this.ChanSpine;
			}
			if (this.KunSpine1 != null)
			{
				this.KunSpine1.parent = this.ChanSpine1;
			}
			if (this.KunSpine2 != null)
			{
				this.KunSpine2.parent = this.ChanSpine2;
			}
			if (this.KunSpine3 != null)
			{
				this.KunSpine3.parent = this.ChanSpine3;
			}
			if (this.KunNeck != null)
			{
				this.KunNeck.parent = this.ChanNeck;
			}
			if (this.KunHead != null)
			{
				this.KunHead.parent = this.ChanHead;
			}
			this.KunRightUpLeg.parent = this.ChanRightUpLeg;
			this.KunRightLeg.parent = this.ChanRightLeg;
			this.KunRightFoot.parent = this.ChanRightFoot;
			this.KunRightToes.parent = this.ChanRightToes;
			this.KunLeftUpLeg.parent = this.ChanLeftUpLeg;
			this.KunLeftLeg.parent = this.ChanLeftLeg;
			this.KunLeftFoot.parent = this.ChanLeftFoot;
			this.KunLeftToes.parent = this.ChanLeftToes;
			this.KunRightShoulder.parent = this.ChanRightShoulder;
			this.KunRightArm.parent = this.ChanRightArm;
			if (this.KunRightArmRoll != null)
			{
				this.KunRightArmRoll.parent = this.ChanRightArmRoll;
			}
			this.KunRightForeArm.parent = this.ChanRightForeArm;
			if (this.KunRightForeArmRoll != null)
			{
				this.KunRightForeArmRoll.parent = this.ChanRightForeArmRoll;
			}
			this.KunRightHand.parent = this.ChanRightHand;
			this.KunLeftShoulder.parent = this.ChanLeftShoulder;
			this.KunLeftArm.parent = this.ChanLeftArm;
			if (this.KunLeftArmRoll != null)
			{
				this.KunLeftArmRoll.parent = this.ChanLeftArmRoll;
			}
			this.KunLeftForeArm.parent = this.ChanLeftForeArm;
			if (this.KunLeftForeArmRoll != null)
			{
				this.KunLeftForeArmRoll.parent = this.ChanLeftForeArmRoll;
			}
			this.KunLeftHand.parent = this.ChanLeftHand;
			if (!this.Man)
			{
				this.KunLeftHandPinky1.parent = this.ChanLeftHandPinky1;
				this.KunLeftHandPinky2.parent = this.ChanLeftHandPinky2;
				this.KunLeftHandPinky3.parent = this.ChanLeftHandPinky3;
				this.KunLeftHandRing1.parent = this.ChanLeftHandRing1;
				this.KunLeftHandRing2.parent = this.ChanLeftHandRing2;
				this.KunLeftHandRing3.parent = this.ChanLeftHandRing3;
				this.KunLeftHandMiddle1.parent = this.ChanLeftHandMiddle1;
				this.KunLeftHandMiddle2.parent = this.ChanLeftHandMiddle2;
				this.KunLeftHandMiddle3.parent = this.ChanLeftHandMiddle3;
				this.KunLeftHandIndex1.parent = this.ChanLeftHandIndex1;
				this.KunLeftHandIndex2.parent = this.ChanLeftHandIndex2;
				this.KunLeftHandIndex3.parent = this.ChanLeftHandIndex3;
				this.KunLeftHandThumb1.parent = this.ChanLeftHandThumb1;
				this.KunLeftHandThumb2.parent = this.ChanLeftHandThumb2;
				this.KunLeftHandThumb3.parent = this.ChanLeftHandThumb3;
				this.KunRightHandPinky1.parent = this.ChanRightHandPinky1;
				this.KunRightHandPinky2.parent = this.ChanRightHandPinky2;
				this.KunRightHandPinky3.parent = this.ChanRightHandPinky3;
				this.KunRightHandRing1.parent = this.ChanRightHandRing1;
				this.KunRightHandRing2.parent = this.ChanRightHandRing2;
				this.KunRightHandRing3.parent = this.ChanRightHandRing3;
				this.KunRightHandMiddle1.parent = this.ChanRightHandMiddle1;
				this.KunRightHandMiddle2.parent = this.ChanRightHandMiddle2;
				this.KunRightHandMiddle3.parent = this.ChanRightHandMiddle3;
				this.KunRightHandIndex1.parent = this.ChanRightHandIndex1;
				this.KunRightHandIndex2.parent = this.ChanRightHandIndex2;
				this.KunRightHandIndex3.parent = this.ChanRightHandIndex3;
				this.KunRightHandThumb1.parent = this.ChanRightHandThumb1;
				this.KunRightHandThumb2.parent = this.ChanRightHandThumb2;
				this.KunRightHandThumb3.parent = this.ChanRightHandThumb3;
			}
		}
		if (this.MyRenderer != null)
		{
			this.MyRenderer.enabled = true;
		}
		if (this.SecondRenderer != null)
		{
			this.SecondRenderer.enabled = true;
		}
		if (this.ThirdRenderer != null)
		{
			this.ThirdRenderer.enabled = true;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0015CD84 File Offset: 0x0015AF84
	private void LateUpdate()
	{
		if (this.Man)
		{
			this.ChanItemParent.position = this.KunItemParent.position;
			if (!this.Adjusted)
			{
				this.KunRightShoulder.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightForeArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunRightHand.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftShoulder.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftForeArm.position += new Vector3(0f, 0.1f, 0f);
				this.KunLeftHand.position += new Vector3(0f, 0.1f, 0f);
				this.Adjusted = true;
			}
		}
		if (this.Kizuna)
		{
			this.KunItemParent.localPosition = new Vector3(0.066666f, -0.033333f, 0.02f);
			this.ChanItemParent.position = this.KunItemParent.position;
			this.KunHips.localPosition = this.ChanHips.localPosition;
			if (this.KunHips != null)
			{
				this.KunHips.eulerAngles = this.ChanHips.eulerAngles;
			}
			if (this.KunSpine != null)
			{
				this.KunSpine.eulerAngles = this.ChanSpine.eulerAngles;
			}
			if (this.KunSpine1 != null)
			{
				this.KunSpine1.eulerAngles = this.ChanSpine1.eulerAngles;
			}
			if (this.KunSpine2 != null)
			{
				this.KunSpine2.eulerAngles = this.ChanSpine2.eulerAngles;
			}
			if (this.KunSpine3 != null)
			{
				this.KunSpine3.eulerAngles = this.ChanSpine3.eulerAngles;
			}
			if (this.KunNeck != null)
			{
				this.KunNeck.eulerAngles = this.ChanNeck.eulerAngles;
			}
			if (this.KunHead != null)
			{
				this.KunHead.eulerAngles = this.ChanHead.eulerAngles;
			}
			this.KunRightUpLeg.eulerAngles = this.ChanRightUpLeg.eulerAngles;
			this.KunRightLeg.eulerAngles = this.ChanRightLeg.eulerAngles;
			this.KunRightFoot.eulerAngles = this.ChanRightFoot.eulerAngles;
			this.KunRightToes.eulerAngles = this.ChanRightToes.eulerAngles;
			this.KunLeftUpLeg.eulerAngles = this.ChanLeftUpLeg.eulerAngles;
			this.KunLeftLeg.eulerAngles = this.ChanLeftLeg.eulerAngles;
			this.KunLeftFoot.eulerAngles = this.ChanLeftFoot.eulerAngles;
			this.KunLeftToes.eulerAngles = this.ChanLeftToes.eulerAngles;
			this.KunRightShoulder.eulerAngles = this.ChanRightShoulder.eulerAngles;
			this.KunRightArm.eulerAngles = this.ChanRightArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunRightArmRoll.eulerAngles = this.ChanRightArmRoll.eulerAngles;
			}
			this.KunRightForeArm.eulerAngles = this.ChanRightForeArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunRightForeArmRoll.eulerAngles = this.ChanRightForeArmRoll.eulerAngles;
			}
			this.KunRightHand.eulerAngles = this.ChanRightHand.eulerAngles;
			this.KunLeftShoulder.eulerAngles = this.ChanLeftShoulder.eulerAngles;
			this.KunLeftArm.eulerAngles = this.ChanLeftArm.eulerAngles;
			if (this.KunLeftArmRoll != null)
			{
				this.KunLeftArmRoll.eulerAngles = this.ChanLeftArmRoll.eulerAngles;
			}
			this.KunLeftForeArm.eulerAngles = this.ChanLeftForeArm.eulerAngles;
			if (this.KunLeftForeArmRoll != null)
			{
				this.KunLeftForeArmRoll.eulerAngles = this.ChanLeftForeArmRoll.eulerAngles;
			}
			this.KunLeftHand.eulerAngles = this.ChanLeftHand.eulerAngles;
			this.KunLeftHandPinky1.eulerAngles = this.ChanLeftHandPinky1.eulerAngles;
			this.KunLeftHandPinky2.eulerAngles = this.ChanLeftHandPinky2.eulerAngles;
			this.KunLeftHandPinky3.eulerAngles = this.ChanLeftHandPinky3.eulerAngles;
			this.KunLeftHandRing1.eulerAngles = this.ChanLeftHandRing1.eulerAngles;
			this.KunLeftHandRing2.eulerAngles = this.ChanLeftHandRing2.eulerAngles;
			this.KunLeftHandRing3.eulerAngles = this.ChanLeftHandRing3.eulerAngles;
			this.KunLeftHandMiddle1.eulerAngles = this.ChanLeftHandMiddle1.eulerAngles;
			this.KunLeftHandMiddle2.eulerAngles = this.ChanLeftHandMiddle2.eulerAngles;
			this.KunLeftHandMiddle3.eulerAngles = this.ChanLeftHandMiddle3.eulerAngles;
			this.KunLeftHandIndex1.eulerAngles = this.ChanLeftHandIndex1.eulerAngles;
			this.KunLeftHandIndex2.eulerAngles = this.ChanLeftHandIndex2.eulerAngles;
			this.KunLeftHandIndex3.eulerAngles = this.ChanLeftHandIndex3.eulerAngles;
			this.KunLeftHandThumb1.eulerAngles = this.ChanLeftHandThumb1.eulerAngles;
			this.KunLeftHandThumb2.eulerAngles = this.ChanLeftHandThumb2.eulerAngles;
			this.KunLeftHandThumb3.eulerAngles = this.ChanLeftHandThumb3.eulerAngles;
			this.KunRightHandPinky1.eulerAngles = this.ChanRightHandPinky1.eulerAngles;
			this.KunRightHandPinky2.eulerAngles = this.ChanRightHandPinky2.eulerAngles;
			this.KunRightHandPinky3.eulerAngles = this.ChanRightHandPinky3.eulerAngles;
			this.KunRightHandRing1.eulerAngles = this.ChanRightHandRing1.eulerAngles;
			this.KunRightHandRing2.eulerAngles = this.ChanRightHandRing2.eulerAngles;
			this.KunRightHandRing3.eulerAngles = this.ChanRightHandRing3.eulerAngles;
			this.KunRightHandMiddle1.eulerAngles = this.ChanRightHandMiddle1.eulerAngles;
			this.KunRightHandMiddle2.eulerAngles = this.ChanRightHandMiddle2.eulerAngles;
			this.KunRightHandMiddle3.eulerAngles = this.ChanRightHandMiddle3.eulerAngles;
			this.KunRightHandIndex1.eulerAngles = this.ChanRightHandIndex1.eulerAngles;
			this.KunRightHandIndex2.eulerAngles = this.ChanRightHandIndex2.eulerAngles;
			this.KunRightHandIndex3.eulerAngles = this.ChanRightHandIndex3.eulerAngles;
			this.KunRightHandThumb1.eulerAngles = this.ChanRightHandThumb1.eulerAngles;
			this.KunRightHandThumb2.eulerAngles = this.ChanRightHandThumb2.eulerAngles;
			this.KunRightHandThumb3.eulerAngles = this.ChanRightHandThumb3.eulerAngles;
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (this.ID > -1)
				{
					for (int i = 0; i < 32; i++)
					{
						this.SecondRenderer.SetBlendShapeWeight(i, 0f);
					}
					if (this.ID > 32)
					{
						this.ID = 0;
					}
					this.SecondRenderer.SetBlendShapeWeight(this.ID, 100f);
				}
				this.ID++;
			}
		}
	}

	// Token: 0x04003709 RID: 14089
	public Transform ChanItemParent;

	// Token: 0x0400370A RID: 14090
	public Transform KunItemParent;

	// Token: 0x0400370B RID: 14091
	public Transform ChanHips;

	// Token: 0x0400370C RID: 14092
	public Transform ChanSpine;

	// Token: 0x0400370D RID: 14093
	public Transform ChanSpine1;

	// Token: 0x0400370E RID: 14094
	public Transform ChanSpine2;

	// Token: 0x0400370F RID: 14095
	public Transform ChanSpine3;

	// Token: 0x04003710 RID: 14096
	public Transform ChanNeck;

	// Token: 0x04003711 RID: 14097
	public Transform ChanHead;

	// Token: 0x04003712 RID: 14098
	public Transform ChanRightUpLeg;

	// Token: 0x04003713 RID: 14099
	public Transform ChanRightLeg;

	// Token: 0x04003714 RID: 14100
	public Transform ChanRightFoot;

	// Token: 0x04003715 RID: 14101
	public Transform ChanRightToes;

	// Token: 0x04003716 RID: 14102
	public Transform ChanLeftUpLeg;

	// Token: 0x04003717 RID: 14103
	public Transform ChanLeftLeg;

	// Token: 0x04003718 RID: 14104
	public Transform ChanLeftFoot;

	// Token: 0x04003719 RID: 14105
	public Transform ChanLeftToes;

	// Token: 0x0400371A RID: 14106
	public Transform ChanRightShoulder;

	// Token: 0x0400371B RID: 14107
	public Transform ChanRightArm;

	// Token: 0x0400371C RID: 14108
	public Transform ChanRightArmRoll;

	// Token: 0x0400371D RID: 14109
	public Transform ChanRightForeArm;

	// Token: 0x0400371E RID: 14110
	public Transform ChanRightForeArmRoll;

	// Token: 0x0400371F RID: 14111
	public Transform ChanRightHand;

	// Token: 0x04003720 RID: 14112
	public Transform ChanLeftShoulder;

	// Token: 0x04003721 RID: 14113
	public Transform ChanLeftArm;

	// Token: 0x04003722 RID: 14114
	public Transform ChanLeftArmRoll;

	// Token: 0x04003723 RID: 14115
	public Transform ChanLeftForeArm;

	// Token: 0x04003724 RID: 14116
	public Transform ChanLeftForeArmRoll;

	// Token: 0x04003725 RID: 14117
	public Transform ChanLeftHand;

	// Token: 0x04003726 RID: 14118
	public Transform ChanLeftHandPinky1;

	// Token: 0x04003727 RID: 14119
	public Transform ChanLeftHandPinky2;

	// Token: 0x04003728 RID: 14120
	public Transform ChanLeftHandPinky3;

	// Token: 0x04003729 RID: 14121
	public Transform ChanLeftHandRing1;

	// Token: 0x0400372A RID: 14122
	public Transform ChanLeftHandRing2;

	// Token: 0x0400372B RID: 14123
	public Transform ChanLeftHandRing3;

	// Token: 0x0400372C RID: 14124
	public Transform ChanLeftHandMiddle1;

	// Token: 0x0400372D RID: 14125
	public Transform ChanLeftHandMiddle2;

	// Token: 0x0400372E RID: 14126
	public Transform ChanLeftHandMiddle3;

	// Token: 0x0400372F RID: 14127
	public Transform ChanLeftHandIndex1;

	// Token: 0x04003730 RID: 14128
	public Transform ChanLeftHandIndex2;

	// Token: 0x04003731 RID: 14129
	public Transform ChanLeftHandIndex3;

	// Token: 0x04003732 RID: 14130
	public Transform ChanLeftHandThumb1;

	// Token: 0x04003733 RID: 14131
	public Transform ChanLeftHandThumb2;

	// Token: 0x04003734 RID: 14132
	public Transform ChanLeftHandThumb3;

	// Token: 0x04003735 RID: 14133
	public Transform ChanRightHandPinky1;

	// Token: 0x04003736 RID: 14134
	public Transform ChanRightHandPinky2;

	// Token: 0x04003737 RID: 14135
	public Transform ChanRightHandPinky3;

	// Token: 0x04003738 RID: 14136
	public Transform ChanRightHandRing1;

	// Token: 0x04003739 RID: 14137
	public Transform ChanRightHandRing2;

	// Token: 0x0400373A RID: 14138
	public Transform ChanRightHandRing3;

	// Token: 0x0400373B RID: 14139
	public Transform ChanRightHandMiddle1;

	// Token: 0x0400373C RID: 14140
	public Transform ChanRightHandMiddle2;

	// Token: 0x0400373D RID: 14141
	public Transform ChanRightHandMiddle3;

	// Token: 0x0400373E RID: 14142
	public Transform ChanRightHandIndex1;

	// Token: 0x0400373F RID: 14143
	public Transform ChanRightHandIndex2;

	// Token: 0x04003740 RID: 14144
	public Transform ChanRightHandIndex3;

	// Token: 0x04003741 RID: 14145
	public Transform ChanRightHandThumb1;

	// Token: 0x04003742 RID: 14146
	public Transform ChanRightHandThumb2;

	// Token: 0x04003743 RID: 14147
	public Transform ChanRightHandThumb3;

	// Token: 0x04003744 RID: 14148
	public Transform KunHips;

	// Token: 0x04003745 RID: 14149
	public Transform KunSpine;

	// Token: 0x04003746 RID: 14150
	public Transform KunSpine1;

	// Token: 0x04003747 RID: 14151
	public Transform KunSpine2;

	// Token: 0x04003748 RID: 14152
	public Transform KunSpine3;

	// Token: 0x04003749 RID: 14153
	public Transform KunNeck;

	// Token: 0x0400374A RID: 14154
	public Transform KunHead;

	// Token: 0x0400374B RID: 14155
	public Transform KunRightUpLeg;

	// Token: 0x0400374C RID: 14156
	public Transform KunRightLeg;

	// Token: 0x0400374D RID: 14157
	public Transform KunRightFoot;

	// Token: 0x0400374E RID: 14158
	public Transform KunRightToes;

	// Token: 0x0400374F RID: 14159
	public Transform KunLeftUpLeg;

	// Token: 0x04003750 RID: 14160
	public Transform KunLeftLeg;

	// Token: 0x04003751 RID: 14161
	public Transform KunLeftFoot;

	// Token: 0x04003752 RID: 14162
	public Transform KunLeftToes;

	// Token: 0x04003753 RID: 14163
	public Transform KunRightShoulder;

	// Token: 0x04003754 RID: 14164
	public Transform KunRightArm;

	// Token: 0x04003755 RID: 14165
	public Transform KunRightArmRoll;

	// Token: 0x04003756 RID: 14166
	public Transform KunRightForeArm;

	// Token: 0x04003757 RID: 14167
	public Transform KunRightForeArmRoll;

	// Token: 0x04003758 RID: 14168
	public Transform KunRightHand;

	// Token: 0x04003759 RID: 14169
	public Transform KunLeftShoulder;

	// Token: 0x0400375A RID: 14170
	public Transform KunLeftArm;

	// Token: 0x0400375B RID: 14171
	public Transform KunLeftArmRoll;

	// Token: 0x0400375C RID: 14172
	public Transform KunLeftForeArm;

	// Token: 0x0400375D RID: 14173
	public Transform KunLeftForeArmRoll;

	// Token: 0x0400375E RID: 14174
	public Transform KunLeftHand;

	// Token: 0x0400375F RID: 14175
	public Transform KunLeftHandPinky1;

	// Token: 0x04003760 RID: 14176
	public Transform KunLeftHandPinky2;

	// Token: 0x04003761 RID: 14177
	public Transform KunLeftHandPinky3;

	// Token: 0x04003762 RID: 14178
	public Transform KunLeftHandRing1;

	// Token: 0x04003763 RID: 14179
	public Transform KunLeftHandRing2;

	// Token: 0x04003764 RID: 14180
	public Transform KunLeftHandRing3;

	// Token: 0x04003765 RID: 14181
	public Transform KunLeftHandMiddle1;

	// Token: 0x04003766 RID: 14182
	public Transform KunLeftHandMiddle2;

	// Token: 0x04003767 RID: 14183
	public Transform KunLeftHandMiddle3;

	// Token: 0x04003768 RID: 14184
	public Transform KunLeftHandIndex1;

	// Token: 0x04003769 RID: 14185
	public Transform KunLeftHandIndex2;

	// Token: 0x0400376A RID: 14186
	public Transform KunLeftHandIndex3;

	// Token: 0x0400376B RID: 14187
	public Transform KunLeftHandThumb1;

	// Token: 0x0400376C RID: 14188
	public Transform KunLeftHandThumb2;

	// Token: 0x0400376D RID: 14189
	public Transform KunLeftHandThumb3;

	// Token: 0x0400376E RID: 14190
	public Transform KunRightHandPinky1;

	// Token: 0x0400376F RID: 14191
	public Transform KunRightHandPinky2;

	// Token: 0x04003770 RID: 14192
	public Transform KunRightHandPinky3;

	// Token: 0x04003771 RID: 14193
	public Transform KunRightHandRing1;

	// Token: 0x04003772 RID: 14194
	public Transform KunRightHandRing2;

	// Token: 0x04003773 RID: 14195
	public Transform KunRightHandRing3;

	// Token: 0x04003774 RID: 14196
	public Transform KunRightHandMiddle1;

	// Token: 0x04003775 RID: 14197
	public Transform KunRightHandMiddle2;

	// Token: 0x04003776 RID: 14198
	public Transform KunRightHandMiddle3;

	// Token: 0x04003777 RID: 14199
	public Transform KunRightHandIndex1;

	// Token: 0x04003778 RID: 14200
	public Transform KunRightHandIndex2;

	// Token: 0x04003779 RID: 14201
	public Transform KunRightHandIndex3;

	// Token: 0x0400377A RID: 14202
	public Transform KunRightHandThumb1;

	// Token: 0x0400377B RID: 14203
	public Transform KunRightHandThumb2;

	// Token: 0x0400377C RID: 14204
	public Transform KunRightHandThumb3;

	// Token: 0x0400377D RID: 14205
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400377E RID: 14206
	public SkinnedMeshRenderer SecondRenderer;

	// Token: 0x0400377F RID: 14207
	public SkinnedMeshRenderer ThirdRenderer;

	// Token: 0x04003780 RID: 14208
	public bool Kizuna;

	// Token: 0x04003781 RID: 14209
	public bool Man;

	// Token: 0x04003782 RID: 14210
	public int ID;

	// Token: 0x04003783 RID: 14211
	private bool Adjusted;
}
