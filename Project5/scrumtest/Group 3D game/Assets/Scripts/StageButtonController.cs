using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButtonController : MonoBehaviour
{	
	public bool on;
	public GameObject arrow;

	void Start()
	{
		on = false;
	}

	//player is on the platform
	void OnCollisionEnter(Collision col)
	{
		if(!on)
		{
			on = true;
			GetComponent<Renderer>().material.SetColor("_Color", Color.green);
			GetComponent<AudioSource>().Play();
            Behaviour h = (Behaviour)GetComponent("Halo");
            h.enabled = false;
			arrow.SetActive(false);
        }
	}
}
