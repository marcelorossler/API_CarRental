using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;

namespace API_CarRental.Controllers;

[ApiController]
[Route("[controller]")]

public class ClienteController : ControllerBase
{

    [HttpGet(Name = "ConsultaCliente")]
    public List<ConsultaCliente> Get()
    {
        // string de conexao
        string stringConexao = "Server=localhost; Port=5432; " +
                            "User Id=postgres; Password=12345678; DataBase=CarRental;";
        // objeto de conexao
        NpgsqlConnection con = new NpgsqlConnection(stringConexao);

        // instrucao sql para o banco de dados
        string instrucao = "SELECT * FROM clientes " +
            "order by codigo";

        DataTable dt = new DataTable(); // tabela virtual pra armazenar resultado

        NpgsqlCommand cmd = new NpgsqlCommand(instrucao, con); // passa por parametro a instrucao sql

        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd); // nunca muda

        da.Fill(dt); // preenche data table com resultado

        // Fecha conexao com o banco
        con.Close();
        con.Dispose();

        var lista = new List<ConsultaCliente>();

        foreach (DataRow linha in dt.Rows)
        {
            var NovoCliente = new ConsultaCliente()
            {
                codigo = int.Parse(linha["codigo"].ToString()),
                nome = linha["nome"].ToString(),
                cpf = linha["cpf"].ToString(), // banco de dados
                nascimento = linha["nascimento"].ToString(),
                endereco = linha["endereco"].ToString(),
            };

            lista.Add(NovoCliente);
        }
        return lista;
    }

}

