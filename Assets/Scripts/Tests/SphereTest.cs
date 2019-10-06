using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class SphereTest {
        
        [Test]
        public void SphereCollisionTestAllAxis_ShouldCollide() {
            var result = CheckSphereAt(new Vector3(2, 2, 2), Vector3.zero, Vector3.one);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(2.46410179f).Within(0.00001f));
        }
        
        [Test]
        public void SphereCollisionTestWrongDirection_ShouldNotCollide() {
            var result = CheckSphereAt(new Vector3(2, 2, 2), Vector3.zero, -Vector3.one);
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void SphereCollisionTestMiss_ShouldNotCollide() {
            var result = CheckSphereAt(new Vector3(2, 2, 2), Vector3.zero, Vector3.forward);
            Assert.That(result, Is.Null);
        }

        private static float? CheckSphereAt(Vector3 spherePosition, Vector3 origin, Vector3 direction) {
            var sphere = new Sphere(spherePosition, Color.red);
            var r = new Ray(origin, direction);
            return sphere.Intersect(r);
        }
    }
}