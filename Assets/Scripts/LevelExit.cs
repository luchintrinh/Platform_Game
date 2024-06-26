using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay=1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
        
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int index = currentSceneIndex + 1;
        if (index == SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(index);
    }
}
