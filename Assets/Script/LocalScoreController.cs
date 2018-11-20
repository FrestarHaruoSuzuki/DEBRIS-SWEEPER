using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// デブリ破壊時にローカルスコアを表示する
// 破壊された位置からポップしフェードアウトする
// 本来であれば時間軸に比例するeaseを使うのが妥当。
//
public class LocalScoreController : MonoBehaviour {

    float elapsedTime;
    Vector3 velocity;
    float limitTime = 1.0f;
    float alphaLimit = 0.01f;

    // Use this for initialization
    void Start() {
        velocity = Vector3.up * 0.1f;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(velocity);
        if (velocity.y < alphaLimit) {
            float alpha = velocity.y / alphaLimit;
            Color c = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(c.r, c.g, c.b, alpha);
        }
        velocity *= 0.9f;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > limitTime) {
            Destroy(gameObject);
        }
    }
}
