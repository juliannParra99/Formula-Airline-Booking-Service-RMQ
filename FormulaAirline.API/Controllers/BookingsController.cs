using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FormulaAirline.API.Controllers
{
    /// Handles HTTP POST requests to create a new booking. 
    /// Validates the incoming booking data, stores it in an in-memory list, 
    /// and sends the booking information to a RabbitMQ queue for further processing.
    /// Returns an HTTP 200 OK response if successful, or a 400 Bad Request if the model is invalid.
    [Route("[controller]")]
    public class BookingsController : Controller
    {
        private readonly ILogger<BookingsController> _logger;

        private readonly IMessageProducer _messageProducer;

        public static readonly List<Booking> _bookings = new List<Booking>();

        public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }


        [HttpPost]
        public IActionResult CreatingBooking([FromBody] Booking newBooking)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _bookings.Add(newBooking);
            _messageProducer.SendingMessage(newBooking);
            return Ok();
        }
    }
}