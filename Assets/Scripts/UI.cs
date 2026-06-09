using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Animator animator;


    public void NextSceneWithDelay(float delay = 0.3f)
    {
        StartCoroutine(LoadSceneWithDelay(SceneManager.GetActiveScene().buildIndex + 1, delay));

        animator.SetTrigger("Start");
    }

    public void PreviousSceneWithDelay(float delay = 0.3f)
    {
        StartCoroutine(LoadSceneWithDelay(SceneManager.GetActiveScene().buildIndex - 1, delay));
        
        animator.SetTrigger("Start");
    }

    IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneIndex);
    }

    public void ApplicationQuit()
    {
        Application.Quit();

        Debug.Log("Exited");
    }
}

