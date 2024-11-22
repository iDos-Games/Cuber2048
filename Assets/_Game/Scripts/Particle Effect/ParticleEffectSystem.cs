using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectSystem : MonoBehaviour
{
	private static ParticleEffectSystem _instance;
	public static ParticleEffectSystem Instance => _instance;

	private const int MERGE_EFFECT_POOL_SIZE = 21;
	private const int EXPLODE_EFFECT_POOL_SIZE = 5;

	[SerializeField] private ParticleSystem _mergeEffect;
	[SerializeField] private ParticleSystem _explodeEffect;
	[SerializeField] private ParticleSystem _explodeRainbowEffect;

	private static readonly Queue<ParticleSystem> _mergeEffectPool = new();
	private static readonly Queue<ParticleSystem> _explodeEffectPool = new();
	private static readonly Queue<ParticleSystem> _explodeRainbowEffectPool = new();

	private void Awake()
	{
		if (_instance == null || ReferenceEquals(this, _instance))
		{
			_instance = this;
		}
		else
		{
			Destroy(this);
		}

		Initialize();
	}

	private void Initialize()
	{
		InitializePool(_mergeEffectPool, _mergeEffect, MERGE_EFFECT_POOL_SIZE);
		InitializePool(_explodeEffectPool, _explodeEffect, EXPLODE_EFFECT_POOL_SIZE);
		InitializePool(_explodeRainbowEffectPool, _explodeRainbowEffect, EXPLODE_EFFECT_POOL_SIZE);
	}

	private void InitializePool(Queue<ParticleSystem> pool, ParticleSystem particleSystem, int poolSize)
	{
		for (int i = 0; i < poolSize; i++)
		{
			var effect = Instantiate(particleSystem, transform);
			effect.gameObject.SetActive(false);
			pool.Enqueue(effect);
		}
	}

	public void PlayMergeEffect(Vector3 position, Quaternion rotation)
	{
		PlayEffect(_mergeEffectPool, position, rotation);
	}

	public void PlayExplodeEffect(Vector3 position, Quaternion rotation)
	{
		PlayEffect(_explodeEffectPool, position, rotation);
	}

	public void PlayExplodeRainbowEffect(Vector3 position, Quaternion rotation)
	{
		PlayEffect(_explodeRainbowEffectPool, position, rotation);
	}

	private void PlayEffect(Queue<ParticleSystem> pool, Vector3 position, Quaternion rotation)
	{
		if (pool.Count == 0)
		{
			return;
		}

		var effect = pool.Dequeue();

		effect.transform.SetPositionAndRotation(position, rotation);

		effect.gameObject.SetActive(true);
		effect.Play();

		StartCoroutine(ReturnToPool(pool, effect));
	}

	private IEnumerator ReturnToPool(Queue<ParticleSystem> pool, ParticleSystem effect)
	{
		yield return new WaitForSeconds(2.0f);
		effect.gameObject.SetActive(false);
		pool.Enqueue(effect);
	}
}
