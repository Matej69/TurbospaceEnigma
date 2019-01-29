using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {

    private static CameraController instance;

    public GameObject playerObj;
    public float followPlayerSpeed;

    float shakeMagnitude;
    Timer timer_shakeTime;
    Timer timer_shakeEvery;
    int lastShakeDir = 1;

   


	void Awake()
	{
        instance = this;
        timer_shakeTime = new Timer(0.2f);
        timer_shakeEvery = new Timer(0.05f);
    }
	
	void Start () 
	{
	}

	void Update () 
	{
        HandleCameraPosition();
        HandleShake();
    }




    public static void Shake(float _magnitude, float _shakingTime, float _shakeEveryXSec)
    {
        instance.shakeMagnitude = _magnitude;
        instance.timer_shakeTime.SetCurrentTime(_shakingTime);
        instance.timer_shakeEvery.SetStartTime(_shakeEveryXSec);
        instance.timer_shakeEvery.SetCurrentTime(0f);
    }

    private void HandleShake()
    {
        timer_shakeTime.Tick(Time.deltaTime);
        timer_shakeEvery.Tick(Time.deltaTime);
        if (!timer_shakeTime.IsFinished() && timer_shakeEvery.IsFinished())
        {
            transform.position = new Vector3(transform.position.x + (shakeMagnitude * lastShakeDir), transform.position.y, transform.position.z);
            lastShakeDir *= -1;
            timer_shakeEvery.Reset();
        }
    }


    private void HandleCameraPosition()
    {
        Vector3 newPos = Vector2.Lerp(transform.position, playerObj.transform.position, followPlayerSpeed * Time.deltaTime);
        //newPos.y = newPos.y + 0.15f;
        newPos.z = -10;
        transform.position = newPos;
    }



}
