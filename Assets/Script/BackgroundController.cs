using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -0.03f*60*Time.deltaTime, 0);
        if (transform.position.y < -9.8f) {
            float y = transform.position.y;
            transform.position = new Vector3(0, y + 19.6f, 0);
        }
	}
}
