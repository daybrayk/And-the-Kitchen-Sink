using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
    #region UI References
    [Header("UI")]
    [SerializeField] private Text m_healthText;
    #endregion

    #region Player Stuff
    [Header("Player Stuff")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float m_throwPower;
    [SerializeField] private BunkerScript m_currentBunker;
    [HideInInspector] public bool isAbleToThrow;
    [HideInInspector] public bool isFacingUI;
    private Animator m_anim;
    private float m_currentHealth;
    #endregion

    #region Object References
    private GameManager m_gameManager;
    #endregion

    #region Sink Variables
    [Header("Sink Stuff")]
    [SerializeField] private float m_sinkThrowCD;
    [SerializeField] private Transform m_sinkSpawnPosition;
    [SerializeField] private Transform m_sinkThrowPosition;
    [SerializeField] private List<GameObject> m_sinks = new List<GameObject>();//List of active sinks in the scene so they can be tracked and destroyed
    private float m_sinkThrowTimer;
    private GameObject m_sinkInHands;
    private SinkController m_currentSinkController;
    #endregion

    #region Touch Variables
    private float m_touchTime;
    private Touch[] m_touches;
    #endregion

    // Use this for initialization
    void Awake () {
        m_anim = GetComponent<Animator>();
        m_gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (m_throwPower <= 0)
            m_throwPower = 600f;

        if (m_sinkThrowTimer <= 0)
            m_sinkThrowTimer = 1;
        m_sinkThrowTimer = m_sinkThrowCD;

        m_currentHealth = maxHealth;
        m_healthText.text = "Health: " + m_currentHealth;

        SpawnSink();
    }

    // Update is called once per frame
    void Update ()
    {
        if (SinkInHands == null)
        {
            m_sinkThrowTimer -= Time.deltaTime;
            if (m_sinkThrowTimer < 0)
            {
                SpawnSink();
                m_sinkThrowTimer = m_sinkThrowCD;
            }
        }

        //Don't want to try and throw if the player is facing the UI menu or if there is no sink ready, and able, to be thrown
        if (!isFacingUI && SinkInHands != null && isAbleToThrow)
        {
            
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
            {
                ThrowSink();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeSink();
                m_anim.SetTrigger("prepSink");
            }

#elif UNITY_ANDROID
            if((Input.touchCount > 0))
            {
                m_touches = Input.touches;
                Touch touch = m_touches[0];
                if(touch.phase == TouchPhase.Ended)
                {
                    ThrowSink();
                }
                else if(touch.phase == TouchPhase.Stationary)
                {
                    m_touchTime += Time.deltaTime;
                    if (m_touchTime >= 1.0f /*&& !m_sinkStored*/)
                    {
                        ChangeSink();
                        m_anim.SetTrigger("prepSink");
                        //m_sinkStored = true;
                    }
                }
                else
                {
                    m_touchTime = 0;
                }
            }
#endif
        }
    }
    private void SpawnSink()
    {
        m_sinkInHands = Instantiate(m_sinks[Random.Range(0, 3)], m_sinkSpawnPosition);  //Randomize sink spawn
        m_sinkInHands.transform.position = m_sinkSpawnPosition.position;    //Place the sink in the player's hands

        if(!(m_currentSinkController = m_sinkInHands.GetComponent<SinkController>())) //If no sink script component is found, add one
        {
            Debug.LogError("SinkScript not found on " + SinkInHands.name + "\nAdding a simple sink script to fix issue");
            m_currentSinkController = m_sinkInHands.AddComponent<SimpleSink>();
        }

        m_currentSinkController.SinkConstructor(m_gameManager, m_sinkSpawnPosition, m_throwPower);  //Call the sink constructor to setup required variables
                                                                    //Sinks require a reference to the GameManager so the sink can be tracked, rather than using GameObject.Find I pass in a reference
        m_anim.SetTrigger("prepSink");
    }

    private void ThrowSink()
    {
        if (!m_currentSinkController)
            return;

        m_currentSinkController.Throw(m_throwPower);
        m_sinkInHands = null;
        m_currentSinkController = null;
    }

    private void ChangeSink()
    {
        if (m_currentBunker.m_storedSink)  //If a sink is already stored here then swap with the sink currently in the players hands
        {
            SinkInHands = m_currentBunker.SwapSink(SinkInHands);
            m_currentSinkController = SinkInHands.GetComponent<SinkController>();
            SinkInHands.GetComponent<LineRenderer>().enabled = true;
            SinkInHands.transform.parent = m_sinkSpawnPosition;
            SinkInHands.transform.position = m_sinkSpawnPosition.position;
            SinkInHands.transform.rotation = m_sinkSpawnPosition.rotation;
        }
        else   //If no sink is stored then store the sink currently in the player's hands and spawn a new sink
        //TODO: Possibly remove Bunker script and have the stored sink attached to the player as bunkers are no longer necessary
        {
            m_currentBunker.StoreSink(SinkInHands);   //null check for SinkInHands is done by the Bunker script
            SpawnSink();
        }
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        if(m_currentHealth <= 0)
        {
            m_gameManager.ResetGame(); 
        }
        m_healthText.text = "Health: " + m_currentHealth;
    }

    public void ResetHealth()
    {
        m_currentHealth = maxHealth;
    }

#region Public Properties
    public GameObject SinkInHands
    {
        get { return m_sinkInHands; }
        set { m_sinkInHands = value; }
    }

    public float SinkCD
    {
        get { return m_sinkThrowCD; }
        set { m_sinkThrowCD = value;
            m_sinkThrowTimer = m_sinkThrowCD; }
    }

    public Transform SinkThrowPosition
    {
        get { return m_sinkThrowPosition; }
    }
    #endregion
}
