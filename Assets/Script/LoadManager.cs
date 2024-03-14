using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public Animator transition;
    public Slider pgSlider;

    public void Start()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadScene(int levelIndex)
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (pgSlider.value < 1.0f)
            {
                pgSlider.value = Mathf.MoveTowards(pgSlider.value, 1.0f, Time.deltaTime);
            }
            else if (pgSlider.value >= 1.0f && operation.progress >= 0.9f)
            {
                transition.SetTrigger("Start");
                yield return new WaitForSeconds(1.0f);
                operation.allowSceneActivation = true;
            }
        }
    }
}
