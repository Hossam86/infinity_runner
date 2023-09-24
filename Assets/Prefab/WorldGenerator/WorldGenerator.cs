using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] Transform StartingPoint;
    [SerializeField] Transform EndPoint;

    [SerializeField] GameObject[] RoadBlocks;

    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 nextBlockPostion = StartingPoint.position;
        float EndPointDistance= Vector3.Distance(StartingPoint.position, EndPoint.position);

        while (Vector3.Distance(StartingPoint.position, nextBlockPostion) < EndPointDistance)
        {
            int pick = Random.Range(0, RoadBlocks.Length);
            GameObject pickedBlock = RoadBlocks[pick];
            GameObject newBlock = Instantiate(pickedBlock);
            newBlock.transform.position = nextBlockPostion;
            float blockLength = newBlock.GetComponent<Renderer>().bounds.size.z;
            nextBlockPostion += (EndPoint.position - StartingPoint.position).normalized * blockLength;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
