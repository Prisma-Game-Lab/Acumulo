using UnityEngine;
using System.Collections;

public class BarcoFrente : MonoBehaviour {

    private GameManager _gm;
	public float hit_time;
	private float timer;
    private AudioSource _lixoSugado;

	private ParticleSystem _lixoParticles;

    void Awake ()
    {
		timer = 0;
		GameObject gm = GameObject.FindGameObjectWithTag("GM");
        if (gm)
        {
            _gm = gm.GetComponent<GameManager>();
        }
		_lixoParticles = GetComponent<ParticleSystem>();

        _lixoSugado = GetComponent<AudioSource>();
    }
	
    void OnCollisionStay2D(Collision2D other)
    {
		if (other.gameObject.tag == "Player" && timer>= hit_time && !_gm.PlayerEvolving)
        {
			_lixoParticles.Stop();
			_lixoParticles.Play();
			timer = 0;
            other.gameObject.GetComponent<Player>().Shrink();
            _gm.ReduceScore(10);
            _lixoSugado.Play();
        }
    }

    private void Update()
    {
        _lixoSugado.volume = _gm.GetVolume();
		timer += Time.deltaTime;
		transform.position = transform.parent.transform.position;
    }
}
