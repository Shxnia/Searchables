using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace WordSearch
{
	public class AdMob : SingletonComponent<AdMob>
	{
		private string TestID = "10A90485B522D77CADDBEB4A73040F93";

		private InterstitialAd interstitial;

		public void EventAdClose(object sender, EventArgs args)
		{
			AdRequest.Builder builder = new AdRequest.Builder();
			AdRequest request = new AdRequest.Builder().Build();
			request = builder.Build();
			this.interstitial.LoadAd(request);
		}

		private void Start()
		{
			if (Entity.admob_InterstitialAd_flag)
			{
				this.interstitial = new InterstitialAd("ca-app-pub-7423364019751577/4627504363");
				this.interstitial.OnAdClosed += new EventHandler<EventArgs>(this.EventAdClose);
				AdRequest.Builder builder = new AdRequest.Builder();
				AdRequest request = new AdRequest.Builder().Build();
				request = builder.Build();
				this.interstitial.LoadAd(request);
				Entity.admob_InterstitialAd_flag = false;
			}
		}

		public void show_InterstitialAd()
		{
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				return;
			}
			if (UnityEngine.Random.Range(0, 2) == 0)
			{
				this.interstitial.Show();
			}
		}
	}
}
