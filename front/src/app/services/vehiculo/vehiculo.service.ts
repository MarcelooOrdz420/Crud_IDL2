import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehiculo } from '../../models/vehiculo/vehiculo.model';

@Injectable({ providedIn: 'root' })
export class VehiculoService {
  http = inject(HttpClient);
  private apiUrl = 'https://localhost:7000/api/Vehiculo';

  getAll(): Observable<Vehiculo[]> {
    return this.http.get<Vehiculo[]>(this.apiUrl);
  }

  create(data: Vehiculo): Observable<Vehiculo> {
    return this.http.post<Vehiculo>(this.apiUrl, data);
  }

  update(data: Vehiculo): Observable<Vehiculo> {
    return this.http.put<Vehiculo>(this.apiUrl, data);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
