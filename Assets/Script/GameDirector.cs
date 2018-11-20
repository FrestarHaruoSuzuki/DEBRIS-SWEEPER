using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//
// ゲーム終了時の演出追加のために
// 状態のステート制御を行います。
//
public class GameDirector : MonoBehaviour {

    enum Mode {
        main,   // ゲームプレイ中
        result  // 結果表示中
    }

    ScorePool scorePool;

    float remainTime;
    UIController ui;
    Mode mode;
    float waitTime;

    // Use this for initialization
    void Start() {
        // アタック時間を設定
        remainTime = 15;

        // ゲームプレイから始める
        mode = Mode.main;

        // uiの取り置き
        ui = GameObject.Find("Canvas").GetComponent<UIController>();

        // スコアプールを準備
        scorePool = new ScorePool();
        scorePool.Load();
    }

    // Update is called once per frame
    void Update() {
        switch (mode) {
            case Mode.main: // ゲームプレイ中
                UpdateGameModeMain();
                break;
            case Mode.result:   // 結果表示中
                UpdateGameModeResult();
                break;
        }

    }

    // ゲームプレイ中の更新処理
    void UpdateGameModeMain() {
        remainTime -= Time.deltaTime;

        UpdateEffectCountDown();

        if (remainTime <= 0) {
            // 時間が過ぎたら終了処理
            ExitGameModeMain();
        }
        // 残り時間を表示
        ui.SetRemainTime(remainTime);
    }

    // 終了時間の表示制御
    void UpdateEffectCountDown() {
        if (remainTime >= 3.0f) {
            return; // 想定時間外なら何もしない。
        }
        ui.SetEffectCountDown(remainTime);
    }

    // ゲームプレイモードの終了処理
    // 今の所結果表示に繋げるだけです。
    void ExitGameModeMain() {
        // ゲーム終了
        remainTime = 0;
        waitTime = 0;

        // 結果表示演出に移行
        mode = Mode.result;

        // ユーザーの操作を禁止
        GameObject.Find("Sweeper").GetComponent<SweeperController>().enabled = false;

        // スコアのカウントを禁止
        ui.SetEnableScoreCount(false);

        // トータルスコアの取得と表示
        int totalScore = ui.GetTotalScore();
        ui.SetResult(totalScore);

        // スコアプールにスコアを登録
        scorePool.SetScore(totalScore);
        scorePool.Save();
    }

    // 結果演出中の更新処理
    // 結果表示の最初の数秒は、連打による急激な画面遷移を防ぐため
    // ユーザーの入力を受け付けません。
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
