using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public GameObject bulletPrefab;
    float leftLimit = -3;
    float rightLimit = 3;
    float threshold = 0.2f;

    // Update is called once per frame
    void Update() {
        bool toLeft = Input.GetKey(KeyCode.LeftArrow) || (Input.acceleration.x < -threshold);
        bool toRight = Input.GetKey(KeyCode.RightArrow) || (Input.acceleration.x > threshold);
        bool reqShoot = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        float speed = 0.1f * 60 * Time.deltaTime;

        if (toLeft) {
            float deltaX = -speed;
            float sampleX = transform.position.x + deltaX;
            if (sampleX < leftLimit) {
                deltaX = leftLimit - transform.position.x;
            }
            transform.Translate(deltaX, 0, 0);
        }

        if (toRight) {
            float deltaX = speed;
            float sampleX = transform.position.x + deltaX;
            if (sampleX > rightLimit) {
                deltaX = rightLimit - transform.position.x;
            }
            transform.Translate(deltaX, 0, 0);
        }

        if (reqShoot) {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
