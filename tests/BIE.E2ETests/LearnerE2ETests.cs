using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace BIE.E2ETests;

[TestClass]
public class LearnerE2ETests : BrowserTestBase
{
    [TestMethod]
    public void TouristAt30DaysRemainsApprovedAfterReload()
    {
        // Arrange
        Fill("Sam Browser", "Tourismus", "30");

        // Act
        Driver.FindElement(By.Id("submit")).Click();

        // Warten
        var result = Decision("approved");
        StringAssert.Contains(result.Text, "GENEHMIGT");

        // Assert
        StringAssert.Contains(result.Text, "GENEHMIGT");

        // Act
        var caseId = result.GetDomAttribute("data-application-id");
        Driver.Navigate().Refresh();

        // Warten
        var row = Wait.Until(d =>
        var row = Wait.Until(d =>
            d.FindElements(By.CssSelector("#application-list tr"))
             .FirstOrDefault(r => r.GetDomAttribute("data-id") == caseId));

        var cells = row.FindElements(By.TagName("td"));

        // Assert
        Assert.AreEqual("Sam Browser", cells[1].Text);
        Assert.AreEqual("Orion", cells[2].Text);
        Assert.AreEqual("30 Tage", cells[3].Text);
        StringAssert.Contains(cells[4].Text, "GENEHMIGT");
    }
}
