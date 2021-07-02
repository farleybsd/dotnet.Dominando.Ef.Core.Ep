using Api.UoW.Repository.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentoController : ControllerBase
    {

        private readonly ILogger<DepartamentoController> _logger;
        private readonly IDepartamentoRepository _departamentoRepository;
        public DepartamentoController
        (
            ILogger<DepartamentoController> logger,
            IDepartamentoRepository repository
         )
        {
            _logger = logger;
            _departamentoRepository = repository;
        }
        /// <summary>
        /// Departamento/1
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)//[FromSe]IDepartamentoRepository repository
        {
            var departamento = await _departamentoRepository.GetByIdAsync(id);
            return Ok(departamento);
        }
    }
}
