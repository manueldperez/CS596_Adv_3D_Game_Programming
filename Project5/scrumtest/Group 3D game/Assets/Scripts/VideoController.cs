﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{

	public RawImage rawImage;
	public VideoPlayer videoPlayer;
	public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

	IEnumerator PlayVideo()
	{
		videoPlayer.Prepare();
		WaitForSeconds wfs = new WaitForSeconds(1);
		while(!videoPlayer.isPrepared)
		{
			yield return wfs;
			break;
		}
		rawImage.texture = videoPlayer.texture;
		videoPlayer.Play();
		audioSource.Play();
	}
}
