using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
	public GameObject easy;
	public GameObject normal;
	public GameObject hard;
	// Start is called before the first frame update
	void Start() 
	{
		
	}

	// Update is called once per frame
	void Update()
	{
	
	}
	//난이도 설정
	public void EasyStart()
	{
		PlayerPrefs.SetInt("level", 1);
		normal.SetActive(false);
		hard.SetActive(false);
		easy.SetActive(true);
	}

	public void NormalStart()
	{
		PlayerPrefs.SetInt("level", 2);
		easy.SetActive(false);
		hard.SetActive(false);
		normal.SetActive(true);
	}

	public void HardStart()
	{ 
		PlayerPrefs.SetInt("level", 3);
		easy.SetActive(false);
		normal.SetActive(false);
		hard.SetActive(true);
	}
	
}
