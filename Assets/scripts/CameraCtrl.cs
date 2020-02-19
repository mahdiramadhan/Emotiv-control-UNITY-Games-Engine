using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Emotiv;

public class CameraCtrl : MonoBehaviour {
    public GameObject player;
    private Vector3 offset;
    private Vector3 defaultOffset;
    
    public Text UserMessage;

    /*
	 * Get the offset distance between the camera and the ball for a future use
	*/
    void Start () {
        offset = transform.position - player.transform.position;
        defaultOffset = offset;
	}

	
	/*
	 * LateUpdate happens after the ball is drawn in Update.
	 * Here is when we move the camera position using the offset distance and 
	 * fix the camera orientation using the gyroscope coordinates
	*/
	void LateUpdate () {
        float move = Input.GetAxis("Vertical");
        if(move != 0)
        {
            Vector3 movement = new Vector3(0.0f, 0.0f, move);
            transform.position = player.transform.position + offset - (movement / 10);
            offset = transform.position - player.transform.position;
            ApplyAction(offset);
        } else
        {
            transform.position = player.transform.position + defaultOffset;
            offset = transform.position - player.transform.position;
        }
    }

    void ApplyAction(Vector3 offset) {
        if (offset.z <= -25.0f)
        {
            UserMessage.text = "External request system applied!";            
        } else
        {
            UserMessage.text = "Internal request system applied!";
        }
    }
}
