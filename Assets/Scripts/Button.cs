using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Renderer floor;
    public Renderer[] oldWalls;
    public Renderer[] newFloors;
    public Renderer[] newWalls;
    public Light[] newLights;
    public Light oldLight;
    public Renderer button;
    public float seconds;
    public Material Material1;
    public Vector3 toScale;
    public Vector3 toScaleFloor;


    public void Start(){
         foreach (Light light in newLights){
            light.gameObject.SetActive(false);
         }
         foreach (Renderer floor in newFloors){
            floor.gameObject.SetActive(false);
         }
         foreach (Renderer wall in newWalls){
             wall.gameObject.SetActive(false);
         }
    }

    IEnumerator ScaleDownAnimation(float tottime)
        {
            float i = 0;
            float rate = 1 / tottime;

            Vector3 fromScale = transform.localScale;
            while (i<1)
            {
                i += Time.deltaTime * rate;
                transform.localScale = Vector3.Lerp(fromScale, toScale, i);
                yield return 0;
            }
        }

    IEnumerator waitAndRemove(float time)
        {
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            // start sleep
            yield return new WaitForSeconds(time);

            // change the floor material
            floor.material = Material1;

            // here we deactivate the walls
            foreach (Renderer wall in oldWalls){
                wall.gameObject.SetActive(false);
            }

            // deativating the button
            button.gameObject.SetActive(false);

            // activating new walls
            foreach (Renderer wall in newWalls){
                wall.gameObject.SetActive(true);
            }

            // activating new floor
            foreach (Renderer floor in newFloors){
                floor.gameObject.SetActive(true);
            }

            // activating new lights
            foreach (Light light in newLights){
                light.gameObject.SetActive(true);
            }

            //disabling old light
            oldLight.gameObject.SetActive(false);

            Debug.Log("Finished Coroutine at timestamp : " + Time.time);

        }

    public void Activate(){
       Debug.Log("Button received the input");

       StartCoroutine(ScaleDownAnimation(1.0f));
       StartCoroutine(waitAndRemove(seconds));

    }
}
