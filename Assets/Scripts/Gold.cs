using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int size = 1;

    public float lifetime = 3.0f;

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
            diver.LoadGold(size);
            Destroy(gameObject);
        }
    }

    void CountTime()
    {
        lifetime -= Time.deltaTime;
    }

    public void SetSizeToSmall()
    {
        size = 1;
    }

    public void SetSizeToMed()
    {
        size = 2;
    }

    public void SetSizeToLarge()
    {
        size = 10;
    }
}
