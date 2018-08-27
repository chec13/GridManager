using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(createImage))]
public class createImageEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        createImage myImage = (createImage)target;

        if(GUILayout.Button("Update Image"))
        {
            myImage.createTexture();
        }
        if(GUILayout.Button("Create"))
        {
            myImage.saveImage();
            AssetDatabase.Refresh();
        }

        //GUI.DrawTexture(new Rect(0,0,128,128), myImage.getTexture());
        
        GUILayout.Label(myImage.getTexture(), new GUILayoutOption[] {GUILayout.MinHeight(128),
            GUILayout.MinWidth(128) });
       
    }
}
