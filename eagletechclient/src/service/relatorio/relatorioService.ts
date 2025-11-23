import axios from "axios";

const headers = () => {
  return {
    'Authorization': `Bearer ${sessionStorage.getItem('token')}`,
    'Content-Type': 'application/json'
  }
}

export interface FiltrosRelatorio {
  dataInicio: string;
  dataFim: string;
  status?: "" | "ABERTO" | "EM_ANDAMENTO" | "FECHADO"
  categoria?: "" | "HARDWARE" | "SOFTWARE" | "REDE" | "IMPRESSORA" | "SISTEMA_OPERACIONAL" | "BANCO_DE_DADOS" | "SEGURANCA" | "OUTROS"
  prioridade?: "" | "CRITICA" | "ALTA" | "MEDIA" | "BAIXA";
  tipoRelatorio: "Detalhado" | "Resumido";
  arquivoRelatorio: "PDF" | "CSV" | "CSV_UTF8";
}

const apiUrl = `${process.env.REACT_APP_API_URL}/api/Relatorio`

const baixarRelatorio = async (filtros: FiltrosRelatorio, setLoading: (v: boolean) => void) => {
  setLoading(true);
  try {
    const res = await axios.post(`${apiUrl}/download-periodo`, filtros, {
      headers: headers(),
      responseType: 'blob'
    });

    if (res.status !== 200) throw new Error('Erro ao baixar arquivo');

    const url = window.URL.createObjectURL(res.data);
    const a = document.createElement('a');
    a.href = url;
    a.download = ''
    document.body.appendChild(a);
    a.click();
    a.remove();
    window.URL.revokeObjectURL(url);
  } catch (err) {
    console.error('Erro ao baixar relatório:', err);
  }
  finally {
    setLoading(false);
  }
};


export { baixarRelatorio };
