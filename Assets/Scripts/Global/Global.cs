using UnityEngine;
using System.Collections;


public class Global : MonoBehaviour {

    static float gravity = 5f;
    static public void ApplyGravity(ref Vector2 velocity) { velocity.y -= gravity * Time.deltaTime; }


}
