import React, { useEffect, useState } from 'react';
import Container from '../../components/container';
import { baixarRelatorio, FiltrosRelatorio } from '../../service/relatorio/relatorioService';
import { ArquivoRelatorio, Categoria, Prioridade, Status, TipoRelatorio } from '../../service/relatorio/Relatorio.model';
import Alert from '../../components/alert';
import { useFirstLogin } from '../../hooks/useFirstLogin';

const Relatorio = () => {
  useFirstLogin();

  const [dataInicio, setDataInicio] = useState('');
  const [dataFim, setDataFim] = useState('');
  const [categoria, setCategoria] = useState('');
  const [status, setStatus] = useState('');
  const [prioridade, setPrioridade] = useState('');
  const [tipoRelatorio, setTipoRelatorio] = useState('Detalhado');
  const [arquivoRelatorio, setArquivoRelatorio] = useState('PDF');
  const [alertMessage, setAlertMessage] = useState('');
  const [alert, setAlert] = useState(false);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
    // @ts-ignore
    [...tooltipTriggerList].map(el => new bootstrap.Tooltip(el));
  }, []);



  const Cat = Object.values(Categoria).map(cat => <option key={cat} value={cat}>{cat}</option>);
  const Stat = Object.values(Status).map(st => {
    return (
      <div className="form-check mb-3" key={st}>
        <input
          className="form-check-input"
          type="radio"
          name="todosSt"
          id={st}
          value={st}
          checked={status === st}
          onChange={(e) => setStatus(e.target.value)} />
        <label className="form-check-label" htmlFor={st}>
          {st}
        </label>
      </div>
    )
  });
  const Prior = Object.values(Prioridade).map(pr => {
    return (
      <div className="form-check mb-3" key={pr}>
        <input
          className="form-check-input"
          type="radio"
          name="todosPr"
          id={pr}
          value={pr}
          checked={prioridade === pr}
          onChange={(e) => setPrioridade(e.target.value)}
        />
        <label className="form-check-label" htmlFor={pr}>
          {pr}
        </label>
      </div>
    )
  });
  const Tipo = Object.values(TipoRelatorio).map(tr => {
    return (
      <div className="form-check mb-3" key={tr}>
        <input
          className="form-check-input"
          type="radio"
          name="todosTr"
          id={tr}
          value={tr}
          checked={tipoRelatorio === tr}
          onChange={(e) => setTipoRelatorio(e.target.value)}
        />
        <label className="form-check-label" htmlFor={tr}>
          {tr}
        </label>
      </div>
    )
  });
  const Arq = Object.values(ArquivoRelatorio).map(ar => {
    let tooltip = "";
    switch (ar) {
      case "PDF":
        tooltip = "Relatório formatado para impressão";
        break;
      case "CSV_UTF8":
        tooltip = "Separado por vírgulas (Padrão mundial UTF-8)";
        break;
      case "CSV":
        tooltip = "Compatível com Excel (usa ponto e vírgula)";
        break;
      default:
        tooltip = "Formato desconhecido";
    }

    return (
      <div className="form-check mb-3" key={ar}>
        <input
          className="form-check-input"
          type="radio"
          name="todosAr"
          id={ar}
          value={ar}
          checked={arquivoRelatorio === ar}
          onChange={(e) => setArquivoRelatorio(e.target.value)}
          data-bs-toggle="tooltip"
          data-bs-placement="top"
          title={tooltip}
        />
        <label className="form-check-label" htmlFor={ar}>
          {ar}
        </label>
      </div>
    );
  });

  useEffect(() => {
    const today = new Date();
    const firstDayOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);
    setDataInicio(new Date(firstDayOfMonth).toISOString().split('T')[0]);
    setDataFim(today.toISOString().split('T')[0]);
  }, []);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (dataInicio === '' || dataFim === '' || tipoRelatorio === '' || arquivoRelatorio === '') {
      setAlertMessage('Preencha todos os campos obrigatórios');
      setAlert(true);

      setTimeout(() => {
        setAlert(false);
        setAlertMessage('');
      }, 5000);
      return;
    }

    const filtros: FiltrosRelatorio = {
      dataInicio,
      dataFim,
      tipoRelatorio: tipoRelatorio as TipoRelatorio,
      arquivoRelatorio: arquivoRelatorio as ArquivoRelatorio,
      ...(categoria && categoria !== '' && { categoria: categoria as Categoria }),
      ...(status && status !== '' && { status: status as Status }),
      ...(prioridade && prioridade !== '' && { prioridade: prioridade as Prioridade })
    };

    baixarRelatorio(filtros, setLoading);
  };

  return (
    <Container>
      <>
        {alert ? <Alert type="alert alert-danger" message={alertMessage} /> : <></>}
        <h1>Relatórios chamados</h1>

        <form onSubmit={handleSubmit} className="w-100 p-5 rounded row">
          <div className="col-4 d-flex flex-column justify-content-between">
            <div className="mb-3">
              <label htmlFor="startDate" className="form-label">Data de início (obrigatorio)</label>
              <input type="date" className={!alert ? "form-control" : dataInicio.trim() == '' ? "form-control is-invalid" : "form-control is-valid"} id="startDate" value={dataInicio} onChange={(e) => setDataInicio(e.target.value)} />
            </div>
            <div className="mb-3">
              <label htmlFor="endDate" className="form-label">Data final (obrigatorio)</label>
              <input type="date" className={!alert ? "form-control" : dataFim.trim() == '' ? "form-control is-invalid" : "form-control is-valid"} id="endDate" value={dataFim} onChange={(e) => setDataFim(e.target.value)} />
            </div>

            <label className="form-label">Categoria</label>
            <select className="form-select mb-3" value={categoria} onChange={(e) => setCategoria(e.target.value)}>
              <option selected value={""}>Categoria</option>
              {Cat}
            </select>
          </div>
          <div className="col-8">
            <h4>Status do chamado</h4>
            <div className="d-flex justify-content-between">
              <div className="form-check mb-3">
                <input
                  className="form-check-input"
                  type="radio"
                  name="todosSt"
                  id="todosSt"
                  value=""
                  checked={status === ""}
                  onChange={(e) => setStatus(e.target.value)} />
                <label className="form-check-label" htmlFor="todosSt">
                  Todos
                </label>
              </div>
              {Stat}
            </div>

            <h4>Prioridade do chamado</h4>
            <div className="d-flex justify-content-between">
              <div className="form-check mb-3">
                <input
                  className="form-check-input"
                  type="radio"
                  name="todosPr"
                  id="todosPr"
                  value=""
                  checked={prioridade === ""}
                  onChange={(e) => setPrioridade(e.target.value)} />
                <label className="form-check-label" htmlFor="todosPr">
                  Todos
                </label>
              </div>
              {Prior}
            </div>

            <h4>Tipo de relatório</h4>
            <div className="d-flex justify-content-between">
              {Tipo}
            </div>

            <h4>Formato para download</h4>
            <div className="d-flex justify-content-between">
              {Arq}
            </div>
          </div>

          <button type='submit' className='btn btn-dark w-100' disabled={loading}>
            {loading ? 'Gerando...' : 'Gerar Relatório'}
          </button>
        </form>
      </>
    </Container >
  );
}


export default Relatorio;