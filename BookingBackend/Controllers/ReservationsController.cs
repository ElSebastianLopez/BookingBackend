using ApiBook.Models.DTOs;
using BookingBackend.Data.Service.IService;
using BookingBackend.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("GetAllReservationByIdCustomer")]
        public async Task<IActionResult> GetAllReservationByIdCustomer()
        {
            Response res = new Response();
            try
            {
                int idCustomer = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                return Ok(await _reservationService.GetAllReservationByIdCustomer(idCustomer));
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        res.Message = "Registro duplicado.";
                    }
                    else
                    {
                        res.Message = $"Error en la base de datos: {sqlEx.Message}";
                    }
                }
                else
                {
                    res.Message = "Error en el servidor de base de datos, contacte a los administradores.";
                }
                res.Succes = false;
                return BadRequest(res);
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Message = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }
        }

        [HttpGet("GetByIdReservation/{id}")]
        public async Task<IActionResult> GetByIdReservation(int id)
        {

            Response res=new Response();
            try
            {
                return Ok(await _reservationService.GetByIdReservation(id));
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        res.Message = "Registro duplicado. El nombre de la máquina ya existe.";
                    }
                    else
                    {
                        res.Message = $"Error en la base de datos: {sqlEx.Message}";
                    }
                }
                else
                {
                    res.Message = "Error en el servidor de base de datos, contacte a los administradores.";
                }
                res.Succes = false;
                return BadRequest(res);
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Message = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }
        }

        [HttpPatch("AddOrEditReservation")]
        public async Task<IActionResult> AddOrEditReservation(ReservationDTO reservationDTO)
        {
            Response res = new Response();
            try
            {
                int idCustomer = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                reservationDTO.IdCustomer = idCustomer;
                return Ok(await _reservationService.AddOrEditReservation(reservationDTO));
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        res.Message = "Registro duplicado. El nombre de la máquina ya existe.";
                    }
                    else
                    {
                        res.Message = $"Error en la base de datos: {sqlEx.Message}";
                    }
                }
                else
                {
                    res.Message = "Error en el servidor de base de datos, contacte a los administradores.";
                }
                res.Succes = false;
                return BadRequest(res);
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Message = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }
        }

        [HttpDelete("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            Response res = new Response();
            try
            {
                return Ok(await _reservationService.DeleteReservation(id));
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        res.Message = "Registro duplicado. El nombre de la máquina ya existe.";
                    }
                    else
                    {
                        res.Message = $"Error en la base de datos: {sqlEx.Message}";
                    }
                }
                else
                {
                    res.Message = "Error en el servidor de base de datos, contacte a los administradores.";
                }
                res.Succes = false;
                return BadRequest(res);
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Message = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }
        }

        [HttpDelete("DeleteReservationDet/{idReservation}/{idReservationDet}")]
        public async Task<IActionResult> DeleteReservationDet(int idReservation, int idReservationDet)
        {
            Response res = new Response();
            try
            {
                return Ok(await _reservationService.DeleteReservationDet(idReservation, idReservationDet));
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        res.Message = "Registro duplicado. El nombre de la máquina ya existe.";
                    }
                    else
                    {
                        res.Message = $"Error en la base de datos: {sqlEx.Message}";
                    }
                }
                else
                {
                    res.Message = "Error en el servidor de base de datos, contacte a los administradores.";
                }
                res.Succes = false;
                return BadRequest(res);
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Message = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }
        }

        [HttpGet("CancelReservation/{idReservation}")]
        public async Task<IActionResult> CancelReservation(int idReservation)
        {
            Response res = new Response();
            try
            {
                return Ok(await _reservationService.CancelReservation(idReservation));
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        res.Message = "Registro duplicado. El nombre de la máquina ya existe.";
                    }
                    else
                    {
                        res.Message = $"Error en la base de datos: {sqlEx.Message}";
                    }
                }
                else
                {
                    res.Message = "Error en el servidor de base de datos, contacte a los administradores.";
                }
                res.Succes = false;
                return BadRequest(res);
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Message = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }
        }

        [HttpGet("BuyReservation/{idReservation}")]
        public async Task<IActionResult> BuyReservation(int idReservation)
        {
            Response res = new Response();
            try
            {
                return Ok(await _reservationService.BuyReservation(idReservation));
            }
            catch (DbUpdateException dbEx)
            {
                if (dbEx.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        res.Message = "Registro duplicado. El nombre de la máquina ya existe.";
                    }
                    else
                    {
                        res.Message = $"Error en la base de datos: {sqlEx.Message}";
                    }
                }
                else
                {
                    res.Message = "Error en el servidor de base de datos, contacte a los administradores.";
                }
                res.Succes = false;
                return BadRequest(res);
            }
            catch (InvalidOperationException ex)
            {
                res.Succes = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception)
            {
                res.Succes = false;
                res.Message = "Error en el servidor, Contactese con los administradores de el aplicativo";

                return StatusCode(500, res);
            }
        }

    }
}
