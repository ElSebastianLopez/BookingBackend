using ApiBook.Models.DTOs;
using BookingBackend.Data.Service;
using BookingBackend.Data.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        public readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            Response res = new Response();
            try
            {
                return Ok(await _customerService.GetAllCustomer());
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

        [HttpGet("GetByIdService/{id}")]
        public async Task<IActionResult> GetByIdService(int id)
        {
            Response res = new Response();
            try
            {
                return Ok(await _customerService.GetByIdCustomer(id));
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
    }
}
