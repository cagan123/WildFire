using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Camera_Tree_Generator : MonoBehaviour
{
    public GameObject treePrefab;  // The tree prefab to instantiate
    public PolygonCollider2D borderCollider;  // The PolygonCollider2D to define the border
    public float treeSpacing = 1.0f;  // The spacing between trees
    public int thickness = 3;  // Number of trees next to each other for thickness
    public float thicknessSpacing = 0.5f;  // Spacing between rows of trees for thickness

    private List<GameObject> instantiatedTrees = new List<GameObject>();

    void Awake(){
        borderCollider = GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        if (treePrefab == null || borderCollider == null)
        {
            Debug.LogError("Tree prefab or border collider is not set correctly.");
            return;
        }

        GenerateTrees();
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

    void GenerateTrees()
    {        
        DestroyAllChildrenObjects(this.transform);
        Vector2[] points = borderCollider.points;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 startPoint = borderCollider.transform.TransformPoint(points[i]);
            Vector2 endPoint = borderCollider.transform.TransformPoint(points[(i + 1) % points.Length]);

            PlaceTreesAlongLineWithThickness(startPoint, endPoint);
        }
    }

    void ClearPreviousTrees()
    {
        foreach (var tree in instantiatedTrees)
        {
            if (tree != null)
            {
                DestroyImmediate(tree);
            }
        }
        instantiatedTrees.Clear();
    }

    void PlaceTreesAlongLineWithThickness(Vector2 startPoint, Vector2 endPoint)
    {
        Vector2 direction = (endPoint - startPoint).normalized;
        Vector2 perpendicular = new Vector2(direction.y, -direction.x);  // Perpendicular vector for thickness

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
            instantiatedTrees.Add(tree);
        }
    }

    private void OnDrawGizmos()
    {
        if (borderCollider == null) return;

        Vector2[] points = borderCollider.points;

        Gizmos.color = Color.green;
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 startPoint = borderCollider.transform.TransformPoint(points[i]);
            Vector2 endPoint = borderCollider.transform.TransformPoint(points[(i + 1) % points.Length]);
            Gizmos.DrawLine(startPoint, endPoint);
        }

        // Visualize thickness
        if (thickness > 1)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < points.Length; i++)
            {
                Vector2 startPoint = borderCollider.transform.TransformPoint(points[i]);
                Vector2 endPoint = borderCollider.transform.TransformPoint(points[(i + 1) % points.Length]);
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
}
