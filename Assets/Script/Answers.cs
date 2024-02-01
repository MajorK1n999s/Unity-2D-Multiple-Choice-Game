using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answers : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public Button btn;


    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer ");
            btn.image.color = Color.green;
            Invoke("Correct", 1.5f);
            
        }
        else
        {
            Debug.Log("Wrong Answer ");
            btn.image.color = Color.red;
            Invoke("wrong", 1.5f);
        }
    }

    void Correct()
    {
        quizManager.Correct();
    }

    void wrong()
    {
        quizManager.Wrong();
    }
}
