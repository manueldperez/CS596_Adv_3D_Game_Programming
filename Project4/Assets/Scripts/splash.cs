﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(SleepTimer());
	}
	
    IEnumerator SleepTimer()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(1);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
