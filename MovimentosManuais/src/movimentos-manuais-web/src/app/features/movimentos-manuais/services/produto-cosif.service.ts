import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProdutoCosif } from '../models/produto-cosif.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProdutoCosifService {
  constructor(private readonly http: HttpClient) { }

  listarPorProduto(codigoProduto: string): Observable<ProdutoCosif[]> {
    return this.http.get<ProdutoCosif[]>(
      `${environment.apiUrl}/produtos/${codigoProduto}/cosifs`
    );
  }
}
