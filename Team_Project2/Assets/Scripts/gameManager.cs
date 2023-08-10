using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.SocialPlatforms.Impl;

public class gameManager : MonoBehaviour
{
	public float maxTime = 60f;
	public float currentTime;
	public GameObject nameTxt;
	public Text nameTxt_name;
	public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioSource audioSource;
	public GameObject endTxt;
	public GameObject firstCard;
	public GameObject secondCard;
    public GameObject card;
    public Text timeTxt;
	public Text clickTxt;
	float click=0;
    public static gameManager I;
	public bool isMatching;
	
	public float time;
	bool nameChk = false;
	float curTime = 0;

	public int level = 1;


	void Awake()
	{
		//���̵��� �������� �޾Ƽ� ������ �� ��ȭ
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
		// level���� ���� ���̵� �迭 ����
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

	void Update()
	{
		time -= Time.deltaTime;
		if (!isMatching)
		{

			// �ð��� 0���� ũ�� ���� �ð��� ���ҽ�Ŵ
			if (currentTime > 0f)
			{
				currentTime -= Time.deltaTime;
				UpdateTimeText();
			}
			else
			{
				// �ð��� �� �Ǹ� ���� ���� ó��
				GameOver();
			}
			// 20�� �̻� ������ �ð��� ������ ���ϴ� ���
			if (currentTime <= 20f)
			{
				timeTxt.color = Color.red;
			}
		}

		if (nameChk && (curTime - 1f > time))
		{
			nameTxt.SetActive(false);
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


		click += 1;
		clickTxt.text = click.ToString("");

		if (firstCardImage == secondCardImage) 
		{
            audioSource.PlayOneShot(correctSound); //�������� ���� �߰�
            firstCard.GetComponent<card>().destroyCard();
			secondCard.GetComponent<card>().destroyCard();

            //click += 1;
            //clickTxt.text = click.ToString("");

            int cardsLeft = GameObject.Find("cards").transform.childCount;

			if (firstCardImage[4] - '0' >= 0 && firstCardImage[4] - '0' < 3)
			{
				nameTxt_name.text = "Kang Sungho";
				nameChk = true;
				curTime = time;
				nameTxt.SetActive(true);
			}
			else if (firstCardImage[4] - '0' >= 3 && firstCardImage[4] - '0' < 6)
			{
				nameTxt_name.text = "Park Jungwoo";
				nameChk = true;
				curTime = time;
				nameTxt.SetActive(true);
			}
			else if (firstCardImage[4] - '0' >= 6 && firstCardImage[4] - '0' < 9)
			{
				nameTxt_name.text = "Park Jongsoo";
				nameChk = true;
				curTime = time;
				nameTxt.SetActive(true);
			}

			if (cardsLeft == 2)
			{
				Time.timeScale = 0f;
				endTxt.SetActive(true);

			}
		}

		else
		{
            audioSource.PlayOneShot(incorrectSound); //Ʋ������ ���� �߰�
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = Color.gray;
			secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = Color.gray;
            FailMatch();

			nameTxt_name.text = "Baka!";
			nameChk = true;
			curTime = time;
			nameTxt.SetActive(true);

			firstCard.GetComponent<card>().closeCard();
			secondCard.GetComponent<card>().closeCard();
		}

		firstCard = null;
		secondCard = null;
	}

    public void FailMatch() //���߱� ������������ �޼ҵ�
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

			string rtanName = "team" + rtans[i].ToString();
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

			string rtanName = "team" + rtans[i].ToString();
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
			newCard.transform.Find("front").transform.localScale = new Vector3(0.17f, 0.17f, 1f);
			float x = (i / 4) * 1.1f - 2.2f;
			float y = (i % 4) * 1.1f - 2.8f;
			newCard.transform.position = new Vector3(x, y, 0);

			string rtanName = "team" + rtans[i].ToString();
			newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
		}
	}
}
