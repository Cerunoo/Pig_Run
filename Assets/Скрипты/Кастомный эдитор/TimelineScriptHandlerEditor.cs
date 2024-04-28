#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(TimelineScriptHandler))]
[CanEditMultipleObjects]

public class TimelineScriptHandlerEditor : Editor
{
    // General
    SerializedProperty selectScene;
    SerializedProperty player;

    // Start
    SerializedProperty animStartButton;
    SerializedProperty startFromHouse;
    SerializedProperty animStartFromHouseButton;
    SerializedProperty playerExit;
    SerializedProperty playerEnter;
    SerializedProperty enterEvent;
    SerializedProperty pointHandle;

    // HouseTransition
    SerializedProperty enterHouse;
    SerializedProperty exitHouse;
    SerializedProperty exitEvent;
    SerializedProperty playerStartPos;
    SerializedProperty ignoreCollider;

    void OnEnable()
    {
        // General
        selectScene = serializedObject.FindProperty("selectScene");
        player = serializedObject.FindProperty("player");

        // Start
        animStartButton = serializedObject.FindProperty("animStartButton");
        startFromHouse = serializedObject.FindProperty("startFromHouse");
        animStartFromHouseButton = serializedObject.FindProperty("animStartFromHouseButton");
        playerExit = serializedObject.FindProperty("playerExit");
        playerEnter = serializedObject.FindProperty("playerEnter");
        enterEvent = serializedObject.FindProperty("enterEvent");
        pointHandle = serializedObject.FindProperty("pointHandle");
        
        // HouseTransition
        enterHouse = serializedObject.FindProperty("enterHouse");
        exitHouse = serializedObject.FindProperty("exitHouse");
        exitEvent = serializedObject.FindProperty("exitEvent");
        playerStartPos = serializedObject.FindProperty("playerStartPos");
        ignoreCollider = serializedObject.FindProperty("ignoreCollider");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(selectScene);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(player);
        EditorGUILayout.Space(5);

        switch (selectScene.enumValueIndex)
        {
            // Menu
            case 0:
                // Start
                EditorGUILayout.PropertyField(animStartButton);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(startFromHouse);
                EditorGUILayout.PropertyField(animStartFromHouseButton);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(playerExit);
                EditorGUILayout.PropertyField(playerEnter);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(enterEvent);
                EditorGUILayout.PropertyField(pointHandle);

                break;
                
            // House
            case 1:
                // HouseTransition
                EditorGUILayout.PropertyField(enterHouse);
                EditorGUILayout.PropertyField(exitHouse);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(exitEvent);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(playerStartPos);
                EditorGUILayout.Space(5);
                EditorGUILayout.PropertyField(ignoreCollider);
                
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif