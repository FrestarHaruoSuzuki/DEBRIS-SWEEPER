using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// RockとあるがDebris
// 0.2秒ごとにランダムな種類のデブリを発生させる
//
public class DebrisGenerator : MonoBehaviour {

    public GameObject debrisPrefab;
    Sprite[] sprites;

    // Use this for initialization
    void Start() {
        sprites = Resources.LoadAll<Sprite>("Images/debris");
        foreach(Sprite s in sprites) {
            Debug.Log("Sprite name : " + s.name);
        }
        InvokeRepeating("GenDebris", 0.1f, 0.2f);
    }

    // プレファブを元にデブリを作成。
    // タイマーから呼ばれる
    void GenDebris() {
        Vector3 startPosition = new Vector3(-2.5f + 5 * Random.value, 6, 0);
        GameObject debris = Instantiate(debrisPrefab, startPosition, Quaternion.identity);
        debris.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
