using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator enemyanimator;
    public GameObject player,enemyposspawn,enemy;
    private SpawnManager spawnmngr;
    private Vector3 direction;
    public static Stack<GameObject> stack = new Stack<GameObject>();
     public Material brdgeenemyclr;
     private GameObject [] accessarray;
     public List<GameObject> enemybrix=new List<GameObject>();
       NavMeshAgent agent;
      static int currentindex=0,bridgeindex=0;
      public GameObject bridgecube,bridgeexitcube;
      public bool pick,other,outbridge,arrived;
      public GameManager gm;
      public List<GameObject> enemybridge=new List<GameObject>();
      
    void Awake(){
      arrived=false;
        enemyanimator=GetComponent<Animator>();
        enemyanimator.SetBool("enemyrun",true);
        Time.timeScale=0; 
        //Time.timeScale=1; 
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnmngr=GameObject.Find("GameManager").GetComponent<SpawnManager>();
        gm=GameObject.Find("GameManager").GetComponent<GameManager>();
        agent=GetComponent<NavMeshAgent>();
        pick=true;
        other=false;
        outbridge=false;
        agent.SetDestination(enemybrix[currentindex].transform.position);
        enemybrix[0].GetComponent<Collider>().isTrigger=true;
       
       
    }
    // Update is called once per frame
    void Update()
    {  
       if(pick==true && arrived==false)
       {
            if (Vector3.Distance(transform.position,enemybrix[currentindex].transform.position) < 4)
               {
                  Debug.Log(currentindex);
                 if(enemybrix!=null && currentindex>=0){
                    Debug.Log(currentindex);
                    currentindex+=1;
                    
                    enemybrix[currentindex].GetComponent<Collider>().isTrigger=true;
                    agent.SetDestination(enemybrix[currentindex].transform.position); 
                    Debug.Log(enemybrix[currentindex].name);
                   // currentindex+=1;
                     Debug.Log(currentindex);
                     Debug.Log(enemybrix.Count);
                   //  enemybrix.Remove(enemybrix[currentindex]);
                    // enemybrix.Add(default);
                     Debug.Log(enemybrix.Count);
                     Debug.Log(currentindex);  
                  }  
               }
       }         
            // if(currentindex==4){
               if(stack.Count==6){
                    pick=false;
                    
                    other=true;   
             }
              if(other==true){
                  agent.SetDestination(bridgeexitcube.transform.position);
              } 
              if(arrived==true){
                  pick=false;
                 other=false;
                 transform.position=bridgeexitcube.transform.position;
              }
            //  if(outbridge==true){
            //    if(Vector3.Distance(transform.position,enemybridge[bridgeindex].transform.position)<4){
            //      Debug.Log("Bridge index"+bridgeindex);
            //    agent.SetDestination(enemybridge[bridgeindex].transform.position);
            //     Debug.Log("Bridge index"+bridgeindex);
            //    bridgeindex+=1;
            //     Debug.Log("Bridge index"+bridgeindex);
            //    agent.SetDestination(enemybridge[bridgeindex].transform.position);
            //    }
            //  } 
        } 
    public void OnTriggerEnter(Collider col){
        if(col.gameObject.tag=="enemybrick"){
            Destroy(col.gameObject);
            GameObject childenemy=Instantiate(spawnmngr.enemybrick,enemyposspawn.transform.position+direction,transform.rotation);
            stack.Push(childenemy);
            childenemy.transform.parent=enemy.transform;
            direction+=new Vector3(0,1f,0);
            Debug.Log("collided enemy");
        }
         if(col.gameObject.tag=="AIbridgeexit"){
           arrived=true;
          gm.destination=true;
          // Time.timeScale=0;
           
           
        //    Debug.Log("collide with bridge enter");
        //    outbridge=true;
            // other=false;
            
           
         }
    }
        public void OnCollisionEnter(Collision col){
          
          if(col.gameObject.tag=="permission"){
            col.gameObject.GetComponent<Collider>().isTrigger=true;

          }
            if(col.gameObject.CompareTag("enemybridge")){
           // Debug.Log("collided");
           if(stack!=null && arrived==false){ 
           try{

           
            GameObject temp=stack.Pop();
            Destroy(temp);
            direction-=new Vector3(0,1f,0);
             col.gameObject.GetComponent<Renderer>().material=brdgeenemyclr;  
            col.gameObject.GetComponent<Collider>().isTrigger=true;
           }

           catch(System.Exception e){

           }
       


           }
            if(stack.Count==0 && arrived==false){ 
              
              Debug.Log("Ceck stack counter");
              agent.SetDestination(enemybrix[currentindex].transform.position);
              enemybrix[currentindex].gameObject.GetComponent<Collider>().isTrigger=true;
              Debug.Log("length of list"+stack.Count); 
              pick=true; 
              Debug.Log("Pickup brickbool checking"+pick);
              other=false; 
              Debug.Log("Destination bool checking"+other);
                         
            }
        }
        }
   // }
}
