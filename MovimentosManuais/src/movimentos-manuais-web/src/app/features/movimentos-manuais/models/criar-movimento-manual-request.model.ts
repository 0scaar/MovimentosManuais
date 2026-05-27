export interface CriarMovimentoManualRequest {
  mes: number;
  ano: number;
  codigoProduto: string;
  codigoCosif: string;
  valor: number;
  descricao: string;
  codigoUsuario: string;
}
