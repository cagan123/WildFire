using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Waypoint))]
public class Waypoint_Editor : Editor
{
    private Waypoint waypointTarget => target as Waypoint;

    private void OnSceneGUI(){
        if(waypointTarget.Points != null){
            Handles.color = Color.red;
            
            for(int i = 0; i< waypointTarget.Points.Length; i++){
                EditorGUI.BeginChangeCheck();
                Vector3 currentPoint = waypointTarget.EntityPos + waypointTarget.Points[i];
                Vector3 newPos = Handles.FreeMoveHandle(currentPoint, 0.5f, Vector3.one*0.5f, Handles.SphereHandleCap);
                GUIStyle text = new GUIStyle();
                text.fontStyle = FontStyle.Bold;
                text.fontSize = 16;
                text.normal.textColor = Color.black;
                Vector3 textPos = new Vector3(0.2f, -0.2f);
                Handles.Label(waypointTarget.EntityPos + waypointTarget.Points[i] + textPos, $"{i + 1}", text);
                
                if(EditorGUI.EndChangeCheck()){
                    Undo.RecordObject(waypointTarget, "Free Move");
                    waypointTarget.Points[i] = newPos - waypointTarget.EntityPos;
                }
            }
        }
    }
}
