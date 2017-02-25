using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayerPrefab : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public ParticleSystem Particle;

    public void Evol1_2()
    {
        player1.SetActive(false);
        player2.SetActive(true);
        Particle.Play();
    }

    public void Evol2_3()
    {
        player2.SetActive(false);
        player3.SetActive(true);
        Particle.Play();
    }
}
