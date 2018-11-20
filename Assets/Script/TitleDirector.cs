using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//
// タイトルの制御
// ボタンの演出などを自力で行っているため
// 状態のステート制御を行います。
//
public class TitleDirector : MonoBehaviour {

    enum Mode {
        start,  // 開始状態
        main,   // 入力待ち
        exit    // 終了状態
    }

    public GameObject startButton;
    public AudioClip[] voices;

    ScorePool myScorePool;
    GameObject scoreListBox;
    Mode mode;
    float waitTime;
    bool flip;
    Text startButtonText;

    // スコアリストを表示
    void SetScoreList(ScorePool scorePool) {
        string scoreText = "";
        for (int i = 0; i < 5; ++i) {
            scoreText += (i + 1) + ":";
            if (scorePool.List[i] <= 0) {
                scoreText += "-".PadLeft(5);
            }
            else {
                scoreText += scorePool.List[i].ToString().PadLeft(5);
            }
            scoreText += "\n";
        }
        scoreListBox = GameObject.Find("ScoreListBox");
        scoreListBox.GetComponent<Text>().text = scoreText;
    }

    // スタートボタンのフリップ表示
    void UpdateFlipStartButton(float waitTimeMax) {
        waitTime += Time.deltaTime;
        if (waitTime > waitTimeMax) {
            flip = flip ? false : true;
            startButtonText.color = flip ? new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0);
            waitTime -= waitTimeMax;
        }
    }

    // Use this for initialization
    void Start() {
        myScorePool = new ScorePool();
        waitTime = 0;
        mode = Mode.start;
        startButton.SetActive(false);
        myScorePool.Load();
        SetScoreList(myScorePool);
        startButtonText = startButton.transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        switch (mode) {
            case Mode.start:
                UpdateStart();
                break;
            case Mode.main:
                UpdateMain();
                break;
            case Mode.exit:
                UpdateExit();
                break;
        }
    }

    // 連打対応のため、数秒入力を無視します。
    void UpdateStart() {
        waitTime += Time.deltaTime;
        if (waitTime < 1.5f) {
            return;
        }
        mode = Mode.main;
        startButton.SetActive(true);
        waitTime = 0;
        flip = true;
    }

    // 入力待ちを行います
    void UpdateMain() {
        UpdateFlipStartButton(1.2f);

        bool isTouched = Input.GetKeyDown(KeyCode.Space);
        if (isTouched) {
            startButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    // ボイスが終わるのを待って、ゲームに遷移します。
    void UpdateExit() {
        UpdateFlipStartButton(0.1f);
        if (GetComponent<AudioSource>().isPlaying) {
            return;
        }
        SceneManager.LoadScene("GameScene");
    }

    // スタートボタンを押した時に呼ばれる。
    public void OnClickStart() {
        if (mode == Mode.exit) {
            return;
        }
        GetComponent<AudioSource>().PlayOneShot(voices[Random.Range(0, voices.Length)]);
        mode = Mode.exit;
    }

    // デバック用にスコアリストのクリアを行う。
    public void OnClickDebug() {
        myScorePool.Clear();
        SetScoreList(myScorePool);
        myScorePool.Save();
    }
}
