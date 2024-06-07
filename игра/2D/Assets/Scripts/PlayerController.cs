using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    List<GameObject> hpimages;
    [SerializeField] private int HeathPoints;
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject hp;
    [SerializeField] private GameObject hpPanel;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletStartPos;
    [SerializeField] TextMeshProUGUI counterText;
    private Vector2 startPos;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip picUpCoinSound;
    [SerializeField] AudioClip JumpSound;
    [SerializeField] AudioClip DeathSound;
    bool hasHorMove;
    GameObject CurFlag;
    int cointCoun = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("PosX"))
            startPos = LoadPos();
        else startPos = new Vector2(-6, -2.5f);
        if (PlayerPrefs.HasKey("HP")) 
        {
           
          HeathPoints = PlayerPrefs.GetInt("HP");
        }
        else HeathPoints = 3;

        ShowHP();
        transform.position = startPos;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.R)) Atack();
        Move();
    }
    void Move()
    {
        float horInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector2(horInput, rb.velocity.y);
        hasHorMove = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", hasHorMove);
        if (hasHorMove)
        {
            Flip();
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
        animator.SetTrigger("IsJumpt");
        audioSource.clip = JumpSound;
        audioSource.Play();
    }
    void Atack()
    {
        GameObject g = Instantiate(bullet, bulletStartPos.position, Quaternion.identity);
        g.transform.localScale = transform.localScale / 5f;
    }

    void Flip()
    {
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PE"))
        {
            manager.EndGame();
        }
        if (collision.gameObject.CompareTag("P"))
        {
            manager.LoadNextLevel();
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            Collect();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            Resart(); 
        }
        if (collision.gameObject.CompareTag("Flag"))
        {
            if (CurFlag != null)
            {

                CurFlag.GetComponent<SpriteRenderer>().color = new Color(0.6666667f, 0.003921569f, 0.003921568f);
                CurFlag = null;
            }
            SetPos(collision.transform);
            collision.GetComponent<SpriteRenderer>().color = new Color(0.003921569f, 1, 0.3529412f);
            CurFlag = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.clip = DeathSound;
            audioSource.Play();
           
            rb.AddForce(Vector2.left*Time.deltaTime*3,ForceMode2D.Impulse);
           StartCoroutine( Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(.6f);
        HeathPoints--;
        PlayerPrefs.SetInt("HP", HeathPoints);
        cointCoun = 0;
        counterText.text = cointCoun.ToString();
        ShowHP();
      
        if (HeathPoints <= 0)
        {
            manager.GameOver();
            PlayerPrefs.DeleteKey("HP");
        }
        Resart();
    }

    void ShowHP()
    {
        if (hpimages != null)
        {
            for (int i = 0; i < hpimages.Count; i++)
            {
                Destroy(hpimages[i]);   
            }
        }
        hpimages = new List<GameObject> ();
        for (int i = 1; i <= HeathPoints; i++)
        {
            GameObject o = Instantiate(hp, hpPanel.transform);
            hpimages.Add(o);
        }
    }

    private void Resart()
    {
        manager.LoadThisLevel();

    }

    void SetPos(Transform newPos)
    {
        startPos = newPos.position;
        SavePos(startPos.x, startPos.y);
    }
    void SavePos(float x, float y)
    {
        PlayerPrefs.SetFloat("PosX", x);
        PlayerPrefs.SetFloat("PosY", y);
    }
    Vector2 LoadPos()
    {
        return new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));
    }
    void Collect()
    {
        audioSource.clip = picUpCoinSound;
        audioSource.Play();
        cointCoun++;
        counterText.text = cointCoun.ToString();
    }
}
