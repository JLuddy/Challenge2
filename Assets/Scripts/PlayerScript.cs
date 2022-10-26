using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private bool gameOver;
    private bool facingRight = true;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
         musicSource.Play();
         gameOver = false;
          anim = GetComponent<Animator>();
         
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
            {
                 Flip();
             }
        else if (facingRight == true && hozMovement < 0)
            {
                Flip();
             }
        if (hozMovement >= 1)
                {
                    anim.SetInteger("State", 1);
                 }
        else if (hozMovement == 0)
            {
                anim.SetInteger("State", 0);
             }
             
    }



  void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
         
         if (livesValue == 0) 
		{
                    loseTextObject.SetActive(true);
                    Destroy(this.gameObject);
		}
        if (scoreValue == 4)
        {
            gameOver = true;
        }

        if (scoreValue == 4)
        {
            if (gameOver = true)
            {
            winTextObject.SetActive(true);
          musicSource.Stop();
          musicSource.clip = musicClipTwo;
          musicSource.Play();
            }
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
            }
        }
    }
 
}
