using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    int totalScore = 0;
    GameObject scoreText;
    GameObject timeText;
    GameObject resultText;
    int lostBulletNumber = 0;
    int hitBulletNumber = 0;
    float remainTime;
    bool enableScoreCount;

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

    public void CountUpLostBullet() {
        lostBulletNumber += 1;
    }

    public void CountUpHitBullet() {
        hitBulletNumber += 1;
    }

    public void SetRemainTime(float newRemainTime) {
        remainTime = newRemainTime;
    }

    public void SetResult(int resultScore) {
        resultText.GetComponent<Text>().text = "RESULT\n" + resultScore.ToString();
    }

    public int GetTotalScore() {
        return totalScore;
    }

    public void SetEnableScoreCount(bool flag) {
        enableScoreCount = flag;
    }

    // Use this for initialization
    void Start() {
        enableScoreCount = true;
        scoreText = GameObject.Find("Score");
        timeText = GameObject.Find("Time");
        resultText = GameObject.Find("Result");
    }

    // Update is called once per frame
    void Update() {
        scoreText.GetComponent<Text>().text = "SCORE " + totalScore.ToString();
        timeText.GetComponent<Text>().text = "TIME " + remainTime.ToString("00.00");
    }
}
