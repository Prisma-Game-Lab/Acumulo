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
	public float minperiod,maxperiod,viewtime;
	// Use this for initialization
	private int Newscounter;

	void RandomNews()
    {
		swap = Place.sprite;
		Place.sprite = News[Newscounter];
		Newscounter++;
        if(Newscounter >= News.Length)
        {
            Newscounter = 0;
        }
		Invoke("TriggerClose",viewtime);		
	}

	void TriggerClose()
    {
		if(swap)
        {
			Place.sprite = swap;
		}
		if(Newscounter<=News.Length)
        {
			TriggerNews();
		}
	}

	void TriggerNews()
    {
		//parar de usar Invoke e comecar a usar corotinas
		Invoke("RandomNews",Random.Range(minperiod,maxperiod));
	}

	void ImportantNews()
    {

	}

	void Start ()
    {
		Newscounter = 0;
		TriggerNews();
		//para as noticias especificas: GameManager.register(index,score)
		//la no GM, a register(int,int) poe num vetor e no update checa esse vetor. se bateu o score, chama ImportantNews com o index associado 
		//problemas: uma noticia importante pode pausar o jogo porem nao pausar o processo de noticias aleatorias
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
