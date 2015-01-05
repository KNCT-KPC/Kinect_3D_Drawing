using UnityEngine;
using System.Collections;

public class Righthand : MonoBehaviour {

	GameObject righthand;
	GameObject lefthand;
	GameObject torso;
	GameObject current;
	public GameObject cursor;
	public GameObject drawer;
	public GameObject Drawers;
	public Material pen;
	bool drawflag;
	TrailRenderer trail;
	float n = 1;
	float width = 3;

	void AddC (GameObject current, float n) {
		current.AddComponent<TrailRenderer>();
		trail = current.GetComponent<TrailRenderer> ();
		trail.material = pen;
		trail.time = 1000;
		trail.startWidth = width / 100;
		trail.endWidth = width / 100;
		if (n == 1) {
			trail.material.color = Color.white;
		} else if (n == 2) {
			trail.material.color = Color.black;
		} else if (n == 3) {
			trail.material.color = Color.red;
		} else if (n == 4) {
			trail.material.color = Color.blue;
		} else if (n == 5) {
			trail.material.color = Color.green;
		}
	}

	void Start () {
		//righthandのオブジェクト(Sphere)を変数に格納
		righthand = GameObject.Find("righthand");
		lefthand = GameObject.Find ("lefthand");
		torso = GameObject.Find ("torso");
		Drawers = GameObject.Find("Drawers");
		current = (GameObject) Instantiate (drawer, righthand.transform.position, Quaternion.identity);
		current.transform.parent = Drawers.transform;
		RenderSettings.ambientLight = new Color(1, 1, 1);
		drawflag = false;
	}

	void Update () {
		current.transform.position = righthand.transform.position;
		if (lefthand.transform.position.y > torso.transform.position.y) {
			drawflag = true;
			AddC (current, n);
		} else if (drawflag && lefthand.transform.position.y <= torso.transform.position.y) {
				drawflag = false;
				current = (GameObject) Instantiate (drawer, righthand.transform.position, Quaternion.identity);
				current.transform.parent = Drawers.transform;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			//trail.enabled = true;
			if (n < 5) {
				n += 1;
				cursor.transform.position -= new Vector3 (0, n / (n * 20), 0);
			} else {
				n = 1;
				cursor.transform.position = new Vector3 (0, 1, 0);
			}
			//current = (GameObject) Instantiate (drawer, righthand.transform.position, Quaternion.identity);
			//count++;
			//AddC (current, n);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			Application.LoadLevel("Te st");
			//Reset();
		}
	}
	
	void Reset() {
		int i;
		for (i = 0; i < Drawers.transform.childCount; i++) {
			Destroy(Drawers.transform.GetChild(i).gameObject);
		}
	}
}
