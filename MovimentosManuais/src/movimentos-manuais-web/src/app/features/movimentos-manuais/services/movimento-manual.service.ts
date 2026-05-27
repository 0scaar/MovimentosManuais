import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { MovimentoManual } from '../models/movimento-manual.model';
import { CriarMovimentoManualRequest } from '../models/criar-movimento-manual-request.model';
import { EditarMovimentoManualRequest } from '../models/editar-movimento-manual-request.model';

@Injectable({
  providedIn: 'root'
})
export class MovimentoManualService {
  private readonly apiUrl = `${environment.apiUrl}/movimentos-manuais`;

  constructor(private readonly http: HttpClient) { }

  listar(): Observable<MovimentoManual[]> {
    return this.http.get<MovimentoManual[]>(this.apiUrl);
  }

  criar(request: CriarMovimentoManualRequest): Observable<MovimentoManual> {
    return this.http.post<MovimentoManual>(this.apiUrl, request);
  }

  editar(
    chave: Pick<MovimentoManual, 'mes' | 'ano' | 'numeroLancamento' | 'codigoProduto' | 'codigoCosif'>,
    request: EditarMovimentoManualRequest
  ): Observable<MovimentoManual> {
    const codigoProduto = encodeURIComponent(chave.codigoProduto);
    const codigoCosif = encodeURIComponent(chave.codigoCosif);

    return this.http.put<MovimentoManual>(
      `${this.apiUrl}/${chave.mes}/${chave.ano}/${chave.numeroLancamento}/${codigoProduto}/${codigoCosif}`,
      request
    );
  }

  excluir(movimento: MovimentoManual): Observable<void> {
    const codigoProduto = encodeURIComponent(movimento.codigoProduto);
    const codigoCosif = encodeURIComponent(movimento.codigoCosif);

    return this.http.delete<void>(
      `${this.apiUrl}/${movimento.mes}/${movimento.ano}/${movimento.numeroLancamento}/${codigoProduto}/${codigoCosif}`
    );
  }
}
