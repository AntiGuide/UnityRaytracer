using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class PlaneTest {
        
        [Test]
        public void PlaneCollisionTestAllAxis_ShouldCollide() {
            var result = CheckPlaneAt(new Vector3(2, 2, 2), -Vector3.one, Vector3.zero, Vector3.one);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(3.46410179f).Within(0.00001f));
        }
        
        [Test]
        public void PlaneCollisionTestWrongDirection_ShouldNotCollide() {
            var result = CheckPlaneAt(new Vector3(2, 2, 2), -Vector3.one, Vector3.zero, -Vector3.one);
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void PlaneCollisionTestMiss_ShouldNotCollide() {
            var result = CheckPlaneAt(new Vector3(2, 2, 2), -Vector3.one, Vector3.zero, new Vector3(-1,0,-1));
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void PlaneCollisionTestBackface_ShouldNotCollide() {
            var result = CheckPlaneAt(new Vector3(2, 2, 2), Vector3.one, Vector3.zero, Vector3.one);
            Assert.That(result, Is.Null);
        }

        private static float? CheckPlaneAt(Vector3 planePosition, Vector3 planeNormal, Vector3 origin, Vector3 direction) {
            var Plane = new Plane(planePosition, planeNormal, Color.red);
            var r = new Ray(origin, direction);
            return Plane.Intersect(r);
        }
    }
}