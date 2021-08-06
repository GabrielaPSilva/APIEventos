using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class Mensagem
    {
        public const string ErroPadrao = "Ops! Ocorreu um erro.";
        public const string ErroCadastrar = "Erro ao cadastrar.";
        public const string ErroAtualizar = "Erro ao atualizar.";
        public const string ErroExcluir = "Erro ao excluir.";
        public const string ErroDesativar = "Erro ao desativar.";
        public const string ErroTipoInvalido = "Opção de pesquisa não encontrada ou inválida.";
        public const string SucessoListar = "Listado com sucesso.";
        public const string SucessoCadastrado = "Cadastrado com sucesso.";
        public const string SucessoAtualizado = "Atualizado com sucesso.";
        public const string SucessoExcluido = "Excluido com sucesso.";
        public const string SucessoDesativado = "Desativado com sucesso.";
        public const string ExisteRegistroDesativar = "Erro ao desativar, esse código possui registro em outras tabelas.";
        public const string ExisteRegistroExcluir = "Erro ao excluir, esse código possui registro em outras tabelas.";
    }
}
