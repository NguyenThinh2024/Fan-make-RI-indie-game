using UnityEngine;
using UnityEditor;
using Player_Model;

[CustomEditor(typeof(PlayerData))]
public class PlayerDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerData playerData = (PlayerData)target;

        EditorGUILayout.Space(10);
        
        // Load button
        if (GUILayout.Button("Load Player Data", GUILayout.Height(30)))
        {
            playerData.LoadPlayerDataFromInspector();
            EditorUtility.SetDirty(playerData);
        }

        // Show loaded status
        if (!string.IsNullOrEmpty(playerData.playerId))
        {
            EditorGUILayout.HelpBox($"Loaded: {playerData.playerName} (ID: {playerData.playerId})", MessageType.Info);
        }
    }
}
