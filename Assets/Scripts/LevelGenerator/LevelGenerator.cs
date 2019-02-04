using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

  public enum Action { FLAT_HOR, UP_SLOPE, DOWN_SLOPE, SIZE, NONE }
  public struct MinMax {
    public int min, max;
    public MinMax(int min, int max) { this.min = min; this.max = max; }
  }
  public struct SequentialAction {
    public Action prev, curr;
    public SequentialAction(Action prev, Action curr) { this.prev = prev; this.curr = curr; }
  }


  GameObject surfaceLevel;

  SequentialAction[] forbidenSequentialActions = new SequentialAction[] {
    new SequentialAction(Action.UP_SLOPE, Action.DOWN_SLOPE),
    new SequentialAction(Action.DOWN_SLOPE, Action.UP_SLOPE)
  };


  void Awake () {
    // Create surface level child object
    surfaceLevel = new GameObject("SurfaceLevel", typeof(PolygonCollider2D));
    surfaceLevel.transform.SetParent(gameObject.transform);
    surfaceLevel.layer = 10;
  }

  void Start() {

    // Generate vertices
    Vector2[] vertices2D = GenerateSurfaceVertices(transform.position, 1000, new MinMax(4, 8), new MinMax(10, 45));
    // Apply vertices to polygon collider
    surfaceLevel.GetComponent<PolygonCollider2D>().points = vertices2D;
    // Apply vertices to mesh
    CreateSurfaceMesh(vertices2D);
  }
  
  void Update () {

  }

  Vector2[] GenerateSurfaceVertices(Vector2 startVertex, int numOfActions, MinMax surfaceLengthLimit, MinMax surfaceAngleLimit)
  {
    List<Vector2> vertices2D = new List<Vector2>() { startVertex };
    Action prevAction = Action.NONE;

    // Each iteration
    for (int i = 0; i < numOfActions; ++i) {

      // Describes if current iteration will be for the first Action. If It is true that means its previous action does not exist yet.
      bool isFirstAction = (i == 0) ? true : false;

      // Generate next action and if it is invalid then generate another one
      Action curAction;
      do { curAction = (Action)Random.Range(0, (int)Action.SIZE); }
      while (!IsNextActionValid(prevAction, curAction));

      // Generate surface vector length
      float length = Random.Range(surfaceLengthLimit.min, surfaceLengthLimit.max + 1);
      Vector2 surfaceVector = Vector2.right * length;

      // Rotate surface vector if it is not horizontal one
      int angle = 0;
      angle = (curAction == Action.UP_SLOPE) ? Random.Range(surfaceAngleLimit.min, surfaceAngleLimit.max + 1) : angle;
      angle = (curAction == Action.DOWN_SLOPE) ? -Random.Range(surfaceAngleLimit.min, surfaceAngleLimit.max + 1) : angle;
      surfaceVector = Quaternion.Euler(0, 0, angle) * surfaceVector;

      // If prev and new vertex both lie on same horizontal line(prev and cur action are both FLAT_HOR) ->  Set prev vertex to be new vertex and don't add new vertex to list(prev vertex becomes new vertex). 
      //                                                                                                    Since there was no NEW action repeat this loop iteration by reducing counter by 1(i--).
      // Else -> Just add new vertex to list.
      Vector2 prevVertex = vertices2D[i];
      Vector2 newVertex = prevVertex + surfaceVector;
      if (curAction == Action.FLAT_HOR && prevAction == curAction) {
        vertices2D[i] = newVertex;
        i--;
      }
      else
        vertices2D.Add(newVertex);

      // save current action so it can be used in next iteration for comparison
      prevAction = curAction;
    }

    // Generated top surface must be closed shape in order for mesh to be rendered.
    // Generated 'vertices2D' are now top part of our shape. We need to close it from right, bottom and left
    float minY = vertices2D[0].y;
    foreach(Vector2 vertex in vertices2D) {
      minY = (vertex.y < minY) ? vertex.y : minY;
    }
    vertices2D.Add(new Vector2(vertices2D[vertices2D.Count - 1].x, minY - 1));
    vertices2D.Add(new Vector2(vertices2D[0].x, minY - 1));


    return vertices2D.ToArray();
  }

  bool IsNextActionValid(Action prev, Action cur)
  {
    foreach(SequentialAction seqAction in forbidenSequentialActions) {
      if (prev == seqAction.prev && cur == seqAction.curr)
        return false;
    }
    return true;
  }

  void CreateSurfaceMesh(Vector2[] vertices2D)
  {
    // Use the triangulator to get indices for creating triangles
    Triangulator tr = new Triangulator(vertices2D);
    int[] indices = tr.Triangulate();

    // Create the Vector3 vertices
    Vector3[] vertices = new Vector3[vertices2D.Length];
    for (int i = 0; i < vertices.Length; i++)
    {
      vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
    }

    // Create the mesh
    Mesh msh = new Mesh();
    msh.vertices = vertices;
    msh.triangles = indices;
    msh.RecalculateNormals();
    msh.RecalculateBounds();

    // Set up game object with mesh;
    surfaceLevel.AddComponent(typeof(MeshRenderer));
    MeshFilter filter = surfaceLevel.AddComponent(typeof(MeshFilter)) as MeshFilter;
    filter.mesh = msh;
  }


}
