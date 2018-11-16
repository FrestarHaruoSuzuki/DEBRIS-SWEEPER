using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// ゲーム中のUI表示関係に関わるクラスです。
//
public class UIController : MonoBehaviour {

    int totalScore = 0;
    GameObject scoreText;
    GameObject timeText;
    GameObject resultText;
    GameObject countDownText;
    int lostBulletNumber = 0;
    int hitBulletNumber = 0;
    float remainTime;
    bool enableScoreCount;

    // スコア計算を行い、トータルスコアに加算します。
    // 加算したスコアを返します。
    public int AddScore(int scoreSeed) {
        if (!enableScoreCount) {
            return 0;
        }
        int validBulletNumber = lostBulletNumber + hitBulletNumber;
        int additionalScore = validBulletNumber == 0 ? 0 : (scoreSeed * hitBulletNumber) / validBulletNumber;
        totalScore += additionalScore;
        //Debug.Log("HBN:" + hitBulletNumber + " LBN:" + lostBulletNumber + " DS:" + additionalScore + " S:" + totalScore);

        return additionalScore;
    }

    // 外した弾の数をカウントします。
    public void CountUpLostBullet() {
        lostBulletNumber += 1;
    }

    // ヒットした弾の数をカウントします。
    public void CountUpHitBullet() {
        hitBulletNumber += 1;
    }

    // 残り時間を設定します。
    public void SetRemainTime(float newRemainTime) {
        remainTime = newRemainTime;
    }

    // 結果スコアを表示します。
    public void SetResult(int resultScore) {
        resultText.GetComponent<Text>().text = "RESULT\n" + resultScore.ToString();
    }

    // カウントダウン演出を表示します。
    public void SetEffectCountDown(float remainTime) {
        int dispSecond = Mathf.FloorToInt(remainTime) + 1;  // 負数は考えていない。
        float alpha = remainTime % 1.0f;
        float fontSize = 88.0f + (alpha * 300.0f);
        var textComponent = countDownText.GetComponent<Text>();

        Color c = textComponent.color;
        textComponent.color = new Color(c.r, c.g, c.b, alpha);

        textComponent.fontSize = Mathf.FloorToInt(fontSize);

        textComponent.text = dispSecond.ToString();
    }

    // トータルスコアを取得します。
    public int GetTotalScore() {
        return totalScore;
    }

    // スコアカウントの加算の可否を指定します。
    public void SetEnableScoreCount(bool flag) {
        enableScoreCount = flag;
    }

    // Use this for initialization
    void Start() {
        enableScoreCount = true;
        scoreText = GameObject.Find("Score");
        timeText = GameObject.Find("Time");
        resultText = GameObject.Find("Result");
        countDownText = GameObject.Find("CountDown");
    }

    // Update is called once per frame
    void Update() {
        scoreText.GetComponent<Text>().text = "SCORE " + totalScore.ToString();
        timeText.GetComponent<Text>().text = "TIME " + remainTime.ToString("00.00");
    }
}
