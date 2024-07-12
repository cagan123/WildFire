using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Waypoint))]

[ExecuteInEditMode]
public class Tree_Generator : MonoBehaviour
{
    public GameObject treePrefab;  // The tree prefab to instantiate
    private List<Vector2> borderPoints;  // The points that define the border of the area
    public float treeSpacing = 1.0f;  // The spacing between trees
    public int thickness = 3;  // Number of trees next to each other for thickness
    public float thicknessSpacing = 0.5f;  // Spacing between rows of trees for thickness
    Waypoint waypoint;
    private int pointIndex;

    void Awake(){
        waypoint = GetComponent<Waypoint>();
    }
    void Start()
    {
        borderPoints = ConvertVector3ArrayToVector2List(waypoint.Points);
        
        if (treePrefab == null || borderPoints == null || borderPoints.Count < 2)
        {
            return;
        }

        GenerateTrees();
    }
    public List<Vector2> ConvertVector3ArrayToVector2List(Vector3[] vector3Array)
    {
        List<Vector2> vector2List = new List<Vector2>();

        foreach (Vector3 vector3 in vector3Array)
        {
            vector2List.Add(new Vector2(vector3.x, vector3.y));
        }

        return vector2List;
    }

    void GenerateTrees()
    {
        
        DestroyAllChildrenObjects(this.transform);
        for (int i = 0; i < borderPoints.Count; i++)
        {
            Vector2 startPoint = borderPoints[i];
            Vector2 endPoint = borderPoints[(i + 1) % borderPoints.Count];  // Loop back to the first point

            PlaceTreesAlongLineWithThickness(startPoint, endPoint);
        }
    }

    void DestroyAllChildrenObjects(Transform parent)
    {
        // Loop through all children of the parent transform
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            // Destroy the child GameObject
            DestroyImmediate(parent.GetChild(i).gameObject);
        }
    }

    void PlaceTreesAlongLineWithThickness(Vector2 startPoint, Vector2 endPoint)
    {
        Vector2 direction = (endPoint - startPoint).normalized;
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);  // Perpendicular vector for thickness

        for (int t = 0; t < thickness; t++)
        {
            Vector2 offsetStart = startPoint + perpendicular * t * thicknessSpacing;
            Vector2 offsetEnd = endPoint + perpendicular * t * thicknessSpacing;

            PlaceTreesAlongLine(offsetStart, offsetEnd);
        }
    }

    void PlaceTreesAlongLine(Vector2 startPoint, Vector2 endPoint)
    {
        float distance = Vector2.Distance(startPoint, endPoint);
        int numberOfTrees = Mathf.CeilToInt(distance / treeSpacing);

        for (int i = 0; i <= numberOfTrees; i++)
        {
            float t = i / (float)numberOfTrees;
            Vector2 position = Vector2.Lerp(startPoint, endPoint, t);
            GameObject tree = Instantiate(treePrefab, new Vector3(position.x, position.y, 0), Quaternion.identity, transform);
        }
    }

    private void OnDrawGizmos()
    {
        if (borderPoints == null || borderPoints.Count < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < borderPoints.Count; i++)
        {
            Vector2 startPoint = borderPoints[i];
            Vector2 endPoint = borderPoints[(i + 1) % borderPoints.Count];
            Gizmos.DrawLine(startPoint, endPoint);
        }

        // Visualize thickness
        if (thickness > 1)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < borderPoints.Count; i++)
            {
                Vector2 startPoint = borderPoints[i];
                Vector2 endPoint = borderPoints[(i + 1) % borderPoints.Count];
                Vector2 direction = (endPoint - startPoint).normalized;
                Vector2 perpendicular = new Vector2(-direction.y, direction.x);

                for (int t = 0; t < thickness; t++)
                {
                    Vector2 offsetStart = startPoint + perpendicular * t * thicknessSpacing;
                    Vector2 offsetEnd = endPoint + perpendicular * t * thicknessSpacing;
                    Gizmos.DrawLine(offsetStart, offsetEnd);
                }
            }
        }
    }
    private void UpdateNextPosition(){
		pointIndex ++;
		if(pointIndex > waypoint.Points.Length - 1){
			pointIndex = 0;
		}
	}
	private Vector3 GetCurrnetPosition(){
		return waypoint.GetPosition(pointIndex);
	}
}
