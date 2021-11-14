using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent playGame;
    Rigidbody rb;
    public float speed = 5f;
    private float sensitivity = 3f;
    public float currentCamRotate;
    private float camRotateLimit = 90f;
    private bool isShoot = false;
    private Transform selectZone;
    private int score = 0;
    public int i;
    [SerializeField] GameObject Gun;
    [SerializeField] Rigidbody[] Bullets;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Text checkText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject crossBar;
    [SerializeField] Image bulletCheck;
    [SerializeField] Image bulletColor;
    RaycastHit hit;
    private float checkRange = 2f;

    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        Gun.SetActive(isShoot);
        crossBar.SetActive(isShoot);
        scoreText.gameObject.SetActive(isShoot);
        bulletColor.gameObject.SetActive(isShoot);
    }

    void Update()
    {
        Gun.SetActive(isShoot);
        crossBar.SetActive(isShoot);
        scoreText.gameObject.SetActive(isShoot);
        bulletColor.gameObject.SetActive(isShoot);
        if(!isShoot)
        {
            Move();
            BodyRotate();
            CamRotate();
            ControllSensitivity();
            CanShoot();
        }
        if(isShoot)
        {
            BodyRotate();
            CamRotate();
            Shoot();
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveX = transform.right * h;
        Vector3 moveZ = transform.forward * v;
        Vector3 velocity = (moveX + moveZ).normalized * speed;

        rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    void BodyRotate()
    {
        float X = Input.GetAxis("Mouse X");
        Vector3 rotation = new Vector3(0f, X, 0f) * sensitivity;
        rb.MoveRotation(rb.rotation *  Quaternion.Euler(rotation));
    }

    void CamRotate()
    {
        float Y = Input.GetAxis("Mouse Y");
        float camRotate = Y * sensitivity;
        currentCamRotate -= camRotate;
        currentCamRotate = Mathf.Clamp(currentCamRotate, -camRotateLimit, camRotateLimit);
        Camera.main.transform.localEulerAngles = new Vector3(currentCamRotate, 0f, 0f);
    }

    void ControllSensitivity()
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            sensitivity -= 1;
        }
        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            sensitivity += 1;
        }
    }

    void CanShoot()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, checkRange, layerMask) && !isShoot)
        {
            if(hit.transform.tag == "Zone")
            {
                checkText.gameObject.SetActive(true);
                checkText.text = "게임에 참가하시려면 F를 누르세요";
                if(Input.GetKeyDown(KeyCode.F))
                {
                    checkText.gameObject.SetActive(false);
                    selectZone = hit.transform;
                    isShoot = true;
                }
            }
        }
        else
        {
            checkText.gameObject.SetActive(false);
        }
    }

    void Shoot()
    {
        transform.position = selectZone.position;
        /*Gun.SetActive(true);
        crossBar.SetActive(true);
        scoreText.gameObject.SetActive(true);
        bulletColor.gameObject.SetActive(true);*/
        Gun.transform.localRotation = Quaternion.Euler(new Vector3(0f, -90f, -currentCamRotate));
        playGame.Invoke();
        Play();
        SelectBullet();
    }

    void Play()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Rigidbody shootBullet = (Rigidbody)Instantiate(Bullets[i], Camera.main.transform.position, Camera.main.transform.rotation);
            shootBullet.AddForce(Camera.main.transform.forward * 100f, ForceMode.Impulse);
        }
    }

    void SelectBullet()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            i = 0;
            bulletCheck.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 110);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            i = 1;
            bulletCheck.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            i = 2;
            bulletCheck.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -110);
        }
    }
    public void GetScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameEnd()
    {
        isShoot = false;
    }
}
