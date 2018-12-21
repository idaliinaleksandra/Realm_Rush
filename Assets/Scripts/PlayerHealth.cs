using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int health = 10;
    [SerializeField] int damage = 1;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip ouchSFX;

    void Start()
    {
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        health = health - damage;
        healthText.text = health.ToString();
        GetComponent<AudioSource>().PlayOneShot(ouchSFX);
    }
}
