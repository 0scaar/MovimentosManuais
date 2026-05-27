export interface MovimentoManual {
  mes: number;
  ano: number;
  numeroLancamento: number;
  codigoProduto: string;
  descricaoProduto?: string;
  codigoCosif: string;
  descricaoCosif?: string;
  valor: number;
  descricao: string;
  dataMovimento: string;
  codigoUsuario: string;
}
