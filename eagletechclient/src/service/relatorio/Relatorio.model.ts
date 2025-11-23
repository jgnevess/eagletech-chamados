export enum Categoria {
  HARDWARE = "HARDWARE",
  SOFTWARE = "SOFTWARE",
  REDE = "REDE",
  IMPRESSORA = "IMPRESSORA",
  SISTEMA_OPERACIONAL = "SISTEMA_OPERACIONAL",
  BANCO_DE_DADOS = "BANCO_DE_DADOS",
  SEGURANCA = "SEGURANCA",
  OUTROS = "OUTROS",
}

export enum Status {
  ABERTO = "ABERTO",
  EM_ANDAMENTO = "EM_ANDAMENTO",
  FECHADO = "FECHADO",
}

export enum Prioridade {
  CRITICA = "CRITICA",
  ALTA = "ALTA",
  MEDIA = "MEDIA",
  BAIXA = "BAIXA",
}

export enum TipoRelatorio {
  DETALHADO = "Detalhado",
  RESUMIDO = "Resumido",
}

export enum ArquivoRelatorio {
  PDF = "PDF",
  CSV = "CSV",
  CSV_UTF8 = "CSV_UTF8",
}