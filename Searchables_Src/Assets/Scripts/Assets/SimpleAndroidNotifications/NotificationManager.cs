using System;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
	public static class NotificationManager
	{
		private const string FullClassName = "com.hippogames.simpleandroidnotifications.Controller";

		private const string MainActivityClassName = "com.unity3d.player.UnityPlayerActivity";

		public static int Send(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = NotificationIcon.Bell)
		{
			return NotificationManager.SendCustom(new NotificationParams
			{
				Id = UnityEngine.Random.Range(0, 2147483647),
				Delay = delay,
				Title = title,
				Message = message,
				Ticker = message,
				Sound = true,
				Vibrate = true,
				Light = true,
				SmallIcon = smallIcon,
				SmallIconColor = smallIconColor,
				LargeIcon = string.Empty
			});
		}

		public static int SendWithAppIcon(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = NotificationIcon.Bell)
		{
			return NotificationManager.SendCustom(new NotificationParams
			{
				Id = UnityEngine.Random.Range(0, 2147483647),
				Delay = delay,
				Title = title,
				Message = message,
				Ticker = message,
				Sound = true,
				Vibrate = true,
				Light = true,
				SmallIcon = smallIcon,
				SmallIconColor = smallIconColor,
				LargeIcon = "app_icon"
			});
		}

		public static int SendCustom(NotificationParams notificationParams)
		{
			long num = (long)notificationParams.Delay.TotalMilliseconds;
			new AndroidJavaClass("com.hippogames.simpleandroidnotifications.Controller").CallStatic("SetNotification", new object[]
			{
				notificationParams.Id,
				num,
				notificationParams.Title,
				notificationParams.Message,
				notificationParams.Ticker,
				(!notificationParams.Sound) ? 0 : 1,
				(!notificationParams.Vibrate) ? 0 : 1,
				(!notificationParams.Light) ? 0 : 1,
				notificationParams.LargeIcon,
				NotificationManager.GetSmallIconName(notificationParams.SmallIcon),
				NotificationManager.ColotToInt(notificationParams.SmallIconColor),
				"com.unity3d.player.UnityPlayerActivity"
			});
			return notificationParams.Id;
		}

		public static void Cancel(int id)
		{
			new AndroidJavaClass("com.hippogames.simpleandroidnotifications.Controller").CallStatic("CancelScheduledNotification", new object[]
			{
				id
			});
		}

		public static void CancelAll()
		{
			new AndroidJavaClass("com.hippogames.simpleandroidnotifications.Controller").CallStatic("CancelAllScheduledNotifications", new object[0]);
		}

		private static int ColotToInt(Color color)
		{
			Color32 color2 = color;
			return (int)color2.r * 65536 + (int)color2.g * 256 + (int)color2.b;
		}

		private static string GetSmallIconName(NotificationIcon icon)
		{
			return "anp_" + icon.ToString().ToLower();
		}
	}
}
