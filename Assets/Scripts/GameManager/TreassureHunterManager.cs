using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreassureHunterManager :MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Image treassureSensor;
    [SerializeField] private TMP_Text treassureSensorText;
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private GameObject treassurePrefab;

    [SerializeField] private Vector2 maxPositions;
    [SerializeField] private Vector2 minPositions;


    [SerializeField] private int currentRound = 0;
    private GameObject currentTressure;
    private List<GameObject> trapsPlaced;
    private bool started = false;

    private void Start()
    {
        started = false;
        Generate(10);
    }
    public void Generate(int maxTrap)
    {
        for (int i = 0; i < maxTrap; i++)
        {

            Vector3 newPos = new Vector3(Random.Range(minPositions.x * 5 , maxPositions.x * 5) , 0.2f , Random.Range(minPositions.y * 5 , maxPositions.y * 5));
            GameObject clone = Instantiate(trapPrefab , newPos , Quaternion.identity);
            clone.name = "Trap " + i;

        }

        Vector3 tPos = new Vector3(Random.Range(minPositions.x * 5 , maxPositions.x * 5) , 0.2f , Random.Range(minPositions.y * 5 , maxPositions.y * 5));
        GameObject tClone = Instantiate(treassurePrefab , tPos , Quaternion.identity);
        tClone.name = "Treassure " + currentRound;
        currentTressure = tClone;
        started = true;
    }


    private void Update()
    {
        if (started)
        {
            float dist = Vector3.Distance(playerPos.position , currentTressure.transform.position);
            Debug.Log(dist);
            float distRemaped = dist.Remap(0f , 70 , 0f , 1f);
            treassureSensor.color = Color.Lerp(Color.red , Color.blue , distRemaped);
            treassureSensorText.text = dist.ToString("0.00") + "m";
        }
    }

}
