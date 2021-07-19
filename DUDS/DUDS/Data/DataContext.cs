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
        public virtual DbSet<TblAcordoDistribuicao> TblAcordoDistribuicao { get; set; }
        public virtual DbSet<TblAdministrador> TblAdministrador { get; set; }
        public virtual DbSet<TblAlocador> TblAlocador { get; set; }
        public virtual DbSet<TblBcbEntidadesSupervisionadas> TblBcbEntidadesSupervisionadas { get; set; }
        public virtual DbSet<TblCalculoPgtoAdmPfee> TblCalculoPgtoAdmPfee { get; set; }
        public virtual DbSet<TblCliente> TblCliente { get; set; }
        public virtual DbSet<TblContas> TblContas { get; set; }
        public virtual DbSet<TblContrato> TblContrato { get; set; }
        public virtual DbSet<TblContratoDistribuicao> TblContratoDistribuicao { get; set; }
        public virtual DbSet<TblCustodiante> TblCustodiante { get; set; }
        public virtual DbSet<TblDeparaFundoproduto> TblDeparaFundoproduto { get; set; }
        public virtual DbSet<TblDistribuidor> TblDistribuidor { get; set; }
        public virtual DbSet<TblDistribuidorAdministrador> TblDistribuidorAdministrador { get; set; }
        public virtual DbSet<TblErrosPagamento> TblErrosPagamento { get; set; }
        public virtual DbSet<TblFeriadosAnbima> TblFeriadosAnbima { get; set; }
        public virtual DbSet<TblFiCad> TblFiCad { get; set; }
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
        public virtual DbSet<TblTeste> TblTeste { get; set; }
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

            modelBuilder.Entity<TblAcordoDistribuicao>(entity =>
            {
                entity.HasKey(e => new { e.CodCliente, e.CodFundo, e.CodDistribuidor });

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblAcordoDistribuicao)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_acordo_distribuicao_tbl_distribuidor");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblAcordoDistribuicao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_acordo_distribuicao_tbl_fundo");
            });

            modelBuilder.Entity<TblAdministrador>(entity =>
            {
                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblAlocador>(entity =>
            {
                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.TblAlocador)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alocador_Cliente");

                entity.HasOne(d => d.CodContratoFundoNavigation)
                    .WithMany(p => p.TblAlocador)
                    .HasForeignKey(d => d.CodContratoFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Alocador_Contrato");
            });

            modelBuilder.Entity<TblBcbEntidadesSupervisionadas>(entity =>
            {
                entity.Property(e => e.DataBase).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NomeEntidadeInteresse).IsUnicode(false);
            });

            modelBuilder.Entity<TblCalculoPgtoAdmPfee>(entity =>
            {
                entity.HasKey(e => new { e.Competencia, e.CodCliente, e.CodFundo });

                entity.Property(e => e.Competencia).IsFixedLength(true);

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_cliente");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblCalculoPgtoAdmPfee)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_calculo_pgto_adm_pfee_tbl_fundo");
            });

            modelBuilder.Entity<TblCliente>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany(p => p.TblCliente)
                    .HasForeignKey(d => d.CodAdministrador)
                    .HasConstraintName("FK_tbl_cliente_tbl_administrador");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblCliente)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_cliente_tbl_distribuidor");

                entity.HasOne(d => d.CodGestorNavigation)
                    .WithMany(p => p.TblCliente)
                    .HasForeignKey(d => d.CodGestor)
                    .HasConstraintName("FK_tbl_cliente_tbl_gestor");
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

            modelBuilder.Entity<TblContrato>(entity =>
            {
                entity.Property(e => e.DirecaoPagamento).IsUnicode(false);

                entity.Property(e => e.IdDocusign).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.TipoContrato).IsUnicode(false);

                entity.Property(e => e.Versao).IsUnicode(false);

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany(p => p.TblContrato)
                    .HasForeignKey(d => d.CodDistribuidor)
                    .HasConstraintName("FK_Contrato_Distribuidor");
            });

            modelBuilder.Entity<TblContratoDistribuicao>(entity =>
            {
                entity.HasOne(d => d.CodContratoNavigation)
                    .WithMany(p => p.TblContratoDistribuicao)
                    .HasForeignKey(d => d.CodContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContratoFundo_Contrato");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblContratoDistribuicao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContratoFundo_Fundo");
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

            modelBuilder.Entity<TblDistribuidorAdministrador>(entity =>
            {
                entity.Property(e => e.CodDistrAdm).IsUnicode(false);

                entity.HasOne(d => d.CodAdministradorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodAdministrador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistribuidorAdministrador_Administrador");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany()
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
            });

            modelBuilder.Entity<TblFiCad>(entity =>
            {
                entity.Property(e => e.Admin).IsUnicode(false);

                entity.Property(e => e.CnpjAdmin).IsUnicode(false);

                entity.Property(e => e.CnpjCustodiante).IsUnicode(false);

                entity.Property(e => e.CnpjFundo).IsUnicode(false);

                entity.Property(e => e.CpfCnpjGestor).IsUnicode(false);

                entity.Property(e => e.Custodiante).IsUnicode(false);

                entity.Property(e => e.DataBase).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DenomSocial).IsUnicode(false);

                entity.Property(e => e.Gestor).IsUnicode(false);

                entity.Property(e => e.Sit).IsUnicode(false);
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
                entity.Property(e => e.Cnpj).IsFixedLength(true);

                entity.Property(e => e.DataModificacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblInvestidor>(entity =>
            {
                entity.Property(e => e.Cnpj).IsUnicode(false);

                entity.Property(e => e.NomeCliente).IsUnicode(false);

                entity.Property(e => e.TipoCliente).IsUnicode(false);

                entity.Property(e => e.UsuarioModificacao).IsUnicode(false);
            });

            modelBuilder.Entity<TblInvestidorDistribuidor>(entity =>
            {
                entity.Property(e => e.CodInvestCustodia).IsUnicode(false);

                entity.HasOne(d => d.CodCustodianteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodCustodiante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvestidorDistribuidor_Custodiante");

                entity.HasOne(d => d.CodDistribuidorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodDistribuidor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvestidorDistribuidor_Distribuidor");

                entity.HasOne(d => d.CodInvestidorNavigation)
                    .WithMany()
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
                entity.HasKey(e => new { e.NumOrdem, e.CdCotista });

                entity.Property(e => e.DsOperacao).IsFixedLength(true);

                entity.HasOne(d => d.CdCotistaNavigation)
                    .WithMany(p => p.TblOrdemPassivo)
                    .HasForeignKey(d => d.CdCotista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ordem_passivo_tbl_cliente");

                entity.HasOne(d => d.CodCustodianteNavigation)
                    .WithMany(p => p.TblOrdemPassivo)
                    .HasForeignKey(d => d.CodCustodiante)
                    .HasConstraintName("FK_tbl_ordem_passivo_tbl_custodiante");

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

            modelBuilder.Entity<TblPagamentoServico>(entity =>
            {
                entity.HasKey(e => new { e.Competencia, e.CodFundo });

                entity.Property(e => e.Competencia)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPagamentoServico)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_pagamento_servico_tbl_fundo");
            });

            modelBuilder.Entity<TblPgtoAdmPfee>(entity =>
            {
                entity.HasKey(e => new { e.Competencia, e.CodCliente, e.CodFundo })
                    .HasName("PK_tbl_pgto_adm_pfee_1");

                entity.Property(e => e.Competencia).IsFixedLength(true);

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.TblPgtoAdmPfee)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_pgto_adm_pfee_tbl_cliente");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPgtoAdmPfee)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_pgto_adm_pfee_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoAcao>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_acao_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoAcao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_acao_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoAdr>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_adr_1");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoAdr)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_adr_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoBdr>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_bdr_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoBdr)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_bdr_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoCambio>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo })
                    .HasName("PK_tbl_posicao_cambio_1");

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

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoCliente)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_cliente_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoContacorrente>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Codigo })
                    .HasName("PK_tbl_posicao_contacorrente_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoContacorrente)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_contacorrente_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoCotaFundo>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.CodigoAtivo })
                    .HasName("PK_tbl_posicao_cota_fundo_1");

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
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.CodOperacao })
                    .HasName("PK_tbl_posicao_empr_acao_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoEmprAcao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_empr_acao_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoFuturo>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo, e.Vencimento, e.Corretora })
                    .HasName("PK_tbl_posicao_futuro_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoFuturo)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_futuro_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoOpcaoAcao>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo, e.Tipo, e.Corretora })
                    .HasName("PK_tbl_posicao_opcao_acao_1");

                entity.Property(e => e.Praca).IsFixedLength(true);

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoOpcaoAcao)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_opcao_acao_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoOpcaoFuturo>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Ativo, e.Vencimento, e.Tipo, e.Corretora })
                    .HasName("PK_tbl_posicao_opcao_futuro_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoOpcaoFuturo)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_opcao_futuro_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoPatrimonio>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo });

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoPatrimonio)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_patrimonio_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoRendafixa>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Codigo })
                    .HasName("PK_tbl_posicao_rendafixa_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoRendafixa)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_rendafixa_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoRentabilidade>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo, e.Indexador });

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoRentabilidade)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_rentabilidade_fundo_tbl_fundo");
            });

            modelBuilder.Entity<TblPosicaoTesouraria>(entity =>
            {
                entity.HasKey(e => new { e.DataRef, e.CodFundo })
                    .HasName("PK_tbl_posicao_tesouraria_1");

                entity.HasOne(d => d.CodFundoNavigation)
                    .WithMany(p => p.TblPosicaoTesouraria)
                    .HasForeignKey(d => d.CodFundo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_posicao_tesouraria_tbl_fundo");
            });

            modelBuilder.Entity<TblTeste>(entity =>
            {
                entity.Property(e => e.Observacao).IsUnicode(false);
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
