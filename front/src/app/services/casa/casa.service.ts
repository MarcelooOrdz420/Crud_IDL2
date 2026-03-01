import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Casa } from '../../models/casa/casa.model';

@Injectable({ providedIn: 'root' })
export class CasaService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7000/api/Casa';

  getAll(): Observable<Casa[]> {
    return this.http.get<Casa[]>(this.apiUrl);
  }

  create(data: Casa): Observable<Casa> {
    return this.http.post<Casa>(this.apiUrl, data);
  }

  update(data: Casa): Observable<Casa> {
    return this.http.put<Casa>(this.apiUrl, data);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
