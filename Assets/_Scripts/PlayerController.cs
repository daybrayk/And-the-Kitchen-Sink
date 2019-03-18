using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
    #region Public Variables
    public Text healthText;
    public Transform sinkSpawn;
    public Transform sinkThrow;
    public int maxHealth;
    public bool canThrow;
    [HideInInspector] public bool isFacingUI;
    #endregion

    #region Private Variables
    [SerializeField] float m_powerLimit;
    [SerializeField] float m_powerMin;
    float m_timer;
    float m_throwPower;
    float m_touchTime;
    [SerializeField] private float m_currentHealth;
    bool m_sinkStored;
    [SerializeField] float m_sinkCD;
    GameObject m_sinkInHands;

    [SerializeField] List<GameObject> m_sinks = new List<GameObject>();
    SinkController m_sinkScript;
    [SerializeField] private GameManager gm;
    [SerializeField] private BunkerScript m_currentBunker;
    private Animator m_anim;
    #endregion

    // Use this for initialization
    void Awake () {
        m_timer = m_sinkCD;
        if (m_powerMin <= 0)
            m_powerMin = 10f;
        m_throwPower = m_powerMin;
        m_anim = GetComponent<Animator>();
	}
    private void Start()
    {
        SpawnSink();
        m_currentHealth = maxHealth;
        healthText.text = "Health: " + m_currentHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        if (sinkInHands == null)
        {
            m_timer -= Time.deltaTime;
            if (m_timer < 0)
            {
                SpawnSink();
                m_timer = m_sinkCD;
            }
        }
        if (!isFacingUI && sinkInHands != null && canThrow)
        {
            
#if UNITY_EDITOR
            /*else if (Input.GetMouseButton(0))
            {
                if (m_throwPower <= m_powerLimit)
                    m_throwPower += m_powerLimit * Time.deltaTime;
                else
                    m_throwPower = m_powerLimit;
                m_sinkScript.force = m_throwPower;
            }
            else*/
            if (Input.GetMouseButtonUp(0))
            {
                ThrowSink();
                //m_anim.SetTrigger("throwSink");
                m_throwPower = m_powerMin;
            }
            #elif UNITY_ANDROID
            if((Input.touchCount > 0))
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Stationary)
                {
                    m_touchTime += Time.deltaTime;
                    if (m_touchTime >= 0.5f && !m_sinkStored)
                    {
                        ChangeSink();
                        m_sinkStored = true;
                    }
                    /*if (m_throwPower <= m_powerLimit)
                        m_throwPower += m_powerLimit * Time.deltaTime;
                    else
                        m_throwPower = m_powerLimit;
                    m_sinkScript.force = m_throwPower;*/
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    if (m_sinkStored)
                    {
                        m_sinkStored = false;
                        m_touchTime = 0;
                    }
                    else
                        ThrowSink();
                        //m_anim.SetTrigger("throwSink");
                    //m_throwPower = m_powerMin;
                }
            }
            #endif
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeSink();
        #endif
    }
    private void SpawnSink()
    {
        sinkInHands = Instantiate(m_sinks[Random.Range(0, 3)], sinkSpawn);  //Spawn a random sink
		//sinkInHands = Instantiate(_sinks[4], sinkSpawn);  //tests the new Fragmentation Sink
        sinkInHands.transform.position = sinkSpawn.position;    //Place the sink in the player's hands
        if(!(m_sinkScript = sinkInHands.GetComponent<SinkController>())) //Check if sink has a sinkscript component, if not add a simple sink script and continue
        {
            Debug.LogError("SinkScript not found on " + sinkInHands.name + "\nAdding a simple sink script to fix issue");
            m_sinkScript = sinkInHands.AddComponent<SimpleSink>();
        }
        m_sinkScript.SinkConstructor(gm, sinkSpawn, m_throwPower);  //Call the sink constructor to setup required variables
                                                                    //Sinks require a reference to the GameManager so the sink can be tracked, rather than using GameObject.Find I pass in a reference
        m_anim.SetTrigger("prepSink");
    }

    private void ThrowSink()
    {
        m_sinkScript.Throw(m_throwPower);
        sinkInHands = null;
        m_sinkScript = null;
    }

    private void ChangeSink()
    {
        if (currentBunker.m_storedSink)  //If a sink is already stored here then swap with the sink currently in the players hands
        {
            sinkInHands = currentBunker.SwapSink(sinkInHands);
            m_sinkScript = sinkInHands.GetComponent<SinkController>();
            sinkInHands.GetComponent<LineRenderer>().enabled = true;
            sinkInHands.transform.parent = sinkSpawn;
            sinkInHands.transform.position = sinkSpawn.position;
            sinkInHands.transform.rotation = sinkSpawn.rotation;
        }
        else   //If no sink is stored in this bunker then store the sink currently in the player's hands and spawn a new sink
        {
            currentBunker.StoreSink(sinkInHands);
            SpawnSink();
        }
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        if(m_currentHealth <= 0)
        {
            gm.ResetGame(); 
        }
        healthText.text = "Health: " + m_currentHealth;
    }

    public void ResetHealth()
    {
        m_currentHealth = maxHealth;
    }

#region Getters and Setters
    public GameObject sinkInHands
    {
        get { return m_sinkInHands; }
        set { m_sinkInHands = value; }
    }

    public float sinkCD
    {
        get { return m_sinkCD; }
        set { m_sinkCD = value;
            m_timer = m_sinkCD; }
    }

    public BunkerScript currentBunker
    {
        get { return m_currentBunker; }
        set { m_currentBunker = value; }
    }
#endregion
}
