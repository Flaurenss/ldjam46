using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventsManager : MonoBehaviour
{
    /*** CONFIGURATION ***/
    [SerializeField]
    float m_maxTimeForNextEvent = 5;
    [SerializeField]
    float m_minTimeForNextEvent = 3;

    [SerializeField]
    float m_maxEventDuration = 10;
    [SerializeField]
    float m_minEventDuration = 6;

    [SerializeField]
    List<Transform> m_eventTargets = new List<Transform>();


    /*** STATE ***/
    [SerializeField]
    List<GameEvent> m_activeEvents = new List<GameEvent>();
    float m_nextEventTime;
    bool m_eventsActive = false;


    /*** Game Flow ***/
    void Update()
    {
        if (m_eventsActive)
        {
            tickEvents();

            if (Time.time >= m_nextEventTime)
            {
                addRandomEvent();
                resetNextEventTime();
            }
        }
    }

    // Enables the creation and update of game events
    public void startEvents () {
        m_eventsActive = true;
        resetNextEventTime();
    }

    // Disables the creation and update of game events
    public void stopEvents () {
        m_eventsActive = true;
    }

    private void resetNextEventTime () {
        float timeForNextEvent = Random.Range(m_minTimeForNextEvent, m_maxTimeForNextEvent);
        m_nextEventTime = Time.time + timeForNextEvent;
    }

    private void tickEvents()
    {
        for (int i = 0; i < m_activeEvents.Count; i++)
        {

            float value = m_activeEvents[i].checkExpiration();
            if (value == 0)
            {
                handleEventExpiration(m_activeEvents[i]);
            }
        }
    }

    private void handleEventExpiration(GameEvent gameEvent)
    {
        m_activeEvents.Remove(gameEvent);
        Debug.Log("event expired");
    }


    /*** Event generation ***/
    private void addRandomEvent()
    {
        Transform target = pickEventTarget();
        GameEvent newEvent = new GameEvent(pickEventType(), calculateEventDuration(), target);
        m_activeEvents.Add(newEvent);
    }

    private float calculateEventDuration()
    {
        // TODO: we might want events to have different duration ranges?
        return Random.Range(m_maxEventDuration, m_minEventDuration);
    }

    private Transform pickEventTarget()
    {
        // TODO: refine logic for pickng a target instead of pure random?
        return m_eventTargets[Random.Range(0, m_eventTargets.Count)];
    }

    private int pickEventType()
    {
        // TODO: refine logic for pickng a type instead of pure random?
        return Random.Range(0, 2);
    }

    void OnDrawGizmos (){
        foreach (GameEvent ev in m_activeEvents)
        {
            Gizmos.color = Color.Lerp(Color.red, Color.green, ev.checkExpiration());
            Gizmos.DrawLine(new Vector3(0,0,0), ev.m_target.position);
        }
    }
}
