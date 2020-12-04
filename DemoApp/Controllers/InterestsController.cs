using System;
using System.Net;
using System.Threading.Tasks;
using DemoApp.Data;
using DemoApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoApp.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly ILogger<InterestsController> _logger;
        private readonly IInterestsRepository _interestsRepository;
        public InterestsController(ILogger<InterestsController> logger, IInterestsRepository interestsRepository)
        {
            _logger = logger;
            _interestsRepository = interestsRepository;
        }

        [HttpGet("interests")]
        public async Task<ActionResult<SuccessResponse>> GetAll()
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {
                if (ModelState.IsValid)
                {
                    var results = await _interestsRepository.GetAll();

                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.FailToGetData));

                _logger.LogError($"{error.RequestId} : Failed to retrieve interests, error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);

        }

        [HttpGet("users")]
        public async Task<ActionResult<SuccessResponse>> GetAllUsers()
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {
                if (ModelState.IsValid)
                {
                    var results = await _interestsRepository.GetAll();

                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.FailToGetData));

                _logger.LogError($"{error.RequestId} : Failed to retrieve interests, error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);

        }

        [HttpGet("interests/{userName}")]
        public async Task<ActionResult<SuccessResponse>> GetAllForUser(string userName)
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {
                if (ModelState.IsValid)
                {
                    var results = await _interestsRepository.GetAllForUser(userName);

                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.FailToGetData));

                _logger.LogError($"{error.RequestId} : Failed to retrieve interests for user, error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);
        }

        [HttpGet("content/{title}")]
        public async Task<ActionResult<SuccessResponse>> GetContentByTitle(string title)
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {
                if (ModelState.IsValid)
                {
                    var results = await _interestsRepository.GetContentByTitle(title);

                    return Ok(results);
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.FailToGetData));

                _logger.LogError($"{error.RequestId} : Failed to retrieve interests for user, error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);
        }

        [HttpPost("register/{userName}")]
        public async Task<ActionResult<SuccessResponse>> RegisterInterest([FromRoute]string userName, [FromBody]InterestRegistration registrationData)
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {

                if (ModelState.IsValid)
                {
                    var apiKey = await _interestsRepository.RegisterInterest(userName, registrationData);

                    return Ok(new SuccessResponse
                    {
                        ApiKey = apiKey,
                        GeneralMessage = ""
                    });
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.UnknownReason));

                _logger.LogError($"{error.RequestId} : Failed to register interests for company: {userName} with error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);

        }

        [HttpPost("interest")]
        public ActionResult<SuccessResponse> Create([FromBody] InterestContent interestData)
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {
                if (ModelState.IsValid)
                {
                    _interestsRepository.Create(interestData);

                    return Ok(new SuccessResponse
                    {
                        ApiKey = interestData.ApiKey,
                        GeneralMessage = "The records was created successsfully"
                    });
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.UnknownReason));

                _logger.LogError($"{error.RequestId} : Failed to create interest for user: {interestData.ApiKey} with error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);

        }

        [HttpPost("subscribe/{username}")]
        public ActionResult<SuccessResponse> SubscribeInterest([FromRoute] string userName, [FromBody] InterestRegistration registrationData)
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {
                if (ModelState.IsValid)
                {
                    _interestsRepository.SubscribeInterest(userName, registrationData);

                    return Ok(new SuccessResponse
                    {
                        ApiKey = null,
                        GeneralMessage = ""
                    });
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.UnknownReason));

                _logger.LogError($"{error.RequestId} : Failed to subscribe to interests for user: {userName} with error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);
        }

        [HttpPost("unsubscribe/{username}")]
        public ActionResult<SuccessResponse> UnSubscribeInterest([FromRoute] string userName, [FromBody] InterestRegistration registrationData)
        {
            var error = new ErrorResponse { RequestId = HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString() };

            try
            {
                if (ModelState.IsValid)
                {
                    _interestsRepository.UnSubscribeInterest(userName, registrationData);

                    return Ok(new SuccessResponse
                    {
                        ApiKey = null,
                        GeneralMessage = ""
                    });
                }
            }
            catch (Exception ex)
            {
                error.Errors.Add(ErrorModel.FromErrorCode(ErrorCode.UnknownReason));

                _logger.LogError($"{error.RequestId} : Failed to subscribe to interests for user: {userName} with error message: {ex.Message}", ex);

                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return BadRequest(error);
        }
    }
}