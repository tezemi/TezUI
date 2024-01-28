using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TezUI;
using NUnit.Framework;

public class CoroutineTimerTests
{
	private Graphic _testGraphic;
	private const float TOLERANCE = 0.1f;

	[SetUp]
	public void SetUpGraphic()
	{
		var graphicGameObject = new GameObject("Test Graphic", typeof(Text));
		_testGraphic = graphicGameObject.GetComponent<Graphic>();
	}

	[UnityTest]
	public IEnumerator SimpleCoroutineTimerTest()
	{
		var time = Time.time;

		_testGraphic.FadeIn(1f);

		yield return CoroutineTimer.Create(3f, _testGraphic);

		yield return _testGraphic.FadeOut(1f);

		UnityEngine.Assertions.Assert.AreApproximatelyEqual(4f, Time.time - time, TOLERANCE);
	}

	[UnityTest]
	public IEnumerator AdvancedCoroutineTimerTest()
	{
		var time = Time.time;

		_testGraphic.FadeIn(1f);

		_testGraphic.StartCoroutine(SubCoroutine());

		yield return CoroutineTimer.Create(3f, _testGraphic);

		yield return _testGraphic.FadeOut(1f);

		UnityEngine.Assertions.Assert.AreApproximatelyEqual(7f, Time.time - time, TOLERANCE);

		IEnumerator SubCoroutine()
		{
			for (var i = 0; i < 3; i++)
			{
				yield return new WaitForSeconds(1f);

				CoroutineTimer.Create(3f, _testGraphic);
			}
		}
	}

	[TearDown]
	public void TeatDownGraphic()
	{
		Object.Destroy(_testGraphic.gameObject);
	}
}
