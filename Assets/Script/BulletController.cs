using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 弾のイメージはプラズマのようなイメージだが、
// アニメーションを組めるほど時間がないので、
// 過度の回転を与える事で放電している様に見せる。
//
public class BulletController : MonoBehaviour {

    public GameObject explosionPrefab;
    bool reqDestroy = false;
    float rotSpeed;
    UIController ui;

    // 弾の移動速度は一定なので事前に計算しておく。単位はm/s
    // 弾の移動速度＝方向＊スカラ量
    Vector3 upVelocity = Vector3.up * 12.0f;

    // Use this for initialization
    void Start() {
        ui = GameObject.Find("Canvas").GetComponent<UIController>();    // キャンバスは静的に設定しているので事前に取得しておく。
        rotSpeed = 222; // 弾イメージのオイラー回転角。値はなんとなく決めている。
    }

    // Update is called once per frame
    void Update() {
        // 次の位置を計算
        Vector3 deltaUpVelocity = upVelocity * Time.deltaTime;
        transform.Translate(deltaUpVelocity, Space.World);

        // 回転で適当に放電している様に見せる。
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    
        // 弾の終了判定
        bool isLostBullet = (transform.position.y > 5);
        if (isLostBullet) {
            ui.CountUpLostBullet();
        }
        if (reqDestroy || isLostBullet) {
            Destroy(gameObject);
        }
    }

    // 弾とデブリがヒットした時に呼ばれる。
    // 場合によっては一つの弾で複数のデブリに接触し
    // 複数回呼ばれることもある。
    void OnTriggerEnter2D(Collider2D collision) {
        GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);

        collision.gameObject.GetComponent<DebrisController>().StartDestroy();

        reqDestroy = true;
    }
}
