using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject winTextObjectEE;
    public GameObject buttonNotAval;
    public GameObject question;
    public Renderer floor;
    public Material Material1;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public Renderer[] newWalls;
    public Renderer[] newGnd;

    private int points;
    private int buttonTimes;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach (Renderer wall in newWalls){
            wall.gameObject.SetActive(false);
        }
        foreach (Renderer floor in newGnd){
            floor.gameObject.SetActive(false);
        }
        points = 0;
        SetCountText ();
        winTextObject.SetActive(false);
        buttonNotAval.SetActive(false);
        question.SetActive(false);
        buttonTimes = 0;
        winTextObjectEE.SetActive(false);
    }
    void SetCountText()
     	{
     		countText.text = "Count: " + points.ToString();

     	}

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    IEnumerator wait(float time, int option)
            {
                Debug.Log("Started Coroutine at timestamp : " + Time.time);

                // start sleep
                yield return new WaitForSeconds(time/2);

                if (option == 1){
                    buttonNotAval.SetActive(false);
                }
                else if (option == 2){
                     winTextObject.SetActive(false);
                     question.SetActive(true);
                     yield return new WaitForSeconds(time/2);
                     question.SetActive(false);
                }
                else if (option == 3){
                     winTextObjectEE.SetActive(true);
                }
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);

            }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            points = points + 1;
            Debug.Log("points!");
            SetCountText();
        }
        if (other.gameObject.CompareTag("Button"))
        {
            if (points < 8){
                buttonNotAval.SetActive(true);
                StartCoroutine(wait(3,1));
                if(buttonTimes >= 4){
                    floor.material = Material1;
                }
                else{
                    buttonTimes += 1;
                }
            }
            else{
                winTextObject.SetActive(true);
                StartCoroutine(wait(3,2));

                other.gameObject.BroadcastMessage("Activate");
                Debug.Log("Going to button");
            }
        }
        if (other.gameObject.CompareTag("ButtonEnd"))
                {
                    winTextObjectEE.SetActive(true);
                    StartCoroutine(wait(6,3));
                }
    }
}
