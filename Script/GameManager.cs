using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text title;
    public Button start,pause,resume,restart,setting;
    public Slider slider;
    public GameObject pannel;
    public PlayerController playerController;
    public bool destination=false;
    public GameObject bomb;
    private GameObject spawnbombclone;
    public ParticleSystem bombpartical;  
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale=0;
       // Time.timeScale=1;
      //   InvokeRepeating("BombSpawn",3,8);
    }

    // Update is called once per frame
    void Update()
    {
        if(destination==true){
           StartCoroutine("ArrivedDestinatoin");
        }
    }
    private  IEnumerator ArrivedDestinatoin(){
           yield return new WaitForSeconds(10);
           Time.timeScale=0;
           restart.gameObject.SetActive(true);
    }
    public void Pause(){
       resume.gameObject.SetActive(true);
       restart.gameObject.SetActive(true);
       pause.gameObject.SetActive(false);
       setting.gameObject.SetActive(true);
       Time.timeScale=0;
    }
    public void StartGame(){
        pannel.SetActive(false);
        title.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
        Time.timeScale=1;
        slider.gameObject.SetActive(false);
        playerController.score.text="Score :";

    }
    public void Resume(){
       resume.gameObject.SetActive(false);
       restart.gameObject.SetActive(false);
       pause.gameObject.SetActive(true);
       setting.gameObject.SetActive(false);
       Time.timeScale=1;
    }
    public void Restart(){
       SceneManager.LoadScene(0);
    }
    public void Setting(){
       setting.gameObject.SetActive(false);
       slider.gameObject.SetActive(true);
    }
    public void BombSpawn(){
       spawnbombclone=Instantiate(bomb,new Vector3(Random.Range(-10,28),1f,Random.Range(12,-12)),Quaternion.identity);
       StartCoroutine("Exploded");
    }
    public  IEnumerator Exploded(){
       yield return new WaitForSeconds(4);
      DestroyBomb();
    } 
    public void DestroyBomb(){
        
         Instantiate(bombpartical,spawnbombclone.transform.position,spawnbombclone.transform.rotation);
            bombpartical.Play();
            Collider[] colliders=Physics.OverlapSphere(transform.position,50);
            foreach(Collider nearby in colliders){
            Rigidbody rb=nearby.GetComponent<Rigidbody>();
            if(rb!=null){
                rb.AddExplosionForce(100,transform.position,50);
            }
            }
            Destroy(spawnbombclone);
    }
}
