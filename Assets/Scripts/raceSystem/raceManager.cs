using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class raceManager : MonoBehaviour
{
    public static raceManager instance;
    [SerializeField] carController1 playerCar;
    [SerializeField] Transform startPoint;
    [SerializeField] checkpoints[] checkpointsArray;
    [SerializeField] int lastCheckedCheckpointIndex = -1;
    [SerializeField] int debugCheckedCheckpointIndex = -1;
    [SerializeField] bool isCircuit = false;
    [SerializeField]public int totalLaps = 1;
    [SerializeField] TextMeshProUGUI lapText;
    [SerializeField] TextMeshProUGUI lapTimerText;
    [SerializeField] TextMeshProUGUI overallTimerText;
    [SerializeField] TextMeshProUGUI bestLapTimerText;
    [SerializeField] TextMeshProUGUI raceFinishText;
    [SerializeField] InputAction respawn;
    int currentLap = 1;
    bool raceFinished = false;
    bool raceStarted = false;
    float currentLapTime=0f;
    float overallRaceTime=0f;
    float bestLapTime= Mathf.Infinity;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        respawn.Enable();
    }
    void Update()
    {
        if(raceStarted && !raceFinished)
        {
            updateTimer();
        }
        updateUI();
        if (respawn.WasPressedThisFrame())
        {
            respawnCar();
        }
    }
    void respawnCar()
        {
            int respawnIndex = lastCheckedCheckpointIndex;
            if(respawnIndex < 0)
            {
                respawnAtStart();
                return;
            }
            while (respawnIndex>0 && checkpointsArray[respawnIndex].disableRespawn)
            {
                respawnIndex--;
            }
            respawnAtCheckpoint(respawnIndex);

        }
    void respawnAtStart()
    {
        Rigidbody rb = playerCar.GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = startPoint.position;
        rb.rotation = startPoint.rotation;

        rb.rotation = Quaternion.LookRotation(startPoint.forward, Vector3.up);

    }
    void respawnAtCheckpoint(int index)
    {
        Transform point = checkpointsArray[index].respawnPoint;

        Rigidbody rb = playerCar.GetComponent<Rigidbody>();


        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = point.position;
        rb.rotation = point.rotation;

        rb.rotation = Quaternion.LookRotation(-point.forward, Vector3.up);
    }
    IEnumerator FlashText(TextMeshProUGUI text)
    {
        while (raceFinished)
        {
            float alpha = Mathf.PingPong(Time.time * 2f, 1f);
            Color c = text.color;
            c.a = alpha;
            text.color = c;
            yield return null;
        }
    }
    void startRace()
    {
        raceStarted = true;
        raceFinished= false;
        currentLap = 1;
        currentLapTime = 0f;
        overallRaceTime = 0f;
        lastCheckedCheckpointIndex = -1;
    }
    void endRace()
    {
        raceFinished = true;
        raceStarted= false;
        if (!raceFinishText.gameObject.activeSelf)
        {
            raceFinishText.gameObject.SetActive(true);
            StartCoroutine(FlashText(raceFinishText));
        }
        Invoke("reloadScene", 5f);
    }
    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void onLapFinish()
    {
        currentLap++;
        if(currentLapTime < bestLapTime)
        {
            bestLapTime = currentLapTime;
        }
        if(currentLap > totalLaps)
        {
            endRace();
        }
        else 
        {
            currentLapTime = 0f;
            lastCheckedCheckpointIndex = isCircuit ? -1: 0;
        }
    }

    public void checkpointReached(int checkpointIndex)
    {
        debugCheckedCheckpointIndex = checkpointIndex;
        if (checkpointIndex == lastCheckedCheckpointIndex) return;
        if(raceFinished) return;
        if(!raceStarted && checkpointIndex !=0) return;
        if(checkpointIndex == lastCheckedCheckpointIndex + 1)
        {
            updateCheckpoint(checkpointIndex);   
        }
        else if (isCircuit &&
             checkpointIndex == 0 &&
             lastCheckedCheckpointIndex == checkpointsArray.Length - 1)
        {
            updateCheckpoint(checkpointIndex);
        }
    }
    void updateCheckpoint(int checkpointIndex)
    {
        if(checkpointIndex == 0)
        {
            if (!raceStarted)
            {
                startRace();
            }
            else if(isCircuit && lastCheckedCheckpointIndex == checkpointsArray.Length - 1)
            {
                onLapFinish();
            }
        }
        else if(!isCircuit && checkpointIndex == checkpointsArray.Length - 1)
        {
            onLapFinish();
        }
        lastCheckedCheckpointIndex = checkpointIndex;
    }
    void updateTimer()
    {   
        currentLapTime += Time.deltaTime;
        overallRaceTime += Time.deltaTime;
    }
    void updateUI()
    {
        lapText.text = "Lap:" + currentLap.ToString() +"/" +totalLaps.ToString();
        lapTimerText.text = "Current Lap Time:"+generateTimer(currentLapTime);
        overallTimerText.text = "Total Time:"+generateTimer(overallRaceTime);
        bestLapTimerText.text = "Best Lap Time:"+generateTimer(bestLapTime);
    }
    string generateTimer(float time)
    {
        if((float.IsInfinity(time)) )
        {
            return "--:--";
        }
        int minutes = (int)(time / 60);
        int seconds = (int)time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}