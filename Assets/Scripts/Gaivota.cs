using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaivota : MonoBehaviour {
    private float _timer;
    private GameManager _gm;
    private AudioSource _gaivotaSound;

    // Use this for initialization
    void Start () {
        _timer = 10;
        _gaivotaSound = gameObject.GetComponent<AudioSource>();
        GameObject obj = GameObject.FindGameObjectWithTag("GM");
        if (obj)
        {
            _gm = obj.GetComponent<GameManager>();
        }
        //gaivotaSound.Play();
    }

    void PlayGaivotaSound()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _gaivotaSound.Play();
            _timer = 10;
        }
    }
	
	// Update is called once per frame
	void Update () {
        _gaivotaSound.volume = _gm.GetVolume();
        if (!_gaivotaSound.isPlaying)
        {
            PlayGaivotaSound();
        }

    }
}
