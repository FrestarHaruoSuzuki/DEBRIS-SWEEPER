using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    // BGの移動速度は一定なので事前に計算しておく。単位はm/s
    // BGの移動速度＝方向＊スカラ量
    Vector3 velocity = Vector3.down  * 1.8f;

    float imageHeight = 9.8f; // BGに使われているイメージの縦サイズ

    // Update is called once per frame
    void Update () {

        // 瞬間的な移動量は時間から算出する。
        Vector3 deltaVelocity = velocity * Time.deltaTime;
        transform.Translate(deltaVelocity, Space.World);

        // イメージの巻き戻しチェック。
        if (transform.position.y < -imageHeight) {
            // 画面外に出たら巻き戻し。
            float y = transform.position.y + (imageHeight * 2); // リセット位置の算出。誤差込み。
            transform.position = new Vector3(0, y, 0);          // リセット位置の設定。
        }
	}
}
