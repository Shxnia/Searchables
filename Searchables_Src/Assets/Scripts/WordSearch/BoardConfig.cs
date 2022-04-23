using System;
using System.Collections.Generic;

namespace WordSearch
{
	public class BoardConfig
	{
		public int rowSize;

		public int columnSize;

		public List<string> words;

		public List<string> words_question;

		public List<string> filterWords;

		public string randomCharacters = "abcdefghijklmnopqrstuvwxyz";

		public long algoTimeoutInMilliseconds = 2000L;
	}
}
