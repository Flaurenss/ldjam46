using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    VampireGameEventRef gameEventRef;
   
    public float threshold;

    private Color originalColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameEventRef = GetComponent<VampireGameEventRef>();
        originalColor = spriteRenderer.color;
    }

    void Start()
    {
        
    }


    void Update()
    {
        GameEvent gameEvent = oldestGameEvent();
        if (gameEvent != null)
        {
            float expiration = gameEvent.checkExpiration();
            
            if (expiration < threshold)
            {
                spriteRenderer.color = new Color(255, 255.0f * expiration, 255.0f * expiration, expiration);
            }    
            else
            {
                spriteRenderer.color = new Color(255, 255.0f * expiration, 255.0f * expiration, expiration);
            }
        }        
    }

    GameEvent oldestGameEvent()
    {
        GameEvent oldEvent = null;
        float oldest = float.MaxValue;

        foreach (GameEvent g in gameEventRef.linkedGameEvent)
        {
            if (g != null && g.checkExpiration() < oldest)
            {
                oldEvent = g;
                oldest = g.checkExpiration();
            }
        }

        return oldEvent;
    }


}
