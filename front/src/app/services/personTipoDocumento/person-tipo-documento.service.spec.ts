import { TestBed } from '@angular/core/testing';

import { PersonTipoDocumentoServices } from './person-tipo-documento.service';

describe('PersonTipoDocumentoServices', () => {
  let service: PersonTipoDocumentoServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PersonTipoDocumentoServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
