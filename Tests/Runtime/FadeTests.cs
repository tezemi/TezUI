using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TezUI;
using NUnit.Framework;

public class FadeTests
{
    private Graphic _testGraphic;

    [SetUp]
    public void SetUpGraphic()
    {
        var graphicGameObject = new GameObject("Test Graphic", typeof(Text));
        _testGraphic = graphicGameObject.GetComponent<Graphic>();
	}

	[UnityTest]
	public IEnumerator FadeInTest()
	{
		_testGraphic.color = new Color(1f, 1f, 1f, 0f);

		yield return _testGraphic.FadeOut(1f);

		Assert.AreEqual(1f, _testGraphic.color.r);
		Assert.AreEqual(1f, _testGraphic.color.g);
		Assert.AreEqual(1f, _testGraphic.color.b);
		Assert.AreEqual(0f, _testGraphic.color.a);
	}

	[UnityTest]
	public IEnumerator FadeTest()
	{
		_testGraphic.color = Color.white;

		yield return _testGraphic.Fade(1f, Color.red);
		Assert.AreEqual(Color.red, _testGraphic.color);

		yield return _testGraphic.Fade(1f, Color.blue);
		Assert.AreEqual(Color.blue, _testGraphic.color);

		_testGraphic.Fade(1f, Color.green);
		yield return new WaitForSeconds(0.5f);
		yield return _testGraphic.Fade(1f, Color.magenta);

		Assert.AreEqual(Color.magenta, _testGraphic.color);
	}

	[UnityTest]
    public IEnumerator FadeOutTest()
    {
        _testGraphic.color = Color.white;

        yield return _testGraphic.FadeOut(1f);

		Assert.AreEqual(1f, _testGraphic.color.r);
		Assert.AreEqual(1f, _testGraphic.color.g);
		Assert.AreEqual(1f, _testGraphic.color.b);
		Assert.AreEqual(0f, _testGraphic.color.a);
	}

    [TearDown]
    public void TeatDownGraphic()
    {
        Object.Destroy(_testGraphic.gameObject);
    }
}
