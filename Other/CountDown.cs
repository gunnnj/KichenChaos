using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Tham chiếu tới UI Text
    private float timeRemaining = 120; // 2 phút tính bằng giây
    private bool timerIsRunning = false;
    public PassCounter passCounter;
    public GameObject timeUp;
    public Image Completed;
    public Sprite One;
    public Sprite Two;
    public Sprite Three;

    public AudioSource stop;

    void Start()
    {
        // Bắt đầu đồng hồ đếm ngược
        timerIsRunning = true;
        UpdateTimerDisplay();
        timeUp.SetActive(false);
        Completed.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                countdownText.text="0:00";
                timerIsRunning = false;
                stop.Play();
                timeUp.SetActive(true);
                StartCoroutine(wait());
                // Thực hiện hành động khi đồng hồ kết thúc
                Debug.Log("Thời gian đã hết!");
            }
        }
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(2f);
        timeUp.SetActive(false);
        if(passCounter.Score>=80){
            Completed.sprite = Three;
        }
        else if(passCounter.Score>=40){
            Completed.sprite = Two;
        }
        else{
            Completed.sprite = One;
        }
        Completed.gameObject.SetActive(true);
       
    }

    void UpdateTimerDisplay()
    {
        // Chuyển đổi thời gian còn lại thành phút và giây
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (timeRemaining <= 10)
        {
            countdownText.color = Color.red; // Đặt màu chữ thành đỏ
        }
        else
        {
            countdownText.color = Color.black; // Đặt màu chữ về trắng
        }
       
    }
}
