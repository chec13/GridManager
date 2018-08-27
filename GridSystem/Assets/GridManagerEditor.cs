using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager myManager = (GridManager)target;

        if (GUILayout.Button("Update Tile Location"))
        {
            myManager.resetTilePosition();
            //Thread t = new Thread(new ThreadStart(myManager.resetTilePosition));
            //t.Start();
        }
        if (GUILayout.Button("Create"))
        {
            myManager.deleteFromGrid();
            myManager.InitializeGrid();
        }
        

        //GUI.DrawTexture(new Rect(0,0,128,128), myImage.getTexture());

        

    }
}
