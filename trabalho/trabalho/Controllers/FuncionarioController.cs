using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trabalho.models;
using trabalho.Dtos;
using trabalho.funcionarioSave;

namespace trabalho.Controllers
{
    [Route("Api/Funcionarios")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        private readonly funcionarioMetodos _funcionarioMetodos;

        public FuncionarioController(funcionarioMetodos metodos)
        {
            _funcionarioMetodos = metodos;
        }

        [HttpGet("Listar todos os registros")] // lista todos os funcionarios armazenados (deu certo)
        public IActionResult Get()
        {
            var funcionarios = funcionarioMetodos.Listar(); //metodo que lista todos os registros, e atribui a variavel funcionarios
            return Ok(funcionarios);
        }

        [HttpGet("procurar registros")] // Diferente do de cima ele procura registros especificos (deu certo)
        public IActionResult GetFuncionarioById(int id)
        {
            var funcionarios = funcionarioMetodos.Listar(); 
            var funcionario = funcionarios.FirstOrDefault(f => f.Id == id); //sistema de buscas por id

            if (funcionario == null) //caso id seja null, e porque nao encontrou nenhum id 
            {
                return NotFound("ID não encontrado");
            }
            else
            {
                return Ok(funcionario);

            }
        }

        [HttpPost("Adicionar registros")] //certo
        public IActionResult Post([FromBody] FuncionarioDtos funcionarioDto)
        {
            if (funcionarioDto == null) //nao pode deixar em branco o registro
            {
                return BadRequest("Dados do funcionário não podem ser nulos.");
            }
            if (validacaoCpf.ValidaCPF(funcionarioDto.CPF) == false)
            {
                return BadRequest("CPF inválido.");
            }
            else
            {
                var funcionario = funcionarioMetodos.Criar(funcionarioDto); //passou das duas vericacoes ele cria com sucesso e adiciona a lista
                return Ok(funcionario);
            }

           
        }


      
        [HttpPut("Atualizar registros")] //certo
        public IActionResult Put(int id, [FromBody] FuncionarioDtos funcionarioDto)
        {
            if (funcionarioDto == null) 
            {
                return BadRequest("Dados do funcionário não podem ser nulos.");
            }

            var funcionario = funcionarioMetodos.Atualizar(id, funcionarioDto); //chama o metodo atualizar, e passado como paramentro o id do funcionarioDto
                                                                                
            if (funcionario == null) //se o id nao for encontrado retorna null
            {
                return NotFound("Id nao encontrado");
            }
            else
            {
                return Ok(funcionario);

            }
        }

        [HttpDelete("Deletar registro")] //deu certo
        public IActionResult Delete(int id)
        {
            var sucesso = funcionarioMetodos.Deletar(id);

            if (sucesso)
            {
                return Ok("DELETADO COM SUCESSO!");
            }
            else
            {
                return Ok($"Nao foi possivel encontrar esse seguinte id: {id}");
            }




        }






    }
}