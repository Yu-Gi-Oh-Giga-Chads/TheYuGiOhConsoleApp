using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheYuGiOhConsoleApp_Tests.PresentationLayer
{
    internal class MenuTest
    {
        [Test]

        public void TestRunIncreasesSelectedIndex()
        {
            //Arrange
            Menu menu = new Menu("YuGiOh Test", ["Play", "Deck Editor", "Info"]);

            //Act
            int curIndex = 0;

            //Assert
        }
    }
}
