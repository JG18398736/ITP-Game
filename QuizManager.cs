using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class QuizManager : MonoBehaviour
{

    public LevelManager theLevelManager;

    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;

    [SerializeField]
    private Text qstText;

    public void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0) 
            {
            unansweredQuestions = questions.ToList<Question>();
            }
        SetCurrentQuestion();

        theLevelManager = GetComponent<LevelManager>();
    }
    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        qstText.text=currentQuestion.qst;

        unansweredQuestions.RemoveAt(randomQuestionIndex);
    }
    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct");
            theLevelManager.Respawn();
        }
        else
        {
            Debug.Log("Incorrect");
            theLevelManager.Respawn();
        }
    }
        public void UserSelectFalse()
        {
        if (!currentQuestion.isTrue)
         {
            Debug.Log("Correct");
            theLevelManager.Respawn();

         }
         else
         {
             Debug.Log("Incorrect");
             theLevelManager.Respawn();
         }
        }

}

