using DemoApp.Controllers;
using DemoApp.Data;
using DemoApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;
using static Xunit.Assert;

namespace DemoAppTest.Controller
{
    public class InterestControllerTest
    {
        [Fact]
        public void GetAll_Handles_Exception_Cases()
        {
            var interestsRepository = new Mock<IInterestsRepository>();
            var logger = new Mock<ILogger<InterestsController>>();

            var interestsController = new InterestsController(logger.Object, interestsRepository.Object);
            var interestsList = new List<InterestRegistration>();
            var interest1 = new InterestRegistration() { Title = "Test Interest Title1", Description = "Some description1", IsSubscribedTo = false };
            var interest2 = new InterestRegistration() { Title = "Test Interest Title2", Description = "Some description2", IsSubscribedTo = true };
            interestsList.Add(interest1);

            interestsRepository.Setup(x => x.GetAll())
                .ReturnsAsync(() => throw new Exception());

            var result = interestsController.GetAll().Result;

            //Assert
            NotNull(result);

            var routeResult = IsAssignableFrom<ObjectResult>(result.Result);

            Equal(routeResult.StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public void GetAll_Handles_Normal_Cases()
        {
            var interestsRepository = new Mock<IInterestsRepository>();
            var logger = new Mock<ILogger<InterestsController>>();

            var interestsController = new InterestsController(logger.Object, interestsRepository.Object);
            var interestsList = new List<InterestRegistration>();
            var interest1 = new InterestRegistration() { Title = "Test Interest Title1", Description = "Some description1", IsSubscribedTo = false };
            var interest2 = new InterestRegistration() { Title = "Test Interest Title2", Description = "Some description2", IsSubscribedTo = true };
            interestsList.Add(interest1);

            interestsRepository.Setup(x => x.GetAll()).ReturnsAsync(interestsList);

            var result = interestsController.GetAll().Result;

            //Assert
            NotNull(result);

            var model = IsType<ActionResult<SuccessResponse>>(result);

            var routeResult = IsAssignableFrom<OkObjectResult>(model.Result);

            var response = IsAssignableFrom<IList<InterestRegistration>>(routeResult.Value).ToList();

            Equal(routeResult.StatusCode, (int)HttpStatusCode.OK);

            Equal(interestsList.Count, response.Count);

            // Should contain what was added
            True(response.Any(a => a.Title == interest1.Title));

            // Should not contain what was not added
            False(response.Any(a => a.Title == interest2.Title));
        }
    }
}
