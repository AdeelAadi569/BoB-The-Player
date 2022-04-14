using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator playeranimator;
    public ParticleSystem brickpickup;  
    public GameObject plyr;
    public Text score;
    public static int scorecount=0;
    public GameObject brickgoingposition;
    public GameObject destinationpoint;
    public static Stack<GameObject> stack = new Stack<GameObject>();
   // private SpawnManager sm;
   private SpawnManager sm;
   private Vector3 direction;
   private GameObject currentpos;
   public GameObject player;
   public Material brdgeclr;
   public bool w,s,d,a,conditional,movementbool=true,movetoward=false;
     public GameManager gm;
   public float speed=8,xrightrange=38,xleftrange=-19,zbackrange=-23;
    // Start is called before the first frame update
    void Start()
    {
        movetoward=false;
        playeranimator=GetComponent<Animator>();
        score.text="Score :"+scorecount;
        sm=GameObject.Find("GameManager").GetComponent<SpawnManager>();
        gm=GameObject.Find("GameManager").GetComponent<GameManager>();
        w=true;
        s=true;
        a=true;
        d=true;
        conditional=false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(movementbool==true){
        Movement();
        }
        if(transform.position.x>=xrightrange){
            transform.position=new Vector3(xrightrange,transform.position.y,transform.position.z);
        }
        if(transform.position.x<=xleftrange){
            transform.position=new Vector3(xleftrange,transform.position.y,transform.position.z);
        }
        if(transform.position.z<=zbackrange){
            transform.position=new Vector3(transform.position.x,transform.position.y,zbackrange);
        }
        if(movetoward==true){
            movementbool=false;
            Debug.Log(movetoward);
             transform.position=  Vector3.MoveTowards(transform.position,destinationpoint.transform.position,5*Time.deltaTime);
             playeranimator.SetBool("Running",true);
        }
        if(movetoward==false){
            movementbool=true;
        }
        
        if(transform.position==destinationpoint.transform.position){
            playeranimator.SetBool("Dance",false);
        } 
        
    }
    public void Movement(){
        if(Input.GetKey(KeyCode.W) && w==true ){
          //  Debug.Log("Pressed w for forward movement");
             playeranimator.SetBool("Running",true);
            // player.GetComponent<Rigidbody>().isKinematic=false;
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
            transform.eulerAngles=new Vector3(0,0,0);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) )
        {
            playeranimator.SetBool("Running",false);
            player.GetComponent<Rigidbody>().isKinematic=true;
        }
         if(Input.GetKey(KeyCode.S) && s==true){
             w=true;
          //  Debug.Log("Pressed S for forward movement");
             playeranimator.SetBool("Running",true);
             //player.GetComponent<Rigidbody>().isKinematic=false;
          //  transform.Translate(Vector3.back*5*Time.deltaTime);
            transform.eulerAngles=new Vector3(0,-180,0);
             transform.Translate(Vector3.forward*speed*Time.deltaTime);
           // plyr.GetComponent<Animator>().Play("Running");
        }
         if(Input.GetKey(KeyCode.A) && a==true){
             w=true;
          //  Debug.Log("Pressed A for forward movement");
             playeranimator.SetBool("Running",true);
             //player.GetComponent<Rigidbody>().isKinematic=false;
             transform.eulerAngles=new Vector3(0,-90,0);
              transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
         if(Input.GetKey(KeyCode.D) && d==true){
             w=true;
          //  Debug.Log("Pressed D for forward movement");
             playeranimator.SetBool("Running",true);
             //player.GetComponent<Rigidbody>().isKinematic=false;
          //  transform.Translate(Vector3.right*5*Time.deltaTime);
             transform.eulerAngles=new Vector3(0,90,0);
              transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
    }
    public void OnTriggerEnter(Collider col){
         if(col.gameObject.tag=="permisionplayer"){
            Debug.Log("collided permision player");
            w=true;
        }
        if(col.gameObject.tag=="AIbridgeexit"){
           gm.destination=true;
           playeranimator.SetBool("Dance",true);
           playeranimator.SetBool("Running",false);
           transform.eulerAngles=new Vector3(0,180,0);
           transform.position=destinationpoint.transform.position;
           movementbool=false;
           
        }
        if(col.gameObject.CompareTag("playerbrick") && stack.Count<=5){
        //    Debug.Log("Collided with bricks");
        //if(stack.Count<6){
            Instantiate(brickpickup,transform.position,brickpickup.transform.rotation);
            brickpickup.Play();
            Destroy(col.gameObject);
            GameObject child=Instantiate(sm.playerbricks,brickgoingposition.transform.position+direction,transform.rotation);
           stack.Push(child);
           child.transform.parent=player.transform;
           direction+=new Vector3(0,1.5f,0);
           scorecount+=1;
            score.text="Score :"+scorecount;
        //}
        }
            if (col.gameObject.CompareTag("truew") && stack==null)
            {
                Debug.Log("True w");
                movetoward=false;
                w=true;
                a=true;
                d=true;
            } 
            
            if (col.gameObject.CompareTag("check") && stack.Count>=1)
            {
                
                Debug.Log("check stack before move"+stack.Count);
                Debug.Log("Front chk");
                transform.position=  Vector3.MoveTowards(transform.position,destinationpoint.transform.position,5*Time.deltaTime);
                movetoward=true;
                Debug.Log("chk movetoward in collision"+movetoward);
                w=true;
                a=true;
                d=true;
                }
            if(col.gameObject.CompareTag("check") && stack.Count==0) {
                Debug.Log(stack+"chk a"+a);
                a=true;
                s=true;
                w=true;
                d=true;
            }
            
            if(col.gameObject.tag=="off"){
                a=false;
                d=false;
            }
            if (col.gameObject.tag=="lastbridgebrick" && stack!=null)
            {
                 col.gameObject.GetComponent<Renderer>().material=brdgeclr; 
                 GameObject temp=stack.Pop();
                 Destroy(temp); 
                 direction-=new Vector3(0,1.5f,0);
                 w=true;
            }
            
           

    }
    public void OnCollisionEnter(Collision col){
        if (col.gameObject.tag=="check")
        {
            Debug.Log("collided with permission collider");
            
            w=false;
        }
        if(col.gameObject.CompareTag("bridge")){
            if(stack.Count==0){
            a=false;
            d=false;
            w=false;
            }
           // Debug.Log("collided");
           if(stack!=null){
               
              // movetoward=true;
               
            //w=true;    
            GameObject temp=stack.Pop();
            Destroy(temp);
            direction-=new Vector3(0,1.5f,0);
             col.gameObject.GetComponent<Renderer>().material=brdgeclr;  
           // col.gameObject.GetComponent<Collider>().enabled=false;
            col.gameObject.GetComponent<Collider>().isTrigger=true;

           }
            if(stack.Count==0){
                w=false;
                Rigidbody rb=GetComponent<Rigidbody>();
                movementbool=true;
                movetoward=false;
                
            }
        }
       
    }
    
}
