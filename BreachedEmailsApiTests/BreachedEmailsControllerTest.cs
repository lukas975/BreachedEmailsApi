using BreachedEmailsApi.Controllers;
using BreachedEmailsApi.Service;
using Xunit;

namespace BreachedEmailsApiTests
{
    public class BreachedEmailsControllerTest
    {
        BreachedEmailsController _controller;
        IBreachedEmailsService _service;

        public BreachedEmailsControllerTest() 
        {
            _service = new BreachedEmailsServiceFake();
            _controller = new BreachedEmailsController(_service);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
