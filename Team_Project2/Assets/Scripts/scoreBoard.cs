using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreBoard : MonoBehaviour
{
    public Text score;
    public Text tryed;
    // Start is called before the first frame update
    void Start()
    {
        score.text = PlayerPrefs.GetFloat("curtime").ToString();
        tryed.text = PlayerPrefs.GetFloat("click").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
