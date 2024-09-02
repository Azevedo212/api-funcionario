using trabalho.models;
using trabalho.Dtos;

namespace trabalho.funcionarioSave
{
    public class funcionarioMetodos
    {

        private const string diretorio = "D:\\IFRO\\3°Ano\\PDS\\trabalho\\funcionarios.txt";

        public static List<funcionario> Listar()
        {
            var funcionariosList = new List<funcionario>();

            if (File.Exists(diretorio))
            {
                var linhas = File.ReadAllLines(diretorio);
                foreach (var line in linhas) //vai percorer todas as linhas
                {
                    var espaco = line.Split('|'); //forma como esta sendo separado

                    var Funcionario = new funcionario
                    {
                        Id = int.Parse(espaco[0]),
                        Nome = espaco[1],
                        CPF = espaco[2],
                        CTPS = espaco[3],
                        RG = espaco[4],
                        Funcao = espaco[5],
                        Setor = espaco[6],
                        Sala = espaco[7],
                        Telefone = espaco[8],
                        UF = espaco[9],
                        Cidade = espaco[10],
                        Bairro = espaco[11],
                        Numero = espaco[12],
                        CEP = espaco[13]
                    };

                    funcionariosList.Add(Funcionario); //apos percorrer cada linha o programa adiciona no funcionariosList, isso ate percorrer todos os registros
                }
            }
            return funcionariosList; //por fim ele retorna
        }


        public static funcionario Criar(FuncionarioDtos funcionarioDto)
        {
            var IdSoma = 0;
            foreach (var a in Listar()/*esse metodo retorna uma lista de todos os registros no arquivo*/) 
            {
                if (IdSoma < a.Id) //verifica se o valor do IdSoma do funcionario atual e menor que o do id, se for ele atualiza
                                   //para o mais recente, e faz a soma +1 logo abaixo
                {
                    IdSoma = a.Id;
                }
            }
            var Funcionario = new funcionario
            {
                Id = IdSoma + 1, //toda vez que criar um registro ele ira pegar o ultimo id e somar + 1
                Nome = funcionarioDto.Nome,
                CPF = funcionarioDto.CPF,
                CTPS = funcionarioDto.CTPS,
                RG = funcionarioDto.RG,
                Funcao = funcionarioDto.Funcao,
                Setor = funcionarioDto.Setor,
                Sala = funcionarioDto.Sala,
                Telefone = funcionarioDto.Telefone,
                UF = funcionarioDto.UF,
                Cidade = funcionarioDto.Cidade,
                Bairro = funcionarioDto.Bairro,
                Numero = funcionarioDto.Numero,
                CEP = funcionarioDto.CEP
            };
            using (var writer = new StreamWriter(diretorio, true)) //o true nao ira sobreescrever e sim adicionar um novo registro ao fim do arquivo
            {
                var line = ($"{Funcionario.Id} | {Funcionario.Nome} | {Funcionario.CPF} | {Funcionario.CTPS} | {Funcionario.RG} | " +
                         $"{Funcionario.Funcao} | {Funcionario.Setor} | {Funcionario.Sala} | {Funcionario.Telefone} | " +
                         $"{Funcionario.UF} | {Funcionario.Cidade} | {Funcionario.Bairro} | {Funcionario.Numero} | {Funcionario.CEP} |");
                writer.WriteLine(line);
            }
            return Funcionario;
        }

       
        public static bool Deletar(int id)
        {
            var funcionarios = Listar();

            //elemento de busca c.Id == id verifica se o campo Id do objeto c é igual ao valor de id fornecido como argumento.
            int funcionario = funcionarios.RemoveAll(c => c.Id == id); //c representa cada elemento ou objeto funcionario.
           //se o id do funcionario for igual ao fornecido ira remover o funcionario.


            if (funcionario == 0) //se nao for compativel com o id fornecido (0), nenhum registro sera removido.
            {
                return false;

            }
  
            Salvar(funcionarios);
            return true;
        }

        public static funcionario Atualizar(int id, FuncionarioDtos funcionarioDto)
        {
            var funcionarios = Listar();
            var funcionarioExistente = funcionarios.FirstOrDefault(c => c.Id == id);

            if (funcionarioExistente == null)
            {
                return null;
            }

            // Atualiza os campos do objeto existente
            funcionarioExistente.Nome = funcionarioDto.Nome;
            funcionarioExistente.CPF = funcionarioDto.CPF;
            funcionarioExistente.CTPS = funcionarioDto.CTPS;
            funcionarioExistente.RG = funcionarioDto.RG;
            funcionarioExistente.Funcao = funcionarioDto.Funcao;
            funcionarioExistente.Setor = funcionarioDto.Setor;
            funcionarioExistente.Sala = funcionarioDto.Sala;
            funcionarioExistente.Telefone = funcionarioDto.Telefone;
            funcionarioExistente.UF = funcionarioDto.UF;
            funcionarioExistente.Cidade = funcionarioDto.Cidade;
            funcionarioExistente.Bairro = funcionarioDto.Bairro;
            funcionarioExistente.Numero = funcionarioDto.Numero;
            funcionarioExistente.CEP = funcionarioDto.CEP;

            // Salva a lista atualizada
            Salvar(funcionarios);

            return funcionarioExistente;
        }

        private static void Salvar(List<funcionario> funcionarios)
        {
            using (var writer = new StreamWriter(diretorio, false)) //false indica que o arquivo sera sobreescrito, não irá adicionar
                                                                    //novas linhas ao arquivo existente, mas sim recriar o arquivo a partir do zero
                                                                    //usar o true ira ocorrer duplicacao de dados, pois ele adiciona conteudo e nao sobreescreve
            {
                foreach (var funcionario in funcionarios) //percoreendo todos os dados
                {
                    var line = ($"{funcionario.Id} | {funcionario.Nome} | {funcionario.CPF} | {funcionario.CTPS} | {funcionario.RG} | " +
                          $"{funcionario.Funcao} | {funcionario.Setor} | {funcionario.Sala} | {funcionario.Telefone} | " +
                          $"{funcionario.UF} | {funcionario.Cidade} | {funcionario.Bairro} | {funcionario.Numero} | {funcionario.CEP} | ");

                    writer.WriteLine(line);
                }
            }
        }

    }
}

      
