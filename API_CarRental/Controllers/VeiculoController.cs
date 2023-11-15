using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;

namespace API_CarRental.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VeiculoController : ControllerBase
    {

        [HttpGet(Name = "consultaVeiculo")]
        public List<Veiculo> Get()
        {
            // string de conexao
            string stringConexao = "Server=localhost; Port=5432; " +
                                "User Id=postgres; Password=12345678; DataBase=CarRental;";

            // objeto de conexao
            NpgsqlConnection con = new NpgsqlConnection(stringConexao);

            // instrucao sql para o banco de dados
            string instrucao = "SELECT * FROM veiculo " +
                "order by codigo";

            DataTable dt = new DataTable(); // tabela virtual pra armazenar resultado

            NpgsqlCommand cmd = new NpgsqlCommand(instrucao, con); // passa por parametro a instrucao sql

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd); // nunca muda

            da.Fill(dt); // preenche data table com resultado

            // Fecha conexao com o banco
            con.Close();
            con.Dispose();

          var lista = new List<Veiculo>();

            foreach (DataRow linha in dt.Rows)
            {
                var Veiculo = new Veiculo()
                {
                    codigo = int.Parse(linha["codigo"].ToString()),
                    marca = linha["marca"].ToString(), // banco de dados
                    modelo = linha["modelo"].ToString(),
                    cor = linha["cor"].ToString(),
                    placa = linha["placa"].ToString()
                };

                lista.Add(Veiculo);
            }
            return lista;
        }

     
    }

    
}
