using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class PopSlot : MonoBehaviour
	{
		public Transform LevelBox;

		public Transform CategoryBox;

		public GameObject Label;

		private List<string> Level_name_ENG = new List<string>();

		private List<string> Level_name_KOR = new List<string>();

		private List<string> Level_name_JPN = new List<string>();

		private List<string> cate_name_ENG = new List<string>();

		private List<string> cate_name_KOR = new List<string>();

		private List<string> cate_name_JPN = new List<string>();

		private List<GameObject> Level_obj = new List<GameObject>();

		private List<GameObject> Cate_obj = new List<GameObject>();

		public void init()
		{
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.Level_name_KOR.Add("쉬움");
			this.Level_name_KOR.Add("보통");
			this.Level_name_KOR.Add("어려움");
			this.cate_name_KOR.Add("동물");
			this.cate_name_KOR.Add("브랜드");
			this.cate_name_KOR.Add("연예인");
			this.cate_name_KOR.Add("캐릭터");
			this.cate_name_KOR.Add("과자");
			this.cate_name_KOR.Add("음식");
			this.cate_name_KOR.Add("IT");
			this.cate_name_KOR.Add("뮤직");
			this.cate_name_KOR.Add("나라이름");
			this.cate_name_KOR.Add("과학");
			this.cate_name_KOR.Add("스포츠");
			this.cate_name_KOR.Add("동물");
			this.cate_name_KOR.Add("브랜드");
			this.cate_name_KOR.Add("연예인");
			this.cate_name_KOR.Add("캐릭터");
			this.cate_name_KOR.Add("과자");
			this.cate_name_KOR.Add("음식");
			this.cate_name_KOR.Add("IT");
			this.cate_name_KOR.Add("뮤직");
			this.cate_name_KOR.Add("나라이름");
			this.cate_name_KOR.Add("과학");
			this.cate_name_KOR.Add("스포츠");
			this.cate_name_KOR.Add("동물");
			this.cate_name_KOR.Add("브랜드");
			this.cate_name_KOR.Add("연예인");
			this.cate_name_KOR.Add("캐릭터");
			this.cate_name_KOR.Add("과자");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.Level_name_JPN.Add("かんたん");
			this.Level_name_JPN.Add("ふつう");
			this.Level_name_JPN.Add("むずかしい");
			this.cate_name_JPN.Add("どうぶつ");
			this.cate_name_JPN.Add("ブランド");
			this.cate_name_JPN.Add("芸能人");
			this.cate_name_JPN.Add("キャラクタ");
			this.cate_name_JPN.Add("お菓子");
			this.cate_name_JPN.Add("たべもの");
			this.cate_name_JPN.Add("IT");
			this.cate_name_JPN.Add("ミュージシャン");
			this.cate_name_JPN.Add("くに");
			this.cate_name_JPN.Add("かがく");
			this.cate_name_JPN.Add("スポーツ");
			this.cate_name_JPN.Add("どうぶつ");
			this.cate_name_JPN.Add("ブランド");
			this.cate_name_JPN.Add("芸能人");
			this.cate_name_JPN.Add("キャラクタ");
			this.cate_name_JPN.Add("お菓子");
			this.cate_name_JPN.Add("たべもの");
			this.cate_name_JPN.Add("IT");
			this.cate_name_JPN.Add("ミュージシャン");
			this.cate_name_JPN.Add("くに");
			this.cate_name_JPN.Add("かがく");
			this.cate_name_JPN.Add("スポーツ");
			this.cate_name_JPN.Add("どうぶつ");
			this.cate_name_JPN.Add("ブランド");
			this.cate_name_JPN.Add("芸能人");
			this.cate_name_JPN.Add("キャラクタ");
			this.cate_name_JPN.Add("お菓子");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.Level_name_ENG.Add("EASY");
			this.Level_name_ENG.Add("NORMAL");
			this.Level_name_ENG.Add("HARD");
			this.cate_name_ENG.Add("ANIMAL");
			this.cate_name_ENG.Add("BRAND");
			this.cate_name_ENG.Add("CELEBRITY");
			this.cate_name_ENG.Add("CHARACTER");
			this.cate_name_ENG.Add("COOKIE");
			this.cate_name_ENG.Add("FOOD");
			this.cate_name_ENG.Add("IT");
			this.cate_name_ENG.Add("MUSICIAN");
			this.cate_name_ENG.Add("NATION");
			this.cate_name_ENG.Add("SCIENCE");
			this.cate_name_ENG.Add("SPORT");
			this.cate_name_ENG.Add("ANIMAL");
			this.cate_name_ENG.Add("BRAND");
			this.cate_name_ENG.Add("CELEBRITY");
			this.cate_name_ENG.Add("CHARACTER");
			this.cate_name_ENG.Add("COOKIE");
			this.cate_name_ENG.Add("FOOD");
			this.cate_name_ENG.Add("IT");
			this.cate_name_ENG.Add("MUSICIAN");
			this.cate_name_ENG.Add("NATION");
			this.cate_name_ENG.Add("SCIENCE");
			this.cate_name_ENG.Add("SPORT");
			this.cate_name_ENG.Add("ANIMAL");
			this.cate_name_ENG.Add("BRAND");
			this.cate_name_ENG.Add("CELEBRITY");
			this.cate_name_ENG.Add("CHARACTER");
			this.cate_name_ENG.Add("COOKIE");
			for (int i = 0; i < 27; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Label);
				gameObject.transform.parent = this.LevelBox;
				gameObject.GetComponent<Text>().text = string.Empty;
				this.Level_obj.Add(gameObject);
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Label);
				gameObject2.transform.parent = this.CategoryBox;
				gameObject2.GetComponent<Text>().text = string.Empty;
				this.Cate_obj.Add(gameObject2);
			}
			this.Change_Language(SingletonComponent<WordSearchController>.Instance.Language);
		}

		public void Change_Language(int index)
		{
			List<string> list;
			List<string> list2;
			if (index == 1)
			{
				list = this.Level_name_KOR;
				list2 = this.cate_name_KOR;
			}
			else if (index == 2)
			{
				list = this.Level_name_JPN;
				list2 = this.cate_name_JPN;
			}
			else
			{
				list = this.Level_name_ENG;
				list2 = this.cate_name_ENG;
			}
			for (int i = 0; i < 27; i++)
			{
				this.Level_obj[i].GetComponent<Text>().text = list[i];
				this.Cate_obj[i].GetComponent<Text>().text = list2[i];
			}
		}

		private void SlotStart()
		{
			this.LevelBox.localPosition = new Vector3(this.LevelBox.localPosition.x, 0f, 0f);
			this.CategoryBox.localPosition = new Vector3(this.CategoryBox.localPosition.x, 0f, 0f);
			this.LevelBox.DOLocalMoveY(-1420f, 2f, false).SetEase(Ease.OutQuad).SetDelay(0.4f).OnStart(new TweenCallback(this.slot_start_event));
			this.CategoryBox.DOLocalMoveY(-1420f, 2f, false).SetEase(Ease.OutQuad).SetDelay(0.7f).OnComplete(new TweenCallback(this.PopClose));
		}

		private void MainSlotStart()
		{
			this.LevelBox.localPosition = new Vector3(this.LevelBox.localPosition.x, 0f, 0f);
			this.CategoryBox.localPosition = new Vector3(this.CategoryBox.localPosition.x, 0f, 0f);
			this.LevelBox.DOLocalMoveY(-1420f, 2f, false).SetEase(Ease.OutQuad).SetDelay(0.4f).OnStart(new TweenCallback(this.slot_start_event));
			this.CategoryBox.DOLocalMoveY(-1420f, 2f, false).SetEase(Ease.OutQuad).SetDelay(0.7f).OnComplete(new TweenCallback(this.MainPopClose));
		}

		private void slot_start_event()
		{
			SingletonComponent<EffectSound>.Instance.GameSound(7);
		}

		public void PopClose()
		{
			base.gameObject.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetDelay(1f).OnComplete(new TweenCallback(this.Complete_PopClose));
		}

		public void MainPopClose()
		{
			SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 2);
			SingletonComponent<WordSearchController>.Instance.StartCategory(SingletonComponent<WordSearchController>.Instance.SelectedCategory);
			base.gameObject.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).SetDelay(1f).OnComplete(new TweenCallback(this.Complete_MainPopClose));
		}

		private void Complete_PopClose()
		{
			base.gameObject.SetActive(false);
			SingletonComponent<WordSearchController>.Instance.StartCategory(SingletonComponent<WordSearchController>.Instance.SelectedCategory);
		}

		private void Complete_MainPopClose()
		{
			base.gameObject.SetActive(false);
		}

		public void PopOpen()
		{
			base.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
			base.gameObject.SetActive(true);
			this.select_random_data();
			base.gameObject.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
			this.SlotStart();
		}

		public void MainPopOpen()
		{
			base.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
			base.gameObject.SetActive(true);
			this.select_random_data();
			base.gameObject.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
			this.MainSlotStart();
		}

		private void select_random_data()
		{
			int num = UnityEngine.Random.Range(0, 3);
			int num2 = UnityEngine.Random.Range(0, SingletonComponent<WordSearchController>.Instance.CategoryInfos.Count);
			SingletonComponent<WordSearchController>.Instance.SetSelectedDifficulty(num);
			SingletonComponent<WordSearchController>.Instance.SelectedCategorys(num2);
			string text = string.Empty;
			string text2 = string.Empty;
			if (num == 0)
			{
				text = SingletonComponent<LanguageController>.Instance.ModeNameEasy();
			}
			else if (num == 1)
			{
				text = SingletonComponent<LanguageController>.Instance.ModeNameNormal();
			}
			else if (num == 2)
			{
				text = SingletonComponent<LanguageController>.Instance.ModeNameHard();
			}
			if (SingletonComponent<WordSearchController>.Instance.Language == 1)
			{
				text2 = SingletonComponent<WordSearchController>.Instance.CategoryInfos[num2].categoryName_KOR;
			}
			else if (SingletonComponent<WordSearchController>.Instance.Language == 2)
			{
				text2 = SingletonComponent<WordSearchController>.Instance.CategoryInfos[num2].categoryName_JPN;
			}
			else
			{
				text2 = SingletonComponent<WordSearchController>.Instance.CategoryInfos[num2].categoryName_ENG;
			}
			this.Level_obj[1].GetComponent<Text>().text = text;
			this.Cate_obj[1].GetComponent<Text>().text = text2;
		}
	}
}
