export interface EditarMovimentoManualRequest {
  mes: number;
  ano: number;
  numeroLancamento: number;
  codigoProduto: string;
  codigoCosif: string;
  valor: number;
  descricao: string;
  codigoUsuario: string;
}
