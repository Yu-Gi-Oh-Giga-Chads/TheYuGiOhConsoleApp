using BusinessLayer;
using NuGet.Frameworks;
using PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace TheYuGiOhConsoleApp_Tests.PresentationLayer
{
    internal class DisplayMenuTest
    {
        [Test]

        public void TestGetCardTextInsertsNewLines()
        {
            //Arrange
            Card testCard = new Card();
            testCard.Desc = "This card can attack while in face-up Defense Position. If it does, apply its DEF for damage calculation. If a \"Superheavy Samurai\" monster(s) you control would be destroyed by battle or card effect, you can destroy 1 \"Superheavy Samurai\" card you control instead. Once per turn, if your opponent activates a Spell/Trap Card or effect (except during the Damage Step): You can draw until you have 3 cards in your hand. ";
            DisplayCardMenu cardMenu = new DisplayCardMenu(testCard);

            //Act
            string testResult = cardMenu.GetCatdText();
            string firstLine = "";
            int i = 0;
            while (testResult[i].Equals('\n')!)
            {
                firstLine += testResult[i]; 
                i++; 
            }
            int expectedFirstLineLength = Console.WindowWidth - 14;

            //Assert
            Assert.AreEqual(firstLine.Length, expectedFirstLineLength, "GetCardText does not break the lines properly.");
        }

        [Test]

        public void TestGetOptionsReturnsTrapCardInfo()
        {
            //Arrange
            Card testCard = new Card();
            testCard.Desc = "This card can attack while in face-up Defense Position. If it does, apply its DEF for damage calculation. If a \"Superheavy Samurai\" monster(s) you control would be destroyed by battle or card effect, you can destroy 1 \"Superheavy Samurai\" card you control instead. Once per turn, if your opponent activates a Spell/Trap Card or effect (except during the Damage Step): You can draw until you have 3 cards in your hand. ";
            testCard.FrameType = "trap";
            DisplayCardMenu cardMenu = new DisplayCardMenu(testCard);
    
            //Act
            string[] res = cardMenu.GetOptions();
            string testResult = "";
            for (int i = 0; i < res.Length; i++) {
                testResult += res[i];
            }
            //Assert
            Assert.IsTrue(testResult.Contains("Trap Type"), "The method does not return trap type card.");
        }

        [Test]
        public void TestGetOptionsReturnsSpellCardInfo()
        {
            //Arrange
            Card testCard = new Card();
            testCard.Desc = "This card can attack while in face-up Defense Position. If it does, apply its DEF for damage calculation. If a \"Superheavy Samurai\" monster(s) you control would be destroyed by battle or card effect, you can destroy 1 \"Superheavy Samurai\" card you control instead. Once per turn, if your opponent activates a Spell/Trap Card or effect (except during the Damage Step): You can draw until you have 3 cards in your hand. ";
            testCard.FrameType = "spell";
            DisplayCardMenu cardMenu = new DisplayCardMenu(testCard);

            //Act
            string[] res = cardMenu.GetOptions();
            string testResult = "";
            for (int i = 0; i < res.Length; i++)
            {
                testResult += res[i];
            }
            //Assert
            Assert.IsTrue(testResult.Contains("Spell Type"), "The method does not return spell type card.");
        }

        [Test]
        public void TestGetOptionsReturnsMonsterCardInfo()
        {
            //Arrange
            Card testCard = new Card();
            testCard.Desc = "This card can attack while in face-up Defense Position. If it does, apply its DEF for damage calculation. If a \"Superheavy Samurai\" monster(s) you control would be destroyed by battle or card effect, you can destroy 1 \"Superheavy Samurai\" card you control instead. Once per turn, if your opponent activates a Spell/Trap Card or effect (except during the Damage Step): You can draw until you have 3 cards in your hand. ";
            testCard.FrameType = "monster";
            DisplayCardMenu cardMenu = new DisplayCardMenu(testCard);

            //Act
            string[] res = cardMenu.GetOptions();
            string testResult = "";
            for (int i = 0; i < res.Length; i++)
            {
                testResult += res[i];
            }
            //Assert
            Assert.IsTrue(testResult.Contains("Monster Type"), "The method does not return monster type card.");
        }
    }
}
