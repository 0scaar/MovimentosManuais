import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../models/produto.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {
  private readonly apiUrl = `${environment.apiUrl}/produtos`;

  constructor(private readonly http: HttpClient) { }

  listarAtivos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.apiUrl);
  }
}
