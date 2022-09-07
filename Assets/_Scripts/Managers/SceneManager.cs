using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
    [SerializeField] GameObject NextLevelCanvas;

    // Start is called before the first frame update
    private void Start()
    {
        Movement.LevelCompleted += Movement_LevelCompleted;
    }

    private void Movement_LevelCompleted()
    {
        OpenNextLevel();
    }

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void NextScene()
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings != UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    public  void OpenNextLevel()
    {
        NextLevelCanvas.SetActive(true);
        Movement.LevelCompleted -= Movement_LevelCompleted;
    }
}
