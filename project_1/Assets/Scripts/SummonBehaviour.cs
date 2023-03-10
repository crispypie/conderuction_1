using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBehaviour : MonoBehaviour
{

    public float enemySpeed;
    public Rigidbody2D enemyrb;
    private Transform playerPos;

    public GameObject eSlashObj;

    private float eSlashCountDown;
    public float eSlashDuration;

    GameObject[] targets;
    Vector3 pos;
    float dist;
    Vector2 targ;

    public int health;

    // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "EnemyWeapon") {
            health -= 1;
        }
    }

    void Start()
    {
        StartCoroutine(spawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Destroy(this.gameObject);
        }

        if(eSlashCountDown > 0) {
            eSlashCountDown -= Time.deltaTime;
        }

        else {
            eSlashObj.SetActive(false);
        }
        
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        // else follow player
        pos = this.transform.position;
        dist = float.PositiveInfinity;

        if(targets.Length != 0) {
            foreach(var obj in targets)
            {   
                var d = (pos - obj.transform.position).sqrMagnitude;
                if(d < dist)
                {
                    Debug.Log("yes if");
                    targ = obj.transform.position;
                    dist = d;
                }
            }
        }

        float step = enemySpeed * Time.deltaTime;
        if(targets.Length != 0) {
            Debug.Log("hi");
            transform.position = Vector2.MoveTowards(transform.position, targ, step);
        }
    }

    void doSlash() {
        if(targets.Length != 0) {
            eSlashObj.SetActive(true);
            Vector2 slashDir = (Vector2)pos - (Vector2)targ;
            float angle = Mathf.Atan2(slashDir.y, slashDir.x) * Mathf.Rad2Deg;
            eSlashObj.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle - 180));

            eSlashCountDown = eSlashDuration;
        }
    }

    public IEnumerator spawnWave() { 
        while(true) {
            yield return new WaitForSeconds(1);
            doSlash();
        }
    }

    

}
