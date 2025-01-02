using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraFollow : MonoBehaviour
{ 
    public GameObject PlayerSprite;
    private float xVelocity; // this is for the smoothdamp
    public int offset;


    public int LivesLeft = 3;
    public int ScorePoint = 0;
    //public Text Lives;
    //public Text Score;

    private Vector3 vel = Vector3.zero;



    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (PlayerSprite)
        {
            Vector3 playerPosition = PlayerSprite.transform.position;
            Vector3 cameraPosition = transform.position;

            cameraPosition.x = Mathf.SmoothDamp(cameraPosition.x, playerPosition.x, ref xVelocity, 0.1f);
            cameraPosition.y = Mathf.SmoothDamp(cameraPosition.y, playerPosition.y + offset, ref xVelocity, 0.1f);

            transform.position = cameraPosition;
        }

    }
}