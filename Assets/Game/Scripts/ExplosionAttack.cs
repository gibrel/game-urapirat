using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExplosionAttack : MonoBehaviour
{
    [SerializeField] private float distanceToExplode = 0.5f;
    [SerializeField] private int damage = 35;
    [SerializeField] private GameObject explosionEffect;

    private Transform playerTransform;
    private HealthController playerHealthController;
    private HealthController selfHealthController;
    private bool exploded = false;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform= player.transform;
        playerHealthController = player.GetComponent<HealthController>();
        selfHealthController = transform.parent.GetComponent<HealthController>();
    }

    private void Update()
    {
        float distanceFromPlayer = Mathf.Abs((playerTransform.position - transform.position).magnitude);
        if(distanceFromPlayer <= distanceToExplode && !exploded) { 
            Explode();
        }
    }

    private void Explode()
    {
        playerHealthController.TakeDamage(damage);
        selfHealthController.TakeDamage(selfHealthController.MaxHealth);
        exploded = true;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        PlaySound();
    }

    private void PlaySound()
    {
        SoundManager.PlaySound(SoundManager.Sound.ShipExplosion, transform.position);
    }
}
