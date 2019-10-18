using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int score;

    // When it's triggered by another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMvt diver = collision.GetComponent<PlayerMvt>();
        if (diver != null)
        {
            score += diver.EmptyLoad();
        }
    }

    public void Reset()
    {
        score = 0;
    }
}
