using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerbricks,enemybrick;
    
    public GameObject[,] brickinstantiate;
    public Transform startPosition;
    public int k,l=0;
    public int totalNumberOfBricksofrow,numofbrickscolom;
    public Vector3 offset;
    public EnemyController enemyController;
    public GameObject spawnbridgepos,bridgecube;
    public Vector3 spawnposbridgechange;
    public GameObject[] enemybridgearray;
    
    // Start is called before the first frame update
    void Start()
    {
        // sp=GameObject.Find("GameManager").GetComponent<SpawnManager>();//chapair maar appne mu [pe]
        brickinstantiate=new GameObject[totalNumberOfBricksofrow,numofbrickscolom];
        enemybridgearray=new GameObject[11];
        SpawnBricks();
        //SpawnEnemyBridge();

        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void SpawnBricks(){
        for(int i=0;i<totalNumberOfBricksofrow;i++)
        {
            for(int j=0;j<numofbrickscolom;j++)
            {
               
                if(j%2==0 ) 
                {                
                  brickinstantiate[i,j]= (GameObject) Instantiate(playerbricks,startPosition.position+offset,startPosition.rotation);
                }
                else
                {
                    brickinstantiate[i,j] =(GameObject) Instantiate(enemybrick,startPosition.position+offset,startPosition.rotation) ;
                    enemyController.enemybrix.Add( brickinstantiate[i,j]);
                    
                }
             
                 offset+=new Vector3(5,0,0);
        
             }
           offset+=new Vector3(startPosition.transform.position.x-offset.x,0,5);
          // Debug.Log(brickinstantiate[0,0]);
        }
    }
    // public void SpawnEnemyBridge(){
    //     for(int bridgevalue=0;bridgevalue<11;bridgevalue++){
    //    //     Instantiate(bridgecube,spawnbridgepos.transform.position+spawnposbridgechange,bridgecube.transform.rotation);
    //     enemybridgearray[bridgevalue] =(GameObject) Instantiate(bridgecube,spawnbridgepos.transform.position+spawnposbridgechange,bridgecube.transform.rotation);
    //     enemyController.enemybridge.Add(enemybridgearray[bridgevalue]);
    //      spawnposbridgechange+=new Vector3(0,0,2);

    //     }
   // }
}
