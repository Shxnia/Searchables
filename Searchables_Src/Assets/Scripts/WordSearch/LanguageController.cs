using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WordSearch
{
	public class LanguageController : SingletonComponent<LanguageController>
	{
		public int DeviceLanguage;

		public Font font_other;

		public Font font_jpn;

		public List<FontStyle> font_weight = new List<FontStyle>();

		public List<Text> text_objs = new List<Text>();

		[Header("UIScreenMain")]
		public Text UIScreenMain_Title;

		public Text UIScreenMain_Btn_Classic;

		public Text UIScreenMain_Btn_Quick;

		public Text UIScreenMain_Btn_Challenge;

		public Text UIScreenMain_Btn_Quiz;

		public Text UIScreenMain_Btn_Language;

		public Text UIScreenMain_Pop_Language_ENG;

		public Text UIScreenMain_Pop_Language_KOR;

		public Text UIScreenMain_Pop_Language_JPN;

		[Header("UIScreenOption")]
		public Text UIScreenOption_Easy;

		public Text UIScreenOption_Normal;

		public Text UIScreenOption_Hard;

		[Header("UIScreenGame")]
		public Text UIScreenGame_Challenge_Mode;

		public Text UIScreenGame_Quiz_Mode;

		[Header("PopContinue")]
		public Text PopContinue_Title;

		public Text PopContinue_Title_Theme;

		public Text PopContinue_Title_Level;

		public Text PopContinue_Btn_Yes;

		public Text PopContinue_Btn_No;

		[Header("PopSetting")]
		public Text PopSetting_Title;

		public Text PopSetting_Btn_Sound;

		public Text PopSetting_Btn_Push;

		public Text PopSetting_Btn_Review;

		public Text PopSetting_Btn_Help;

		public Text PopSetting_Btn_Confirm;

		[Header("PopPause")]
		public Text PopPause_Title;

		public Text PopPause_Btn_Sound;

		public Text PopPause_Btn_Push;

		public Text PopPause_Btn_Review;

		public Text PopPause_Btn_Help;

		public Text PopPause_Btn_Main;

		public Text PopPause_Btn_Confirm;

		[Header("PopEnd")]
		public Text PopEnd_Title;

		public Text PopEnd_Yes;

		public Text PopEnd_No;

		[Header("PopReview")]
		public Text PopReview_Title;

		public Text PopReview_Yes;

		public Text PopReview_No;

		[Header("PopFinishiClassic")]
		public Text PopFinishiClassic_Title;

		public Text PopFinishiClassic_Btn_NewGame;

		[Header("PopFinishiChallenge")]
		public Text PopFinishiChallenge_Title;

		public Text PopFinishiChallenge_Btn_NewGame;

		[Header("PopFinishiQuiz")]
		public Text PopFinishiQuiz_Title;

		public Text PopFinishiQuiz_Btn_NewGame;

		[Header("PopFinishiTutorial")]
		public Text PopFinishiTutorial_Title;

		[Header("PopHint")]
		public Text PopHint_Title;

		public Text PopHint_Yes;

		public Text PopHint_No;

		[Header("PopHelp")]
		public Text PopHelp_Title;

		public Text PopHelp_Contents;

		public Text PopHelp_Yes;

		public List<string> Text_UIScreenMain_Title = new List<string>();

		public List<string> Text_UIScreenMain_Btn_Classic = new List<string>();

		public List<string> Text_UIScreenMain_Btn_Quick = new List<string>();

		public List<string> Text_UIScreenMain_Btn_Challenge = new List<string>();

		public List<string> Text_UIScreenMain_Btn_Quiz = new List<string>();

		public List<string> Text_UIScreenMain_Btn_Language = new List<string>();

		public List<string> Text_UIScreenMain_Pop_Language_ENG = new List<string>();

		public List<string> Text_UIScreenMain_Pop_Language_KOR = new List<string>();

		public List<string> Text_UIScreenMain_Pop_Language_JPN = new List<string>();

		public List<string> Text_UIScreenOption_Easy = new List<string>();

		public List<string> Text_UIScreenOption_Normal = new List<string>();

		public List<string> Text_UIScreenOption_Hard = new List<string>();

		public List<string> Text_UIScreenOption_Tutorial = new List<string>();

		public List<string> Text_UIScreenGame_Challenge_Mode = new List<string>();

		public List<string> Text_UIScreenGame_Quiz_Mode = new List<string>();

		public List<string> Text_PopContinue_Title = new List<string>();

		public List<string> Text_PopContinue_Title_Theme = new List<string>();

		public List<string> Text_PopContinue_Title_Level = new List<string>();

		public List<string> Text_PopContinue_Btn_Yes = new List<string>();

		public List<string> Text_PopContinue_Btn_No = new List<string>();

		public List<string> Text_PopSetting_Title = new List<string>();

		public List<string> Text_PopSetting_Btn_Sound = new List<string>();

		public List<string> Text_PopSetting_Btn_Push = new List<string>();

		public List<string> Text_PopSetting_Btn_Review = new List<string>();

		public List<string> Text_PopSetting_Btn_help = new List<string>();

		public List<string> Text_PopSetting_Btn_Confirm = new List<string>();

		public List<string> Text_PopPause_Title = new List<string>();

		public List<string> Text_PopPause_Btn_Sound = new List<string>();

		public List<string> Text_PopPause_Btn_Push = new List<string>();

		public List<string> Text_PopPause_Btn_Review = new List<string>();

		public List<string> Text_PopPause_Btn_Help = new List<string>();

		public List<string> Text_PopPause_Btn_Main = new List<string>();

		public List<string> Text_PopPause_Btn_Confirm = new List<string>();

		public List<string> Text_PopEnd_Title = new List<string>();

		public List<string> Text_PopEnd_Yes = new List<string>();

		public List<string> Text_PopEnd_No = new List<string>();

		public List<string> Text_PopReview_Title = new List<string>();

		public List<string> Text_PopReview_Yes = new List<string>();

		public List<string> Text_PopReview_No = new List<string>();

		public List<string> Text_PopFinishiClassic_Title = new List<string>();

		public List<string> Text_PopFinishiClassic_Btn_NewGame = new List<string>();

		public List<string> Text_PopFinishiChallenge_Title = new List<string>();

		public List<string> Text_PopFinishiChallenge_Btn_NewGame = new List<string>();

		public List<string> Text_PopFinishiQuiz_Title = new List<string>();

		public List<string> Text_PopFinishiQuiz_Btn_NewGame = new List<string>();

		public List<string> Text_PopFinishiTutorial_Title = new List<string>();

		public List<string> Text_PopHint_Title = new List<string>();

		public List<string> Text_PopHint_Yes = new List<string>();

		public List<string> Text_PopHint_No = new List<string>();

		public List<string> Text_PopHelp_Title = new List<string>();

		public List<string> Text_PopHelp_Contents = new List<string>();

		public List<string> Text_PopHelp_Yes = new List<string>();

		private new void Awake()
		{
			if (PlayerPrefs.GetInt("Language", 100) != 100)
			{
				this.DeviceLanguage = PlayerPrefs.GetInt("Language");
			}
			else if (Application.systemLanguage == SystemLanguage.Korean)
			{
				this.DeviceLanguage = 1;
			}
			else if (Application.systemLanguage == SystemLanguage.Japanese)
			{
				this.DeviceLanguage = 2;
			}
			else
			{
				this.DeviceLanguage = 0;
			}
			this.Text_UIScreenMain_Title.Add("Searchables");
			this.Text_UIScreenMain_Btn_Classic.Add("Login");
			this.Text_UIScreenMain_Btn_Quick.Add("Sign Up");
			this.Text_UIScreenMain_Btn_Challenge.Add("Exit");
			this.Text_UIScreenMain_Btn_Quiz.Add("Play as Guest");
			this.Text_UIScreenMain_Btn_Language.Add("Language");
			this.Text_UIScreenMain_Pop_Language_ENG.Add("English");
			this.Text_UIScreenMain_Pop_Language_KOR.Add("Korean");
			this.Text_UIScreenMain_Pop_Language_JPN.Add("Japanese");
			this.Text_UIScreenOption_Easy.Add("EASY");
			this.Text_UIScreenOption_Normal.Add("NORMAL");
			this.Text_UIScreenOption_Hard.Add("HARD");
			this.Text_UIScreenOption_Tutorial.Add("TUTORIAL");
			this.Text_UIScreenGame_Challenge_Mode.Add("Challenge Mode");
			this.Text_UIScreenGame_Quiz_Mode.Add("Quiz Mode");
			this.Text_PopContinue_Title.Add("Would you like to continue \n last game");
			this.Text_PopContinue_Title_Theme.Add("Category");
			this.Text_PopContinue_Title_Level.Add("Level");
			this.Text_PopContinue_Btn_Yes.Add("Yes");
			this.Text_PopContinue_Btn_No.Add("No");
			this.Text_PopSetting_Title.Add("Setting");
			this.Text_PopSetting_Btn_Sound.Add("Sound");
			this.Text_PopSetting_Btn_Push.Add("Push Alert");
			this.Text_PopSetting_Btn_Review.Add("Review");
			this.Text_PopSetting_Btn_help.Add("Help");
			this.Text_PopSetting_Btn_Confirm.Add("Close");
			this.Text_PopPause_Title.Add("Pause");
			this.Text_PopPause_Btn_Sound.Add("Sound");
			this.Text_PopPause_Btn_Push.Add("Push Alert");
			this.Text_PopPause_Btn_Review.Add("Review");
			this.Text_PopPause_Btn_Help.Add("Help");
			this.Text_PopPause_Btn_Main.Add("Main");
			this.Text_PopPause_Btn_Confirm.Add("HOME");
			this.Text_PopEnd_Title.Add("Are you sure \n you want to exit?");
			this.Text_PopEnd_Yes.Add("Yes");
			this.Text_PopEnd_No.Add("No");
			this.Text_PopReview_Title.Add("Please write a review \n in this game!");
			this.Text_PopReview_Yes.Add("Yes");
			this.Text_PopReview_No.Add("No");
			this.Text_PopFinishiClassic_Title.Add("CLEAR!");
			this.Text_PopFinishiClassic_Btn_NewGame.Add("New Game");
			this.Text_PopFinishiChallenge_Title.Add("GAME OVER");
			this.Text_PopFinishiChallenge_Btn_NewGame.Add("New Game");
			this.Text_PopFinishiQuiz_Title.Add("GAME OVER");
			this.Text_PopFinishiQuiz_Btn_NewGame.Add("New Game");
			this.Text_PopFinishiTutorial_Title.Add("Good Job \n Let's Play!");
			this.Text_PopHint_Title.Add("Would you watch the ad and get three more hints?");
			this.Text_PopHint_Yes.Add("Yes");
			this.Text_PopHint_No.Add("No");
			this.Text_PopHelp_Title.Add("HELP");
			this.Text_PopHelp_Contents.Add("This is a game where you find the word suggested at the top in randomly mixed letters and drag it from the first letter to the last letter.\n\nClassic Mode: You can start the game with the desired theme and difficulty.Easy level is 8x8, normal level is 10x10, difficult level is 12x12.The higher the difficulty, the more words you need to find.\n\nQuick Start: This mode randomly starts the theme and level of a classic mode.\n\nChallenge Mode: This mode must find the word shown at the top within the time limit.The higher the level, the more letters are show and the less time allowed.\n\nQuiz Mode: This mode finds the answer to the quiz shown above within the time limit.The number of characters for the answer is displayed, and if you don't know the answer, you can see a hint. The higher the level, the more letters are show and the less time allowed.");
			this.Text_PopHelp_Yes.Add("Close");
			this.Text_UIScreenMain_Title.Add("단어찾기의 신");
			this.Text_UIScreenMain_Btn_Classic.Add("클래식게임");
			this.Text_UIScreenMain_Btn_Quick.Add("빠른시작");
			this.Text_UIScreenMain_Btn_Challenge.Add("도전모드");
			this.Text_UIScreenMain_Btn_Quiz.Add("퀴즈모드");
			this.Text_UIScreenMain_Btn_Language.Add("언어설정");
			this.Text_UIScreenMain_Pop_Language_ENG.Add("영어");
			this.Text_UIScreenMain_Pop_Language_KOR.Add("한국어");
			this.Text_UIScreenMain_Pop_Language_JPN.Add("일본어");
			this.Text_UIScreenOption_Easy.Add("쉬움");
			this.Text_UIScreenOption_Normal.Add("보통");
			this.Text_UIScreenOption_Hard.Add("어려움");
			this.Text_UIScreenOption_Tutorial.Add("튜토리얼");
			this.Text_UIScreenGame_Challenge_Mode.Add("도전모드");
			this.Text_UIScreenGame_Quiz_Mode.Add("퀴즈모드");
			this.Text_PopContinue_Title.Add("지난게임을 이어서 하시겠습니까?");
			this.Text_PopContinue_Title_Theme.Add("테마");
			this.Text_PopContinue_Title_Level.Add("난이도");
			this.Text_PopContinue_Btn_Yes.Add("예");
			this.Text_PopContinue_Btn_No.Add("아니요");
			this.Text_PopSetting_Title.Add("설정");
			this.Text_PopSetting_Btn_Sound.Add("사운드");
			this.Text_PopSetting_Btn_Push.Add("푸쉬알림");
			this.Text_PopSetting_Btn_Review.Add("리뷰");
			this.Text_PopSetting_Btn_help.Add("도움말");
			this.Text_PopSetting_Btn_Confirm.Add("확인");
			this.Text_PopPause_Title.Add("잠시멈춤");
			this.Text_PopPause_Btn_Sound.Add("사운드");
			this.Text_PopPause_Btn_Push.Add("푸쉬알림");
			this.Text_PopPause_Btn_Review.Add("리뷰");
			this.Text_PopPause_Btn_Help.Add("도움말");
			this.Text_PopPause_Btn_Main.Add("메인");
			this.Text_PopPause_Btn_Confirm.Add("홈으로");
			this.Text_PopEnd_Title.Add("앱을 종료 하시겠습니까?");
			this.Text_PopEnd_Yes.Add("예");
			this.Text_PopEnd_No.Add("아니요");
			this.Text_PopReview_Title.Add("이 앱을 평가해주세요!");
			this.Text_PopReview_Yes.Add("예");
			this.Text_PopReview_No.Add("아니요");
			this.Text_PopFinishiClassic_Title.Add("클리어!");
			this.Text_PopFinishiClassic_Btn_NewGame.Add("새로운게임");
			this.Text_PopFinishiChallenge_Title.Add("게임오버");
			this.Text_PopFinishiChallenge_Btn_NewGame.Add("재도전");
			this.Text_PopFinishiQuiz_Title.Add("게임오버");
			this.Text_PopFinishiQuiz_Btn_NewGame.Add("재도전");
			this.Text_PopFinishiTutorial_Title.Add("와우! 잘 하셨어요. 이제 시작해 볼까요?");
			this.Text_PopHint_Title.Add("광고를 보시고 \n힌트 3개 더 얻어보세요!");
			this.Text_PopHint_Yes.Add("예");
			this.Text_PopHint_No.Add("아니요");
			this.Text_PopHelp_Title.Add("도움말");
			this.Text_PopHelp_Contents.Add("랜덤으로 섞인 글자들 속에서 상단에 제시되는 단어를 찾아 첫 글자부터 마지막 글자까지 드래그하여 맞추는 게임입니다.\n\n클래식게임: 원하는 주제와 난이도로 게임을 시작할 수 있습니다.쉬움은 8x8, 보통은 10x10, 어려움은 12x12의 글자판으로 이루어지고, 난이도가 올라갈 수록 찾아야하는 단어의 수가 많아집니다.\n\n빠른시작 : 클래식게임의 주제와 난이도를 랜덤하게 시작합니다.\n\n도전모드 : 제한시간 내에 상단에 제시되는 단어를 찾는 모드입니다.레벨이 올라갈 수록 글자들이 많아지며, 시간제한이 줄어듭니다.\n\n퀴즈모드 : 제한시간 내에 상단에 제시되는 퀴즈에 대한 답을 찾는 모드입니다.답에 대한 글자 수가 표시되고, 답을 모르겠으면 힌트를 볼 수 있습니다.레벨이 올라갈 수록 글자들이 많아지며, 시간제한이 줄어듭니다. \n\n v103");
			this.Text_PopHelp_Yes.Add("확인");
			this.Text_UIScreenMain_Title.Add("単語パズルの神");
			this.Text_UIScreenMain_Btn_Classic.Add("クラシック\nモード");
			this.Text_UIScreenMain_Btn_Quick.Add("クイック\nスタート");
			this.Text_UIScreenMain_Btn_Challenge.Add("チャレンジ\nモード");
			this.Text_UIScreenMain_Btn_Quiz.Add("クイズ\nモード");
			this.Text_UIScreenMain_Btn_Language.Add("言語設定");
			this.Text_UIScreenMain_Pop_Language_ENG.Add("英語");
			this.Text_UIScreenMain_Pop_Language_KOR.Add("韓国語");
			this.Text_UIScreenMain_Pop_Language_JPN.Add("日本語");
			this.Text_UIScreenOption_Easy.Add("かんたん");
			this.Text_UIScreenOption_Normal.Add("ふつう");
			this.Text_UIScreenOption_Hard.Add("むずかしい");
			this.Text_UIScreenOption_Tutorial.Add("チュートリアル");
			this.Text_UIScreenGame_Challenge_Mode.Add("挑戦モード");
			this.Text_UIScreenGame_Quiz_Mode.Add("クイズモード");
			this.Text_PopContinue_Title.Add("前のゲームをつづぎますか。");
			this.Text_PopContinue_Title_Theme.Add("テーマ");
			this.Text_PopContinue_Title_Level.Add("むずかしさ");
			this.Text_PopContinue_Btn_Yes.Add("はい");
			this.Text_PopContinue_Btn_No.Add("いいえ");
			this.Text_PopSetting_Title.Add("設定");
			this.Text_PopSetting_Btn_Sound.Add("サウンド");
			this.Text_PopSetting_Btn_Push.Add("プッシュメッセージ");
			this.Text_PopSetting_Btn_Review.Add("レビュー");
			this.Text_PopSetting_Btn_help.Add("ヘルプ");
			this.Text_PopSetting_Btn_Confirm.Add("OK");
			this.Text_PopPause_Title.Add("パズ");
			this.Text_PopPause_Btn_Sound.Add("サウンド");
			this.Text_PopPause_Btn_Push.Add("プッシュメッセージ");
			this.Text_PopPause_Btn_Review.Add("レビュー");
			this.Text_PopPause_Btn_Help.Add("ヘルプ");
			this.Text_PopPause_Btn_Main.Add("メイン");
			this.Text_PopPause_Btn_Confirm.Add("ホーム");
			this.Text_PopEnd_Title.Add("ゲームを終了しましょうか。");
			this.Text_PopEnd_Yes.Add("はい");
			this.Text_PopEnd_No.Add("いいえ");
			this.Text_PopReview_Title.Add("このゲームを評価してください!");
			this.Text_PopReview_Yes.Add("はい");
			this.Text_PopReview_No.Add("いいえ");
			this.Text_PopFinishiClassic_Title.Add("クリア!");
			this.Text_PopFinishiClassic_Btn_NewGame.Add("新しいゲーム");
			this.Text_PopFinishiChallenge_Title.Add("ゲームオーバー");
			this.Text_PopFinishiChallenge_Btn_NewGame.Add("新しいゲーム");
			this.Text_PopFinishiQuiz_Title.Add("ゲームオーバー");
			this.Text_PopFinishiQuiz_Btn_NewGame.Add("新しいゲーム");
			this.Text_PopFinishiTutorial_Title.Add("とてもお上手でした。これから始めましょう!");
			this.Text_PopHint_Title.Add("広告を見ればヒント3個が手に入ります。");
			this.Text_PopHint_Yes.Add("はい");
			this.Text_PopHint_No.Add("いいえ");
			this.Text_PopHelp_Title.Add("ヘルプ");
			this.Text_PopHelp_Contents.Add("ランダムな文字の中で上段に提示される単語を探して初文字から最後の文字までドラッグして合わせるゲームです。\n\nクラシックモード: 好きなテーマと難易度でゲームを始めることができます。 かんたんなことは8x8, 通常は10x10, 困難は12x12の文字板で成り, 難易度が上がるほど, 探さなければならない単語の数が多くなります。\n\nクイックスタート: クラシックゲームの主題と難易度をランダムに始めます。\n\nチャレンジモード: 制限時間内に上段に提示される単語を探すモードです。 レベルが上がるほど文字が多くなり, 時間制限が減ります。\n\nクイズモード: 制限時間内に上段に提示されるクイズに対する答えを見つけるモードです。 答えの文字数が表示され, 答えが分からなければヒントを見ることができます。 レベルが上がるほど文字が多くなり, 時間制限が減ります。");
			this.Text_PopHelp_Yes.Add("確認");
			this.text_objs.Add(this.UIScreenMain_Title);
			this.text_objs.Add(this.UIScreenMain_Btn_Classic);
			this.text_objs.Add(this.UIScreenMain_Btn_Quick);
			this.text_objs.Add(this.UIScreenMain_Btn_Challenge);
			this.text_objs.Add(this.UIScreenMain_Btn_Quiz);
			this.text_objs.Add(this.UIScreenMain_Btn_Language);
			this.text_objs.Add(this.UIScreenMain_Pop_Language_ENG);
			this.text_objs.Add(this.UIScreenMain_Pop_Language_KOR);
			this.text_objs.Add(this.UIScreenMain_Pop_Language_JPN);
			this.text_objs.Add(this.UIScreenOption_Easy);
			this.text_objs.Add(this.UIScreenOption_Normal);
			this.text_objs.Add(this.UIScreenOption_Hard);
			this.text_objs.Add(this.UIScreenGame_Challenge_Mode);
			this.text_objs.Add(this.UIScreenGame_Quiz_Mode);
			this.text_objs.Add(this.PopContinue_Title);
			this.text_objs.Add(this.PopContinue_Title_Theme);
			this.text_objs.Add(this.PopContinue_Title_Level);
			this.text_objs.Add(this.PopContinue_Btn_Yes);
			this.text_objs.Add(this.PopContinue_Btn_No);
			this.text_objs.Add(this.PopSetting_Title);
			this.text_objs.Add(this.PopSetting_Btn_Sound);
			this.text_objs.Add(this.PopSetting_Btn_Push);
			this.text_objs.Add(this.PopSetting_Btn_Review);
			this.text_objs.Add(this.PopSetting_Btn_Help);
			this.text_objs.Add(this.PopSetting_Btn_Confirm);
			this.text_objs.Add(this.PopPause_Title);
			this.text_objs.Add(this.PopPause_Btn_Sound);
			this.text_objs.Add(this.PopPause_Btn_Push);
			this.text_objs.Add(this.PopPause_Btn_Review);
			this.text_objs.Add(this.PopPause_Btn_Help);
			this.text_objs.Add(this.PopPause_Btn_Main);
			this.text_objs.Add(this.PopPause_Btn_Confirm);
			this.text_objs.Add(this.PopEnd_Title);
			this.text_objs.Add(this.PopEnd_Yes);
			this.text_objs.Add(this.PopEnd_No);
			this.text_objs.Add(this.PopReview_Title);
			this.text_objs.Add(this.PopReview_Yes);
			this.text_objs.Add(this.PopReview_No);
			this.text_objs.Add(this.PopFinishiClassic_Title);
			this.text_objs.Add(this.PopFinishiClassic_Btn_NewGame);
			this.text_objs.Add(this.PopFinishiChallenge_Title);
			this.text_objs.Add(this.PopFinishiChallenge_Btn_NewGame);
			this.text_objs.Add(this.PopFinishiQuiz_Title);
			this.text_objs.Add(this.PopFinishiQuiz_Btn_NewGame);
			this.text_objs.Add(this.PopFinishiTutorial_Title);
			this.text_objs.Add(this.PopHint_Title);
			this.text_objs.Add(this.PopHint_Yes);
			this.text_objs.Add(this.PopHint_No);
			this.text_objs.Add(this.PopHelp_Title);
			this.text_objs.Add(this.PopHelp_Contents);
			this.text_objs.Add(this.PopHelp_Yes);
			for (int i = 0; i < this.text_objs.Count; i++)
			{
				this.font_weight.Add(this.text_objs[i].fontStyle);
			}
			this.Change_Language_UI(this.DeviceLanguage);
		}

		public void Change_Language_UI(int index)
		{
			this.UIScreenMain_Title.text = this.Text_UIScreenMain_Title[index];
			this.UIScreenMain_Btn_Classic.text = this.Text_UIScreenMain_Btn_Classic[index];
			this.UIScreenMain_Btn_Quick.text = this.Text_UIScreenMain_Btn_Quick[index];
			this.UIScreenMain_Btn_Challenge.text = this.Text_UIScreenMain_Btn_Challenge[index];
			this.UIScreenMain_Btn_Quiz.text = this.Text_UIScreenMain_Btn_Quiz[index];
			this.UIScreenMain_Btn_Language.text = this.Text_UIScreenMain_Btn_Language[index];
			this.UIScreenMain_Pop_Language_ENG.text = this.Text_UIScreenMain_Pop_Language_ENG[index];
			this.UIScreenMain_Pop_Language_KOR.text = this.Text_UIScreenMain_Pop_Language_KOR[index];
			this.UIScreenMain_Pop_Language_JPN.text = this.Text_UIScreenMain_Pop_Language_JPN[index];
			this.UIScreenOption_Easy.text = this.Text_UIScreenOption_Easy[index];
			this.UIScreenOption_Normal.text = this.Text_UIScreenOption_Normal[index];
			this.UIScreenOption_Hard.text = this.Text_UIScreenOption_Hard[index];
			this.UIScreenGame_Challenge_Mode.text = this.Text_UIScreenGame_Challenge_Mode[index];
			this.UIScreenGame_Quiz_Mode.text = this.Text_UIScreenGame_Quiz_Mode[index];
			this.PopContinue_Title.text = this.Text_PopContinue_Title[index];
			this.PopContinue_Title_Theme.text = this.Text_PopContinue_Title_Theme[index];
			this.PopContinue_Title_Level.text = this.Text_PopContinue_Title_Level[index];
			this.PopContinue_Btn_Yes.text = this.Text_PopContinue_Btn_Yes[index];
			this.PopContinue_Btn_No.text = this.Text_PopContinue_Btn_No[index];
			this.PopSetting_Title.text = this.Text_PopSetting_Title[index];
			this.PopSetting_Btn_Sound.text = this.Text_PopSetting_Btn_Sound[index];
			this.PopSetting_Btn_Push.text = this.Text_PopSetting_Btn_Push[index];
			this.PopSetting_Btn_Review.text = this.Text_PopSetting_Btn_Review[index];
			this.PopSetting_Btn_Help.text = this.Text_PopSetting_Btn_help[index];
			this.PopSetting_Btn_Confirm.text = this.Text_PopSetting_Btn_Confirm[index];
			this.PopPause_Title.text = this.Text_PopPause_Title[index];
			this.PopPause_Btn_Sound.text = this.Text_PopPause_Btn_Sound[index];
			this.PopPause_Btn_Push.text = this.Text_PopPause_Btn_Push[index];
			this.PopPause_Btn_Review.text = this.Text_PopPause_Btn_Review[index];
			this.PopPause_Btn_Help.text = this.Text_PopPause_Btn_Help[index];
			this.PopPause_Btn_Main.text = this.Text_PopPause_Btn_Main[index];
			this.PopPause_Btn_Confirm.text = this.Text_PopPause_Btn_Confirm[index];
			this.PopEnd_Title.text = this.Text_PopEnd_Title[index];
			this.PopEnd_Yes.text = this.Text_PopEnd_Yes[index];
			this.PopEnd_No.text = this.Text_PopEnd_No[index];
			this.PopReview_Title.text = this.Text_PopReview_Title[index];
			this.PopReview_Yes.text = this.Text_PopReview_Yes[index];
			this.PopReview_No.text = this.Text_PopReview_No[index];
			this.PopFinishiClassic_Title.text = this.Text_PopFinishiClassic_Title[index];
			this.PopFinishiClassic_Btn_NewGame.text = this.Text_PopFinishiClassic_Btn_NewGame[index];
			this.PopFinishiChallenge_Title.text = this.Text_PopFinishiChallenge_Title[index];
			this.PopFinishiChallenge_Btn_NewGame.text = this.Text_PopFinishiChallenge_Btn_NewGame[index];
			this.PopFinishiQuiz_Title.text = this.Text_PopFinishiQuiz_Title[index];
			this.PopFinishiQuiz_Btn_NewGame.text = this.Text_PopFinishiQuiz_Btn_NewGame[index];
			this.PopFinishiTutorial_Title.text = this.Text_PopFinishiTutorial_Title[index];
			this.PopHint_Title.text = this.Text_PopHint_Title[index];
			this.PopHint_Yes.text = this.Text_PopHint_Yes[index];
			this.PopHint_No.text = this.Text_PopHint_No[index];
			this.PopHelp_Title.text = this.Text_PopHelp_Title[index];
			this.PopHelp_Contents.text = this.Text_PopHelp_Contents[index];
			this.PopHelp_Yes.text = this.Text_PopHelp_Yes[index];
			if (index == 2)
			{
				for (int i = 0; i < this.text_objs.Count; i++)
				{
					this.text_objs[i].fontStyle = FontStyle.Normal;
					this.text_objs[i].font = this.font_jpn;
				}
			}
			else
			{
				for (int j = 0; j < this.text_objs.Count; j++)
				{
					this.text_objs[j].fontStyle = this.font_weight[j];
					this.text_objs[j].font = this.font_other;
				}
			}
		}

		public string ModeNameEasy()
		{
			return this.Text_UIScreenOption_Easy[SingletonComponent<WordSearchController>.Instance.Language];
		}

		public string ModeNameNormal()
		{
			return this.Text_UIScreenOption_Normal[SingletonComponent<WordSearchController>.Instance.Language];
		}

		public string ModeNameHard()
		{
			return this.Text_UIScreenOption_Hard[SingletonComponent<WordSearchController>.Instance.Language];
		}

		public string ModeNameTutorial()
		{
			return this.Text_UIScreenOption_Tutorial[SingletonComponent<WordSearchController>.Instance.Language];
		}
	}
}
