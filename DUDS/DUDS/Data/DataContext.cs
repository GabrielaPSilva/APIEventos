using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DUDS.Models;

#nullable disable

namespace DUDS.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdministrador> TblAdministrador { get; set; }
        public virtual DbSet<TblCalculoPgtoAdmPfee> TblCalculoPgtoAdmPfee { get; set; }
        public virtual DbSet<TblCliente> TblCliente { get; set; }
        public virtual DbSet<TblCondicaoRemuneracao> TblCondicaoRemuneracao { get; set; }
        public virtual DbSet<TblContas> TblContas { get; set; }
        public virtual DbSet<TblContrato> TblContrato { get; set; }
        public virtual DbSet<TblContratoAlocador> TblContratoAlocador { get; set; }
        public virtual DbSet<TblContratoFundo> TblContratoFundo { get; set; }
        public virtual DbSet<TblContratoRemuneracao> TblContratoRemuneracao { get; set; }
        public virtual DbSet<TblCustodiante> TblCustodiante { get; set; }
        public virtual DbSet<TblDistribuidor> TblDistribuidor { get; set; }
        public virtual DbSet<TblDistribuidorAdministrador> TblDistribuidorAdministrador { get; set; }
        public virtual DbSet<TblErrosPagamento> TblErrosPagamento { get; set; }
        public virtual DbSet<TblFundo> TblFundo { get; set; }
        public virtual DbSet<TblGestor> TblGestor { get; set; }
        public virtual DbSet<TblInvestidor> TblInvestidor { get; set; }
        public virtual DbSet<TblInvestidorDistribuidor> TblInvestidorDistribuidor { get; set; }
        public virtual DbSet<TblLogErros> TblLogErros { get; set; }
        public virtual DbSet<TblPagamentoServico> TblPagamentoServico { get; set; }
        public virtual DbSet<TblPgtoAdmPfee> TblPgtoAdmPfee { get; set; }
        public virtual DbSet<TblSubContrato> TblSubContrato { get; set; }
        public virtual DbSet<TblTipoClassificacao> TblTipoClassificacao { get; set; }
        public virtual DbSet<TblTipoCondicao> TblTipoCondicao { get; set; }
        public virtual DbSet<TblTipoConta> TblTipoConta { get; set; }
        public virtual DbSet<TblTipoContrato> TblTipoContrato { get; set; }
        public virtual DbSet<TblTipoEstrategia> TblTipoEstrategia { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:srvdbdahlia02.database.windows.net;Database=db_dahlia_dev;User ID=sadb;Password=S@$NHujY&jkmjkl;");
            }
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblAdministrador>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblCalculoPgtoAdmPfee>(entity =>
            {
                entity.HasKey(e => new { e.Competencia, e.CodInvestidor, e.CodFundo });

                entity.Property(e => e.Competencia).IsFixedLength(true);

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_administrador");

                entity.HasOne(d => d.CodCondicaoRemuneracaoNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodCondicaoRemuneracao)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_condicao_remuneracao");

                entity.HasOne(d => d.CodContratoNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_contrato");

                entity.HasOne(d => d.CodContratoFundoNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodContratoFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_contrato_fundo");

                entity.HasOne(d => d.CodContratoRemuneracaoNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodContratoRemuneracao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_contrato_remuneracao");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_fundo");

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodInvestidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_investidor");

                entity.HasOne(d => d.CodSubContratoNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodSubContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_sub_contrato");
            });

            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblCliente)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_cliente_tbl_distribuidor");
            });

            modelBuilder.Entity<TblCondicaoRemuneracao>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodContratoRemuneracaoNavigation)
                    .WithMany(p => p.TblCondicaoRemuneracao)
                    .HasForeignKey(d => d.CodContratoRemuneracao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListCond_AcordoCond");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblCondicaoRemuneracao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListCond_Fundo");
            });

            modelBuilder.Entity<TblContas>(entity =>
            {
                entity.Property(e => e.Agencia).IsUnicode(false);

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Banco).IsUnicode(false);

                entity.Property(e => e.Conta).IsUnicode(false);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblContas)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_contas_tbl_fundo");

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany(p => p.TblContas)
                    .HasForeignKey(d => d.CodInvestidor)
                    .HasConstraintName("FK_tbl_contas_tbl_investidor");

                entity.HasOne(d => d.CodTipoContaNavigation)
                    .WithMany(p => p.TblContas)
                    .HasForeignKey(d => d.CodTipoConta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contas_tbl_tipo_conta");
            });

            modelBuilder.Entity<TblContrato>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblContrato)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .HasConstraintName("FK_Contrato_Distribuidor");

                entity.HasOne(d => d.CodTipoContratoNavigation)
                    .WithMany(p => p.TblContrato)
                    .HasForeignKey(d => d.CodTipoContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contrato_tbl_tipo_contrato");

                entity.HasOne(d => d.ParceiroNavigation)
                    .WithMany(p => p.TblContrato)
                    .HasForeignKey(d => d.Parceiro)
                    .HasConstraintName("FK_Contrato_Gestor");
            });

            modelBuilder.Entity<TblContratoAlocador>(entity =>
            {
                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany(p => p.TblContratoAlocador)
                    .HasForeignKey(d => d.CodInvestidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alocador_Cliente");

                entity.HasOne(d => d.CodSubContratoNavigation)
                    .WithMany(p => p.TblContratoAlocador)
                    .HasForeignKey(d => d.CodSubContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_alocador_tbl_sub_contrato");
            });

            modelBuilder.Entity<TblContratoFundo>(entity =>
            {
                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblContratoFundo)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contrato_distribuicao_tbl_fundo");

                entity.HasOne(d => d.CodSubContratoNavigation)
                    .WithMany(p => p.TblContratoFundo)
                    .HasForeignKey(d => d.CodSubContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contrato_distribuicao_tbl_sub_contrato");

                entity.HasOne(d => d.CodTipoCondicaoNavigation)
                    .WithMany(p => p.TblContratoFundo)
                    .HasForeignKey(d => d.CodTipoCondicao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contrato_fundo_tbl_tipo_condicao");
            });

            modelBuilder.Entity<TblContratoRemuneracao>(entity =>
            {
                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodContratoFundoNavigation)
                    .WithOne(p => p.TblContratoRemuneracao)
                    .HasForeignKey<TblContratoRemuneracao>(d => d.CodContratoFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contrato_remuneracao_tbl_contrato_fundo");
            });

            modelBuilder.Entity<TblCustodiante>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblDistribuidor>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CodTipoClassificacaoNavigation)
                    .WithMany(p => p.TblDistribuidor)
                    .HasForeignKey(d => d.CodTipoClassificacao)
                    .HasConstraintName("FK_Distribuidor_TipoClassificacao");
            });

            modelBuilder.Entity<TblDistribuidorAdministrador>(entity =>
            {
                entity.HasKey(e => new { e.CodDistribuidor, e.CodAdministrador });

                entity.Property(e => e.CodAdministrador).HasComment("Administrador na qual possui o valor do campo cod_distr_adm");

                entity.Property(e => e.CodDistrAdm).IsUnicode(false);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany(p => p.TblDistribuidorAdministrador)
                    .HasForeignKey(d => d.CodAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistribuidorAdministrador_Administrador");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblDistribuidorAdministrador)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistribuidorAdministrador_Distribuidor");
            });

            modelBuilder.Entity<TblErrosPagamento>(entity =>
            {
                entity.Property(e => e.CnpjFundoInvestidor).IsUnicode(false);

                entity.Property(e => e.Competencia).IsUnicode(false);

                entity.Property(e => e.ContaFavorecida).IsUnicode(false);

                entity.Property(e => e.CpfCnpjFavorecido).IsUnicode(false);

                entity.Property(e => e.Favorecido).IsUnicode(false);

                entity.Property(e => e.MensagemErro).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.TipoDespesa).IsUnicode(false);

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblErrosPagamento)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ErrosPagamento_Fundo");
            });

            modelBuilder.Entity<TblFundo>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.ContagemDiasCotizacaoAplicacao).IsFixedLength(true);

                entity.Property(e => e.ContagemDiasCotizacaoResgate).IsFixedLength(true);

                entity.Property(e => e.ContagemDiasLiquidacaoAplicacao).IsFixedLength(true);

                entity.Property(e => e.ContagemDiasLiquidacaoResgate).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Isin).IsFixedLength(true);

                entity.Property(e => e.MoedaFundo).IsFixedLength(true);

                entity.Property(e => e.TipoCota).IsFixedLength(true);

                entity.Property(e => e.UsuarioModificacao).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany(p => p.TblFundo)
                    .HasForeignKey(d => d.CodAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_fundo_tbl_administrador");

                entity.HasOne(d => d.CodCustodianteNavigation)
                    .WithMany(p => p.TblFundo)
                    .HasForeignKey(d => d.CodCustodiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_fundo_tbl_custodiante");

                entity.HasOne(d => d.CodGestorNavigation)
                    .WithMany(p => p.TblFundo)
                    .HasForeignKey(d => d.CodGestor)
                    .HasConstraintName("FK_tbl_fundo_tbl_gestor");

                entity.HasOne(d => d.CodTipoEstrategiaNavigation)
                    .WithMany(p => p.TblFundo)
                    .HasForeignKey(d => d.CodTipoEstrategia)
                    .HasConstraintName("FK_Fundo_TipoEstrategia");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.InverseMaster)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_tbl_fundo_tbl_fundo");
            });

            modelBuilder.Entity<TblGestor>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodTipoClassificacaoNavigation)
                    .WithMany(p => p.TblGestor)
                    .HasForeignKey(d => d.CodTipoClassificacao)
                    .HasConstraintName("FK_Gestor_TipoClassificacao_Gestor");
            });

            modelBuilder.Entity<TblInvestidor>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj).IsUnicode(false);

                entity.Property(e => e.CodAdministrador).HasComment("Administrador do fundo investidor");

                entity.Property(e => e.CodGestor).HasComment("Gestor do fundo investidor");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NomeCliente).IsUnicode(false);

                entity.Property(e => e.TipoCliente).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodTipoContratoNavigation)
                    .WithMany(p => p.TblInvestidor)
                    .HasForeignKey(d => d.CodTipoContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_investidor_tbl_tipo_contrato");
            });

            modelBuilder.Entity<TblInvestidorDistribuidor>(entity =>
            {
                entity.HasKey(e => new { e.CodInvestidor, e.CodDistribuidor, e.CodAdministrador });

                entity.Property(e => e.CodInvestAdministrador).IsUnicode(false);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany(p => p.TblInvestidorDistribuidor)
                    .HasForeignKey(d => d.CodAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvestidorDistribuidor_Administrador");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblInvestidorDistribuidor)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvestidorDistribuidor_Distribuidor");

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany(p => p.TblInvestidorDistribuidor)
                    .HasForeignKey(d => d.CodInvestidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvestidorDistribuidor_Investidor");
            });

            modelBuilder.Entity<TblLogErros>(entity =>
            {
                entity.Property(e => e.DataCadastro).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sistema).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblPagamentoServico>(entity =>
            {
                entity.HasKey(e => new { e.Competencia, e.CodFundo });

                entity.Property(e => e.Competencia)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPagamentoServico)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_pagamento_servico_tbl_fundo");
            });

            modelBuilder.Entity<TblPgtoAdmPfee>(entity =>
            {
                entity.HasKey(e => new { e.Competencia, e.CodInvestidorDistribuidor, e.CodFundo, e.CodAdministrador })
                    .HasName("PK_tbl_pgto_adm_pfee_1");

                entity.Property(e => e.Competencia).IsFixedLength(true);

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany(p => p.TblPgtoAdmPfee)
                    .HasForeignKey(d => d.CodAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_pgto_adm_pfee_tbl_administrador");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPgtoAdmPfee)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_pgto_adm_pfee_tbl_fundo");

                entity.HasOne(d => d.CodInvestidorDistribuidorNavigation)
                    .WithMany(p => p.TblPgtoAdmPfee)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.CodInvestidorDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_pgto_adm_pfee_tbl_investidor_distribuidor");
            });

            modelBuilder.Entity<TblSubContrato>(entity =>
            {
                entity.Property(e => e.DataInclusaoContrato).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdDocusign).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.Property(e => e.Versao).IsUnicode(false);

                entity.HasOne(d => d.CodContratoNavigation)
                    .WithMany(p => p.TblSubContrato)
                    .HasForeignKey(d => d.CodContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubContrato_Contrato");
            });

            modelBuilder.Entity<TblTipoClassificacao>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Classificacao).IsUnicode(false);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblTipoCondicao>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TipoCondicao).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblTipoConta>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DescricaoConta).IsUnicode(false);

                entity.Property(e => e.TipoConta).IsUnicode(false);
            });

            modelBuilder.Entity<TblTipoContrato>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TipoContrato).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblTipoEstrategia>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Estrategia).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
