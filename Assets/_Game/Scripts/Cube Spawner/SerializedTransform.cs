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
		Position[0] = transform.localPosition.x;
		Position[1] = transform.localPosition.y;
		Position[2] = transform.localPosition.z;

		Rotation[0] = transform.localRotation.w;
		Rotation[1] = transform.localRotation.x;
		Rotation[2] = transform.localRotation.y;
		Rotation[3] = transform.localRotation.z;
	}
}