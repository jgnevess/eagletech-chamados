namespace eagletechapi.dto.chamado;

public class Fechamento
{
    public int NumeroChamado {get; set;}
    public int TecnicoId {get;set;}
    public string JustificativaFechamento { get;set; }

    public Fechamento(int numeroChamado, int tecnicoId, string justificativaFechamento)
    {
        NumeroChamado = numeroChamado;
        TecnicoId = tecnicoId;
        JustificativaFechamento = justificativaFechamento;
    }
}