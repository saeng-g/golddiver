using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D octopus;

    public float lifetime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.55f * Random.Range(1f, 5f)%6.0f;
        octopus.velocity = transform.right * -speed;
    }

    private void Update()
    {
        CountTime();
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CountTime()
    {
        lifetime -= Time.deltaTime;
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

    public void ChangeVelocity(int level)
    {
        octopus.velocity = octopus.velocity * (1.0f + (0.07f * (level-1)));
    }
}
