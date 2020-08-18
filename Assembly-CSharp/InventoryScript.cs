using System;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class InventoryScript : MonoBehaviour
{
	// Token: 0x06001786 RID: 6022 RVA: 0x000CEC2C File Offset: 0x000CCE2C
	private void Start()
	{
		this.PantyShots = PlayerGlobals.PantyShots;
		this.Money = PlayerGlobals.Money;
		this.UpdateMoney();
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000CEC4A File Offset: 0x000CCE4A
	public void UpdateMoney()
	{
		this.MoneyLabel.text = "$" + this.Money.ToString("F2");
	}

	// Token: 0x04002147 RID: 8519
	public SchemesScript Schemes;

	// Token: 0x04002148 RID: 8520
	public bool ModifiedUniform;

	// Token: 0x04002149 RID: 8521
	public bool DirectionalMic;

	// Token: 0x0400214A RID: 8522
	public bool DuplicateSheet;

	// Token: 0x0400214B RID: 8523
	public bool AnswerSheet;

	// Token: 0x0400214C RID: 8524
	public bool MaskingTape;

	// Token: 0x0400214D RID: 8525
	public bool RivalPhone;

	// Token: 0x0400214E RID: 8526
	public bool LockPick;

	// Token: 0x0400214F RID: 8527
	public bool Headset;

	// Token: 0x04002150 RID: 8528
	public bool FakeID;

	// Token: 0x04002151 RID: 8529
	public bool IDCard;

	// Token: 0x04002152 RID: 8530
	public bool Book;

	// Token: 0x04002153 RID: 8531
	public bool AmnesiaBomb;

	// Token: 0x04002154 RID: 8532
	public bool SmokeBomb;

	// Token: 0x04002155 RID: 8533
	public bool StinkBomb;

	// Token: 0x04002156 RID: 8534
	public bool LethalPoison;

	// Token: 0x04002157 RID: 8535
	public bool ChemicalPoison;

	// Token: 0x04002158 RID: 8536
	public bool EmeticPoison;

	// Token: 0x04002159 RID: 8537
	public bool RatPoison;

	// Token: 0x0400215A RID: 8538
	public bool HeadachePoison;

	// Token: 0x0400215B RID: 8539
	public bool Tranquilizer;

	// Token: 0x0400215C RID: 8540
	public bool Sedative;

	// Token: 0x0400215D RID: 8541
	public bool Cigs;

	// Token: 0x0400215E RID: 8542
	public bool Ring;

	// Token: 0x0400215F RID: 8543
	public bool Rose;

	// Token: 0x04002160 RID: 8544
	public bool Sake;

	// Token: 0x04002161 RID: 8545
	public bool Soda;

	// Token: 0x04002162 RID: 8546
	public bool Bra;

	// Token: 0x04002163 RID: 8547
	public bool CabinetKey;

	// Token: 0x04002164 RID: 8548
	public bool CaseKey;

	// Token: 0x04002165 RID: 8549
	public bool SafeKey;

	// Token: 0x04002166 RID: 8550
	public bool ShedKey;

	// Token: 0x04002167 RID: 8551
	public int MysteriousKeys;

	// Token: 0x04002168 RID: 8552
	public int RivalPhoneID;

	// Token: 0x04002169 RID: 8553
	public int PantyShots;

	// Token: 0x0400216A RID: 8554
	public float Money;

	// Token: 0x0400216B RID: 8555
	public bool[] ShrineCollectibles;

	// Token: 0x0400216C RID: 8556
	public UILabel MoneyLabel;
}
