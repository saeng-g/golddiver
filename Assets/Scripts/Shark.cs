using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public float speed_coeff = 0.35f;
    public float speed;
    public Rigidbody2D shark;

    public float lifetime = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = speed_coeff * Random.Range(1f, 5f)%6.0f;
        shark.velocity = transform.right * -speed;
    }

    private void Update()
    {
        CountTime();
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector2(-transform.position.x, transform.position.y);
    }

    // When it's triggered by another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMvt diver = collision.GetComponent<PlayerMvt>();
        if (diver != null)
        {
            diver.TakeDamage();
            Destroy(gameObject);
        }
    }

    void CountTime()
    {
        lifetime -= Time.deltaTime;
    }

    public void ChangeVelocity(int level)
    {
        speed_coeff = speed_coeff * (1.0f + (0.07f * (level-1)));
    }
}
