using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;
    private Vector2 mousePos;

    [SerializeField] private float shootCooldown;
    private float setShootCooldown;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private GameObject[] hearts;

    [SerializeField] private int playerHealth;
    private int setPlayerHealth;
    private int heartCount;

    GameObject bullet;
    public GameObject summonPrefab;
    public float eSpawnTime;

    public GameObject e1Prefab;

    private Vector2 screenBounds;
    // private float objectWidth;
    // private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnWave());
        setPlayerHealth = playerHealth;
        setShootCooldown = shootCooldown;
        heartCount = hearts.Length;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        // objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        // objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
          playerHealth -= 1;
          hearts[heartCount - 1].SetActive(false);

          if(heartCount > 0) { // hearts.length - 1;
            heartCount -= 1;
          }

        }
    }

    void sSpawn(GameObject gObj) {
        GameObject summon = Instantiate(gObj) as GameObject;
        summon.transform.position = transform.position;
    }

    void Update()
    {

        if(heartCount <= 0) {
            // deathCanvas.SetActive(true);
            // bool playSounds = false; // check for sounds later...
            // GameObject[] projectiles = GameObject.FindGameObjectsWithTag("ball");
            // for(int i = 0; i < projectiles.Length; i++) {
            //     Destroy(projectiles[i]);
            // }
        }   

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if(moveInput.x > 0) {
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        if(moveInput.x < 0) {
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }       

       if (Input.GetKeyDown(KeyCode.Space)) {
           sSpawn(summonPrefab);
       }

        shootCooldown -= Time.deltaTime;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0) && shootCooldown <= 0) {
            bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = transform.position;
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // -90f
            bullet.GetComponent<Rigidbody2D>().rotation = angle; // +90f
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            GameObject.Destroy(bullet, 2f);

            shootCooldown = setShootCooldown;

        }

        // dash End

       // if space pressed: new array get components of all tagged enemy (e1behaviour)'s check each 1 making it 0 each itteration undo all at
    }
    void FixedUpdate() {
        shootCooldown -= Time.deltaTime;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }

    // void LateUpdate(){
    //     Vector3 viewPos = transform.position;
    //     viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
    //     viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
    //     transform.position = viewPos;
    // }


    // public void restartButton() {
    //     for(int i = 0; i < hearts.Length; i++) {
    //         hearts[i].SetActive(true);
    //     }
    //     heartCount = hearts.Length;
    //     playerHealth = hearts.Length;
    // }

    public void doSpawn() {
       GameObject enemy = Instantiate(e1Prefab) as GameObject;
        enemy.transform.position = new Vector2(Random.Range(screenBounds.x + 10f, screenBounds.x - 10f), Random.Range(screenBounds.y + 10f, screenBounds.y - 10f));
    }

    public IEnumerator spawnWave() { 
        while(true) {
            yield return new WaitForSeconds(eSpawnTime);
            doSpawn();
        }
    }







}
