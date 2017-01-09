using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using KeeperHelper.Utils;

namespace KeeperHelper
{
	public class VariantSelectionUI : MonoBehaviour 
	{
        [SerializeField]
        private LocalizedText m_questionText = null;

        [SerializeField]
        private LocalizedText[] m_answerTexts = null;

        private Quest m_quest = null;

        private uint m_currentQuestionNumber = 0;
        public uint CurrentQuestionNumber { get { return m_currentQuestionNumber; } }

        public void ManualAwake() 
		{
            Assert.IsNotNull(m_questionText);
            Assert.IsNotNull(m_answerTexts);
            Assert.IsTrue(m_answerTexts.Length > 0);
        }

        public void SetQuest(Quest quest)
        {
            Assert.IsNotNull(quest);
            m_quest = quest;
        }

        public void SetQuestHistoryQuestion(uint questionNumber)
        {
            Assert.IsNotNull(m_quest);
            m_currentQuestionNumber = questionNumber;

            QuestHistoryQuestion question = m_quest.Questions[m_currentQuestionNumber];

            m_questionText.UpdateKey(question.QuestionLocId);
    
            for (int i = 0; i < m_answerTexts.Length ; i++)
            {
                bool isActive = (i < question.Answers.Length);
                m_answerTexts[i].transform.parent.gameObject.SetActive(isActive);
                if(isActive)
                    m_answerTexts[i].UpdateKey(question.Answers[i].AnswerLocId);
            }
        }

        public void ShowMenu()
        {
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
        }

        public void OnHistoryAnswerClick(int answerNumber)
        {
            // If last question
            if (m_currentQuestionNumber + 1 >= m_quest.Questions.Length)
            {
                MainProcess.Instance.UIManager.OnFinalQuestionAnswered();
            }
            // else ask next question
            else
            {
                // Store history marker
                HistoryMarker savedMarker = m_quest.Questions[m_currentQuestionNumber].Answers[answerNumber].HistoryMarker;
                MainProcess.Instance.UIManager.OnHistoryAnswerClick();

                SetQuestHistoryQuestion(m_currentQuestionNumber + 1);
            }
        }
    }
}