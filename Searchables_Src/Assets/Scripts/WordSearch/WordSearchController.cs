using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WordSearch
{
	public class WordSearchController : SingletonComponent<WordSearchController>
	{
		[Serializable]
		public class ModeInfo
		{
			public enum SortType
			{
				Random,
				Alphabetical,
				Length
			}

			public string modeName;

			public WordSearchController.ModeInfo.SortType sortType;

			public bool isTimed;

			public bool isSequential;
		}

		[Serializable]
		public class DifficultyInfo
		{
			public string difficultyName;

			public int boardRowSize;

			public int boardColumnSize;

			public int maxWords;

			public int maxWordLength;

			public float timedModeStartTime;

			public float timedModeFoundBouns;
		}

		[Serializable]
		public class CategoryInfo
		{
			public string categoryName_ENG;

			public string categoryName_KOR;

			public string categoryName_JPN;

			public Sprite categoryIcon;

			public TextAsset wordsFile_ENG;

			public TextAsset wordsFile_KOR;

			public TextAsset wordsFile_JPN;
		}

		public enum BoardState
		{
			None,
			Generating,
			BoardActive,
			Completed,
			TimesUp
		}

		private sealed class _SetupSavedGame_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal WordSearchController _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _SetupSavedGame_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForEndOfFrame();
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._this.SetActiveBoard(this._this.ActiveBoard);
					this._current = new WaitForEndOfFrame();
					if (!this._disposing)
					{
						this._PC = 2;
					}
					return true;
				case 2u:
					this._this.HighlightSavedWords();
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		[SerializeField]
		private CharacterGrid characterGrid;

		[SerializeField]
		private WordList wordList;

		[SerializeField]
		private string randomCharacters = "abcdefghijklmnopqrstuvwxyz";

		[SerializeField]
		private int numLevelStartBeforeAdShown;

		[SerializeField]
		private List<WordSearchController.ModeInfo> modeInfos;

		[SerializeField]
		private List<WordSearchController.DifficultyInfo> difficultyInfos;

		[SerializeField]
		private List<WordSearchController.CategoryInfo> categoryInfos;

		[SerializeField]
		private List<string> filterWords;

		[SerializeField]
		private List<TextAsset> tutorialCategory;

		private static readonly int WordLengthChoiceDivider = 1;

		private Dictionary<string, List<string>> allCategoryWords;

		private Dictionary<string, List<string>> TutorialCategoryWords;

		private int _Language_k__BackingField;

		private int _Challenge_Level_k__BackingField;

		private int _Quiz_Level_k__BackingField;

		private int _Quiz_Hint_Count_k__BackingField;

		private bool _Quiz_Hint_AD_Flag_k__BackingField;

		public List<WordSearchController.DifficultyInfo> DifficultyInfos_Challenge = new List<WordSearchController.DifficultyInfo>();

		public List<WordSearchController.DifficultyInfo> DifficultyInfos_Quiz = new List<WordSearchController.DifficultyInfo>();

		private QuizInformation _QuizCategoryInfos_k__BackingField;

		private WordSearchController.ModeInfo _SelectedMode_k__BackingField;

		private WordSearchController.DifficultyInfo _SelectedDifficulty_k__BackingField;

		private WordSearchController.CategoryInfo _SelectedCategory_k__BackingField;

		private WordSearchController.ModeInfo _ActiveMode_k__BackingField;

		private WordSearchController.DifficultyInfo _ActiveDifficulty_k__BackingField;

		private WordSearchController.CategoryInfo _ActiveCategory_k__BackingField;

		private Board _ActiveBoard_k__BackingField;

		private WordSearchController.BoardState _ActiveBoardState_k__BackingField;

		private int _ActiveSeqIndex_k__BackingField;

		private HashSet<string> _FoundWords_k__BackingField;

		private float _TimeLeft_k__BackingField;

		private int _LevelsToStartBeforeAd_k__BackingField;

		private bool _IsAdShowing_k__BackingField;

		private bool _IsPause_k__BackingField;

		public UIOption UIOptionObj;

		public PopSlot PopSlotObj;

		public bool IsPopLanguage;

		public bool IsSetting;

		public int IsPushAlert = 1;

		public int IsSound = 1;

		public Action OnBoardStateChanged;

		public Action OnWordFound;

		private static Comparison<string> __f__am_cache0;

		public int Language
		{
			get;
			private set;
		}

		public int Challenge_Level
		{
			get;
			set;
		}

		public int Quiz_Level
		{
			get;
			set;
		}

		public int Quiz_Hint_Count
		{
			get;
			set;
		}

		public bool Quiz_Hint_AD_Flag
		{
			get;
			set;
		}

		public List<WordSearchController.ModeInfo> ModeInfos
		{
			get
			{
				return this.modeInfos;
			}
		}

		public List<WordSearchController.DifficultyInfo> DifficultyInfos
		{
			get
			{
				return this.difficultyInfos;
			}
		}

		public List<WordSearchController.CategoryInfo> CategoryInfos
		{
			get
			{
				return this.categoryInfos;
			}
		}

		public QuizInformation QuizCategoryInfos
		{
			get;
			private set;
		}

		public WordSearchController.ModeInfo SelectedMode
		{
			get;
			private set;
		}

		public WordSearchController.DifficultyInfo SelectedDifficulty
		{
			get;
			private set;
		}

		public WordSearchController.CategoryInfo SelectedCategory
		{
			get;
			private set;
		}

		public WordSearchController.ModeInfo ActiveMode
		{
			get;
			private set;
		}

		public WordSearchController.DifficultyInfo ActiveDifficulty
		{
			get;
			private set;
		}

		public WordSearchController.CategoryInfo ActiveCategory
		{
			get;
			private set;
		}

		public Board ActiveBoard
		{
			get;
			private set;
		}

		public WordSearchController.BoardState ActiveBoardState
		{
			get;
			private set;
		}

		public int ActiveSeqIndex
		{
			get;
			private set;
		}

		public HashSet<string> FoundWords
		{
			get;
			private set;
		}

		public float TimeLeft
		{
			get;
			private set;
		}

		public int TimeLeftInSeconds
		{
			get
			{
				return Mathf.CeilToInt(this.TimeLeft);
			}
		}

		public bool IsTimedMode
		{
			get
			{
				return this.ActiveMode != null && this.ActiveMode.isTimed;
			}
		}

		public bool IsSequentialMode
		{
			get
			{
				return this.ActiveMode != null && this.ActiveMode.isSequential;
			}
		}

		private string SaveFilePath
		{
			get
			{
				return Application.persistentDataPath + "/save_data.json";
			}
		}

		private int LevelsToStartBeforeAd
		{
			get;
			set;
		}

		private bool IsAdShowing
		{
			get;
			set;
		}

		public bool IsPause
		{
			get;
			set;
		}

		protected override void Awake()
		{
			base.Awake();
            Application.targetFrameRate = 60;
			this.IsPushAlert = PlayerPrefs.GetInt("IsPushAlert", 1);
			this.IsSound = PlayerPrefs.GetInt("IsSound", 1);
			this.IsPause = false;
			this.Challenge_Level = 0;
			for (int i = 0; i < 5; i++)
			{
				WordSearchController.DifficultyInfo difficultyInfo = new WordSearchController.DifficultyInfo();
				difficultyInfo.difficultyName = i.ToString();
				difficultyInfo.boardRowSize = 7;
				difficultyInfo.boardColumnSize = 7;
				difficultyInfo.maxWords = 4;
				difficultyInfo.maxWordLength = 6;
				difficultyInfo.timedModeStartTime = (float)(20 - i * 3);
				difficultyInfo.timedModeFoundBouns = (float)(20 - i * 3);
				this.DifficultyInfos_Challenge.Add(difficultyInfo);
			}
			for (int j = 0; j < 5; j++)
			{
				WordSearchController.DifficultyInfo difficultyInfo2 = new WordSearchController.DifficultyInfo();
				difficultyInfo2.difficultyName = j.ToString();
				difficultyInfo2.boardRowSize = 8;
				difficultyInfo2.boardColumnSize = 8;
				difficultyInfo2.maxWords = 5;
				difficultyInfo2.maxWordLength = 7;
				difficultyInfo2.timedModeStartTime = (float)(20 - j * 3);
				difficultyInfo2.timedModeFoundBouns = (float)(20 - j * 3);
				this.DifficultyInfos_Challenge.Add(difficultyInfo2);
			}
			for (int k = 0; k < 5; k++)
			{
				WordSearchController.DifficultyInfo difficultyInfo3 = new WordSearchController.DifficultyInfo();
				difficultyInfo3.difficultyName = k.ToString();
				difficultyInfo3.boardRowSize = 9;
				difficultyInfo3.boardColumnSize = 9;
				difficultyInfo3.maxWords = 6;
				difficultyInfo3.maxWordLength = 8;
				difficultyInfo3.timedModeStartTime = (float)(20 - k * 3);
				difficultyInfo3.timedModeFoundBouns = (float)(20 - k * 3);
				this.DifficultyInfos_Challenge.Add(difficultyInfo3);
			}
			for (int l = 0; l < 5; l++)
			{
				WordSearchController.DifficultyInfo difficultyInfo4 = new WordSearchController.DifficultyInfo();
				difficultyInfo4.difficultyName = l.ToString();
				difficultyInfo4.boardRowSize = 10;
				difficultyInfo4.boardColumnSize = 10;
				difficultyInfo4.maxWords = 7;
				difficultyInfo4.maxWordLength = 9;
				difficultyInfo4.timedModeStartTime = (float)(20 - l * 3);
				difficultyInfo4.timedModeFoundBouns = (float)(20 - l * 3);
				this.DifficultyInfos_Challenge.Add(difficultyInfo4);
			}
			for (int m = 0; m < 5; m++)
			{
				WordSearchController.DifficultyInfo difficultyInfo5 = new WordSearchController.DifficultyInfo();
				difficultyInfo5.difficultyName = m.ToString();
				difficultyInfo5.boardRowSize = 11;
				difficultyInfo5.boardColumnSize = 11;
				difficultyInfo5.maxWords = 8;
				difficultyInfo5.maxWordLength = 10;
				difficultyInfo5.timedModeStartTime = (float)(20 - m * 3);
				difficultyInfo5.timedModeFoundBouns = (float)(20 - m * 3);
				this.DifficultyInfos_Challenge.Add(difficultyInfo5);
			}
			for (int n = 0; n < 5; n++)
			{
				WordSearchController.DifficultyInfo difficultyInfo6 = new WordSearchController.DifficultyInfo();
				difficultyInfo6.difficultyName = n.ToString();
				difficultyInfo6.boardRowSize = 12;
				difficultyInfo6.boardColumnSize = 12;
				difficultyInfo6.maxWords = 9;
				difficultyInfo6.maxWordLength = 11;
				difficultyInfo6.timedModeStartTime = (float)(20 - n * 3);
				difficultyInfo6.timedModeFoundBouns = (float)(20 - n * 3);
				this.DifficultyInfos_Challenge.Add(difficultyInfo6);
			}
			for (int num = 0; num < 5; num++)
			{
				WordSearchController.DifficultyInfo difficultyInfo7 = new WordSearchController.DifficultyInfo();
				difficultyInfo7.difficultyName = num.ToString();
				difficultyInfo7.boardRowSize = 7;
				difficultyInfo7.boardColumnSize = 7;
				difficultyInfo7.maxWords = 5;
				difficultyInfo7.maxWordLength = 6;
				difficultyInfo7.timedModeStartTime = (float)(60 - num * 5);
				difficultyInfo7.timedModeFoundBouns = 5f;
				this.DifficultyInfos_Quiz.Add(difficultyInfo7);
			}
			for (int num2 = 0; num2 < 5; num2++)
			{
				WordSearchController.DifficultyInfo difficultyInfo8 = new WordSearchController.DifficultyInfo();
				difficultyInfo8.difficultyName = num2.ToString();
				difficultyInfo8.boardRowSize = 8;
				difficultyInfo8.boardColumnSize = 8;
				difficultyInfo8.maxWords = 5;
				difficultyInfo8.maxWordLength = 7;
				difficultyInfo8.timedModeStartTime = (float)(60 - num2 * 5);
				difficultyInfo8.timedModeFoundBouns = 5f;
				this.DifficultyInfos_Quiz.Add(difficultyInfo8);
			}
			for (int num3 = 0; num3 < 5; num3++)
			{
				WordSearchController.DifficultyInfo difficultyInfo9 = new WordSearchController.DifficultyInfo();
				difficultyInfo9.difficultyName = num3.ToString();
				difficultyInfo9.boardRowSize = 9;
				difficultyInfo9.boardColumnSize = 9;
				difficultyInfo9.maxWords = 5;
				difficultyInfo9.maxWordLength = 8;
				difficultyInfo9.timedModeStartTime = (float)(60 - num3 * 5);
				difficultyInfo9.timedModeFoundBouns = 5f;
				this.DifficultyInfos_Quiz.Add(difficultyInfo9);
			}
			for (int num4 = 0; num4 < 5; num4++)
			{
				WordSearchController.DifficultyInfo difficultyInfo10 = new WordSearchController.DifficultyInfo();
				difficultyInfo10.difficultyName = num4.ToString();
				difficultyInfo10.boardRowSize = 10;
				difficultyInfo10.boardColumnSize = 10;
				difficultyInfo10.maxWords = 5;
				difficultyInfo10.maxWordLength = 9;
				difficultyInfo10.timedModeStartTime = (float)(60 - num4 * 5);
				difficultyInfo10.timedModeFoundBouns = 5f;
				this.DifficultyInfos_Quiz.Add(difficultyInfo10);
			}
			for (int num5 = 0; num5 < 5; num5++)
			{
				WordSearchController.DifficultyInfo difficultyInfo11 = new WordSearchController.DifficultyInfo();
				difficultyInfo11.difficultyName = num5.ToString();
				difficultyInfo11.boardRowSize = 11;
				difficultyInfo11.boardColumnSize = 11;
				difficultyInfo11.maxWords = 5;
				difficultyInfo11.maxWordLength = 10;
				difficultyInfo11.timedModeStartTime = (float)(60 - num5 * 5);
				difficultyInfo11.timedModeFoundBouns = 5f;
				this.DifficultyInfos_Quiz.Add(difficultyInfo11);
			}
			for (int num6 = 0; num6 < 5; num6++)
			{
				WordSearchController.DifficultyInfo difficultyInfo12 = new WordSearchController.DifficultyInfo();
				difficultyInfo12.difficultyName = num6.ToString();
				difficultyInfo12.boardRowSize = 12;
				difficultyInfo12.boardColumnSize = 12;
				difficultyInfo12.maxWords = 5;
				difficultyInfo12.maxWordLength = 11;
				difficultyInfo12.timedModeStartTime = (float)(60 - num6 * 5);
				difficultyInfo12.timedModeFoundBouns = 5f;
				this.DifficultyInfos_Quiz.Add(difficultyInfo12);
			}
			if (PlayerPrefs.GetInt("Language", 100) != 100)
			{
				this.Language = PlayerPrefs.GetInt("Language");
			}
			else
			{
				if (Application.systemLanguage == SystemLanguage.Korean)
				{
					this.Language = 1;
				}
				else if (Application.systemLanguage == SystemLanguage.Japanese)
				{
					this.Language = 2;
				}
				else
				{
					this.Language = 0;
				}
				PlayerPrefs.SetInt("Language", this.Language);
			}
			if (this.Language == 1)
			{
				this.randomCharacters = "고라니사자호랑이표범치타하에나기린거북조양강아지살족제비수달해프물개상어래팽렁돼코뿔소낙끼리슴도두더햄스터극곰오여우늑대컹크바다돌쇠흰향람쥐피그버토저왈캥루알원숭침팬탄꺼구롱뇽앵무새딱따총펭귄공작독왜가까벌마귀전갈잠매미금쟁당레박풍뎅브카멜온뱀등연참멸문꼴뚜징쭈꾸릴위늘보캣악얼룩말병영염르딜로칠면청설모친백둘러재규방불멍게삼쿼맹꽁장메복냐명태붕광올빼앙찌꾀꼬락동송골울콘담너노뉴트머드멧반순록시파일각판황핥플밍목서란긴팔철갑왕잘셋만든감깡낵칩건빵틱밥운국희샌계과깨깔꽃꿀꽈배꿍닙티닭키디뜨딸쿠땅콩정떡볶롤폴롯데링렛맛산포테먹즈쯔잉킥와넛집핏별난뽀빠닝빈츠뻥또요뿌셔베컨섹윙신짱쌀본썬쏘야씨몬체커채임쎈애액옛날클글웨옥칲인언절쨈쟈유죠퐁진째앰냉찹선쿵팝촉한쵸초픽땡틴칙칸캬츄콤림통투캅편켓핑벨헤핫화튀홈런볼후네누낫걸있군깐싱팅몽허페업놀부블랙주년톰논밭랜얀쫄석효중김종준형돈홍승근탁훈휘환혁경성민현예슬윤욱음권손천혜봉헌남세차길학최은필립엄숙룡창심켠려섭렬교류열변완육균안찬웅줄엔율추택폰렐푼젤엘맨앤캡헐윈솔져닥슈퍼먼혼앨뮬널덕딘푸튜둠킹맥랫겐븐식솜턴젠욘느론즐콜월틸옹간검룸잭패곤웬센솝망빛렉실쉬짐칼처직슨핀털용캐쇼워룬단";
			}
			else if (this.Language == 2)
			{
				this.randomCharacters = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをんがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポ";
			}
			else
			{
				this.randomCharacters = "abcdefghijklmnopqrstuvwxyz";
			}
			this.QuizCategoryInfos = new QuizInformation();
			this.allCategoryWords = new Dictionary<string, List<string>>();
			this.TutorialCategoryWords = new Dictionary<string, List<string>>();
			this.FoundWords = new HashSet<string>();
			if (this.SelectedMode == null)
			{
				if (this.modeInfos.Count == 0)
				{
					UnityEngine.Debug.LogError("[WordSearchController] There are no Mode Infos set in the inspector.");
				}
				else
				{
					this.SelectedMode = this.modeInfos[0];
				}
			}
			if (this.SelectedDifficulty == null)
			{
				if (this.difficultyInfos.Count == 0)
				{
					UnityEngine.Debug.LogError("[WordSearchController] There are no Difficulty Infos set in the inspector.");
				}
				else
				{
					this.SelectedDifficulty = this.difficultyInfos[0];
				}
			}
			this.characterGrid.Initialize();
			this.wordList.Initialize();
			SingletonComponent<UIScreenController>.Instance.returnPopOBJ(9).GetComponent<PopSlot>().init();
			this.TutorialCategory();
			int num7 = 0;
			for (int num8 = 0; num8 < this.difficultyInfos.Count; num8++)
			{
				if (this.difficultyInfos[num8].maxWordLength > num7)
				{
					num7 = this.difficultyInfos[num8].maxWordLength;
				}
			}
			for (int num9 = 0; num9 < this.categoryInfos.Count; num9++)
			{
				WordSearchController.CategoryInfo categoryInfo = this.categoryInfos[num9];
				List<string> list = new List<string>();
				string key = string.Empty;
				if (this.Language == 1)
				{
					list = new List<string>(categoryInfo.wordsFile_KOR.text.Split(new char[]
					{
						'\n'
					}));
					key = categoryInfo.categoryName_KOR;
				}
				else if (this.Language == 2)
				{
					list = new List<string>(categoryInfo.wordsFile_JPN.text.Split(new char[]
					{
						'\n'
					}));
					key = categoryInfo.categoryName_JPN;
				}
				else
				{
					list = new List<string>(categoryInfo.wordsFile_ENG.text.Split(new char[]
					{
						'\n'
					}));
					key = categoryInfo.categoryName_ENG;
				}
				for (int num10 = 0; num10 < list.Count; num10++)
				{
					string text = list[num10].Replace("\r", string.Empty).Trim();
					if (list.IndexOf(text, num10 + 1) != -1)
					{
						UnityEngine.Debug.LogWarningFormat("[WordSearchController] A category word is duplicated. Category: {0}, Word: {1}", new object[]
						{
							categoryInfo.categoryName_ENG,
							text
						});
						list.RemoveAt(num10);
						num10--;
					}
					else if (text.Replace(" ", string.Empty).Length > num7)
					{
						UnityEngine.Debug.LogWarningFormat("[WordSearchController] A category word is longer than the longest possible word that can fit on any board. Category: {0}, Word: {1}", new object[]
						{
							categoryInfo.categoryName_ENG,
							text
						});
						list.RemoveAt(num10);
						num10--;
					}
					else
					{
						for (int num11 = 0; num11 < this.filterWords.Count; num11++)
						{
							string text2 = this.filterWords[num11];
							if (text.ToLower().Contains(text2.ToLower()))
							{
								UnityEngine.Debug.LogWarningFormat("[WordSearchController] A category word contains a filter word, it cannot be placed on a board. Category: {0}, Word: {1}, Filter Word: {2}", new object[]
								{
									categoryInfo.categoryName_ENG,
									text,
									text2
								});
								list.RemoveAt(num10);
								num10--;
							}
						}
					}
				}
				this.allCategoryWords.Add(key, list);
			}
			if (this.ActiveBoard != null && this.ActiveBoardState == WordSearchController.BoardState.BoardActive)
			{
				base.StartCoroutine(this.SetupSavedGame());
			}
		}

		protected void Start()
		{
			SingletonComponent<UIScreenController>.Instance.returnOBJ(0).GetComponent<UIMain>().ChangeFlag(this.Language);
			SingletonComponent<EffectSound>.Instance.SoundOnOff(this.IsSound);
			SingletonComponent<EffectSound_ing>.Instance.SoundOnOff(this.IsSound);
			if (PlayerPrefs.GetInt("First_Start_Tutorial", 0) == 0)
			{
				base.Invoke("StartTutorial", 0.5f);
			}
			SingletonComponent<AdMob_Banner>.Instance.show_BannerView();
		}

		private void Update()
		{
			if (this.IsTimedMode && this.ActiveBoardState == WordSearchController.BoardState.BoardActive && SingletonComponent<UIScreenController>.Instance.CurrentScreenId == "game" && !this.IsAdShowing && !this.IsPause)
			{
				this.TimeLeft -= Time.deltaTime;
				if (this.TimeLeft <= 0f)
				{
					this.SetActiveBoardState(WordSearchController.BoardState.TimesUp);
				}
			}
			if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
			{
				SingletonComponent<UIScreenController>.Instance.BackKey_PopClose();
			}
		}

		private void OnDestroy()
		{
			if (this.SelectedMode.modeName == "Classic")
			{
			}
		}

		private void OnApplicationPause(bool paused)
		{
			if (!paused || this.SelectedMode.modeName == "Classic")
			{
			}
		}

		public void StartTutorial()
		{
			this.SetSelectedMode(4);
			SingletonComponent<UIScreenController>.Instance.MoveScreen(0, 2);
			PlayerPrefs.SetInt("First_Start_Tutorial", 1);
		}

		public void SettingGameLanguage(int index)
		{
			SingletonComponent<UIScreenController>.Instance.returnOBJ(1).GetComponent<UIOption>().Btn_Languege_Changer(index);
			SingletonComponent<UIScreenController>.Instance.returnPopOBJ(9).GetComponent<PopSlot>().Change_Language(index);
			SingletonComponent<LanguageController>.Instance.Change_Language_UI(index);
			PlayerPrefs.SetInt("Language", index);
			if (index == 1)
			{
				this.Language = 1;
				this.randomCharacters = "고라니사자호랑이표범치타하에나기린거북조양강아지살족제비수달해프물개상어래팽렁돼코뿔소낙끼리슴도두더햄스터극곰오여우늑대컹크바다돌쇠흰향람쥐피그버토저왈캥루알원숭침팬탄꺼구롱뇽앵무새딱따총펭귄공작독왜가까벌마귀전갈잠매미금쟁당레박풍뎅브카멜온뱀등연참멸문꼴뚜징쭈꾸릴위늘보캣악얼룩말병영염르딜로칠면청설모친백둘러재규방불멍게삼쿼맹꽁장메복냐명태붕광올빼앙찌꾀꼬락동송골울콘담너노뉴트머드멧반순록시파일각판황핥플밍목서란긴팔철갑왕잘셋만든감깡낵칩건빵틱밥운국희샌계과깨깔꽃꿀꽈배꿍닙티닭키디뜨딸쿠땅콩정떡볶롤폴롯데링렛맛산포테먹즈쯔잉킥와넛집핏별난뽀빠닝빈츠뻥또요뿌셔베컨섹윙신짱쌀본썬쏘야씨몬체커채임쎈애액옛날클글웨옥칲인언절쨈쟈유죠퐁진째앰냉찹선쿵팝촉한쵸초픽땡틴칙칸캬츄콤림통투캅편켓핑벨헤핫화튀홈런볼후네누낫걸있군깐싱팅몽허페업놀부블랙주년톰논밭랜얀쫄석효중김종준형돈홍승근탁훈휘환혁경성민현예슬윤욱음권손천혜봉헌남세차길학최은필립엄숙룡창심켠려섭렬교류열변완육균안찬웅줄엔율추택폰렐푼젤엘맨앤캡헐윈솔져닥슈퍼먼혼앨뮬널덕딘푸튜둠킹맥랫겐븐식솜턴젠욘느론즐콜월틸옹간검룸잭패곤웬센솝망빛렉실쉬짐칼처직슨핀털용캐쇼워룬단";
			}
			else if (index == 2)
			{
				this.Language = 2;
				this.randomCharacters = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをんがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲンガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポ";
			}
			else
			{
				this.Language = 0;
				this.randomCharacters = "abcdefghijklmnopqrstuvwxyz";
			}
			this.allCategoryWords.Clear();
			this.TutorialCategoryWords.Clear();
			this.TutorialCategory();
			int num = 0;
			for (int i = 0; i < this.difficultyInfos.Count; i++)
			{
				if (this.difficultyInfos[i].maxWordLength > num)
				{
					num = this.difficultyInfos[i].maxWordLength;
				}
			}
			for (int j = 0; j < this.categoryInfos.Count; j++)
			{
				WordSearchController.CategoryInfo categoryInfo = this.categoryInfos[j];
				List<string> list = new List<string>();
				string key = string.Empty;
				if (this.Language == 1)
				{
					list = new List<string>(categoryInfo.wordsFile_KOR.text.Split(new char[]
					{
						'\n'
					}));
					key = categoryInfo.categoryName_KOR;
				}
				else if (this.Language == 2)
				{
					list = new List<string>(categoryInfo.wordsFile_JPN.text.Split(new char[]
					{
						'\n'
					}));
					key = categoryInfo.categoryName_JPN;
				}
				else
				{
					list = new List<string>(categoryInfo.wordsFile_ENG.text.Split(new char[]
					{
						'\n'
					}));
					key = categoryInfo.categoryName_ENG;
				}
				for (int k = 0; k < list.Count; k++)
				{
					string text = list[k].Replace("\r", string.Empty).Trim();
					if (list.IndexOf(text, k + 1) != -1)
					{
						list.RemoveAt(k);
						k--;
					}
					else if (text.Replace(" ", string.Empty).Length > num)
					{
						UnityEngine.Debug.LogWarningFormat("[WordSearchController] A category word is longer than the longest possible word that can fit on any board. Category: {0}, Word: {1}", new object[]
						{
							categoryInfo.categoryName_ENG,
							text
						});
						list.RemoveAt(k);
						k--;
					}
					else
					{
						for (int l = 0; l < this.filterWords.Count; l++)
						{
							string text2 = this.filterWords[l];
							if (text.ToLower().Contains(text2.ToLower()))
							{
								UnityEngine.Debug.LogWarningFormat("[WordSearchController] A category word contains a filter word, it cannot be placed on a board. Category: {0}, Word: {1}, Filter Word: {2}", new object[]
								{
									categoryInfo.categoryName_ENG,
									text,
									text2
								});
								list.RemoveAt(k);
								k--;
							}
						}
					}
				}
				this.allCategoryWords.Add(key, list);
			}
		}

		private void TutorialCategory()
		{
			int num = 0;
			for (int i = 0; i < this.difficultyInfos.Count; i++)
			{
				if (this.difficultyInfos[i].maxWordLength > num)
				{
					num = this.difficultyInfos[i].maxWordLength;
				}
			}
			List<string> list = new List<string>();
			string key = string.Empty;
			list = new List<string>(this.tutorialCategory[this.Language].text.Split(new char[]
			{
				'\n'
			}));
			if (this.Language == 1)
			{
				key = "튜토리얼";
			}
			else if (this.Language == 2)
			{
				key = "チュートリアル";
			}
			else
			{
				key = "TUTORIAL";
			}
			for (int j = 0; j < list.Count; j++)
			{
				string text = list[j].Replace("\r", string.Empty).Trim();
				if (list.IndexOf(text, j + 1) != -1)
				{
					list.RemoveAt(j);
					j--;
				}
				else if (text.Replace(" ", string.Empty).Length > num)
				{
					list.RemoveAt(j);
					j--;
				}
				else
				{
					for (int k = 0; k < this.filterWords.Count; k++)
					{
						string text2 = this.filterWords[k];
						if (text.ToLower().Contains(text2.ToLower()))
						{
							list.RemoveAt(j);
							j--;
						}
					}
				}
			}
			this.TutorialCategoryWords.Add(key, list);
		}

		public void SetSelectedMode(int modeIndex)
		{
			if (modeIndex == 0)
			{
				this.SelectedMode = this.modeInfos[modeIndex];
				this.SelectedCategory = this.CategoryInfos[0];
				this.SelectedDifficulty = this.DifficultyInfos[1];
				this.UIOptionObj.Btn_Level_OnOff(1);
			}
			if (modeIndex == 1)
			{
                //this.GamePlayCounter();
                //this.SelectedMode = this.modeInfos[0];
                //this.PopSlotObj.MainPopOpen();
            }
			else if (modeIndex == 2)
			{
				this.Challenge_New_Game();
			}
			else if (modeIndex == 3)
			{
				this.Quiz_New_Game();
			}
			else if (modeIndex == 4)
			{
				this.SelectedMode = this.modeInfos[3];
				this.SelectedDifficulty = this.DifficultyInfos[3];
				this.StartCategory(this.SelectedCategory);
			}
		}

		public void SetSelectedDifficulty(int difficultyIndex)
		{
			this.SelectedDifficulty = this.difficultyInfos[difficultyIndex];
		}

		public void SelectedCategorys(int categoryIndex)
		{
			this.SelectedCategory = this.CategoryInfos[categoryIndex];
		}

		public void StartGame()
		{
			this.GamePlayCounter();
			this.StartCategory(this.SelectedCategory);
		}

		public void Challenge_Next_Game()
		{
			this.IsPause = true;
			this.Challenge_Level++;
			this.SelectedMode = this.modeInfos[1];
			this.SelectedDifficulty = this.DifficultyInfos_Challenge[this.Challenge_Level];
			this.SelectedCategory = this.CategoryInfos[UnityEngine.Random.Range(0, this.CategoryInfos.Count)];
			if (this.Language == 0 && this.SelectedDifficulty.boardColumnSize >= 5 && this.SelectedDifficulty.boardColumnSize < 7)
			{
				int num = UnityEngine.Random.Range(0, this.CategoryInfos.Count);
				if (num == 2)
				{
					num = 0;
				}
				this.SelectedCategory = this.CategoryInfos[num];
			}
			this.StartCategory(this.SelectedCategory);
		}

		public void Challenge_New_Game()
		{
			this.GamePlayCounter();
			this.IsPause = true;
			this.Challenge_Level = 0;
			this.SelectedMode = this.modeInfos[1];
			this.SelectedDifficulty = this.DifficultyInfos_Challenge[this.Challenge_Level];
			this.SelectedCategory = this.CategoryInfos[UnityEngine.Random.Range(0, this.CategoryInfos.Count)];
			if (this.Language == 0 && this.SelectedDifficulty.boardColumnSize >= 5 && this.SelectedDifficulty.boardColumnSize < 7)
			{
				int num = UnityEngine.Random.Range(0, this.CategoryInfos.Count);
				if (num == 2)
				{
					num = 0;
				}
				this.SelectedCategory = this.CategoryInfos[num];
			}
			this.StartCategory(this.SelectedCategory);
		}

		public void Quiz_Next_Game()
		{
			this.IsPause = true;
			this.Quiz_Level++;
			this.SelectedMode = this.modeInfos[2];
			this.SelectedDifficulty = this.DifficultyInfos_Quiz[this.Quiz_Level];
			this.SelectedCategory = this.CategoryInfos[UnityEngine.Random.Range(0, this.CategoryInfos.Count)];
			this.StartCategory(this.SelectedCategory);
		}

		public void Quiz_New_Game()
		{
			this.GamePlayCounter();
			this.IsPause = true;
			this.Quiz_Level = 0;
			this.Quiz_Hint_Count = 3;
			this.Quiz_Hint_AD_Flag = false;
			this.SelectedMode = this.modeInfos[2];
			this.SelectedDifficulty = this.DifficultyInfos_Quiz[this.Quiz_Level];
			this.SelectedCategory = this.CategoryInfos[UnityEngine.Random.Range(0, this.CategoryInfos.Count)];
			this.StartCategory(this.SelectedCategory);
		}

		public void StartCategory(WordSearchController.CategoryInfo categoryInfo)
		{
			if (this.SelectedDifficulty == null)
			{
				UnityEngine.Debug.LogError("[WordSearchController] Cannot start a category because there is no selected difficult.");
				return;
			}
			if (this.SelectedMode == null)
			{
				UnityEngine.Debug.LogError("[WordSearchController] Cannot start a category because there is no selected mode.");
				return;
			}
			if (SingletonComponent<AdsController>.Exists() && SingletonComponent<AdsController>.Instance.IsInterstitialAdsEnabled)
			{
				this.LevelsToStartBeforeAd--;
				if (this.LevelsToStartBeforeAd <= 0)
				{
					this.IsAdShowing = SingletonComponent<AdsController>.Instance.ShowInterstitialAd(delegate
					{
						this.IsAdShowing = false;
					});
					if (this.IsAdShowing)
					{
						this.LevelsToStartBeforeAd = this.numLevelStartBeforeAdShown;
					}
				}
			}
			this.FoundWords.Clear();
			this.characterGrid.Clear();
			this.wordList.Clear();
			this.ActiveMode = this.SelectedMode;
			this.ActiveDifficulty = this.SelectedDifficulty;
			this.ActiveCategory = categoryInfo;
			this.ActiveBoard = null;
			this.ActiveSeqIndex = 0;
			if (this.ActiveMode.isTimed)
			{
				this.TimeLeft = this.ActiveDifficulty.timedModeStartTime;
			}
			this.CreateBoard(this.ActiveCategory);
		}

		public string OnWordSelected(Position start, Position end)
		{
			int num = (start.row != end.row) ? ((start.row >= end.row) ? (-1) : 1) : 0;
			int num2 = (start.col != end.col) ? ((start.col >= end.col) ? (-1) : 1) : 0;
			int num3 = Mathf.Max(Mathf.Abs(start.row - end.row), Mathf.Abs(start.col - end.col));
			string text = string.Empty;
			string text2 = string.Empty;
			for (int i = 0; i <= num3; i++)
			{
				char c = this.ActiveBoard.boardCharacters[start.row + i * num][start.col + i * num2];
				text += c;
				text2 = c + text2;
			}
			if (this.IsSequentialMode)
			{
				string text3 = this.ActiveBoard.usedWords[this.ActiveSeqIndex].Replace(" ", string.Empty);
				if ((text == text3 && !this.FoundWords.Contains(text)) || (text2 == text3 && !this.FoundWords.Contains(text2)))
				{
					this.SetWordFound(text3);
					SingletonComponent<EffectSound>.Instance.GameSound(0);
					this.ActiveSeqIndex++;
					return this.ActiveBoard.usedWords[this.ActiveSeqIndex - 1];
				}
				SingletonComponent<EffectSound>.Instance.GameSound(1);
			}
			else
			{
				for (int j = 0; j < this.ActiveBoard.usedWords.Count; j++)
				{
					string text4 = this.ActiveBoard.usedWords[j].Replace(" ", string.Empty);
					if ((text == text4 && !this.FoundWords.Contains(text)) || (text2 == text4 && !this.FoundWords.Contains(text2)))
					{
						this.SetWordFound(text4);
						SingletonComponent<EffectSound>.Instance.GameSound(0);
						return this.ActiveBoard.usedWords[j];
					}
				}
				SingletonComponent<EffectSound>.Instance.GameSound(1);
			}
			return null;
		}

		public void GamePlayCounter()
		{
			int num = PlayerPrefs.GetInt("GamePlayTotal", 0);
			num++;
			PlayerPrefs.SetInt("GamePlayTotal", num);
		}

		public void GamePlayOpenReview()
		{
			if (PlayerPrefs.GetInt("Open_Review_Flag", 0) > 0)
			{
				return;
			}
			int @int = PlayerPrefs.GetInt("GamePlayTotal", 0);
			if (@int == 5 || @int == 15 || @int == 30 || @int == 45 || @int == 60)
			{
				SingletonComponent<EffectSound>.Instance.SystemSound(0);
				SingletonComponent<UIScreenController>.Instance.PopupOpen(4);
			}
		}

		private void CreateBoard(WordSearchController.CategoryInfo categoryInfo)
		{
			BoardConfig boardConfig = new BoardConfig();
			if (this.ActiveMode.modeName == "Quiz")
			{
				List<string[]> list = this.QuizCategoryInfos.Shuffle_Retrun(this.Language);
				List<string> list2 = new List<string>();
				List<string> list3 = new List<string>();
				for (int i = 0; i < this.ActiveDifficulty.maxWords; i++)
				{
					list2.Add(list[i][0]);
					list3.Add(list[i][1]);
				}
				boardConfig.rowSize = this.ActiveDifficulty.boardRowSize;
				boardConfig.columnSize = this.ActiveDifficulty.boardColumnSize;
				boardConfig.words = list2;
				boardConfig.words_question = list3;
				boardConfig.filterWords = this.filterWords;
				boardConfig.randomCharacters = this.randomCharacters;
			}
			else
			{
				if (this.ActiveMode.modeName == "Tutorial")
				{
					string key = string.Empty;
					if (this.Language == 1)
					{
						key = "튜토리얼";
					}
					else if (this.Language == 2)
					{
						key = "チュートリアル";
					}
					else
					{
						key = "TUTORIAL";
					}
					List<string> list4 = new List<string>(this.TutorialCategoryWords[key]);
					while (list4.Count > 0 && list4[0].Replace(" ", string.Empty).Length > this.ActiveDifficulty.maxWordLength)
					{
						list4.RemoveAt(0);
					}
					List<string> list5 = new List<string>();
					if (WordSearchController.WordLengthChoiceDivider > 0)
					{
						List<List<string>> list6 = new List<List<string>>();
						int num = 0;
						for (int j = 0; j < list4.Count; j++)
						{
							if (list4[j].Length != num)
							{
								num = list4[j].Length;
								list6.Add(new List<string>());
							}
							list6[list6.Count - 1].Add(list4[j]);
						}
						int num2 = list6.Count / WordSearchController.WordLengthChoiceDivider;
						int k = 0;
						while (k < this.ActiveDifficulty.maxWords)
						{
							int num3 = 0;
							for (int l = 0; l < list6.Count; l++)
							{
								if (list6[l].Count == 0)
								{
									num3++;
								}
								else
								{
									int num4 = 1;
									if (num2 > 0)
									{
										num4 += Mathf.FloorToInt((float)l / (float)num2);
									}
									for (int m = 0; m < num4; m++)
									{
										if (list6[l].Count == 0)
										{
											break;
										}
										string item = list6[l][0];
										list6[l].RemoveAt(0);
										list5.Add(item);
										k++;
										if (k >= this.ActiveDifficulty.maxWords)
										{
											break;
										}
									}
									if (k >= this.ActiveDifficulty.maxWords)
									{
										break;
									}
								}
							}
							if (num3 == list6.Count)
							{
								break;
							}
						}
					}
					boardConfig.rowSize = this.ActiveDifficulty.boardRowSize;
					boardConfig.columnSize = this.ActiveDifficulty.boardColumnSize;
					boardConfig.words = list5;
					boardConfig.words_question = new List<string>();
					boardConfig.filterWords = this.filterWords;
					boardConfig.randomCharacters = this.randomCharacters;
					this.SetActiveBoardState(WordSearchController.BoardState.Generating);
					BoardCreator.TutorialCreateBoard(boardConfig, new Action<Board>(this.OnBoardCreated));
					return;
				}
				string key2 = string.Empty;
				if (this.Language == 1)
				{
					key2 = categoryInfo.categoryName_KOR;
				}
				else if (this.Language == 2)
				{
					key2 = categoryInfo.categoryName_JPN;
				}
				else
				{
					key2 = categoryInfo.categoryName_ENG;
				}
				List<string> list7 = new List<string>(this.allCategoryWords[key2]);
				list7.Sort((string x, string y) => y.Length - x.Length);
				while (list7.Count > 0 && list7[0].Replace(" ", string.Empty).Length > this.ActiveDifficulty.maxWordLength)
				{
					list7.RemoveAt(0);
				}
				List<string> list8 = new List<string>();
				if (WordSearchController.WordLengthChoiceDivider > 0)
				{
					List<List<string>> list9 = new List<List<string>>();
					int num5 = 0;
					for (int n = 0; n < list7.Count; n++)
					{
						if (list7[n].Length != num5)
						{
							num5 = list7[n].Length;
							list9.Add(new List<string>());
						}
						list9[list9.Count - 1].Add(list7[n]);
					}
					int num6 = list9.Count / WordSearchController.WordLengthChoiceDivider;
					int num7 = 0;
					while (num7 < this.ActiveDifficulty.maxWords)
					{
						int num8 = 0;
						for (int num9 = 0; num9 < list9.Count; num9++)
						{
							if (list9[num9].Count == 0)
							{
								num8++;
							}
							else
							{
								int num10 = 1;
								if (num6 > 0)
								{
									num10 += Mathf.FloorToInt((float)num9 / (float)num6);
								}
								for (int num11 = 0; num11 < num10; num11++)
								{
									if (list9[num9].Count == 0)
									{
										break;
									}
									int index = UnityEngine.Random.Range(0, list9[num9].Count);
									string item2 = list9[num9][index];
									list9[num9].RemoveAt(index);
									list8.Add(item2);
									num7++;
									if (num7 >= this.ActiveDifficulty.maxWords)
									{
										break;
									}
								}
								if (num7 >= this.ActiveDifficulty.maxWords)
								{
									break;
								}
							}
						}
						if (num8 == list9.Count)
						{
							break;
						}
					}
				}
				else
				{
					while (list7.Count > 0 && list8.Count < this.ActiveDifficulty.maxWords)
					{
						int index2 = UnityEngine.Random.Range(0, list7.Count);
						string item3 = list7[index2];
						list7.RemoveAt(index2);
						list8.Add(item3);
					}
				}
				boardConfig.rowSize = this.ActiveDifficulty.boardRowSize;
				boardConfig.columnSize = this.ActiveDifficulty.boardColumnSize;
				boardConfig.words = list8;
				boardConfig.words_question = new List<string>();
				boardConfig.filterWords = this.filterWords;
				boardConfig.randomCharacters = this.randomCharacters;
			}
			this.SetActiveBoardState(WordSearchController.BoardState.Generating);
			BoardCreator.CreateBoard(boardConfig, new Action<Board>(this.OnBoardCreated));
		}

		private void OnBoardCreated(Board board)
		{
			if (board != null)
			{
				if (this.ActiveMode.modeName != "Quiz")
				{
					switch (SingletonComponent<WordSearchController>.Instance.ActiveMode.sortType)
					{
					case WordSearchController.ModeInfo.SortType.Random:
						board.usedWords.Sort(new Comparison<string>(this.SortWordsRandomly));
						goto IL_9B;
					case WordSearchController.ModeInfo.SortType.Length:
						board.usedWords.Sort(new Comparison<string>(this.SortWordsByLength));
						goto IL_9B;
					}
					board.usedWords.Sort(new Comparison<string>(this.SortWordsAlphabetically));
				}
				IL_9B:
				this.SetActiveBoard(board);
			}
			else
			{
				UnityEngine.Debug.LogError("[WordSearchController] Failed to create board.");
				this.SetActiveBoardState(WordSearchController.BoardState.None);
			}
		}

		private void SetActiveBoard(Board board)
		{
			this.ActiveBoard = board;
			this.characterGrid.Setup(this.ActiveBoard);
			this.wordList.Setup(this.ActiveBoard);
			this.SetActiveBoardState(WordSearchController.BoardState.BoardActive);
			this.wordList.UIWordCountChanger(this.FoundWords.Count, this.ActiveBoard.usedWords.Count);
		}

		private void SetActiveBoardState(WordSearchController.BoardState activeBoardState)
		{
			this.ActiveBoardState = activeBoardState;
			this.InvokeEvent(this.OnBoardStateChanged);
		}

		private void SetWordFound(string word)
		{
			this.FoundWords.Add(word);
			this.wordList.SetWordFound(word, false);
			if (this.IsTimedMode)
			{
				if (this.ActiveMode.modeName == "Challenge")
				{
					this.TimeLeft = this.ActiveDifficulty.timedModeFoundBouns;
				}
				else if (this.ActiveMode.modeName == "Quiz")
				{
					this.TimeLeft += this.ActiveDifficulty.timedModeFoundBouns;
				}
				else
				{
					this.TimeLeft += this.ActiveDifficulty.timedModeFoundBouns;
				}
			}
			this.InvokeEvent(this.OnWordFound);
			if (this.FoundWords.Count == this.ActiveBoard.usedWords.Count)
			{
				this.SetActiveBoardState(WordSearchController.BoardState.Completed);
			}
		}

		private IEnumerator SetupSavedGame()
		{
			WordSearchController._SetupSavedGame_c__Iterator0 _SetupSavedGame_c__Iterator = new WordSearchController._SetupSavedGame_c__Iterator0();
			_SetupSavedGame_c__Iterator._this = this;
			return _SetupSavedGame_c__Iterator;
		}

		private void HighlightSavedWords()
		{
			string[] array = new string[this.FoundWords.Count];
			this.FoundWords.CopyTo(array);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				this.wordList.SetWordFound(text, true);
				for (int j = 0; j < this.ActiveBoard.wordPlacements.Count; j++)
				{
					WordPlacement wordPlacement = this.ActiveBoard.wordPlacements[j];
					if (text == wordPlacement.word)
					{
						int row = wordPlacement.startingPosition.row + (text.Length - 1) * wordPlacement.verticalDirection;
						int col = wordPlacement.startingPosition.col + (text.Length - 1) * wordPlacement.horizontalDirection;
						this.characterGrid.HighlightWord(wordPlacement.startingPosition, new Position(row, col), false);
					}
				}
			}
		}

		private void InvokeEvent(Action evnt)
		{
			if (evnt != null)
			{
				evnt();
			}
		}

		private int SortWordsRandomly(string x, string y)
		{
			return UnityEngine.Random.Range(-1, 2);
		}

		private int SortWordsAlphabetically(string x, string y)
		{
			int num = 0;
			while (num < x.Length && num < y.Length)
			{
				if (x[num] != y[num])
				{
					return (int)(x[num] - y[num]);
				}
				num++;
			}
			return x.Length - y.Length;
		}

		private int SortWordsByLength(string x, string y)
		{
			return x.Length - y.Length;
		}

		private void LoadSave()
		{
			if (!File.Exists(this.SaveFilePath))
			{
				return;
			}
			JSONNode jSONNode = JSON.Parse(File.ReadAllText(this.SaveFilePath));
			int asInt = jSONNode["SelectedModeIndex"].AsInt;
			int asInt2 = jSONNode["SelectedDifficultyIndex"].AsInt;
			int asInt3 = jSONNode["ActiveModeIndex"].AsInt;
			int asInt4 = jSONNode["ActiveDifficultyIndex"].AsInt;
			int asInt5 = jSONNode["ActiveCategoryIndex"].AsInt;
			if (asInt < this.modeInfos.Count)
			{
				this.SelectedMode = this.modeInfos[asInt];
			}
			if (asInt2 < this.difficultyInfos.Count)
			{
				this.SelectedDifficulty = this.difficultyInfos[asInt2];
			}
			if (asInt3 != -1 && asInt3 < this.modeInfos.Count)
			{
				this.ActiveMode = this.modeInfos[asInt3];
			}
			if (asInt4 != -1 && asInt4 < this.difficultyInfos.Count)
			{
				this.ActiveDifficulty = this.difficultyInfos[asInt4];
			}
			if (asInt5 != -1 && asInt5 < this.categoryInfos.Count)
			{
				this.ActiveCategory = this.categoryInfos[asInt5];
			}
			if (this.ActiveMode == null || this.ActiveDifficulty == null || this.ActiveCategory == null)
			{
				return;
			}
			this.ActiveBoardState = (WordSearchController.BoardState)jSONNode["ActiveBoardState"].AsInt;
			this.ActiveSeqIndex = jSONNode["ActiveSeqIndex"].AsInt;
			this.TimeLeft = (float)jSONNode["TimeLeft"].AsDouble;
			this.LevelsToStartBeforeAd = jSONNode["LevelsToStartBeforeAd"].AsInt;
            var enumerator = jSONNode["FoundWords"].AsArray.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					JSONNode jSONNode2 = (JSONNode)enumerator.Current;
					this.FoundWords.Add(jSONNode2.Value);
				}
			}
			finally
			{
                /*
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
				*/
			}
			JSONNode jSONNode3 = jSONNode["ActiveBoard"];
			if (jSONNode3.Value != "null")
			{
				this.ActiveBoard = new Board();
				this.ActiveBoard.rowSize = jSONNode3["rowSize"].AsInt;
				this.ActiveBoard.columnSize = jSONNode3["columnSize"].AsInt;
				this.ActiveBoard.usedWords = new List<string>();
				this.ActiveBoard.boardCharacters = new List<List<char>>();
				this.ActiveBoard.wordPlacements = new List<WordPlacement>();
                var enumerator2 = jSONNode3["usedWords"].AsArray.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						JSONNode jSONNode4 = (JSONNode)enumerator2.Current;
						this.ActiveBoard.usedWords.Add(jSONNode4.Value);
					}
				}
				finally
				{
                    /*
					IDisposable disposable2;
					if ((disposable2 = (enumerator2 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
					*/
				}
                var enumerator3 = jSONNode3["boardCharacters"].AsArray.GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						JSONNode jSONNode5 = (JSONNode)enumerator3.Current;
						List<char> list = new List<char>();
                        var enumerator4 = jSONNode5.AsArray.GetEnumerator();
						try
						{
							while (enumerator4.MoveNext())
							{
								JSONNode jSONNode6 = (JSONNode)enumerator4.Current;
								list.Add((char)jSONNode6.AsInt);
							}
						}
						finally
						{
                            /*
							IDisposable disposable3;
							if ((disposable3 = (enumerator4 as IDisposable)) != null)
							{
								disposable3.Dispose();
							}
							*/
						}
						this.ActiveBoard.boardCharacters.Add(list);
					}
				}
				finally
				{
                    /*
					IDisposable disposable4;
					if ((disposable4 = (enumerator3 as IDisposable)) != null)
					{
						disposable4.Dispose();
					}
					*/
				}
                var enumerator5 = jSONNode3["wordPlacements"].AsArray.GetEnumerator();
				try
				{
					while (enumerator5.MoveNext())
					{
						JSONNode jSONNode7 = (JSONNode)enumerator5.Current;
						string[] array = jSONNode7["startingPosition"].Value.Split(new char[]
						{
							'-'
						});
						WordPlacement wordPlacement = new WordPlacement();
						wordPlacement.word = jSONNode7["word"].Value;
						wordPlacement.startingPosition = new Position(Convert.ToInt32(array[0]), Convert.ToInt32(array[1]));
						wordPlacement.verticalDirection = jSONNode7["verticalDirection"].AsInt;
						wordPlacement.horizontalDirection = jSONNode7["horizontalDirection"].AsInt;
						this.ActiveBoard.wordPlacements.Add(wordPlacement);
					}
				}
				finally
				{
                    /*
					IDisposable disposable5;
					if ((disposable5 = (enumerator5 as IDisposable)) != null)
					{
						disposable5.Dispose();
					}
					*/
				}
			}
		}

		private void Save()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["SelectedModeIndex"] = ((this.SelectedMode == null) ? 0 : this.modeInfos.IndexOf(this.SelectedMode));
			dictionary["SelectedDifficultyIndex"] = ((this.SelectedDifficulty == null) ? 0 : this.difficultyInfos.IndexOf(this.SelectedDifficulty));
			dictionary["ActiveModeIndex"] = ((this.ActiveMode == null) ? (-1) : this.modeInfos.IndexOf(this.ActiveMode));
			dictionary["ActiveDifficultyIndex"] = ((this.ActiveDifficulty == null) ? (-1) : this.difficultyInfos.IndexOf(this.ActiveDifficulty));
			dictionary["ActiveCategoryIndex"] = ((this.ActiveCategory == null) ? (-1) : this.categoryInfos.IndexOf(this.ActiveCategory));
			dictionary["ActiveBoardState"] = (int)this.ActiveBoardState;
			dictionary["ActiveSeqIndex"] = this.ActiveSeqIndex;
			dictionary["TimeLeft"] = this.TimeLeft;
			dictionary["LevelsToStartBeforeAd"] = this.LevelsToStartBeforeAd;
			string[] array = new string[this.FoundWords.Count];
			this.FoundWords.CopyTo(array);
			dictionary.Add("FoundWords", new List<string>(array));
			if (this.ActiveBoard != null)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				dictionary2["rowSize"] = this.ActiveBoard.rowSize;
				dictionary2["columnSize"] = this.ActiveBoard.columnSize;
				dictionary2["usedWords"] = this.ActiveBoard.usedWords;
				List<List<int>> list = new List<List<int>>();
				for (int i = 0; i < this.ActiveBoard.boardCharacters.Count; i++)
				{
					list.Add(new List<int>());
					for (int j = 0; j < this.ActiveBoard.boardCharacters[i].Count; j++)
					{
						list[i].Add((int)this.ActiveBoard.boardCharacters[i][j]);
					}
				}
				dictionary2["boardCharacters"] = list;
				List<object> list2 = new List<object>();
				for (int k = 0; k < this.ActiveBoard.wordPlacements.Count; k++)
				{
					Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
					dictionary3["word"] = this.ActiveBoard.wordPlacements[k].word;
					dictionary3["startingPosition"] = string.Format("{0}-{1}", this.ActiveBoard.wordPlacements[k].startingPosition.row, this.ActiveBoard.wordPlacements[k].startingPosition.col);
					dictionary3["verticalDirection"] = this.ActiveBoard.wordPlacements[k].verticalDirection;
					dictionary3["horizontalDirection"] = this.ActiveBoard.wordPlacements[k].horizontalDirection;
					list2.Add(dictionary3);
				}
				dictionary2["wordPlacements"] = list2;
				dictionary["ActiveBoard"] = dictionary2;
			}
			else
			{
				dictionary["ActiveBoard"] = "null";
			}
			File.WriteAllText(this.SaveFilePath, Utilities.ConvertToJsonString(dictionary, false));
		}
	}
}
