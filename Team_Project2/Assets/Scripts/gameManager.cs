using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using Unity.Collections.LowLevel.Unsafe;

public class gameManager : MonoBehaviour
{
	public float maxTime = 60f;
	public float currentTime;

    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioSource audioSource;
	public GameObject endTxt;
	public GameObject firstCard;
	public GameObject secondCard;
    public GameObject card;
    public Text timeTxt;
	public static gameManager I;
	public bool isMatching;
	public int level = 1;


    void Awake()
	{
		//난이도의 정보값을 받아서 레벨의 값 변화
		if (PlayerPrefs.HasKey("level"))
		{
			level = PlayerPrefs.GetInt("level");
		}
		I = this;
	}
	// Start is called before the first frame update
	void Start()
	{
        currentTime = maxTime;
        isMatching = false;
        UpdateTimeText();

        Time.timeScale = 1.0f;
		// level값에 따른 난이도 배열 설정
		if (level == 1)
		{
			EasyStage();
		}
		else if (level == 2)
		{
			NormalStage();
		}
		else
		{
			HardStage();
		}
		
    }

	// Update is called once per frame
	void Update()
	{
		if (!isMatching)
		{
			// 시간이 0보다 크면 제한 시간을 감소시킴
			if (currentTime > 0f)
			{
				currentTime -= Time.deltaTime;
				UpdateTimeText();
			}
			else
			{
				// 시간이 다 되면 게임 오버 처리
				GameOver();
			}
		}
	}
    private void UpdateTimeText()
    {
        timeTxt.text = Mathf.Ceil(currentTime).ToString();
    }


    public void isMatched()
	{
		string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
		string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

		if (firstCardImage == secondCardImage) 
		{
            audioSource.PlayOneShot(correctSound); //맞췄을때 사운드 추가
            firstCard.GetComponent<card>().destroyCard();
			secondCard.GetComponent<card>().destroyCard();

			int cardsLeft = GameObject.Find("cards").transform.childCount;

			if(cardsLeft == 2)
			{
				Time.timeScale = 0f;
				endTxt.SetActive(true);
			}
		}

		else
		{
            audioSource.PlayOneShot(incorrectSound); //틀렸을때 사운드 추가
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = Color.gray;
			secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = Color.gray;
            FailMatch();
  
            firstCard.GetComponent<card>().closeCard();
			secondCard.GetComponent<card>().closeCard();
		}

		firstCard = null;
		secondCard = null;
	}

    public void FailMatch() //맞추기 실패했을때의 메소드
    {
        isMatching = true;
        currentTime -= 3f;
        if (currentTime < 0f)
        {
            currentTime = 0f;
		}
		UpdateTimeText();
		isMatching = false;
    }



	public void GameOver()
    {
		endTxt.gameObject.SetActive(true);

    }

	public void EasyStage()
	{
		int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };

		rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

		for (int i = 0; i < 12; i++)
		{
			GameObject newCard = Instantiate(card);
			newCard.transform.parent = GameObject.Find("cards").transform;

			float x = (i % 4) * 1.4f - 2.1f;
			float y = (i / 4) * 1.4f - 2.0f;
			newCard.transform.position = new Vector3(x, y, 0);

			string rtanName = "rtan" + rtans[i].ToString();
			newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
		}
	}

	public void NormalStage()
	{
		int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };

		rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

		for (int i = 0; i < 16; i++)
		{
			GameObject newCard = Instantiate(card);
			newCard.transform.parent = GameObject.Find("cards").transform;

			float x = (i / 4) * 1.4f - 2.1f;
			float y = (i % 4) * 1.4f - 4.0f;
			newCard.transform.position = new Vector3(x, y, 0);

			string rtanName = "rtan" + rtans[i].ToString();
			newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
		}
	}

	public void HardStage()
	{
		int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 0, 0, 1, 1 };

		rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

		for (int i = 0; i < 20; i++)
		{
			GameObject newCard = Instantiate(card);
			newCard.transform.parent = GameObject.Find("cards").transform;
			newCard.transform.Find("back").transform.localScale = new Vector3(0.8f, 0.8f, 1f);
			newCard.transform.Find("front").transform.localScale = new Vector3(0.8f, 0.8f, 1f);
			float x = (i / 4) * 1.1f - 2.2f;
			float y = (i % 4) * 1.1f - 2.8f;
			newCard.transform.position = new Vector3(x, y, 0);

			string rtanName = "rtan" + rtans[i].ToString();
			newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
		}
	}
}
