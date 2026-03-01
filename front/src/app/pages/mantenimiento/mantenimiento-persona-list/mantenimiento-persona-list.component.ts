import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PersonaService } from '../../../services/persona/persona.service';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { MantenimientoPersonaEditarComponent } from '../mantenimiento-persona-editar/mantenimiento-persona-editar.component';
import { RouterModule } from '@angular/router';
import { PersonTipoDocumentoServices } from '../../../services/personTipoDocumento/person-tipo-documento.service';
import { PersonaTipoDocumentoDto } from '../../../models/persona/PersonaTipoDocumentoDto.model';

@Component({
  selector: 'app-mantenimiento-persona-list',
  standalone: true,
  imports: [MantenimientoPersonaEditarComponent, RouterModule, FormsModule],
  templateUrl: './mantenimiento-persona-list.component.html',
  styleUrls: ['./mantenimiento-persona-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoPersonaListComponent implements OnInit {


  //INYECTAR LOS SERVICIOS HHTPCLIENT ==> PERSONA SERVICE

  _personaService = inject(PersonaService);
  _personaTipoDocumentoService = inject(PersonTipoDocumentoServices);

  personas = signal<PersonaDto[]>([]);
  tiposDocumentos:PersonaTipoDocumentoDto[] = [];
  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  personaSeleccionada: PersonaDto | null = null;
  filtroNombre = '';
  filtroApellidoPaterno = '';
  busquedaRealizada = false;

  constructor() { }

  ngOnInit() {
    this.getTipoDocumento();
  }

  getTipoDocumento(){
    this._personaTipoDocumentoService.getAll().subscribe({
      next: (data) => {
        console.log(data);
        this.tiposDocumentos = data;
      },
      error: (err) => { console.log("ocurrio un error", err); },
    });
  }

  buscarPersonas() {
    const nombre = this.filtroNombre.trim().toLowerCase();
    const apellido = this.filtroApellidoPaterno.trim().toLowerCase();

    this._personaService.getAll().subscribe({
      next: (data) => {
        const filtradas = data.filter((persona) => {
          const matchNombre = !nombre || (persona.nombres ?? '').toLowerCase().includes(nombre);
          const matchApellido = !apellido || (persona.apellidoPaterno ?? '').toLowerCase().includes(apellido);
          return matchNombre && matchApellido;
        });

        this.personas.set(filtradas);
        this.busquedaRealizada = true;
      },
      error: (err) => { console.log("ocurrio un error", err); },
    });
  }

  limpiarBusqueda(): void {
    this.filtroNombre = '';
    this.filtroApellidoPaterno = '';
    this.personas.set([]);
    this.busquedaRealizada = false;
  }

  abrirAgregar(): void {
    this.modoEdicion = 'crear';
    this.personaSeleccionada = null;
    this.mostrarModal = true;
  }

  abrirEditar(persona: PersonaDto): void {
    this.modoEdicion = 'editar';
    this.personaSeleccionada = { ...persona };
    this.mostrarModal = true;
  }

  cerrarModal(): void {
    this.mostrarModal = false;
    this.personaSeleccionada = null;
  }

  onGuardado(): void {
    this.cerrarModal();
    if (this.busquedaRealizada) {
      this.buscarPersonas();
    }
  }

  eliminarPersona(persona: PersonaDto): void {
    const confirmado = window.confirm(
      `¿Está seguro de eliminar el registro de ${persona.nombres ?? ''} ${persona.apellidoPaterno ?? ''}?`
    );

    if (!confirmado) {
      return;
    }

    this._personaService.delete(persona.id).subscribe({
      next: () => {
        if (this.busquedaRealizada) {
          this.buscarPersonas();
        }
      },
      error: (err) => {
        console.log('ocurrio un error al eliminar', err);
      },
    });
  }


}
