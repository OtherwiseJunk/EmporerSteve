using EmporerSteve.Services;
using Moq;

namespace EmporerSteveTests
{    

    [TestClass]
    public class Traveller2eSericeTests
    {
        Traveller2eService _sut;
        private Mock<Traveller2eDiceRoller> _mockDiceRoller;

        [TestInitialize]
        public void Setup()
        {
            _mockDiceRoller = new Mock<Traveller2eDiceRoller>();
            _sut = new Traveller2eService();
        }

        [TestMethod]
        [DataRow(-1, -3)]
        [DataRow(0, -3)]
        [DataRow(1, -2)]
        [DataRow(2, -2)]
        [DataRow(3, -1)]
        [DataRow(4, -1)]
        [DataRow(5, -1)]
        [DataRow(6, 0)]
        [DataRow(7, 0)]
        [DataRow(8, 0)]
        [DataRow(9, 1)]
        [DataRow(10, 1)]
        [DataRow(11, 1)]
        [DataRow(12, 2)]
        [DataRow(13, 2)]
        [DataRow(14, 2)]
        [DataRow(15, 3)]
        [DataRow(16, 3)]
        [DataRow(17, 3)]
        [DataRow(18, 3)]
        [DataRow(19, 3)]
        public void CharacteristicDiceModifierIsAccurate(int characteristic, int expectedModifier)
        {
            Assert.AreEqual(expectedModifier, _sut.GetCharacteristicModifier(characteristic));
        }

        [TestMethod]
        public void GetValidStartingCharacteristics_ShouldReturnValidCharacteristics()
        {
            for(int i = 0; i < 20; i++)
            {
                var characteristics = _sut.GetValidStartingCharacteristics(0);

                Assert.IsTrue(_sut.IsValidStartingCharacteristics(characteristics, 0));
            }            
        }
        
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(7)]
        [DataRow(8)]
        [DataRow(9)]
        [DataRow(-1)]
        [DataRow(-2)]
        [DataRow(-3)]
        [DataRow(-4)]
        [DataRow(-5)]
        [DataRow(-6)]
        [DataRow(-7)]
        [DataRow(-8)]
        [DataRow(-9)]
        public void GetValidStartingCharacteristics_ShouldRespectMinimumModifierSum(int minimumModifierSum)
        {
            for(int i = 0; i < 2; i++)
            {
                var characteristics = _sut.GetValidStartingCharacteristics(minimumModifierSum);

                Assert.IsTrue(_sut.IsValidStartingCharacteristics(characteristics, minimumModifierSum));
            }            
        }

        [TestMethod]
        public void RollStartingCharacteristics_ShouldReturnArrayOfSixElementsInRange()
        {
            for(int i = 0; i < 20 ; i++)
            {
                var characteristics = _sut.RollStartingCharacteristics();

                Assert.AreEqual(6, characteristics.Length);
                foreach (var characteristic in characteristics)
                {
                    Assert.IsTrue(characteristic <= 12);
                    Assert.IsTrue(characteristic >= 2);
                }
            }
        }

        [TestMethod]
        public void IsValidStartingCharacteristics_ShouldReturnTrueForValidCharacteristics()
        {
            var characteristics = new[] { 7, 7, 7, 7, 7, 7 };

            var result = _sut.IsValidStartingCharacteristics(characteristics, 0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidStartingCharacteristics_ShouldReturnFalseForInvalidCharacteristics()
        {
            var characteristics = new[] { 1, 1, 1, 1, 1, 1 };

            var result = _sut.IsValidStartingCharacteristics(characteristics, 0);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        [DataRow(new[] { 7, 7, 7, 7, 7, 7 }, 7, false)]
        [DataRow(new[] { 12, 11, 11, 11, 11, 11 }, 7, true)]
        public void IsValidStartingCharacteristics_ShouldRespectMinimumModifierSum(int[] array, int minimumModifierSum, bool expected)
        {
            var result = _sut.IsValidStartingCharacteristics(array, minimumModifierSum);

            Assert.AreEqual(expected, result);
        }
    }
}