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
        [Tooltip("Will the object use physics in webaverse (Note that in webaverse physics refers to collision detection)")]
        public bool hasPhysics;
        [Tooltip("The position multiplier for the object (Recomended: 100)")]
        public float positionMultiplier;
    }

    [Header("General Settings")]
    [Tooltip("If true, will generate all required lines of an scn file")]
    [SerializeField] private bool m_generateAllFileLines = false;

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

        if (m_generateAllFileLines) //if true, generate the beginning lines of the scn file
        {
            output += "{\n  \"objects\":\n  [\n";

            //Change with code to auto detect lighting when available
            output += "    { \"type\": \"application/light\", \"content\": { \"lightType\": \"ambient\", \"args\": [[255, 255, 255], 0.5] } },\n";
            output += "    { \"type\": \"application/light\", \"content\": { \"lightType\": \"directional\", \"args\": [[255, 255, 255], 5], \"position\": [1, 2, 3], \"shadow\": [150, 5120, 0.1, 10000, -0.0001] } },\n";
        }

        for (int i = 0; i < m_objects.Length; i++)
        {
            //generating a scene line and adding it to the string
            output += GenerateScnLine(m_objects[i]);

            //if not last object add a comma and return character
            if (!(i == m_objects.Length - 1))
            {
                output += ",\n";
            }
            else //if last object, add just a return
            {
                output += "\n";
            }
        }

        if (m_generateAllFileLines) //if true, generate the beginning lines of the scn file
        {
            output += "  ]\n}";
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
        string output = "    { ";

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

        //adding the scale 
        output += "\"scale\" : [" + target.obj.transform.localScale.x + ", " + target.obj.transform.localScale.y + ", " + target.obj.transform.localScale.z + "], ";

        if (!m_condensedLines) output += "\n    ";

        //adding the dynamic flag
        output += "\"dynamic\": " + target.isDynamic.ToString().ToLower() + ", ";

        if (!m_condensedLines) output += "\n    ";


        //adding the dynamic flag
        output += "\"physics\": " + target.hasPhysics.ToString().ToLower();

        if (!m_condensedLines) output += "\n";


        output += "}";

        //assigning the output to the serialized line so it can be grabbed from the inspector
        m_webaverseCompatibleScnLine = output;

        return output;
    }
}