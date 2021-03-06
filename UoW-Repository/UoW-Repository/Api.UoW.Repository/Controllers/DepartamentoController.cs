using Api.UoW.Repository.Data;
using Api.UoW.Repository.Data.Repositories;
using Api.UoW.Repository.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
       // private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IUnitOfWork _uow;
        public DepartamentoController
        (
            ILogger<DepartamentoController> logger,
            //IDepartamentoRepository repository,
            IUnitOfWork uow
         )
        {
            _logger = logger;
           // _departamentoRepository = repository;
            _uow = uow;
        }
        /// <summary>
        /// Departamento/1
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)//[FromSe]IDepartamentoRepository repository
        {
            // var departamento = await _departamentoRepository.GetByIdAsync(id);
            var departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);
            return Ok(departamento);
        }

        [HttpPost]
        public  IActionResult CreateDepartamentoAsync(Departamento departamento)//[FromSe]IDepartamentoRepository repository
        {
            //_departamentoRepository.Add(departamento);
            _uow.DepartamentoRepository.Add(departamento);
            //var saved = _departamentoRepository.Save();
            _uow.Commit();
            return Ok(departamento);
        }

        //departamento/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDepartamentoAsync(int id)
        {
            var departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);

            _uow.DepartamentoRepository.Remove(departamento);

            _uow.Commit();

            return Ok(departamento);
        }

        //departamento/?descricao=teste
        [HttpGet]
        public async Task<IActionResult> ConsultarDepartamentoAsync([FromQuery] string descricao)
        {
            var departamentos = await _uow.DepartamentoRepository
                                         .GetDataAsync(p =>
                                            p.Descricao.Contains(descricao),
                                            p=> p.Include(c =>c.Colaboradores),
                                            take:2
                                            );


            return Ok(departamentos);
        }
    }
}
