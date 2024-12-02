using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class SerializedTransform
{
    [JsonProperty(JsonProperty.POSITION)] public float[] Position = new float[3];
    [JsonProperty(JsonProperty.ROTATION)] public float[] Rotation = new float[4];

    public SerializedTransform(Transform transform)
    {
        Position[0] = (float)Math.Round(transform.localPosition.x, 4);
        Position[1] = (float)Math.Round(transform.localPosition.y, 4);
        Position[2] = (float)Math.Round(transform.localPosition.z, 4);

        Rotation[0] = (float)Math.Round(transform.localRotation.w, 4);
        Rotation[1] = (float)Math.Round(transform.localRotation.x, 4);
        Rotation[2] = (float)Math.Round(transform.localRotation.y, 4);
        Rotation[3] = (float)Math.Round(transform.localRotation.z, 4);
    }
}
