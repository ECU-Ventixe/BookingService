using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(HttpClient httpClient) : ControllerBase
    {
        private readonly HttpClient _httpClient = httpClient;

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] TicketDto ticket)
        {
            if (ticket == null)
            {
                return BadRequest("Booking cannot be null.");
            }
            var eventResponse = await _httpClient.GetAsync($"https://ventixe-event-ecu-dvddbqcpewahfdcz.swedencentral-01.azurewebsites.net/api/event/getevent/{ticket.EventId}");
            
            var eventContent = await eventResponse.Content.ReadFromJsonAsync<EventDto>();

            if (eventContent == null)
            {
                return NotFound("Event not found.");
            }

            if (eventContent.TicketAmount < ticket.TicketQuantity)
            {
                return BadRequest("Not enough tickets available for this event.");
            }

            var ticketDto = new TicketDto
            {
                EventId = ticket.EventId,
                UserId = ticket.UserId,
                FirstName = ticket.FirstName,
                LastName = ticket.LastName,
                Email = ticket.Email,
                PhoneNumber = ticket.PhoneNumber,
                PostalCode = ticket.PostalCode,
                Address = ticket.Address,
                TicketQuantity = ticket.TicketQuantity
            };

            var ticketResponse = await _httpClient.PostAsJsonAsync("https://ventixe-ticket-ecu-bpbqcchqddg6ath9.swedencentral-01.azurewebsites.net/api/ticket/create", ticketDto);
            if (!ticketResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)ticketResponse.StatusCode, "Failed to create ticket.");
            }

            return Ok();
        }
    }
}
