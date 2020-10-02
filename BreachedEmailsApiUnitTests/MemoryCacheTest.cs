using BreachedEmailsApi.Models;
using BreachedEmailsApi.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;

namespace BreachedEmailsApiUnitTests
{
    [TestClass]
    public class MemoryCacheTest
    {
        [TestMethod]
        public void TestGetCache()
        {
            // Arrange
            IMemoryCache memoryCache = Substitute.For<IMemoryCache>();
            List<BreachedEmail> expectedEmails = null;
            
            // Act
            List<BreachedEmail> breachedEmails = memoryCache.GetCache<List<BreachedEmail>>("breachedemails");

            // Assert
            Assert.AreEqual(expectedEmails, breachedEmails);

        }
    }
}
