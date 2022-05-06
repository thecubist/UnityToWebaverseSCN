using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Webaverse/Unity To Webaverse Scn")]
public class UnityToWebaScn : MonoBehaviour
{
    [System.Serializable]
    public struct WebaverseObject
    {
        [Tooltip("The target object")]
        public GameObject obj;
        [Tooltip("A reference to the URL of the asset")]
        public string assetURL;
        [Tooltip("Will the object be dynamic in webaverse")]
        public bool isDynamic;
        [Tooltip("The position multiplier for the object (Recomended: 100)")]
        public float positionMultiplier;
    }

    [Header("General Settings")]
    [Tooltip("If true, will generate 1 scn line per asset with no indentation ")]
    [SerializeField] private bool m_condensedLines = true;

    [Tooltip("An array of structures containing settings for each object")]
    [SerializeField] private WebaverseObject[] m_objects;
   
    [Header("Output")]
    [Tooltip("The completed string will be outputted here")]
    [SerializeField] private string m_webaverseCompatibleScnLine;

    /// <summary>
    /// Generates a string containing webaverse scn compatible
    /// reference to each object contained in the m_objects array
    /// </summary>
    /// <returns> a multi-line webaverse scn compatible string </returns>
    public string GenerateBatchedScnLines()
    {
        string output = "";

        foreach (WebaverseObject item in m_objects)
        {
            output += GenerateScnLine(item) + ",\n";
        }

        m_webaverseCompatibleScnLine = output;
        return output;
    }

    /// <summary>
    /// Gathers all information from the passed Webaver 
    /// and formats it so it can be pasted into a webaverse
    /// scn file
    /// </summary>
    /// <param name="target"> A structure containing onject references and parameters </param>
    /// <returns> webaverse scn compatible string </returns>
    public string GenerateScnLine(WebaverseObject target)
    {
        string output = "{ ";

        if (!m_condensedLines) output += "\n    ";

        //adding the URL for the asset
        output += "\"start_url\": \"" + target.assetURL + "\", ";

        if (!m_condensedLines) output += "\n    ";

        //formatting the position
        output += "\"position\": [" + -(target.obj.transform.position.x * target.positionMultiplier) + ", " + target.obj.transform.position.y * target.positionMultiplier + ", " + target.obj.transform.position.z * target.positionMultiplier + "], ";

        if (!m_condensedLines) output += "\n    ";

        //adding the rotation
        output += "\"quaternion\" : [" + -target.obj.transform.rotation.x + ", " + -target.obj.transform.rotation.y + ", " + -target.obj.transform.rotation.z + ", " + target.obj.transform.rotation.w + "], ";

        if (!m_condensedLines) output += "\n    ";

        //adding the dynamic flag
        output += "\"dynamic\": " + target.isDynamic.ToString().ToLower();

        if (!m_condensedLines) output += "\n";

        output += "}";
        //assigning the output to the serialized line so it can be grabbed from the inspector
        m_webaverseCompatibleScnLine = output;

        Debug.Log("RanBatch");
        return output;
    }
}