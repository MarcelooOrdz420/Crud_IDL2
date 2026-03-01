import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { PersonaDto } from '../../models/persona/PersonaDto.model';
import { PersonaTipoDocumentoDto } from '../../models/persona/PersonaTipoDocumentoDto.model';

@Injectable({
  providedIn: 'root',
})


export class PersonTipoDocumentoServices {


  //inyectar el servicio de httpclient
  http = inject(HttpClient);

  private apiUrl = 'https://localhost:7000/api/PersonaTipoDocumento';


  getAll(): Observable<PersonaTipoDocumentoDto[]> {
    return this.http.get<PersonaTipoDocumentoDto[]>(this.apiUrl);
  }



}
