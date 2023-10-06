using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class PlayerController : BaseController
{
    private Rigidbody myBody;

    public Transform bulletStartPoint;
    public GameObject bulletPrefab;
    public ParticleSystem shootFX;

    private Animator shootSliderAnim;

    [HideInInspector] public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();

        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();

        GameObject.Find("ShootBtn").GetComponent<Button>().onClick.AddListener(ShootingControl);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
        ChangeRotation();

        
    }

    private void FixedUpdate()
    {
        MoveTank();
    }

    private void MoveTank()
    {
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
    }

    private void ControlMovement()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
        {
            MoveLeft();
        }

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();            
        }

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight();
        }

        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveNormal();
        }
    }

    private void ChangeRotation()
    {
        if(speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if(speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl()
    {
        if (Time.timeScale != 0)
        {
            if (canShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletStartPoint.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                shootFX.Play();

                canShoot = false;
                shootSliderAnim.Play("Fill");
            }
        }
        
    }
}
