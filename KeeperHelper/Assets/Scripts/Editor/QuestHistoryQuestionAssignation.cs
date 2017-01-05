using KeeperHelper.Utils;
using System;
using UnityEditor;
using UnityEngine;

namespace KeeperHelper
{
    public class QuestHistoryQuestionAssignation
    {
        private const string c_menuPath = "Keeper Helper/Offline Tools/Assign Questions";
        private const string c_questHistoryQuestionCSV = "TextFiles/QuestHistoryQuestions";
        private const int c_asciiOfA = 65;

        private static string[] s_englishNumbers = { "Zero", "One", "Two", "Three", "Four", "Five", "Six" };

        [MenuItem(c_menuPath)]
        private static void AssignQuestions()
        {
            string[][] data = CSVReader.Read(c_questHistoryQuestionCSV,';');

            for (int i = 0; i < data.Length; i++)
            {
                string[] column = data[i];
                string questId = column[0];

                // Get quest
                Quest quest = Resources.Load<Quest>(GlobalVariables.QuestAssetPath + questId);

                if (quest == null)
                    continue;

                string removeStr = "Quest_";
                string partId = questId.Remove(0, removeStr.Length);

                // Create questions
                int questionsCount = GetQuestionsCount(column);
                quest.Questions = new QuestHistoryQuestion[questionsCount];

                for (int questionIndex = 1; questionIndex < questionsCount+1; questionIndex++)
                {
                    // Assign data to question
                    QuestHistoryQuestion question = new QuestHistoryQuestion();

                    int answerCount = int.Parse(column[questionIndex]);
                    question.QuestionLocId = string.Format("QHQ_{0}_{1}", partId, questionIndex);
                    question.Answers = new QuestHistoryAnswer[answerCount];

                    for (int k = 0; k < answerCount; k++)
                    {
                        // Assign data to answer
                        QuestHistoryAnswer answer = new QuestHistoryAnswer();

                        char answerVariant = (char)(65 + k);
                        answer.AnswerLocId = string.Format("QHA_{0}_{1}_{2}", partId, questionIndex, answerVariant);

                        string historyMarkerStr = string.Format("{0}_{1}", s_englishNumbers[questionIndex], answerVariant);
                        answer.HistoryMarker = (HistoryMarker)Enum.Parse(typeof(HistoryMarker), historyMarkerStr);

                        question.Answers[k] = answer;
                    }

                    quest.Questions[questionIndex-1] = question;
                }

                // Save asset
                EditorUtility.SetDirty(quest);
            }
        }

        private static int GetQuestionsCount(string[] data)
        {
            int questionsCount = 0;
            for (int i = 1; i < data.Length; i++)
            {
                string answerCountStr = data[i];
                if (string.IsNullOrEmpty(answerCountStr))
                    break;

                int answerCount = int.Parse(answerCountStr);
                if (answerCount == 0)
                    break;

                questionsCount++;
            }
            return questionsCount;
        }
    }
}