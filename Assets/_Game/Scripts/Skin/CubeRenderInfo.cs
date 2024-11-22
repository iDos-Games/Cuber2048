using System;
using UnityEngine;

[Serializable]
public class CubeRenderInfo
{
	[SerializeField] private string _name;
	[SerializeField] private CubeSkinCollection _collection;
	public CubeSkinCollection Collection => _collection;

	[SerializeField] private Sprite _render;
	public Sprite Render => _render;
}
