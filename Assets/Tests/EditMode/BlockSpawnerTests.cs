using System.Collections;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Tests.EditMode
{
    public class BlockSpawnerTests : ZenjectUnitTestFixture
    {
        [SetUp]
        public new void Setup()
        {
            Container.BindFactory<Vector3, Vector3, Block, Block.Factory>()
                .FromInstance(new GameObject().AddComponent<Block>());
            Container.Bind<IBlockSizeSetter>().FromMock();
            Container.Bind<IBlockSlicer>().FromMock();
        }

        private static IEnumerable PositionsVariations()
        {
            const int plusValue = 10;
            const int minusValue = -10;
            var xPlusVector = new Vector3(plusValue, 0, 0);
            var xMinusVector = new Vector3(minusValue, 0, 0);
            var zPlusVector = new Vector3(0, 0, plusValue);
            var zMinusVector = new Vector3(0, 0, minusValue);
            
            yield return new TestCaseData(xPlusVector, xMinusVector, xPlusVector, xPlusVector);
            yield return new TestCaseData(xPlusVector, xMinusVector, zPlusVector, new Vector3(plusValue, 0, plusValue));
            yield return new TestCaseData(xMinusVector, xPlusVector, xMinusVector, xMinusVector);
            yield return new TestCaseData(xMinusVector, xPlusVector, zMinusVector, new Vector3(minusValue, 0, minusValue));
            yield return new TestCaseData(zMinusVector, zPlusVector, xPlusVector, new Vector3(plusValue, 0, minusValue));
            yield return new TestCaseData(zMinusVector, zPlusVector, zPlusVector, zMinusVector);
            yield return new TestCaseData(zPlusVector, zMinusVector, xMinusVector, new Vector3(minusValue, 0, plusValue));
            yield return new TestCaseData(zPlusVector, zMinusVector, zMinusVector, zPlusVector);
        }

        [TestCaseSource(nameof(PositionsVariations))]
        public void OnBlockPositionChanged_PositionDelta_BlockSpawnerPositionChangedAccordingly(Vector3 blockSpawnerPosition, 
            Vector3 finishPointPosition, Vector3 positionDelta, Vector3 resultBlockSpawnerPosition)
        {
            var blockSpawnerGameObject = new GameObject();
            blockSpawnerGameObject.transform.position = blockSpawnerPosition;
            var blockSpawner = Container.InstantiateComponent<BlockSpawner>(blockSpawnerGameObject);

            var finishPointGameObject = new GameObject();
            finishPointGameObject.transform.position = finishPointPosition;
            blockSpawner.finishPoint = finishPointGameObject.transform;

            blockSpawner.OnBlockPositionChanged(new object(), new PositionChangedEventArgs(positionDelta));

            //Через AreEqual ошибки при сравнении векторов из-за проблем со сложением float
            Assert.IsTrue(resultBlockSpawnerPosition == blockSpawner.transform.position);
        }
    }
}