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

        public virtual DbSet<AuditEmployeeData> AuditEmployeeData { get; set; }
        public virtual DbSet<Auditoria> Auditoria { get; set; }
        public virtual DbSet<EmployeeData> EmployeeData { get; set; }
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
        public virtual DbSet<TblDeparaFundoproduto> TblDeparaFundoproduto { get; set; }
        public virtual DbSet<TblDistribuidor> TblDistribuidor { get; set; }
        public virtual DbSet<TblDistribuidorAdministrador> TblDistribuidorAdministrador { get; set; }
        public virtual DbSet<TblErrosPagamento> TblErrosPagamento { get; set; }
        public virtual DbSet<TblFeriadosAnbima> TblFeriadosAnbima { get; set; }
        public virtual DbSet<TblFundo> TblFundo { get; set; }
        public virtual DbSet<TblGestor> TblGestor { get; set; }
        public virtual DbSet<TblInvestidor> TblInvestidor { get; set; }
        public virtual DbSet<TblInvestidorDistribuidor> TblInvestidorDistribuidor { get; set; }
        public virtual DbSet<TblLogErros> TblLogErros { get; set; }
        public virtual DbSet<TblMovimentacaoNota> TblMovimentacaoNota { get; set; }
        public virtual DbSet<TblOrdemPassivo> TblOrdemPassivo { get; set; }
        public virtual DbSet<TblPagamentoServico> TblPagamentoServico { get; set; }
        public virtual DbSet<TblPgtoAdmPfee> TblPgtoAdmPfee { get; set; }
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
        public virtual DbSet<TblSubContrato> TblSubContrato { get; set; }
        public virtual DbSet<TblTipoCondicao> TblTipoCondicao { get; set; }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:srvdbdahlia02.database.windows.net;Database=db_dahlia_dev;User ID=sadb;Password=S@$NHujY&jkmjkl;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AuditEmployeeData>(entity =>
            {
                entity.HasKey(e => e.AuditLogId)
                    .HasName("PK__auditEmp__6031F9F888C749FC");

                entity.Property(e => e.AuditLogId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AuditChanged).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AuditEmpBankAccountNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.AuditEmpSsn)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.AuditLogType)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.AuditUser).HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Auditoria>(entity =>
            {
                entity.Property(e => e.Acao)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Campo).IsUnicode(false);

                entity.Property(e => e.Conteudoanterior).IsUnicode(false);

                entity.Property(e => e.Conteudoatual).IsUnicode(false);

                entity.Property(e => e.Tabela).IsUnicode(false);

                entity.Property(e => e.Usuario).IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeData>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__employee__1299A8616EDE8CD0");

                entity.Property(e => e.EmpId).ValueGeneratedNever();

                entity.Property(e => e.EmpBankAccountNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.EmpFname).IsFixedLength(true);

                entity.Property(e => e.EmpFname2).IsFixedLength(true);

                entity.Property(e => e.EmpFname3).IsFixedLength(true);

                entity.Property(e => e.EmpFname4).IsFixedLength(true);

                entity.Property(e => e.EmpFname5).IsFixedLength(true);

                entity.Property(e => e.EmpFname6).IsFixedLength(true);

                entity.Property(e => e.EmpLname).IsFixedLength(true);

                entity.Property(e => e.EmpSsn)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

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

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodInvestidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_cliente");
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

                entity.Property(e => e.TipoContrato).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblContrato)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .HasConstraintName("FK_Contrato_Distribuidor");

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

            modelBuilder.Entity<TblDeparaFundoproduto>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CodCustodianteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodCustodiante)
                    .HasConstraintName("FK_tbl_depara_fundoproduto_tbl_custodiante");
            });

            modelBuilder.Entity<TblDistribuidor>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");
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
            });

            modelBuilder.Entity<TblInvestidor>(entity =>
            {
                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj).IsUnicode(false);

                entity.Property(e => e.CodAdministrador).HasComment("Administrador do fundo investidor");

                entity.Property(e => e.CodGestor).HasComment("Gestor do fundo investidor");

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DirecaoPagamento).IsUnicode(false);

                entity.Property(e => e.NomeCliente).IsUnicode(false);

                entity.Property(e => e.TipoCliente).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
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

            modelBuilder.Entity<TblMovimentacaoNota>(entity =>
            {
                entity.HasKey(e => new { e.CodMovimentacao, e.NotaAplicacao, e.NumOrdem, e.CodOrdemMae })
                    .HasName("PK_tbl_movimentacao_nota_1");

                entity.Property(e => e.Operador).IsFixedLength(true);

                entity.Property(e => e.Penalty).IsFixedLength(true);

                entity.Property(e => e.TipoMovimentacao).IsFixedLength(true);

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_distribuidor");

                entity.HasOne(d => d.CodGestorNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CodGestor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_gestor");

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany(p => p.TblMovimentacaoNota)
                    .HasForeignKey(d => d.CodInvestidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_movimentacao_nota_tbl_cliente");
            });

            modelBuilder.Entity<TblOrdemPassivo>(entity =>
            {
                entity.HasKey(e => new { e.NumOrdem, e.CodInvestidor });

                entity.Property(e => e.DsOperacao).IsFixedLength(true);

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany(p => p.TblOrdemPassivo)
                    .HasForeignKey(d => d.CodAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ordem_passivo_tbl_administrador");

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

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany(p => p.TblOrdemPassivo)
                    .HasForeignKey(d => d.CodInvestidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ordem_passivo_tbl_cliente");
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

            modelBuilder.Entity<TblPosicaoAcao>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_acao_1");
            });

            modelBuilder.Entity<TblPosicaoAdr>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_adr_1");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TblPosicaoBdr>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_bdr_1");
            });

            modelBuilder.Entity<TblPosicaoCambio>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_cambio_1");

                entity.Property(e => e.MoedaComprada).IsFixedLength(true);

                entity.Property(e => e.MoedaVendida).IsFixedLength(true);

                entity.Property(e => e.Tipo).IsFixedLength(true);
            });

            modelBuilder.Entity<TblPosicaoCliente>(entity =>
            {
                entity.HasKey(e => new { e.CodCliente, e.CodFundo, e.DataRef })
                    .HasName("PK_tbl_posicao_cliente_1");

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.TblPosicaoCliente)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cliente_tbl_cliente");

                entity.HasOne(d => d.CodCustodianteNavigation)
                    .WithMany(p => p.TblPosicaoCliente)
                    .HasForeignKey(d => d.CodCustodiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cliente_tbl_custodiante");
            });

            modelBuilder.Entity<TblPosicaoContacorrente>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Codigo })
                    .HasName("PK_tbl_posicao_contacorrente_1");
            });

            modelBuilder.Entity<TblPosicaoCotaFundo>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.CodigoAtivo })
                    .HasName("PK_tbl_posicao_cota_fundo_1");

                entity.Property(e => e.Cnpj).IsFixedLength(true);
            });

            modelBuilder.Entity<TblPosicaoEmprAcao>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.CodOperacao })
                    .HasName("PK_tbl_posicao_empr_acao_1");
            });

            modelBuilder.Entity<TblPosicaoFuturo>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo, e.Vencimento, e.Corretora })
                    .HasName("PK_tbl_posicao_futuro_1");
            });

            modelBuilder.Entity<TblPosicaoOpcaoAcao>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo, e.Tipo, e.Corretora })
                    .HasName("PK_tbl_posicao_opcao_acao_1");

                entity.Property(e => e.Praca).IsFixedLength(true);
            });

            modelBuilder.Entity<TblPosicaoOpcaoFuturo>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo, e.Vencimento, e.Tipo, e.Corretora })
                    .HasName("PK_tbl_posicao_opcao_futuro_1");
            });

            modelBuilder.Entity<TblPosicaoPatrimonio>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo });
            });

            modelBuilder.Entity<TblPosicaoRendafixa>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Codigo })
                    .HasName("PK_tbl_posicao_rendafixa_1");
            });

            modelBuilder.Entity<TblPosicaoRentabilidade>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Indexador });
            });

            modelBuilder.Entity<TblPosicaoTesouraria>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo })
                    .HasName("PK_tbl_posicao_tesouraria_1");
            });

            modelBuilder.Entity<TblSubContrato>(entity =>
            {
                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataVigenciaFim).IsFixedLength(true);

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
