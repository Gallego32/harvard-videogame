using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePlayers : MonoBehaviour
{
    public bool twoPlayers;

    public List<Transform> targets;

    public Vector3 offset;

    private Vector3 speed;

    public float smoothTime = .5f;

    void Start()
    {
    }

    // Updated after Update
    void LateUpdate()
    {
        Vector3 centerPoint = CenterPoint();

        transform.position = Vector3.SmoothDamp(transform.position, centerPoint + offset, ref speed, smoothTime);
    }

    Vector3 CenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
