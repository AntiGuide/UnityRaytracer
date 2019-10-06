using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class CameraTest {
        private Camera camera;
        private Vector3 position;
        private Vector3 lookAt;
        private Vector3 upVector;
        private float fov;
        private int xMax;
        private int yMax;

        [SetUp]
        public void SetUp() {
            position = new Vector3(0,0,0);
            lookAt = new Vector3(1,0,0);
            upVector = new Vector3(0,1,0);
            fov = 35f;
            xMax = 1000;
            yMax = 1000;
            camera = new PerspCam(position, lookAt, upVector, fov, xMax, yMax);
        }
        
        [Test]
        public void CameraTestDestinationPoint_CenterShouldBeSameAsLookAt() {
            var result = camera.CalculateDestinationPoint(Mathf.RoundToInt(xMax / 2f), Mathf.RoundToInt(yMax / 2f));
            Assert.That(Vector3.Dot(lookAt.normalized, result.normalized), Is.EqualTo(1f).Within(0.1f));
        }
        
        [Test]
        public void CameraTestDestinationPoint_DownShouldBeDefinedVector() {
            var result = camera.CalculateDestinationPoint(Mathf.RoundToInt(xMax / 2f), 0);
            Assert.That(Vector3.Dot(new Vector3(1f,-0.5f,0f).normalized, result.normalized), Is.EqualTo(1f).Within(0.1f));
        }
        
        [Test]
        public void CameraTestDestinationPoint_UpShouldBeDefinedVector() {
            var result = camera.CalculateDestinationPoint(Mathf.RoundToInt(xMax / 2f), yMax);
            Assert.That(Vector3.Dot(new Vector3(1f,0.5f,0f).normalized, result.normalized), Is.EqualTo(1f).Within(0.1f));
        }
        
        [Test]
        public void CameraTestDestinationPoint_LeftShouldBeDefinedVector() {
            var result = camera.CalculateDestinationPoint(0, Mathf.RoundToInt(yMax / 2f));
            Assert.That(Vector3.Dot(new Vector3(1f,0f,-0.5f).normalized, result.normalized), Is.EqualTo(1f).Within(0.1f));
        }
        
        [Test]
        public void CameraTestDestinationPoint_RightShouldBeDefinedVector() {
            var result = camera.CalculateDestinationPoint(xMax, Mathf.RoundToInt(yMax / 2f));
            Assert.That(Vector3.Dot(new Vector3(1f,0f,0.5f).normalized, result.normalized), Is.EqualTo(1f).Within(0.1f));
        }
    }
}