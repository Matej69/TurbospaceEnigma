using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {

  private static CameraController instance;
  
  public GameObject objToFollow;
  public float followPlayerSpeed;
  
  float shakeMagnitude;
  Timer timer_shakeTime;
  Timer timer_shakeEvery;
  int lastShakeDir = 1;
  

  private Coroutine ref_cameraZoomCorutine;


  void Awake()
	{
    instance = this;
    timer_shakeTime = new Timer(0.2f);
    timer_shakeEvery = new Timer(0.05f);
    EventManager.event_sceneChanged.AddListener(OnSceneChange);
    EventManager.event_spaceshipLanded.AddListener(OnSpaceshipLanded);
  }

  private void Start()
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
    if (!timer_shakeTime.IsFinished() && timer_shakeEvery.IsFinished()) {
      transform.position = new Vector3(transform.position.x + (shakeMagnitude * lastShakeDir), transform.position.y, transform.position.z);
      lastShakeDir *= -1;
      timer_shakeEvery.Reset();
    }
  }
  
  private void HandleCameraPosition() {
    if (objToFollow != null) {
      Vector3 newPos = Vector2.Lerp(transform.position, objToFollow.transform.position, followPlayerSpeed * Time.deltaTime);
      newPos.z = -10;
      transform.position = newPos;
    }
  }

  public void SetGameObjectToFollow(GameObject obj) {
    objToFollow = obj;
  }

  
  private IEnumerator StartCameraZoomCoroutine(float targetZoom, float zoomSpeed) {
    float startZoom = Camera.main.orthographicSize;
    float currentZoom = startZoom;
    float zoomProgress = 0;

    // Seed value for easy in, easy out function. This function will take incremential seed and generate propper zoomProgress value for that seed. This makes our zoomProgress number increment slow, keep growing at faster rate, decrement slow.
    // For seed values from 0-1 it will generate propper value of zoomProgress from 0-1. WHen seed=1.0f -> zoomProgress=1.0f and currentZoom=targetZoom which means that zooming is completed.
    float seed = 0;
    while (currentZoom != targetZoom) {
      // Seed is being incremented but Lerp is being used if seed is greater then 1 it becomes 1
      seed = Mathf.Lerp(0, 1, seed += zoomSpeed * Time.deltaTime);
      zoomProgress = Mathf.Pow(seed, 2) / (Mathf.Pow(seed, 2) + Mathf.Pow(seed - 1, 2));
      currentZoom = Mathf.Lerp(startZoom, targetZoom, zoomProgress);
      Camera.main.orthographicSize = currentZoom;
      yield return null;
    }
  }

  public void StartCameraZoom(float targetZoom, float zoomSpeed) {
    if (ref_cameraZoomCorutine != null)
      StopCoroutine(ref_cameraZoomCorutine);
    ref_cameraZoomCorutine = StartCoroutine(StartCameraZoomCoroutine(targetZoom, zoomSpeed));
  }

  public void SetCameraSize(float camSize) {
    Camera.main.orthographicSize = camSize;
  }

  private void OnSceneChange(SceneManager.e_sceneID sceneID) { 
    if (sceneID == SceneManager.e_sceneID.IN_SPACE) {
      objToFollow = GameObject.FindGameObjectWithTag("Spaceship");
      SetCameraSize(3);
      StartCameraZoom(6, 0.2f);
    }
    else {
      objToFollow = GameObject.FindGameObjectWithTag("SpaceshipOnPlanet");
      SetCameraSize(15);
      StartCameraZoom(6, 0.45f);
    }
  }

  private void OnSpaceshipLanded() {
    objToFollow = GameObject.FindWithTag("PlayerOnPlanet");
  }


}
