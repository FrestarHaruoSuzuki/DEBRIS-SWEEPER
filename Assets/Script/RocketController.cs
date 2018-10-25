using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public GameObject bulletPrefab;
    float leftLimit = -3;
    float rightLimit = 3;
    float targetX;
    bool moveToLeft;
    bool moveToRight;

    // Use this for initialization
    void Start() {
        targetX = transform.position.x;
        moveToLeft = false;
        moveToRight = false;
    }

    // Update is called once per frame
    void Update() {
        float speed = 0.1f * 60 * Time.deltaTime;

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

        if (targetX < transform.position.x) {
            float deltaX = -speed;
            float sampleX = transform.position.x + deltaX;
            if (sampleX < targetX) {
                deltaX = targetX - transform.position.x;
            }
            transform.Translate(deltaX, 0, 0);
        }

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
