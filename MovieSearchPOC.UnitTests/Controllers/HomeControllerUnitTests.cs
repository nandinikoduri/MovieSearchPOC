using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieSearchPOC.Controllers;
using MovieSearchPOC.Models;

namespace MovieSearchPOC.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerUnitTests
    {
        private HomeController _systemUnderTest;

        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = new HomeController();
        }

        /// <summary>
        /// Test for Index method
        /// </summary>
        [TestMethod]
        public void IndexTest()
        {
            var result = _systemUnderTest.Index() as ViewResult ;
            Assert.IsNotNull(result, "IndexTest failed");
        }

        /// <summary>
        /// Test for SearchMovie method by passing proper parameter
        /// </summary>
        [TestMethod]
        public async Task SearchMovieTest_ProperParameter()
        {
            int expected = 20;
            var result = await _systemUnderTest.SearchMovie("Hello") as PartialViewResult;
            Debug.Assert(result != null, "result != null");
            Assert.AreEqual(expected,((List<Movie>)(result.Model)).Count, "SearchMovieTest_ProperParameter failed");
        }

        /// <summary>
        /// Test for SearchMovie method by passing null parameter
        /// </summary>
        [TestMethod]
        public async Task SearchMovieTest_NullParameter()
        {
            int expected = 0;
            var result = await _systemUnderTest.SearchMovie(null) as PartialViewResult;
            Debug.Assert(result != null, "result != null");
            Assert.AreEqual(expected, ((List<Movie>)result.Model).Count, "SearchMovieTest_NullParameter failed");
        }

        [TestCleanup]
        public void OnTestCleanUp()
        {
            _systemUnderTest.Dispose();
        }
    }
}