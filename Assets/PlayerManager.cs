using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float SolarMeter = 100;
    public float SoilMeter = 100;
    public float WaterMeter = 100;
    public GameObject FailScreen;
    public GameObject WinScreen;
    public static PlayerManager instance;
    public float[] personalityRankings = { 0, 0, 0, 0, 0 }; //{boring,cute,intelligent,cool,sporty}
    public int growthState = 0;
    public bool isFemme = false;
    Text deathText;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        FailScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(SolarMeter <= 0 || WaterMeter <= 0 || SoilMeter <= 0)
        {
            FailScreen.SetActive(true);
            DialogueManager.instance.wait = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SolarMeter = 100;
                SoilMeter = 100;
                WaterMeter = 100;
                for(int i = 0;i < personalityRankings.Length; i++)
                {
                    personalityRankings[i] = 0;
                }
                DialogueManager.instance.turn = 1;
                DialogueManager.instance.wait = false;
                FailScreen.SetActive(false);
            }               
        }
        if(growthState > 2)
        {
            int maxIndex = 0;
            float max = -999;
            for (int i = 0; i < personalityRankings.Length; i++)
            {
                if (personalityRankings[i] > max) { max = personalityRankings[i]; maxIndex = i; }
            }
            //Switch Sprite
            if (isFemme)
            {
                switch (maxIndex)
                {
                    case 0:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your woman kinda sucks to be honest. She is pretty boring and vanilla. Your sure about this?";
                        Debug.Log("Your woman kinda sucks to be honest. She is pretty boring and vanilla. Your sure about this?");
                        break;
                    case 1:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your woman is adorable! The ultimate culmiation of cuteness. Great job!";
                       Debug.Log("Your woman is adorable! The ultimate culmiation of cuteness. Great job!");
                        break;
                    case 2:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your woman is wicked smart! A perfect conversationalist. Great job!";
                        Debug.Log("Your woman is wicked smart! A perfect conversationalist. Great job!");
                        break;
                    case 3:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your woman is super cool! Nothing bothers him and he is rocking the look. Great job!";
                        Debug.Log("Your woman is super cool! Nothing bothers him and he is rocking the look. Great job!");
                        break;
                    case 4:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your woman is very buff! The ultimate sporty super athele. Great job!";
                        Debug.Log("Your woman is very buff! The ultimate sporty super athele. Great job!");
                        break;
                }
            }else{
                switch (maxIndex)
                {
                    case 0:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your man kinda sucks to be honest. He is pretty boring and vanilla. Your sure about this?";
                        Debug.Log("Your man kinda sucks to be honest. He is pretty boring and vanilla. Your sure about this?");
                        break;
                    case 1:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your man is adorable! The ultimate culmiation of cuteness. Great job!";
                       Debug.Log("Your man is adorable! The ultimate culmiation of cuteness. Great job!");
                        break;
                    case 2:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your man is wicked smart! A perfect conversationalist. Great job!";
                       Debug.Log("Your man is wicked smart! A perfect conversationalist. Great job!");
                        break;
                    case 3:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your man is super cool! Nothing bothers him and he is rocking the look. Great job!";
                       Debug.Log("Your man is super cool! Nothing bothers him and he is rocking the look. Great job!");
                        break;
                    case 4:
                        WinScreen.transform.GetChild(0).GetComponent<Text>().text = "Your man is very buff! The ultimate himbo jock. Great job!";
                       Debug.Log("Your man is very buff! The ultimate himbo jock. Great job!");
                        break;

                }
            }
            WinScreen.SetActive(true);
            DialogueManager.instance.wait = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SolarMeter = 100;
                SoilMeter = 100;
                WaterMeter = 100;
                for (int i = 0; i < personalityRankings.Length; i++)
                {
                    personalityRankings[i] = 0;
                }
                DialogueManager.instance.turn = 1;
                DialogueManager.instance.wait = false;
                FailScreen.SetActive(false);
            }
        }
    }
}
