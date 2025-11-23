import React, { useState } from "react";

interface Props {
  onLoadMore: () => void;
  hasMore: boolean;
  loading: boolean;
  page: number;
}


const BtnChamados = ({
  onLoadMore,
  hasMore,
  loading,
  page,
}: Props) => {
  return (
    <>
      {loading && <p className="text-center mt-2">Carregando...</p>}

      {!hasMore ? (
        <div className="w-100 d-flex justify-content-center">
          <div className="w-100">
            Fim da lista
          </div>
        </div>
      ) : (
        <div className="w-100 d-flex justify-content-center">
          <button
            className="btn w-100 p-3"
            onClick={onLoadMore}
            disabled={loading}
          >
            Carregar mais
          </button>
        </div>
      )}
    </>
  )
}

const style = {
  btn: {
    width: '100%',
    padding: '10px',
    cursor: 'pointer',
    borderRadius: '0',
  }
}

export default BtnChamados;