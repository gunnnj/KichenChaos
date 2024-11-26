using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public GameObject goHiden;
    public GameObject goDis;
    public Image image;

    public AudioSource btnClick;
    void Start()
    {
        goHiden.SetActive(true);
        goDis.SetActive(false);
    }
    public void StartGame(){
        btnClick.Play();
        goHiden.SetActive(false);
        goDis.SetActive(true);
        StartCoroutine(LoadScene(1));
    }
    public void Quite(){
        btnClick.Play();
        Application.Quit();
    }
    public void Menu(){
        btnClick.Play();
        SceneManager.LoadSceneAsync(0);
    }
    IEnumerator LoadScene(int ind){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ind);

        while (!asyncLoad.isDone)
        {
            float progressAsyn = Mathf.Clamp01(asyncLoad.progress/0.9f);
            image.fillAmount = progressAsyn;
            yield return null; // Chờ cho đến frame tiếp theo
        }
        yield return null;
    }
}
