using System;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class InterestManagerScript : MonoBehaviour
{
	// Token: 0x06001775 RID: 6005 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Start()
	{
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x000CADBC File Offset: 0x000C8FBC
	private void Update()
	{
		if (this.Yandere.Follower != null)
		{
			int studentID = this.Yandere.Follower.StudentID;
			for (int i = 1; i < 11; i++)
			{
				if (!ConversationGlobals.GetTopicLearnedByStudent(i, studentID) && Vector3.Distance(this.Yandere.Follower.transform.position, this.Clubs[i].position) < 4f)
				{
					this.Yandere.NotificationManager.TopicName = this.TopicNames[i];
					if (!ConversationGlobals.GetTopicDiscovered(i))
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
						ConversationGlobals.SetTopicDiscovered(i, true);
					}
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(i, studentID, true);
				}
			}
			if (!ConversationGlobals.GetTopicLearnedByStudent(11, studentID) && Vector3.Distance(this.Yandere.Follower.transform.position, this.Clubs[11].position) < 4f)
			{
				if (!ConversationGlobals.GetTopicDiscovered(11))
				{
					this.Yandere.NotificationManager.TopicName = "Video Games";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					this.Yandere.NotificationManager.TopicName = "Anime";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					this.Yandere.NotificationManager.TopicName = "Cosplay";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					this.Yandere.NotificationManager.TopicName = "Memes";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					ConversationGlobals.SetTopicDiscovered(11, true);
					ConversationGlobals.SetTopicDiscovered(12, true);
					ConversationGlobals.SetTopicDiscovered(13, true);
					ConversationGlobals.SetTopicDiscovered(14, true);
				}
				this.Yandere.NotificationManager.TopicName = "Video Games";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				this.Yandere.NotificationManager.TopicName = "Anime";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				this.Yandere.NotificationManager.TopicName = "Cosplay";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				this.Yandere.NotificationManager.TopicName = "Memes";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				ConversationGlobals.SetTopicLearnedByStudent(11, studentID, true);
				ConversationGlobals.SetTopicLearnedByStudent(12, studentID, true);
				ConversationGlobals.SetTopicLearnedByStudent(13, studentID, true);
				ConversationGlobals.SetTopicLearnedByStudent(14, studentID, true);
			}
			if (!ConversationGlobals.GetTopicLearnedByStudent(15, studentID) && Vector3.Distance(this.Yandere.Follower.transform.position, this.Kitten.position) < 2.5f)
			{
				this.Yandere.NotificationManager.TopicName = "Cats";
				if (!ConversationGlobals.GetTopicDiscovered(15))
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					ConversationGlobals.SetTopicDiscovered(15, true);
				}
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				ConversationGlobals.SetTopicLearnedByStudent(15, studentID, true);
			}
			if (!ConversationGlobals.GetTopicLearnedByStudent(16, studentID) && Vector3.Distance(this.Yandere.Follower.transform.position, this.Clubs[6].position) < 4f)
			{
				this.Yandere.NotificationManager.TopicName = "Justice";
				if (!ConversationGlobals.GetTopicDiscovered(16))
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					ConversationGlobals.SetTopicDiscovered(16, true);
				}
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				ConversationGlobals.SetTopicLearnedByStudent(16, studentID, true);
			}
			if (!ConversationGlobals.GetTopicLearnedByStudent(17, studentID) && Vector3.Distance(this.Yandere.Follower.transform.position, this.DelinquentZone.position) < 4f)
			{
				this.Yandere.NotificationManager.TopicName = "Violence";
				if (!ConversationGlobals.GetTopicDiscovered(17))
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					ConversationGlobals.SetTopicDiscovered(17, true);
				}
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				ConversationGlobals.SetTopicLearnedByStudent(17, studentID, true);
			}
			if (!ConversationGlobals.GetTopicLearnedByStudent(18, studentID) && Vector3.Distance(this.Yandere.Follower.transform.position, this.Library.position) < 4f)
			{
				this.Yandere.NotificationManager.TopicName = "Reading";
				if (!ConversationGlobals.GetTopicDiscovered(18))
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					ConversationGlobals.SetTopicDiscovered(18, true);
				}
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				ConversationGlobals.SetTopicLearnedByStudent(18, studentID, true);
			}
		}
	}

	// Token: 0x040020C8 RID: 8392
	public StudentManagerScript StudentManager;

	// Token: 0x040020C9 RID: 8393
	public YandereScript Yandere;

	// Token: 0x040020CA RID: 8394
	public Transform[] Clubs;

	// Token: 0x040020CB RID: 8395
	public Transform DelinquentZone;

	// Token: 0x040020CC RID: 8396
	public Transform Library;

	// Token: 0x040020CD RID: 8397
	public Transform Kitten;

	// Token: 0x040020CE RID: 8398
	public string[] TopicNames;
}
