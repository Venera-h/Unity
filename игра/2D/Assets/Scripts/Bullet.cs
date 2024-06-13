using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip DmgSound;

    
    private void OnEnable()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.clip = DmgSound;
            audioSource.Play();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestryBulletOnTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryGetComponent<DamageComponent>(out DamageComponent d) && collision.CompareTag("Enemy"))
        {
            d.DealDamage(collision.gameObject);
            Destroy(gameObject);

        }
    }
  
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * transform.localScale.x, 0);

    }
   
    IEnumerator DestryBulletOnTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
