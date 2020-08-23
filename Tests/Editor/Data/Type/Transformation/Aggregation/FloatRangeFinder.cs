using Zinnia.Data.Collection.List;
using Zinnia.Data.Type.Transformation.Aggregation;

namespace Test.Zinnia.Data.Type.Transformation.Aggregation
{
    using NUnit.Framework;
    using Test.Zinnia.Utility.Mock;
    using UnityEngine;
    using Assert = UnityEngine.Assertions.Assert;

    public class FloatRangeFinderTest
    {
        private GameObject containingObject;
        private FloatRangeFinder subject;

        [SetUp]
        public void SetUp()
        {
            containingObject = new GameObject();
            subject = containingObject.AddComponent<FloatRangeFinder>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(containingObject);
        }

        [Test]
        public void Transform()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            FloatObservableList collection = containingObject.AddComponent<FloatObservableList>();
            subject.Collection = collection;
            subject.Collection.Add(1f);
            subject.Collection.Add(2f);
            subject.Collection.Add(3f);
            subject.Collection.Add(4f);

            Assert.AreEqual(0f, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            float result = subject.Transform();

            Assert.AreEqual(3f, result);
            Assert.AreEqual(3f, subject.Result);
            Assert.IsTrue(transformedListenerMock.Received);
        }

        [Test]
        public void TransformUnordered()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            FloatObservableList collection = containingObject.AddComponent<FloatObservableList>();
            subject.Collection = collection;
            subject.Collection.Add(2f);
            subject.Collection.Add(3f);
            subject.Collection.Add(4f);
            subject.Collection.Add(1f);

            Assert.AreEqual(0f, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            float result = subject.Transform();

            Assert.AreEqual(3f, result);
            Assert.AreEqual(3f, subject.Result);
            Assert.IsTrue(transformedListenerMock.Received);
        }

        [Test]
        public void TransformInactiveGameObject()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            FloatObservableList collection = containingObject.AddComponent<FloatObservableList>();
            subject.Collection = collection;
            subject.Collection.Add(1f);
            subject.Collection.Add(2f);
            subject.Collection.Add(3f);
            subject.Collection.Add(4f);

            subject.gameObject.SetActive(false);

            Assert.AreEqual(0f, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            float result = subject.Transform();

            Assert.AreEqual(0f, result);
            Assert.AreEqual(0f, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);
        }

        [Test]
        public void TransformInactiveComponent()
        {
            UnityEventListenerMock transformedListenerMock = new UnityEventListenerMock();
            subject.Transformed.AddListener(transformedListenerMock.Listen);
            FloatObservableList collection = containingObject.AddComponent<FloatObservableList>();
            subject.Collection = collection;
            subject.Collection.Add(1f);
            subject.Collection.Add(2f);
            subject.Collection.Add(3f);
            subject.Collection.Add(4f);

            subject.enabled = false;

            Assert.AreEqual(0f, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);

            float result = subject.Transform();

            Assert.AreEqual(0f, result);
            Assert.AreEqual(0f, subject.Result);
            Assert.IsFalse(transformedListenerMock.Received);
        }
    }
}
