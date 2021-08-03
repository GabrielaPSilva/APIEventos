using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DUDS.Models
{
    public class Mensagem
    {
        public const string ErroPadrao = "Ops! Ocorreu um erro.";
        public const string ErroCadastrarRelatorio = "Erro ao cadastrar relatório.";
        public const string ErroAtualizarRelatorio = "Erro ao atualizar relatório.";
        public const string ErroExcluirRelatorio = "Erro ao excluir relatório.";
        public const string ErroCadastrarColuna = "Erro ao cadastrar coluna de configuração.";
        public const string ErroAtualizarColuna = "Erro ao atualizar coluna de configuração.";
        public const string ErroTipoInvalido = "Opção de filtro não encontrado ou inválido.";
        public const string RelatorioCadastrado = "Relatório cadastrado com sucesso.";
        public const string RelatorioAtualizado = "Relatório atualizado com sucesso.";
        public const string RelatorioExcluido = "Relatório excluido com sucesso.";
        public const string ColunaCadastrada = "Coluna de configuração cadastrada com sucesso.";
        public const string ColunaAtualizada = "Coluna de configuração atualizada com sucesso.";
        public const string LogAtualizarGeradorRelatorio = "Erro ao atualizar tabela GeradorRelatorio.";
        public const string LogAtualizarRelatorioConfiguracao = "Erro ao atualizar tabela GeradorRelatorioConfiguracao.";
        public const string LogCadastrarGeradorRelatorio = "Erro ao inserir registro na tabela GeradorRelatorio.";
        public const string LogCadastrarRelatorioConfiguracao = "Erro ao inserir registro na tabela GeradorRelatorioConfiguracao.";
        public const string LogDesativarRelatorio = "Erro ao desativar relatório na tabela GeradorRelatorio.";
    }
}
