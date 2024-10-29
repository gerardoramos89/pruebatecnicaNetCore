using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Domain.Entitys;
using ECommerce.Api.Infra.Repositories.ReadRepositories;
using ECommerce.Api.Infra.Repositories.WriteRepositories;
using ECommerce.Api.Infra.Repositories.ReadRepositories.CustomerReadRepositories;
using ECommerce.Api.Domain; // Usa el espacio de nombres correcto donde esté definida tu interfaz

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IReservationReadRepository _reservationReadRepository;

        public ReservationController(IReservationWriteRepository reservationWriteRepository, IReservationReadRepository reservationReadRepository)
        {
            _reservationWriteRepository = reservationWriteRepository;
            _reservationReadRepository = reservationReadRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            await _reservationWriteRepository.CreateAsync(reservation);
            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> Get()
        {
            return await _reservationReadRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> Get(int id)
        {
            var reservation = await _reservationReadRepository.GetAsync(id);
            if (reservation == null)
                return NotFound();

            return reservation;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
                return BadRequest();

            var result = await _reservationWriteRepository.UpdateAsync(reservation);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reservationWriteRepository.RemoveAsync(id);
            return NoContent();
        }

        [HttpGet("byCustomer/{customerId}")]
        public async Task<ActionResult<List<Reservation>>> GetByCustomerId(int customerId)
        {
            return await _reservationReadRepository.GetByCustomerIdAsync(customerId);
        }
    }
}
