using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour {
    #region Public Variables
    public Transform sinkSpawn;
    [HideInInspector] public bool isFacingUI;
    #endregion

    #region Private Variables
    [SerializeField] float m_powerLimit;
    [SerializeField] float m_powerMin;
    float m_timer;
    float m_throwPower;
    [SerializeField] float m_sinkCD;
    GameObject m_sinkInHands;

    [SerializeField] List<GameObject> m_sinks = new List<GameObject>();
    SinkController m_sinkScript;
    [SerializeField] private GameManager gm;
    private BunkerScript m_currentBunker;
    #endregion

    // Use this for initialization
    void Awake () {
        m_timer = m_sinkCD;
        if (m_powerMin <= 0)
            m_powerMin = 10f;
        m_throwPower = m_powerMin;
	}
    private void Start()
    {
        SpawnSink();
    }

    // Update is called once per frame
    void Update () {
/*#if UNITY_EDITOR
        float hValue = Input.GetAxisRaw("Mouse X");
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + hValue, transform.eulerAngles.z);
#endif*/
        if (!isFacingUI)
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
            else if (Input.GetMouseButton(0))
            {
                if (m_throwPower <= m_powerLimit)
                    m_throwPower += m_powerLimit * Time.deltaTime;
                else
                    m_throwPower = m_powerLimit;
                m_sinkScript.force = m_throwPower;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ThrowSink();
                m_throwPower = m_powerMin;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            ChangeSink();
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
        }
        else   //If no sink is stored in this bunker then store the sink currently in the player's hands and spawn a new sink
        {
            currentBunker.StoreSink(sinkInHands);
            SpawnSink();
        }
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
