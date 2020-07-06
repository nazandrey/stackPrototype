using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.PlayMode
{
    public class BlockTests : ZenjectIntegrationTestFixture
    {
        [SetUp]
        public new void Setup()
        {
            PreInstall();
            Container.Bind<IGameOverHandler>().FromMock();
            Container.Bind<IBlockSlicer>().FromMock();
            PostInstall();
        }
        
        [UnityTest]
        public IEnumerator MovingForwardAndBackward()
        {
            var startPosition = Vector3.zero;
            var finishPosition = Vector3.one;
            var blockGameObject = new GameObject();
            var blockComponent = Container.InstantiateComponent<Block>(blockGameObject,new object[]{startPosition, finishPosition});
            blockComponent.GetComponent<Rigidbody>().useGravity = false;
            blockComponent.speed = 10;

            var changedDirection = false;
            var initDirection = finishPosition - startPosition;
            var position = blockComponent.transform.position;
            Assert.IsTrue(position == startPosition);
            var maxCheckFixedUpdatesCount = 10;
            var prevPosition = position;
            while (!changedDirection || maxCheckFixedUpdatesCount > 0)
            {
                yield return new WaitForFixedUpdate();
                position = blockComponent.transform.position;

                if (Vector3.Dot(position - prevPosition, initDirection) < 0)
                    changedDirection = true;

                prevPosition = position;
                
                maxCheckFixedUpdatesCount--;
            }
            Assert.IsTrue(changedDirection);
        }
    }
}
