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

  editar(request: EditarMovimentoManualRequest): Observable<MovimentoManual> {
    return this.http.put<MovimentoManual>(
      `${this.apiUrl}/${request.mes}/${request.ano}/${request.numeroLancamento}`,
      request
    );
  }

  excluir(movimento: MovimentoManual): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${movimento.mes}/${movimento.ano}/${movimento.numeroLancamento}`
    );
  }
}
