using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Text questionText;

    public GameObject gameOverPanel;
    public Text ScoreTxt;
    public int score;
    int totalQuestions = 0;

    public Button[] btn;
    public UserLogin userLogin;

    public string jsonFileName = "MCQ.json";

    [System.Serializable]
    public class QuestionsAnswers
    {
        public string question;
        public string[] answers;
        public int correctAnswer;
    }

    [System.Serializable]
    public class QuizData
    {
        public List<QuestionsAnswers> questions;
    }

    public void Start()
    {
        gameOverPanel.SetActive(false);
        LoadJsonFile();
        totalQuestions = QnA.Count;
        GenerateQuestion();
    }

    void LoadJsonFile()
    {
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        string jsonFileContent;

        if (jsonFilePath.Contains("://"))
        {
            UnityWebRequest www = UnityWebRequest.Get(jsonFilePath);
            www.SendWebRequest();
            while (!www.isDone) { }
            jsonFileContent = www.downloadHandler.text;
        }
        else
        {
            jsonFileContent = File.ReadAllText(jsonFilePath);
        }

        if (!string.IsNullOrEmpty(jsonFileContent))
        {
            QuizData quizData = JsonUtility.FromJson<QuizData>(jsonFileContent);
            QnA = new List<QuestionsAnswers>(quizData.questions);
        }
        else
        {
            Debug.LogError("Failed to load JSON file at path: " + jsonFilePath);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        ScoreTxt.text = score.ToString();
    }

    public void Correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    public void Wrong()
    {
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            questionText.text = QnA[currentQuestion].question;
            SetAnswer();
        }
        else
        {
            GameOver();
        }
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].answers[i];
            btn[i].image.color = new Color(0.1215686f, 0.145098f, 0.2666667f);

            if (QnA[currentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
    }
}
