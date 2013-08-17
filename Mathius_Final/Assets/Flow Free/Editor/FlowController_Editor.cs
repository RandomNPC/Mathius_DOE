using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FlowController))]

public class FlowController_Editor : Editor
{
    FlowController _target;

    int[] materialArray;
    string[] str;

    void OnEnable()
    {
        _target = (FlowController)target;

        RebuildMaterialIndexArray();
    }

    public override void OnInspectorGUI()
    {
        RebuildMaterialIndexArray();        

        _target.materialIndex = EditorGUILayout.IntPopup("Material index:", _target.materialIndex, str, materialArray);
        _target.speed = EditorGUILayout.Slider("Speed: ", _target.speed, -1.0f, 1.0f);
        _target.phaseLength = EditorGUILayout.Slider("Stretching: ", _target.phaseLength, 0.3f, 5.0f);
        _target.useSpecial = false;     
                        
    }

    void OnDisable()
    {
        _target = null;
    }

    void RebuildMaterialIndexArray()
    {
        materialArray = new int[_target.GetComponent<MeshRenderer>().sharedMaterials.Length];
        str = new string[materialArray.Length];

        for (int i = 0; i < materialArray.Length; i++)
        {
            materialArray[i] = i;
            str[i] = i.ToString() + ". " + _target.GetComponent<MeshRenderer>().sharedMaterials[i].ToString();

            //
            if (str[i].IndexOf("null") == -1)
                str[i] = str[i].Remove(str[i].IndexOf("("));
        }

        if (_target.materialIndex > materialArray.Length - 1)
            _target.materialIndex = materialArray.Length - 1;
    }
}
