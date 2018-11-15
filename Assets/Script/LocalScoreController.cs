﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// デブリ破壊時にローカルスコアを表示する
// 破壊された位置からポップしフェードアウトする
//
public class LocalScoreController : MonoBehaviour {

    float elapsedTime;
    Vector3 velocity;

    // Use this for initialization
    void Start() {
        velocity = Vector3.up * 10.0f;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(velocity);
        if (velocity.y < 1.0f) {
            Color c = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(c.r, c.g, c.b, velocity.y);
        }
        velocity *= 0.9f;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1.0f) {
            Destroy(gameObject);
        }
    }
}
