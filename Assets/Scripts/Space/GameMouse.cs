using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMouse : MonoBehaviour {

    private static GameMouse ref_gameMouse;
    static public GameMouse GetRefrence() { return ref_gameMouse; }

    [HideInInspector]
    public GameObject lastHoveredPlanet;
    [HideInInspector]
    public bool isMouseOnAnyPlanet;

    public enum MOUSE_STATE { NORMAL, INTERACTIVE, WAITING }
    [HideInInspector]
    public MOUSE_STATE mouseState;
    private bool mouseStateLocked = false;
    Dictionary<MOUSE_STATE, Sprite> mouseSpriteMap = new Dictionary<MOUSE_STATE, Sprite>();

    private void Awake()
    {
        ref_gameMouse = this;
    }

    // Use this for initialization
    void Start ()
    {
        LoadMouseImgs();
        SetMouseSprite(MOUSE_STATE.NORMAL);
        Cursor.visible = false;
        EventManager.event_travelingStateChange.AddListener(OnSpaceshipTravelingFinished);
        EventManager.event_mouseOnPlanet.AddListener(MouseOnPlanetStateChange);
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMousePos();

    }



    void UpdateMousePos()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }


    void LoadMouseImgs()
    {
        mouseSpriteMap.Add(MOUSE_STATE.NORMAL, ResourceManager.GetSprite("Graphics/mouse/normal"));
        mouseSpriteMap.Add(MOUSE_STATE.INTERACTIVE, ResourceManager.GetSprite("Graphics/mouse/interactive"));
        mouseSpriteMap.Add(MOUSE_STATE.WAITING, ResourceManager.GetSprite("Graphics/mouse/waiting"));
    }

    public void SetMouseSprite(MOUSE_STATE spriteId)
    {
        GetComponent<SpriteRenderer>().sprite = mouseSpriteMap[spriteId];
        mouseState = spriteId;
    }

    public bool IsMouseOnAnyPlanet()
    {
        return isMouseOnAnyPlanet;
    }

  private void OnSpaceshipTravelingFinished(SpaceTravelerBehaviour.e_travelState state) {
    if (state == SpaceTravelerBehaviour.e_travelState.TRAVELING) {
      SetMouseSprite(MOUSE_STATE.WAITING);
      mouseStateLocked = true;
    }
    else {
      SetMouseSprite(MOUSE_STATE.NORMAL);
      mouseStateLocked = false;
    }
    Debug.Log(mouseStateLocked);
  }

  private void MouseOnPlanetStateChange(bool isMouseOnPlanet) {
    if (mouseStateLocked)
      return;
    SetMouseSprite( (isMouseOnPlanet) ? MOUSE_STATE.INTERACTIVE : MOUSE_STATE.NORMAL);
  }



}
                                                                                                                 