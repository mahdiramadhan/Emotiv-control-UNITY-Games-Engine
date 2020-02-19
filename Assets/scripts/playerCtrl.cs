using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Emotiv;

public class playerCtrl : MonoBehaviour {
    public Text CurrentMentalCommand;
    public Text UserMessage;

    public float speed, jumpConstant;
    public Camera cam;
    private Rigidbody rb;
    
    EmoEngine engine;

    /*
	 * This method handle the EmoEngine update event, 
	 * if the EmoState has the PUSH action, 
	 * a force is applied to the ball in the direction of the camara
	*/
    void engine_EmoStateUpdated(object sender, EmoStateUpdatedEventArgs e)
    {
        EmoState es = e.emoState;
        DefaultNoActionCondition();
        if (e.userId != 0)
            return;
        Debug.Log("Corrent action: " + es.MentalCommandGetCurrentAction().ToString());
        if (es.MentalCommandGetCurrentAction() == EdkDll.IEE_MentalCommandAction_t.MC_PUSH) {
            Vector3 movement = new Vector3(cam.transform.forward.x, cam.transform.forward.y, cam.transform.forward.z);
            Debug.Log("Push");
            Action(movement);
            CurrentMentalCommand.text = "Push command";
        }
    }

    /*
	 * Set the handler for update event
	*/
    void Start() {
        rb = GetComponent<Rigidbody>();
        EmoEngine.Instance.EmoStateUpdated += new EmoEngine.EmoStateUpdatedEventHandler(engine_EmoStateUpdated);
        DefaultNoActionCondition();
    }

    /*
     * You can also use the up arrow key to move the ball.
    */
    void FixedUpdate()
    {
        float moveZ = Input.GetAxis("Vertical");
        if (moveZ != 0)
        {
            CurrentMentalCommand.text = "Push command applied";
        } else
        {
            DefaultNoActionCondition();
        }

    }

    void DefaultNoActionCondition() {
        CurrentMentalCommand.text = "Nothing happened";
        UserMessage.text = "None!";
    }

    void Action(Vector3 ThisMovement) {
        /*
         * Experimental logic
         * -------------------
         * You can change this line of codes for experimental purpose.
         */

        float PosX = ThisMovement.x;    // Getting current object's x position            
        if (PosX != 0)
        {
            // Write your command or messsages here based on current object's x position
            UserMessage.text = "User need an attention! Waiting a couple of seconds for a response.";
            for (int i = 0; i < 360; i++) { }
            float tmpPosX = PosX;   // Saving temporary variable for latest x position
            if (PosX == tmpPosX && PosX <= tmpPosX + 50.0f)
            {
                UserMessage.text = "Entering Yes and No response system. Waiting a couple of seconds for a response";
                float tmpTmpPosX = PosX;    // Saving temporary variable for latest x position
                if (PosX >= tmpTmpPosX + 50.0f)
                {
                    UserMessage.text = "Yes";
                }
                else
                {
                    UserMessage.text = "No";
                }
            }
            else
            {
                UserMessage.text = "Nothing";
            }
        }
        else
        {
            UserMessage.text = "Nothing";
        }
    }

}
