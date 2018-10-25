using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject explosionPrefab;
    bool reqDestroy = false;
    float rotSpeed;
    UIController ui;

    void Start() {
        ui = GameObject.Find("Canvas").GetComponent<UIController>();
        rotSpeed = 222;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(0, 0.2f * 60 * Time.deltaTime, 0, Space.World);
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    
        bool isLostBullet = (transform.position.y > 5);
        if (isLostBullet) {
            ui.CountUpLostBullet();
        }
        if (reqDestroy || isLostBullet) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);

        collision.gameObject.GetComponent<RockController>().StartDestroy();

        reqDestroy = true;
    }
}
