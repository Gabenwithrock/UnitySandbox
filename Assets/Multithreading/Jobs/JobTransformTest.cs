using UnityEngine;
using UnityEngine.Jobs;

public class JobTransformTest : MonoBehaviour
{
	private Transform cachedTransform;
	private RotationJob job;
	private TransformAccessArray ar;

	private void Awake()
	{
		cachedTransform = transform;
		job = new RotationJob();
		ar = new TransformAccessArray(new Transform[]{cachedTransform});
	}

	private void Update()
	{
		job.Schedule(ar);
	}

	private void OnDestroy()
	{
		ar.Dispose();
	}

	struct RotationJob : IJobParallelForTransform
	{
		public void Execute(int index, TransformAccess transform)
		{
			ThreadHelper.LogCurThread("RotationJob.Execute");
			transform.rotation = transform.rotation * Quaternion.Euler(1, 1, 1);
			transform.position += Vector3.one * 0.001f;
			transform.localScale += Vector3.one * 0.001f;
		}
	}
}
