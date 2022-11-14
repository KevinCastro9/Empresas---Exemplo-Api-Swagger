using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repository;
using Models.Empresas;
using Models.Grupo;
using Views.Grupo;
using Views.Empresas;


namespace DesafioDotNet.Controllers
{
    [ApiController]
    [Route("DesafioDotNet/v1")]
    public class PrincipalController : Controller
    {
        [HttpGet]
        [Route("empresa/{_id}")]
        public IActionResult GetEmpresaId([FromRoute] string _id)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();
                Empresas empresa = principal.SelectEmpresasId(_id);

                if (empresa != null) { return Ok(empresa); } else { return NotFound(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("grupo/{_id}")]
        public IActionResult GetGrupoId([FromRoute] int _id)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();
                Grupo grupo = principal.SelectGrupoId(_id);

                if (grupo != null) { return Ok(grupo); } else { return NotFound(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("grupos/{_datetime}")]
        public IActionResult GetGrupos([FromRoute] DateTime _datetime)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();
                List<Empresas> empresas = principal.SelectEmpresasDate(_datetime);

                if (empresas.Count > 0) { return Ok(empresas); } else { return NotFound(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("grupo")]
        public IActionResult PostGrupo([FromBody] GrupoView grupoView)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();

                bool retorno = principal.InsertGrupo(grupoView);

                if (retorno == true) { return Created("grupo", grupoView); } else { return BadRequest(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("empresa")]
        public IActionResult PostEmpresa([FromBody] EmpresaView empresaView)
        {
            try
            {
                if (empresaView.Status != "ATIVO" && empresaView.Status != "INATIVO")
                {
                    return BadRequest();
                }

                PrincipalRepository principal = new PrincipalRepository();

                bool retorno = principal.InsertEmpresa(empresaView);

                if (retorno == true) { return Created("empresa", empresaView); } else { return BadRequest(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("empresa/custos/{_id}")]
        public IActionResult PutEmpresaCustoId([FromBody] Custos custos, [FromRoute] string _id)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();

                bool retorno = principal.UpdateEmpresaCustos(_id, custos);

                if (retorno == true) { return Created("empresa", custos); } else { return BadRequest(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("grupo/{_idGrupo}")]
        public IActionResult PutGrupoId([FromRoute] int _idGrupo, [FromQuery] string _idEmpresa)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();
                bool retorno = principal.UpdateGrupoEmpresas(_idGrupo, _idEmpresa);

                if (retorno == true) { return Created("grupo", retorno); } else { return BadRequest(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("empresa/{_id}")]
        public IActionResult DeleteEmpresaId([FromRoute] string _id)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();

                bool retorno = principal.DeleteEmpresa(_id);

                if (retorno == true) { return Created("empresa", retorno); } else { return BadRequest(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("grupo/custos/{_id}")]
        public IActionResult GetGrupoCustoId([FromRoute] int _id)
        {
            try
            {
                PrincipalRepository principal = new PrincipalRepository();
                Grupo grupo = principal.SelectGrupoId(_id);

                if (grupo == null)
                {
                    return BadRequest();
                }

                TiposCustosView tiposCustosView = principal.SelectGrupoCustos(_id);

                return Ok(tiposCustosView);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
