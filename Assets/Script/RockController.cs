using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockController : MonoBehaviour {

    public GameObject localScorePrefab;
    float fallSpeed;
    float rotSpeed;
    int scoreSeed = 100;

    public void StartDestroy() {
        GameObject canvas = GameObject.Find("Canvas");
        UIController ui = canvas.GetComponent<UIController>();
        ui.CountUpHitBullet();
        int deltaScore = ui.AddScore(scoreSeed);
        Vector2 firstPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        GameObject localScore = Instantiate(localScorePrefab, firstPosition, Quaternion.identity);
        localScore.transform.SetParent(canvas.transform);
        localScore.GetComponent<Text>().text = deltaScore.ToString("D");
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start() {
        fallSpeed = (0.01f + 0.1f * Random.value) * 60;
        rotSpeed = (5f + 3f * Random.value) * 60;
        if (Random.value < 0.5f) {
            rotSpeed = -rotSpeed;
        }
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(0, -fallSpeed * Time.deltaTime, 0, Space.World);
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        if (transform.position.y < -5.5f) {
            Destroy(gameObject);
        }
    }
}
