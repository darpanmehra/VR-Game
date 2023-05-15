using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectMovement : MonoBehaviour
{
    bool keepMoving = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (keepMoving)
        {
            float speed = LevelManager.Instance.getSpeed();
            float step = speed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, -15);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position.z <= -15)
            {
                Destroy(gameObject);
            }
        }


    }

    public void setKeepMoving(bool movement)
    {
        keepMoving = movement;
    }


}
