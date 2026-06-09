using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public GameObject healthBar;
    public GameObject enemyHealthBar;

    public Question[] hiraganaQuestions;
    public Question[] katakanaQuestions;
    public string[] commonAnswers;  // Add a common array for answers


    [SerializeField] private float timeBetweenQuestions;

    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField] private TMP_Text factText;
    [SerializeField] private Button[] answerButtons;

    private int correctAnswerIndex;

    private bool isAnsweringQuestion = false;

    void Start()
    {
        LoadSelectedCategory();
        SetCurrentQuestion();
    }

    private void LoadSelectedCategory()
    {
        bool isHiraganaSelected = PlayerPrefs.GetInt("IsHiraganaSelected", 1) == 1;

        if (isHiraganaSelected)
        {
            unansweredQuestions = hiraganaQuestions.ToList();
        }
        else
        {
            unansweredQuestions = katakanaQuestions.ToList();
        }
    }


    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.question;

        List<string> answerChoices = new List<string>();

        // Use commonAnswers array for indexing
        answerChoices.Add(commonAnswers[currentQuestion.correctAnswerIndex]);
        List<string> incorrectAnswers = commonAnswers.ToList();

        incorrectAnswers.RemoveAt(currentQuestion.correctAnswerIndex);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, incorrectAnswers.Count);
            answerChoices.Add(incorrectAnswers[randomIndex]);
            incorrectAnswers.RemoveAt(randomIndex);
        }

        // Shuffle
        int n = answerChoices.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            string temp = answerChoices[k];
            answerChoices[k] = answerChoices[n];
            answerChoices[n] = temp;
        }

        // Text for buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = answerChoices[i];
        }

        // Correct answer index
        correctAnswerIndex = answerChoices.IndexOf(commonAnswers[currentQuestion.correctAnswerIndex]);
    }



    public void CheckAnswer(int answerIndex)
    {
        if (isAnsweringQuestion)
        {
            Debug.Log("Wait for the next question!");
            return;
        }

        isAnsweringQuestion = true; // Gracz aktualnie odpowiada

        StartCoroutine(CheckAnswerCoroutine(answerIndex));
    }

    private IEnumerator CheckAnswerCoroutine(int answerIndex)
    {
        //yield return new WaitForSeconds(0.3f);

        // Store the original colors of the buttons
        Color[] originalColors = new Color[answerButtons.Length];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            originalColors[i] = answerButtons[i].GetComponent<Image>().color;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i == correctAnswerIndex)
            {
                // Change the color of the correct answer button to a specific green color (0.1f, 0.8f, 0.1f)
                answerButtons[i].GetComponent<Image>().color = new Color(0.1f, 0.8f, 0.1f);
            }
            else if (i == answerIndex)
            {
                // Change the color of the selected (incorrect) answer button to a specific red color (0.8f, 0.1f, 0.1f)
                answerButtons[i].GetComponent<Image>().color = new Color(0.8f, 0.1f, 0.1f);
            }
        }

        yield return new WaitForSeconds(1f); // Wait for a short time to show the button colors

        // Reset button colors to their original state
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Image>().color = originalColors[i];
        }

        if (answerIndex == correctAnswerIndex)
        {
            Debug.Log("Correct!");

            Attack();
            enemyHealthBar.GetComponent<EnemyHealthBar>().TakeDamage(1);
        }
        else
        {
            Debug.Log("Wrong answer");

            GetHit();
            healthBar.GetComponent<HealthBar>().TakeDamage(1);
        }

        yield return new WaitForSeconds(timeBetweenQuestions); // Wait for the next question

        isAnsweringQuestion = false;

        SetCurrentQuestion();
    }

    void Attack()
    {
        playerAnimator.SetTrigger("Samurai Attack");

        enemyAnimator.SetTrigger("Hit");
    }

    void GetHit()
    {
        playerAnimator.SetTrigger("Samurai Hit");

        enemyAnimator.SetTrigger("Attack 1");
    }

}

