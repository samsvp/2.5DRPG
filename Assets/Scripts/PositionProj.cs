using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionProj : MonoBehaviour
{
    [SerializeField]
    private List<Transform> transformsChangeZ = new List<Transform>();
    [SerializeField]
    private List<float> targetZs = new List<float>();

    // Dictionary to hold transforms and their original Z position
    private static Dictionary<Transform, float> transformsDict = new Dictionary<Transform, float>();

#pragma warning disable IDE0051 // Remove unused private members
    // Start is called before the first frame update
    void Start()
    {
        if (transformsChangeZ.Count != targetZs.Count) 
            throw new System.Exception("Not enough Z position targets");

        for (int i = 0; i < transformsChangeZ.Count; i++)
            if(!transformsDict.ContainsKey(transformsChangeZ[i])) 
                transformsDict.Add(transformsChangeZ[i], transformsChangeZ[i].position.z);
    }

#pragma warning restore IDE0051 // Remove unused private members

    /// <summary>
    /// Changes z position when the camera projection changes
    /// </summary>
    /// <param name="speed"></param>
    private IEnumerator IChangeZPosition(Transform t, float targetZ, float speed)
    {
        while (Mathf.Abs(t.position.z - targetZ) > 0.05f && CameraProjectionChange.isChanging)
        {
            Vector3 pos = t.position;
            Vector3 targetPos = new Vector3(pos.x, pos.y, targetZ);
            t.position = Vector3.Lerp(pos, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        t.position = new Vector3(t.position.x, t.position.y, targetZ);
    }

    private void ChangeZPosition(Transform t, float z, float speed, bool changeTo2D)
    {
        float targetZ = changeTo2D ? z : transformsDict[t];
        StartCoroutine(IChangeZPosition(t, targetZ, speed));
    }

    public void TriggerChange(float speed, bool changeTo2D)
    {
        if (changeTo2D)
            for (int i = 0; i < transformsChangeZ.Count; i++)
                StartCoroutine(IChangeZPosition(transformsChangeZ[i], targetZs[i], speed));
        else
            foreach (KeyValuePair<Transform, float> kvPair in transformsDict)
                StartCoroutine(IChangeZPosition(kvPair.Key, kvPair.Value, speed));
    }

}
