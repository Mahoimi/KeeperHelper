using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    public enum HistoryMarker
    {
        One_A,
        One_B,
        One_C,

        Two_A,
        Two_B,

        Three_A,
        Three_B,

        Four_A,
        Four_B,

        Five_A,
        Five_B,

        Six_A,
        Six_B,

        Count,
    }

    [System.Serializable]
    public class QuestHistoryQuestion
    {
        public string QuestionLocId = GlobalVariables.UnknownLocId;
        public QuestHistoryAnswer[] Answers = null;
    }

    [System.Serializable]
    public class QuestHistoryAnswer
    {
        public string AnswerLocId = GlobalVariables.UnknownLocId;
        public HistoryMarker HistoryMarker = HistoryMarker.One_A;
    }
}