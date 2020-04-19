using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
    [SerializeField]
    public Transform m_target;

    [SerializeField]
    public int m_type;
    [SerializeField]
    private float m_startTime;
    [SerializeField]
    private float m_duration;

    public GameEvent(int type, float duration, Transform target)
    {
        m_type = type;
        m_startTime = Time.time;
        m_duration = duration;
        m_target = target;
    }

    // Returns a value between 0 and 1 representing the time left.
    // 0 means the time has expired. 1 means the event just started.
    public float checkExpiration()
    {
        float elapsedTime = Time.time - m_startTime;
        return Mathf.Clamp01(1 - elapsedTime / m_duration);
    }
}
