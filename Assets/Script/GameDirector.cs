using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {

    enum Mode {
        main,
        result
    }

    ScorePool scorePool;

    float remainTime;
    UIController ui;
    Mode mode;
    float waitTime;

    // Use this for initialization
    void Start() {
        remainTime = 15;
        mode = Mode.main;
        ui = GameObject.Find("Canvas").GetComponent<UIController>();

        scorePool = new ScorePool();
        scorePool.Load();
    }

    // Update is called once per frame
    void Update() {
        switch (mode) {
            case Mode.main:
                UpdateGameModeMain();
                break;
            case Mode.result:
                UpdateGameModeResult();
                break;
        }

    }

    void UpdateGameModeMain() {
        remainTime -= Time.deltaTime;
        if (remainTime <= 0) {
            // ゲーム終了
            remainTime = 0;
            waitTime = 0;
            mode = Mode.result;
            GameObject.Find("Rocket").GetComponent<RocketController>().enabled = false;
            ui.SetEnableScoreCount(false);
            int totalScore = ui.GetTotalScore();
            ui.SetResult(totalScore);
            scorePool.SetScore(totalScore);
            scorePool.Save();
        }
        ui.SetRemainTime(remainTime);
    }

    void UpdateGameModeResult() {
        waitTime += Time.deltaTime;
        if (waitTime < 1.5f) {
            return;
        }

        bool isTouched = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || (waitTime > 6);

        if (isTouched) {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
