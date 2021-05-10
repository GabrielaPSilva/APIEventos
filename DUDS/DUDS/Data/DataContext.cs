﻿using System;
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

        public virtual DbSet<TblAcordoDistribuicao> TblAcordoDistribuicao { get; set; }
        public virtual DbSet<TblAdministrador> TblAdministrador { get; set; }
        public virtual DbSet<TblCliente> TblCliente { get; set; }
        public virtual DbSet<TblContas> TblContas { get; set; }
        public virtual DbSet<TblCustodiante> TblCustodiante { get; set; }
        public virtual DbSet<TblDeparaFundoproduto> TblDeparaFundoproduto { get; set; }
        public virtual DbSet<TblDistribuidor> TblDistribuidor { get; set; }
        public virtual DbSet<TblFeriadosAnbima> TblFeriadosAnbima { get; set; }
        public virtual DbSet<TblFundo> TblFundo { get; set; }
        public virtual DbSet<TblGestor> TblGestor { get; set; }
        public virtual DbSet<TblMovimentacaoNota> TblMovimentacaoNota { get; set; }
        public virtual DbSet<TblOrdemPassivo> TblOrdemPassivo { get; set; }
        public virtual DbSet<TblPosicaoAcao> TblPosicaoAcao { get; set; }
        public virtual DbSet<TblPosicaoAdr> TblPosicaoAdr { get; set; }
        public virtual DbSet<TblPosicaoBdr> TblPosicaoBdr { get; set; }
        public virtual DbSet<TblPosicaoCambio> TblPosicaoCambio { get; set; }
        public virtual DbSet<TblPosicaoCliente> TblPosicaoCliente { get; set; }
        public virtual DbSet<TblPosicaoContacorrente> TblPosicaoContacorrente { get; set; }
        public virtual DbSet<TblPosicaoCotaFundo> TblPosicaoCotaFundo { get; set; }
        public virtual DbSet<TblPosicaoCpr> TblPosicaoCpr { get; set; }
        public virtual DbSet<TblPosicaoEmprAcao> TblPosicaoEmprAcao { get; set; }
        public virtual DbSet<TblPosicaoFuturo> TblPosicaoFuturo { get; set; }
        public virtual DbSet<TblPosicaoOpcaoAcao> TblPosicaoOpcaoAcao { get; set; }
        public virtual DbSet<TblPosicaoOpcaoFuturo> TblPosicaoOpcaoFuturo { get; set; }
        public virtual DbSet<TblPosicaoPatrimonio> TblPosicaoPatrimonio { get; set; }
        public virtual DbSet<TblPosicaoRendafixa> TblPosicaoRendafixa { get; set; }
        public virtual DbSet<TblPosicaoRentabilidade> TblPosicaoRentabilidade { get; set; }
        public virtual DbSet<TblPosicaoTesouraria> TblPosicaoTesouraria { get; set; }
        public virtual DbSet<TblTipoConta> TblTipoConta { get; set; }
        public virtual DbSet<TblXmlAnbimaAcoes> TblXmlAnbimaAcoes { get; set; }
        public virtual DbSet<TblXmlAnbimaCaixa> TblXmlAnbimaCaixa { get; set; }
        public virtual DbSet<TblXmlAnbimaCorretagem> TblXmlAnbimaCorretagem { get; set; }
        public virtual DbSet<TblXmlAnbimaCotas> TblXmlAnbimaCotas { get; set; }
        public virtual DbSet<TblXmlAnbimaDebenture> TblXmlAnbimaDebenture { get; set; }
        public virtual DbSet<TblXmlAnbimaDespesas> TblXmlAnbimaDespesas { get; set; }
        public virtual DbSet<TblXmlAnbimaForwardsmoedas> TblXmlAnbimaForwardsmoedas { get; set; }
        public virtual DbSet<TblXmlAnbimaFuturos> TblXmlAnbimaFuturos { get; set; }
        public virtual DbSet<TblXmlAnbimaHeader> TblXmlAnbimaHeader { get; set; }
        public virtual DbSet<TblXmlAnbimaOpcoesacoes> TblXmlAnbimaOpcoesacoes { get; set; }
        public virtual DbSet<TblXmlAnbimaOutrasdespesas> TblXmlAnbimaOutrasdespesas { get; set; }
        public virtual DbSet<TblXmlAnbimaProvisao> TblXmlAnbimaProvisao { get; set; }
        public virtual DbSet<TblXmlAnbimaTitprivado> TblXmlAnbimaTitprivado { get; set; }
        public virtual DbSet<TblXmlAnbimaTitpublico> TblXmlAnbimaTitpublico { get; set; }

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

            modelBuilder.Entity<TblAcordoDistribuicao>(entity =>
            {
                entity.HasKey(e => new { e.CodCliente, e.CodFundo, e.CodDistribuidor });

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblAdministrador>(entity =>
            {
                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblClienteCodDistribuidorNavigation)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_cliente_tbl_distribuidor");

                entity.HasOne(d => d.CodParceiroNavigation)
                    .WithMany(p => p.TblClienteCodParceiroNavigation)
                    .HasForeignKey(d => d.CodParceiro)
                    .HasConstraintName("FK_tbl_cliente_tbl_distribuidor1");
            });

            modelBuilder.Entity<TblContas>(entity =>
            {
                entity.HasKey(e => new { e.CodFundo, e.CodTipoConta });

                entity.Property(e => e.Agencia).IsUnicode(false);

                entity.Property(e => e.Banco).IsUnicode(false);

                entity.Property(e => e.Conta).IsUnicode(false);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblContas)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contas_tbl_fundo");

                entity.HasOne(d => d.CodTipoContaNavigation)
                    .WithMany(p => p.TblContas)
                    .HasForeignKey(d => d.CodTipoConta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_contas_tbl_tipo_conta");
            });

            modelBuilder.Entity<TblCustodiante>(entity =>
            {
                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblDeparaFundoproduto>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CodCustodianteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodCustodiante)
                    .HasConstraintName("FK_tbl_depara_fundoproduto_tbl_custodiante");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_depara_fundoproduto_tbl_fundo");
            });

            modelBuilder.Entity<TblDistribuidor>(entity =>
            {
                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblFundo>(entity =>
            {
                entity.Property(e => e.ContagemDiasCotizacaoAplicacao).IsFixedLength(true);

                entity.Property(e => e.ContagemDiasCotizacaoResgate).IsFixedLength(true);

                entity.Property(e => e.ContagemDiasLiquidacaoAplicacao).IsFixedLength(true);

                entity.Property(e => e.ContagemDiasLiquidacaoResgate).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Isin).IsFixedLength(true);

                entity.Property(e => e.MoedaFundo).IsFixedLength(true);

                entity.Property(e => e.TipoCota).IsFixedLength(true);

                entity.Property(e => e.UsuariaModificacao).HasDefaultValueSql("((0))");

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

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.InverseMaster)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_tbl_fundo_tbl_fundo");
            });

            modelBuilder.Entity<TblGestor>(entity =>
            {
                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblMovimentacaoNota>(entity =>
            {
                entity.HasKey(e => new { e.CodMovimentacao, e.NotaAplicacao, e.NumOrdem, e.CodOrdemMae })
                    .HasName("PK_tbl_movimentacao_nota_1");

                entity.Property(e => e.Operador).IsFixedLength(true);

                entity.Property(e => e.Penalty).IsFixedLength(true);

                entity.Property(e => e.TipoMovimentacao).IsFixedLength(true);

                entity.HasOne(d => d.CdCotistaNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CdCotista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_cliente");

                entity.HasOne(d => d.CodAdmNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CodAdm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_administrador");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_distribuidor");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_fundo");

                entity.HasOne(d => d.CodGestorNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CodGestor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_gestor");
            });

            modelBuilder.Entity<TblOrdemPassivo>(entity =>
            {
                entity.Property(e => e.NumOrdem).ValueGeneratedNever();

                entity.Property(e => e.DsLiquidacao).IsFixedLength(true);

                entity.Property(e => e.DsOperacao).IsFixedLength(true);

                entity.Property(e => e.NmOperador).IsFixedLength(true);

                entity.Property(e => e.Penalty).IsFixedLength(true);

                entity.Property(e => e.SnBloqueio).IsFixedLength(true);

                entity.HasOne(d => d.CdCotistaNavigation)
                    .WithMany(p => p.TblOrdemPassivo)
                    .HasForeignKey(d => d.CdCotista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ordem_passivo_tbl_cliente");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblOrdemPassivo)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ordem_passivo_tbl_distribuidor");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblOrdemPassivo)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ordem_passivo_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoAcao>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoAcao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_acao_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoAdr>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoAdr)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_adr_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoBdr>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoBdr)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_bdr_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoCambio>(entity =>
            {
                entity.Property(e => e.MoedaComprada).IsFixedLength(true);

                entity.Property(e => e.MoedaVendida).IsFixedLength(true);

                entity.Property(e => e.Tipo).IsFixedLength(true);

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoCambio)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cambio_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoCliente>(entity =>
            {
                entity.HasKey(e => new { e.CodCliente, e.CodFundo, e.CodDistribuidor, e.DataRef });

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.TblPosicaoCliente)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cliente_tbl_cliente");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblPosicaoCliente)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cliente_tbl_distribuidor");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoCliente)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cliente_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoContacorrente>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoContacorrente)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_contacorrente_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoCotaFundo>(entity =>
            {
                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoCotaFundo)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cota_fundo_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoCpr>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoCpr)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cpr_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoEmprAcao>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoEmprAcao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_empr_acao_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoFuturo>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoFuturo)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_futuro_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoOpcaoAcao>(entity =>
            {
                entity.Property(e => e.Praca).IsFixedLength(true);

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoOpcaoAcao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_opcao_acao_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoOpcaoFuturo>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoOpcaoFuturo)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_opcao_futuro_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoPatrimonio>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoPatrimonio)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_patrimonio_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoRendafixa>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoRendafixa)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_rendafixa_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoRentabilidade>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoRentabilidade)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_rentabilidade_fundo_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoTesouraria>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoTesouraria)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_tesouraria_tbl_fundo");
            });

            modelBuilder.Entity<TblTipoConta>(entity =>
            {
                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DescricaoConta).IsUnicode(false);

                entity.Property(e => e.TipoConta).IsUnicode(false);
            });

            modelBuilder.Entity<TblXmlAnbimaAcoes>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaAcoes)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_acoes_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaCaixa>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaCaixa)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_caixa_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaCorretagem>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaCorretagem)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_corretagem_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaCotas>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaCotas)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_cotas_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaDebenture>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaDebenture)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_debenture_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaDespesas>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaDespesas)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_despesas_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaForwardsmoedas>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaForwardsmoedas)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_forwardsmoedas_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaFuturos>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaFuturos)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_futuros_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaHeader>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaHeader)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_header_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaOpcoesacoes>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaOpcoesacoes)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_opcoesacoes_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaOutrasdespesas>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaOutrasdespesas)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_outrasdespesas_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaProvisao>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaProvisao)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_provisao_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaTitprivado>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaTitprivado)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_titprivado_tbl_fundo");
            });

            modelBuilder.Entity<TblXmlAnbimaTitpublico>(entity =>
            {
                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblXmlAnbimaTitpublico)
                    .HasForeignKey(d => d.CodFundo)
                    .HasConstraintName("FK_tbl_xmlAnbima_titpublico_tbl_fundo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
