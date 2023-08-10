using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip bgmusic;
	public AudioClip endmusic;
	bool timerChk = false;
	// Start is called before the first frame update
	void Start()
    {
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
		// 시간이 20초 이상 지나면 bool timechk를 이용한 음악 변경
		if (gameManager.I.currentTime <= 20f && !timerChk)
		{
			timerChk = true;
			audioSource.clip = endmusic;
			audioSource.Play();
		}
	}
}
