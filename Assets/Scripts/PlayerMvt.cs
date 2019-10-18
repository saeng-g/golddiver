using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMvt : MonoBehaviour
{
    public int life = 2;
    private float velocity = 1.5f;
    private float gold = 0.0f;
    public int goldType = 0; //0 None, 1 Small, 2 Med, 3 Large
    Vector2 mvt;
    Rigidbody2D rb;

    public bool gameStarted = false;

    public bool booster = false;
    public bool boosterEnabled = false;
    public bool boosterRecharged = false;

    public float boosterTime = 0.75f;
    public float upwardTime = 0.75f;

    public Sprite life2Sprite;
    public Sprite life1Sprite;
    public Sprite life0Sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()  
    {
        mvt.x = Input.GetAxisRaw("Horizontal");
        mvt.y = Input.GetAxisRaw("Vertical");
        if (boosterEnabled && boosterRecharged)
        {
            booster = Input.GetKey(KeyCode.Space);
        }
        if (boosterTime <= 0.01f || !boosterRecharged)
        {
            booster = false;
            boosterRecharged = false;
            AddBoosterTime();
        }
        if (boosterTime >= 0.75f)
        {
            boosterRecharged = true;
        }

        if (mvt.y > 0.0f)
            CountUpwardTime(false);
        else
            CountUpwardTime(true);

        if (gameStarted)
            ChangeSprite();
    }

    public void FixedUpdate()
    {
        if (upwardTime <= 0.0f && mvt.y > 0.0f)
            mvt.y = 0;
        gold = goldType * 0.05f;
        if (booster)
        {
            rb.MovePosition(rb.position + (4f
                                           * mvt
                                           * (velocity - gold)
                                           * Time.fixedDeltaTime));
            CountBoosterTime();
        }
        else
        {
            rb.MovePosition(rb.position + (mvt
                                       * (velocity - gold)
                                       * Time.fixedDeltaTime));
        }
        if (life == 0)
            EndGame();
    }

    void CountUpwardTime(bool refill)
    {
        if (refill)
        {
            if (upwardTime < 0.75f)
                upwardTime += 3*Time.deltaTime;
        }
        else
            upwardTime = Mathf.Max(upwardTime - Time.deltaTime, 0.0f);
    }

    void CountBoosterTime()
    {
        boosterTime = Mathf.Max(0.0f, boosterTime - 0.5f*Time.deltaTime);
    }

    void AddBoosterTime()
    {
        boosterTime = Mathf.Min(boosterTime + Time.deltaTime, 0.75f);
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector2(-transform.position.x, transform.position.y);
    }

    public void TakeDamage()
    {
        if (life > 0)
        {
            life -= 1;
            ChangeSprite();
        }
    }

    public void LoadGold(int goldSize)
    {
        goldType += goldSize;
    }

    public int EmptyLoad()
    {
        int a = goldType;
        goldType = 0;
        return a;
    }

    void ChangeSprite()
    {
        if (life == 1)
        {
            GetComponent<SpriteRenderer>().sprite = life1Sprite;
        }
        else if (life == 0)
        {
            GetComponent<SpriteRenderer>().sprite = life0Sprite;
        }
        else if (life == 2)
        {
            GetComponent<SpriteRenderer>().sprite = life2Sprite;
        }
    }

    void Reset()
    {
        rb.position = new Vector2(0.00f, 1.05f);
        goldType = 0;
        life = 2;
        ChangeSprite();
    }

    public void StartGame(bool boosterMode)
    {
        gameStarted = true;
        boosterEnabled = boosterMode;
        Reset();
    }

    public void EndGame()
    {
        gameStarted = false;
        rb.position = new Vector2(0.00f, 1.05f);
    }
}
