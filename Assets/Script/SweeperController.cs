using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// プレイヤーが操作するアバター
// イメージはデブリ排除の作業宇宙船
// キー入力の場合は直接操作できるが
// タップの場合は弾を出しつつそこまで移動する。
//
public class SweeperController : MonoBehaviour {

    public GameObject bulletPrefab;
    float leftLimit = -3;
    float rightLimit = 3;
    float targetX;

    // Use this for initialization
    void Start() {
        targetX = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        float speed = 6.0f * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            targetX = Mathf.Max(transform.position.x - speed, leftLimit);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            targetX = Mathf.Min(transform.position.x + speed, rightLimit);
        }

        bool reqShoot = Input.GetKeyDown(KeyCode.Space);

        if (Input.GetMouseButtonDown(0)) {
            reqShoot = true;
            targetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }

        // 現在の位置がターゲットより右なら左に補正
        if (targetX < transform.position.x) {
            float deltaX = -speed;
            float sampleX = transform.position.x + deltaX;
            if (sampleX < targetX) {
                deltaX = targetX - transform.position.x;
            }
            transform.Translate(deltaX, 0, 0);
        }

        // 現在の位置がターゲットより左なら右に補正
        if (targetX > transform.position.x) {
            float deltaX = speed;
            float sampleX = transform.position.x + deltaX;
            if (sampleX > targetX) {
                deltaX = targetX - transform.position.x;
            }
            transform.Translate(deltaX, 0, 0);
        }

        if (reqShoot) {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
