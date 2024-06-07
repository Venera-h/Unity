using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    AudioSource audioSource;
    [SerializeField] AudioClip DmgSound;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = FindObjectOfType<AudioSource>();
        StartCoroutine(DestryBulletOnTime());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.clip = DmgSound;
        audioSource.Play();
        GetComponent<DamageComponent>().DealDamage(collision.gameObject);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (speed*transform.localScale.x,0);

    }
    IEnumerator DestryBulletOnTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
