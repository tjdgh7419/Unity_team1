using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
	public GameObject easy;
	public GameObject normal;
	bool chk = false;
	// Start is called before the first frame update
	void Start()
	{
		easy.gameObject.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
	
	}
	//난이도 설정
	public void EasyStart()
	{
		PlayerPrefs.SetInt("level", 1);
	}

	public void NormalStart()
	{
		PlayerPrefs.SetInt("level", 2);
	}

	public void HardStart()
	{ 
		PlayerPrefs.SetInt("level", 3);
	}
	
}
