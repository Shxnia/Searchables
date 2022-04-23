using Assets.SimpleAndroidNotifications;
//using GooglePlayGames;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WordSearch
{
	public class Global : SingletonComponent<Global>
	{
		private string toastString;

		private AndroidJavaObject currentActivity;

		private static Action<bool> __f__am_cache0;

		private void Start()
		{
		//PlayGamesPlatform.Activate();
			this.ConneterGoogle();
			this.CancelAll();
			if (SingletonComponent<WordSearchController>.Instance.IsPushAlert == 1)
			{
				this.ScheduleCustom();
			}
		}

		public void ConneterGoogle()
		{
			Social.localUser.Authenticate(delegate(bool success)
			{
				if (success)
				{
					Entity.google_login_flag = false;
				}
				else
				{
					Entity.google_login_flag = true;
				}
			});
		}

		public void ShareAndroidText()
		{
			string empty = string.Empty;
			string text = "Word Search of God \r\n\r\n\r\n\r\nLet's play 'Word Search'! \r\n\r\nDownload from Google Play for free.\r\n\r\nhttps://play.google.com/store/apps/details?id=com.twentysevengames.android.puzzle.wordsearchofgod";
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]);
			androidJavaObject.Call<AndroidJavaObject>("setAction", new object[]
			{
				androidJavaClass.GetStatic<string>("ACTION_SEND")
			});
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
			{
				"text/plain"
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
				empty
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TITLE"),
				empty
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
				text
			});
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
			@static.Call("startActivity", new object[]
			{
				androidJavaObject
			});
		}

		public void ShowAchievement()
		{
			UnityEngine.Debug.Log("ShowAchievement");
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				this.MyShowToastMethod("Unconnected to the network.");
				return;
			}
			if (Social.localUser.userName == string.Empty)
			{
				this.ConneterGoogle();
			}
			Social.ShowAchievementsUI();
		}

		public void go_market()
		{
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				this.MyShowToastMethod("Unconnected to the network.");
				return;
			}
			Application.OpenURL("market://details?id=com.twentysevengames.android.puzzle.wordsearchofgod");
		}

		public void go_developer()
		{
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				this.MyShowToastMethod("Unconnected to the network.");
				return;
			}
			Application.OpenURL("https://play.google.com/store/apps/dev?id=");
		}

		public void MyShowToastMethod(string message)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				this.showToastOnUiThread(message);
			}
		}

		private void showToastOnUiThread(string toastString)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			this.currentActivity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.toastString = toastString;
			this.currentActivity.Call("runOnUiThread", new object[]
			{
				new AndroidJavaRunnable(this.showToast)
			});
		}

		private void showToast()
		{
			UnityEngine.Debug.Log("Running on UI thread");
			AndroidJavaObject androidJavaObject = this.currentActivity.Call<AndroidJavaObject>("getApplicationContext", new object[0]);
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.widget.Toast");
			AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.lang.String", new object[]
			{
				this.toastString
			});
			AndroidJavaObject androidJavaObject3 = androidJavaClass.CallStatic<AndroidJavaObject>("makeText", new object[]
			{
				androidJavaObject,
				androidJavaObject2,
				androidJavaClass.GetStatic<int>("LENGTH_SHORT")
			});
			androidJavaObject3.Call("show", new object[0]);
		}

		public void ScheduleCustom()
		{
			NotificationParams notificationParams = new NotificationParams
			{
				Id = UnityEngine.Random.Range(0, 2147483647),
				Delay = TimeSpan.FromSeconds(604800.0),
				Title = "Fix it! Slide Puzzle",
				Message = "Tap here to fix it pictures!",
				Ticker = "Fix it! Slide Puzzle",
				Sound = true,
				Vibrate = false,
				Light = true,
				SmallIcon = NotificationIcon.Bell,
				SmallIconColor = new Color(0f, 0.5f, 0f),
				LargeIcon = "app_icon"
			};
			NotificationParams notificationParams2 = new NotificationParams
			{
				Id = UnityEngine.Random.Range(0, 2147483647),
				Delay = TimeSpan.FromSeconds(1209600.0),
				Title = "Fix it! Slide Puzzle",
				Message = "Tap here to fix it pictures!",
				Ticker = "Fix it! Slide Puzzle",
				Sound = true,
				Vibrate = false,
				Light = true,
				SmallIcon = NotificationIcon.Bell,
				SmallIconColor = new Color(0f, 0.5f, 0f),
				LargeIcon = "app_icon"
			};
			NotificationParams notificationParams3 = new NotificationParams
			{
				Id = UnityEngine.Random.Range(0, 2147483647),
				Delay = TimeSpan.FromSeconds(2419200.0),
				Title = "Fix it! Slide Puzzle",
				Message = "Tap here to fix it pictures!",
				Ticker = "Fix it! Slide Puzzle",
				Sound = true,
				Vibrate = false,
				Light = true,
				SmallIcon = NotificationIcon.Bell,
				SmallIconColor = new Color(0f, 0.5f, 0f),
				LargeIcon = "app_icon"
			};
			NotificationManager.SendCustom(notificationParams);
			NotificationManager.SendCustom(notificationParams2);
			NotificationManager.SendCustom(notificationParams3);
		}

		public void CancelAll()
		{
			NotificationManager.CancelAll();
		}
	}
}
