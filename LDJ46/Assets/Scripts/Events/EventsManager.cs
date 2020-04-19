using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Event type for int events
[System.Serializable]
public class FloatUnityEvent : UnityEvent<float> {}


public class EventsManager : MonoBehaviour
{
    /*** Public Events ***/
    public FloatUnityEvent gameEventCompleted = new FloatUnityEvent();
    public UnityEvent gameEventFailed = new UnityEvent();
    

    /*** CONFIGURATION ***/
    [SerializeField]
    List<Transform> m_eventTargets = new List<Transform>();

    [Header("Timer ranges")]
    [SerializeField]
    float m_maxTimeForNextEvent = 5;
    [SerializeField]
    float m_minTimeForNextEvent = 3;

    [SerializeField]
    float m_maxEventDuration = 10;
    [SerializeField]
    float m_minEventDuration = 6;

    [SerializeField]
    [Range(0,1)]
    private float m_eventsWarningThreshold = 0.5f;


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
                exipireEvent(m_activeEvents[i]);
            }
        }
    }

    // Checks in the list of active game events if there is any event with the passed itemId and target transform
    // If succcessful: complete found event and return true
    // If no event found: return false
    // <param><c>itemId</c> Identifier of the item currently in the player's inventory</param>
    // <param><c>interactionTarget</c> Transform of the gameObject being interacted with</param>
    public bool checkEventCompletable (int itemId, Transform interactionTarget) {
        foreach (GameEvent gameEvent in m_activeEvents) {
            if (
                gameEvent.m_type == itemId && 
                gameEvent.m_target == interactionTarget && 
                gameEvent.checkExpiration() > 0
            ) {
                completeEvent(gameEvent);
                return true;
            }
        }

        return false;
    }

    private void completeEvent(GameEvent ev) {
        // TODO: define actions when an event is completed: add score, play sounds...
        m_activeEvents.Remove(ev);
        gameEventCompleted.Invoke(ev.checkExpiration());
    }

    private void exipireEvent(GameEvent gameEvent)
    {
        // TODO: do something when an event's time runs out: lose the game, substract score...
        m_activeEvents.Remove(gameEvent);
        gameEventFailed.Invoke();
    }


    /*** Event generation ***/
    private void addRandomEvent()
    {
        Transform target = pickEventTarget();
        GameEvent newEvent = new GameEvent(pickEventType(), calculateEventDuration(), target, m_eventsWarningThreshold);
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
        // TODO: there should not be 2 events of the same type with the same target
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
