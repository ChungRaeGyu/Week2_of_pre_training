using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject square;

    public Text timeTxt;
    public Text nowScore;
    public Text bestScore;
    public GameObject endPanel;

    bool isPlay = true;

    float time = 0.0f;

    public Animator animator;
    string key = "bestScore";
    private void Awake() {
        if(Instance==null){
            Instance = this;
        }
    }
    void Start()
    {
        InvokeRepeating("MakeSquare",0f,1f);
        Time.timeScale=1;
    }
    void Update() {
        if(isPlay){
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
    }
    // Update is called once per frame
    void MakeSquare(){
        Instantiate(square);
    }

    public void GameOver(){
        isPlay = false;
        animator.SetBool("isDie",true);
        Invoke("TimeStop",0.5f);
        nowScore.text = time.ToString("N2");
        if(PlayerPrefs.HasKey(key)){
            float best = PlayerPrefs.GetFloat(key);
            if(best<time){
                PlayerPrefs.SetFloat(key, time);
                bestScore.text = time.ToString("N2");
            }else{
                bestScore.text = best.ToString("N2");
            }
        }else{
            PlayerPrefs.SetFloat(key, time);
            bestScore.text = time.ToString("N2");
        }
        endPanel.SetActive(true);
    }

    void TimeStop(){
        Time.timeScale = 0;
    }
}
