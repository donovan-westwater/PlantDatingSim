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
    public GameObject tutorial;
    public static PlayerManager instance;
    public float[] personalityRankings = { 0, 0, 0, 0, 0 }; //{boring,cute,intelligent,cool,sporty}
    public Sprite[] growthSprites;
    public Sprite[] spritesMasc;
    public Sprite[] spritesFemme;
    public Image plantSprite;
    public Image finalSprite;
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
    public void setMale()
    {
        isFemme = false;
        tutorial.SetActive(false);
        DialogueManager.instance.start = false;
        DialogueManager.instance.wait = false;
    }
    public void setFemale()
    {
        isFemme = true;
        tutorial.SetActive(false);
        DialogueManager.instance.start = false;
        DialogueManager.instance.wait = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.start)
        {
            tutorial.SetActive(true);
        }
        if(SolarMeter <= 0 || WaterMeter <= 0 || SoilMeter <= 0)
        {
            FailScreen.SetActive(true);
            DialogueManager.instance.wait = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SolarMeter = 100;
                SoilMeter = 100;
                WaterMeter = 100;
                growthState = 0;
                for(int i = 0;i < personalityRankings.Length; i++)
                {
                    personalityRankings[i] = 0;
                }
                DialogueManager.instance.turn = 0;
                DialogueManager.instance.wait = false;
                FailScreen.SetActive(false);
                plantSprite.sprite = growthSprites[0];
            }               
        }
        if(growthState > 3)
        {
            int maxIndex = 0;
            float max = -999;
            for (int i = 0; i < personalityRankings.Length; i++)
            {
                if (personalityRankings[i] > max) { max = personalityRankings[i]; maxIndex = i; }
            }
            plantSprite.transform.gameObject.SetActive(false);
            //Switch Sprite
            if (isFemme)
            {
                
                finalSprite.sprite = spritesFemme[maxIndex];
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
                finalSprite.sprite = spritesMasc[maxIndex];
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
                growthState = 0;
                
                for (int i = 0; i < personalityRankings.Length; i++)
                {
                    personalityRankings[i] = 0;
                }
                DialogueManager.instance.turn = 0;
                DialogueManager.instance.wait = false;
                WinScreen.SetActive(false);
                plantSprite.sprite = growthSprites[0];
                plantSprite.transform.gameObject.SetActive(true);
            }
        }
        else if( growthState > 0)
        {
            plantSprite.sprite = growthSprites[growthState-1];
        }
    }
}
