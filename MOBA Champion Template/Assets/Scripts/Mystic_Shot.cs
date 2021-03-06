﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystic_Shot : MonoBehaviour {
	public float speed = 100000000f;
	public float base_damage = 200000;

	public bool red;

	public Vector3 dir;

	//public GameObject test_obj;

	public float distance;

	public float end_x = 0;
	public float end_z = 0;
	public Vector3 end_pos;

	public float dist_to_end;

	public void Create(float tar_x, float tar_z, Quaternion rot, bool r)	{
		//*****************NEED TO CHANGE ROTATION TO BE CONSISTENT WITH DIRECTION OF MOVEMENT

		float dir_x = tar_x - transform.position.x;
		float dir_z = tar_z - transform.position.z;

		dir = new Vector3 (dir_x, 0, dir_z);

		dir.Normalize ();

		end_pos = this.transform.position + (dir * distance);

		float theta = Mathf.Atan (dir.z/dir.x) * Mathf.Rad2Deg * -1;

		this.transform.rotation = Quaternion.Euler(0, theta, 0);

		red = r;
		//test_obj = (GameObject)Resources.Load ("Test_Cube");
		//Instantiate (test_obj, end_pos, Quaternion.identity);
	}

	void OnTriggerEnter(Collider col)	{
		if (red == false) {
			if (col.gameObject.tag == "Red_Minion" || col.gameObject.tag == "Red_Champ") {
				col.gameObject.GetComponent<Stats> ().TakeDamage (base_damage);
				Destroy (this.gameObject);
			}
		} 
		else {
			if (col.gameObject.tag == "Blue_Minion" || col.gameObject.tag == "Blue_Champ") {
				col.gameObject.GetComponent<Stats> ().TakeDamage (base_damage);
				Destroy (this.gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dist_to_end = Vector3.Distance (this.transform.position, end_pos);

		if (dist_to_end < 0.05f) {
			Destroy (this.gameObject);
		}

		transform.position = Vector3.MoveTowards (transform.position, end_pos, speed *  0.5f);

	}
}
