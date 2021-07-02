using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DUDS.Models
{
    [Table("tbl_erros_pagamento")]
    public partial class TblErrosPagamento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("data_agendamento", TypeName = "date")]
        public DateTime DataAgendamento { get; set; }
        [Column("cod_fundo")]
        public int CodFundo { get; set; }
        [Required]
        [Column("tipo_despesa")]
        [StringLength(50)]
        public string TipoDespesa { get; set; }
        [Column("valor_bruto")]
        public double ValorBruto { get; set; }
        [Required]
        [Column("cpf_cnpj_favorecido")]
        [StringLength(14)]
        public string CpfCnpjFavorecido { get; set; }
        [Required]
        [Column("favorecido")]
        [StringLength(50)]
        public string Favorecido { get; set; }
        [Column("conta_favorecida")]
        [StringLength(100)]
        public string ContaFavorecida { get; set; }
        [Column("competencia")]
        [StringLength(10)]
        public string Competencia { get; set; }
        [Column("status")]
        [StringLength(30)]
        public string Status { get; set; }
        [Column("cnpj_fundo_investidor")]
        [StringLength(14)]
        public string CnpjFundoInvestidor { get; set; }
        [Required]
        [Column("mensagem_erro")]
        [StringLength(100)]
        public string MensagemErro { get; set; }
    }
}
