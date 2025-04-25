using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class QuizController : MonoBehaviour
{
    public float[] answers;
    private float scoreFinal = 0;
    public float answer;

    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreFinal = 0;
        answers = new float[10];
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetAnswer1(float answer)
    {
        answers[0] = answer;
        //Debug.Log("Answer Recorded");
    }
    public void SetAnswer2(float answer)
    {
        answers[1] = answer;
    }
    public void SetAnswer3(float answer)
    {
        answers[2] = answer;
    }
    public void SetAnswer4(float answer)
    {
        answers[3] = answer;
    }
    public void SetAnswer5(float answer)
    {
        answers[4] = answer;
    }
    public void SetAnswer6(float answer)
    {
        answers[5] = answer;
    }
    public void SetAnswer7(float answer)
    {
        answers[6] = answer;
    }
    public void SetAnswer8(float answer)
    {
        answers[7] = answer;
    }
    public void SetAnswer9(float answer)
    {
        answers[8] = answer;
    }
    public void SetAnswer10(float answer)
    {
        answers[9] = answer;
    }
    public void QuizEnding()
    {
        if (answers[0] == 3)
        {
            scoreFinal++;
        }
        if (answers[1] == 4)
        {
            scoreFinal++;
        }
        if (answers[2] == 3)
        {
            scoreFinal++;
        }
        if (answers[3] == 1)
        {
            scoreFinal++;
        }
        if (answers[4] == 2)
        {
            scoreFinal++;
        }
        if (answers[5] == 3)
        {
            scoreFinal++;
        }
        if (answers[6] == 2)
        {
            scoreFinal++;
        }
        if (answers[7] == 2)
        {
            scoreFinal++;
        }
        if (answers[8] == 4)
        {
            scoreFinal++;
        }
        if (answers[9] == 4)
        {
            scoreFinal++;
        }

        scoreText.SetText(scoreFinal + "/10");
        //Debug.Log(scoreFinal);

    }

     public void ReturnToGame()
     {
          SceneManager.LoadScene("Overworld");
     }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
