using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Noticia : MonoBehaviour {
	//as noticias teriam ordem nesse esquema, ao inves de bolsoes aleatorios entre fases
	//da p fazer o esquema de bolsoes apenas com indexes mas fica confuso, faria com varios vetores
	public Sprite[] News;
	public Image Place;
	public Sprite swap;
    public TextAsset[] Texts1;
    public TextAsset[] Texts2;
    public TextAsset[] Texts3;
    public Text NewsText;
    public float minperiod,maxperiod,viewtime;
    public GameObject Spawner;

    private int Newscounter;
    private int textCounter;

	void Start ()
    {
		Newscounter = 0;

        if (!PlayerPrefs.HasKey("counter"))
        {
            PlayerPrefs.SetInt("counter", 0);
        }

        textCounter = 0;
        TriggerNews();
		//para as noticias especificas: GameManager.register(index,score)
		//la no GM, a register(int,int) poe num vetor e no update checa esse vetor. se bateu o score, chama ImportantNews com o index associado 
		//problemas: uma noticia importante pode pausar o jogo porem nao pausar o processo de noticias aleatorias
	}

    public void TriggerTextNews()
    {
        if (textCounter < 5)
        {
            NewsText.text = Texts1[textCounter].text;
        }
        else if (textCounter < 10)
        {
            NewsText.text = Texts2[textCounter-5].text;
            if(textCounter - 5 == 2)
            {
                //barreira
                Spawner.transform.FindChild("spawner barreira").gameObject.SetActive(true);
            }
            else if(textCounter - 5 == 4)
            {
                //bacteria
                Spawner.transform.FindChild("spawner bacteria").gameObject.SetActive(true);
            }
        }
        else if (textCounter < 15)
        {
            NewsText.text = Texts3[textCounter - 10].text;
            if (textCounter - 10 == 1)
            {
                //barco
                Spawner.transform.FindChild("spawner barco de lixo").gameObject.SetActive(true);
            }
            else if (textCounter - 10 == 3)
            {
                //biodegradavel
                Spawner.transform.FindChild("spawner lixobio").gameObject.SetActive(true);
            }
        }
        textCounter++;
    }

    void RandomNews()
    {
        swap = Place.sprite;
        Place.sprite = News[Newscounter];
        Newscounter++;

        if (Newscounter > PlayerPrefs.GetInt("counter"))
        {
            PlayerPrefs.SetInt("counter", Newscounter);
        }

        if (Newscounter >= News.Length)
        {
            Newscounter = 0;
        }
        Invoke("TriggerClose", viewtime);
    }

    void TriggerClose()
    {
        if (swap)
        {
            Place.sprite = swap;
        }
        if (Newscounter <= News.Length)
        {
            TriggerNews();
        }
    }

    void TriggerNews()
    {
        //parar de usar Invoke e comecar a usar corotinas
        Invoke("RandomNews", Random.Range(minperiod, maxperiod));
    }
}
