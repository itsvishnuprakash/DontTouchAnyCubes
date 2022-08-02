using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highscoreText;
    public Slider slider;
    public AudioSource btnAudio;
    public GameObject loadingScreen;
    int highscore;
    
    void Start()
    {
        highscore=PlayerPrefs.GetInt("highscore");
        highscoreText.text="Highscore : "+highscore+"s";

    }
    public void Quit()
    {
        Application.Quit();
        btnAudio.Play();
    }
    public void Play(int sceneIndex)
    {
        btnAudio.Play();
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation=SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress/0.9f);
            slider.value=progress;
            yield return null;
        }
    }

    
}
