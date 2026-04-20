namespace SistemaNotas.Models
{
    public enum StatusNota
    {
        Recebida,
        Classificada,
        EmProcesso,
        Concluida
    }

    public class NotaFiscal
    {
       public int Id { get; set; }

        public string Numero { get; set; } // Nº da NF
        public string Fornecedor { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }

        public string Setor { get; set; }

        public StatusNota Status { get; set; }
    }
}