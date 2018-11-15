﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// RockとあるがDebris
// 0.2秒ごとにランダムな種類のデブリを発生させる
//
public class RockGenerator : MonoBehaviour {

    public GameObject rockPrefab;
    Sprite[] rocks;

    // Use this for initialization
    void Start() {
        rocks = Resources.LoadAll<Sprite>("Images/debris");
        foreach(Sprite s in rocks) {
            Debug.Log("Rock name : " + s.name);
        }
        InvokeRepeating("GenRock", 0.1f, 0.2f);
    }

    // プレファブを元にデブリを作成。
    // タイマーから呼ばれる
    void GenRock() {
        Vector3 startPosition = new Vector3(-2.5f + 5 * Random.value, 6, 0);
        GameObject rock = Instantiate(rockPrefab, startPosition, Quaternion.identity);
        rock.GetComponent<SpriteRenderer>().sprite = rocks[Random.Range(0, rocks.Length)];
    }
}
