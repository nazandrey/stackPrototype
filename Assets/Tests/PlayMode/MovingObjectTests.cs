using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.PlayMode
{
    public class MovingObjectTests : ZenjectIntegrationTestFixture
    {
        [SetUp]
        public new void Setup()
        {
            PreInstall();
            PostInstall();
        }
        
        [UnityTest]
        public IEnumerator MovingForwardAndBackward()
        {
            var startPosition = Vector3.zero;
            var finishPosition = Vector3.one;
            var movingGameObject = new GameObject();
            var movingObjectComponent = Container.InstantiateComponent<MovingObject>(movingGameObject);
            movingObjectComponent.Init(startPosition, finishPosition);
            movingObjectComponent.speed = 10;

            var changedDirection = false;
            var initDirection = finishPosition - startPosition;
            var position = movingObjectComponent.transform.position;
            Assert.IsTrue(position == startPosition);
            var maxCheckFixedUpdatesCount = 100;
            var prevPosition = position;
            while (!changedDirection || maxCheckFixedUpdatesCount > 0)
            {
                yield return new WaitForFixedUpdate();
                position = movingObjectComponent.transform.position;

                if (Vector3.Dot(position - prevPosition, initDirection) < 0)
                    changedDirection = true;

                prevPosition = position;
                
                maxCheckFixedUpdatesCount--;
            }
            Assert.IsTrue(changedDirection);
        }
    }
}
