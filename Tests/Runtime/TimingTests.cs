using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TezUI;
using NUnit.Framework;

public class TimingTests
{
	private Graphic _testGraphic;
	private const float TOLERANCE = 0.005f;

	[SetUp]
	public void SetUpGraphic()
	{
		var graphicGameObject = new GameObject("Test Graphic", typeof(Text));
		_testGraphic = graphicGameObject.GetComponent<Graphic>();
	}

	[UnityTest]
	public IEnumerator FastTimingTest()
	{
		var time = Time.time;

		yield return _testGraphic.FadeOut(0.05f);

		UnityEngine.Assertions.Assert.AreApproximatelyEqual(0.05f, Time.time - time, TOLERANCE);
	}

	[UnityTest]
	public IEnumerator NormalTimingTest()
	{
		var time = Time.time;

		yield return _testGraphic.FadeOut(1f);

		UnityEngine.Assertions.Assert.AreApproximatelyEqual(1f, Time.time - time , TOLERANCE);
	}

	[UnityTest]
	public IEnumerator LongTimingTest()
	{
		var time = Time.time;

		yield return _testGraphic.FadeOut(10f);

		UnityEngine.Assertions.Assert.AreApproximatelyEqual(10f, Time.time - time, TOLERANCE);
	}

	[TearDown]
	public void TeatDownGraphic()
	{
		Object.Destroy(_testGraphic.gameObject);
	}
}
