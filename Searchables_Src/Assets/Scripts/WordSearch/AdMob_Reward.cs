using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace WordSearch
{
	public class AdMob_Reward : SingletonComponent<AdMob_Reward>
	{
		[SerializeField]
		private WordList wl_obj;

		private RewardBasedVideoAd reward;

		private bool rewarded_flag;

		private bool reward_close_flag;

		private void Start()
		{
			this.reward = RewardBasedVideoAd.Instance;
			this.reward.OnAdClosed += new EventHandler<EventArgs>(this.EventAdClose);
			this.reward.OnAdRewarded += new EventHandler<Reward>(this.EventRewarded);
			this.AdReLoad();
		}

		private void Update()
		{
			if (this.reward_close_flag)
			{
				if (this.rewarded_flag)
				{
					base.Invoke("EventComplete_true", 0.5f);
					this.rewarded_flag = false;
				}
				else
				{
					base.Invoke("EventComplete_false", 0.5f);
				}
				this.reward_close_flag = false;
			}
		}

		public void EventAdClose(object sender, EventArgs args)
		{
			this.AdReLoad();
			this.reward_close_flag = true;
		}

		public void EventRewarded(object sender, Reward args)
		{
			this.rewarded_flag = true;
		}

		public void EventAdLoaded(object sender, EventArgs args)
		{
			SingletonComponent<Global>.Instance.MyShowToastMethod("AD Load");
		}

		public void EventAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
		{
			UnityEngine.Debug.Log("Filed Loading Ad : " + args.Message);
		}

		public void EventComplete_true()
		{
			this.wl_obj.MoreHint();
		}

		public void EventComplete_false()
		{
		}

		private void AdReLoad()
		{
			AdRequest request = new AdRequest.Builder().Build();
			this.reward.LoadAd(request, "ca-app-pub-7423364019751577/7062096014");
		}

		public void show_rewardAd()
		{
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				SingletonComponent<Global>.Instance.MyShowToastMethod("Unconnected to the network.");
				return;
			}
			if (this.reward.IsLoaded())
			{
				this.reward.Show();
			}
			else
			{
				if (Application.systemLanguage == SystemLanguage.Korean)
				{
					SingletonComponent<Global>.Instance.MyShowToastMethod("비디오 광고를 로드하지 못했습니다.");
				}
				else if (Application.systemLanguage == SystemLanguage.Japanese)
				{
					SingletonComponent<Global>.Instance.MyShowToastMethod("ビデオ広告をロードできませんでした。");
				}
				else
				{
					SingletonComponent<Global>.Instance.MyShowToastMethod("Failed to load video AD");
				}
				base.Invoke("EventComplete_true", 0.5f);
				this.AdReLoad();
			}
		}
	}
}
