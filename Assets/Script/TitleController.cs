using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    enum Mode {
        start,
        main,
        exit
    }

    public GameObject startButton;
    public AudioClip[] voices;

    ScorePool myScorePool;
    GameObject scoreListBox;
    Mode mode;
    float waitTime;
    bool flip;
    Text startButtonText;

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

    void UpdateMain() {
        UpdateFlipStartButton(1.2f);

        bool isTouched = Input.GetKeyDown(KeyCode.Space);
        if (isTouched) {
            startButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    void UpdateExit() {
        UpdateFlipStartButton(0.1f);
        if (GetComponent<AudioSource>().isPlaying) {
            return;
        }
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickStart() {
        if (mode != Mode.exit) {
            GetComponent<AudioSource>().PlayOneShot(voices[Random.Range(0, voices.Length)]);
            mode = Mode.exit;
        }
    }

    public void OnClickDebug() {
        myScorePool.Clear();
        SetScoreList(myScorePool);
        myScorePool.Save();
    }
}
