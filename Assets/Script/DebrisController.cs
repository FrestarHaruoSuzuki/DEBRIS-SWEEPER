using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// Rockとなっているが正式にはDebris
//
public class DebrisController : MonoBehaviour {

    public GameObject localScorePrefab;
    Vector3 fallVelocity;
    float rotSpeed;
    int scoreSeed = 100;

    // デブリの破壊開始処理をまとめたもの
    public void StartDestroy() {
        // キャンバスとUIを確保
        GameObject canvas = GameObject.Find("Canvas");
        UIController ui = canvas.GetComponent<UIController>();
        // ヒットした弾をカウントする
        ui.CountUpHitBullet();
        // 点数をポップする
        int deltaScore = ui.AddScore(scoreSeed);
        Vector2 firstPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        GameObject localScore = Instantiate(localScorePrefab, firstPosition, Quaternion.identity);
        localScore.transform.SetParent(canvas.transform, false);
        localScore.GetComponent<Text>().text = deltaScore.ToString("D");
        // 自身を破壊する
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start() {
        // 落ちる速度をばらけさせる
        float fallSpeed = 0.6f + 6.0f * Random.value;
        fallVelocity = Vector3.down * fallSpeed;
        // 回転をばらけさせる
        rotSpeed = 300f + 180f * Random.value;
        if (Random.value < 0.5f) {
            rotSpeed = -rotSpeed;
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 deltaFallVelocity = fallVelocity * Time.deltaTime;
        transform.Translate(deltaFallVelocity, Space.World);
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        // デブリの退場判定
        if (transform.position.y < -5.5f) {
            Destroy(gameObject);
        }
    }
}
