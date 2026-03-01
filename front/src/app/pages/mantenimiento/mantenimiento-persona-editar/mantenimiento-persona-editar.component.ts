import {
  ChangeDetectionStrategy,
  Component,
  effect,
  inject,
  input,
  output,
} from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { PersonaService } from '../../../services/persona/persona.service';
import { PersonaTipoDocumentoDto } from '../../../models/persona/PersonaTipoDocumentoDto.model';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-mantenimiento-persona-editar',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe],
  templateUrl: './mantenimiento-persona-editar.component.html',
  styleUrls: ['./mantenimiento-persona-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,

})
export class MantenimientoPersonaEditarComponent {

  //DECLARANDO VARIABLES DE ENTRADA QUE PROVIENE DEL COMPONENTE PADRE (MANTENIMIENTO-PERSONA-LIST)
  // PARA SABER SI SE ESTA CREANDO O EDITANDO UNA PERSONA
  persona = input<PersonaDto | null>(null);
  tiposDocumentos = input<PersonaTipoDocumentoDto[] | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  //DECLARANDO VARIABLES DE SALIDA QUE PROVIENE DEL COMPONENTE PADRE (MANTENIMIENTO-PERSONA-LIST)
  cancelado = output<void>();
  guardado = output<void>();

  //INYECTACMOS EL SERVICIO DE PERSONA SERVICES
  private readonly personaService = inject(PersonaService);
  //INYECTAMOS EL FORM BUILDER PARA CONSTRUIR EL FORMULARIO REACTIVO
  private readonly formBuilder = inject(FormBuilder);

  //INICIAMOS LA CONSTRUCCIÓN DEL FORMULARIO REACTIVO CON LOS CAMPOS CORRESPONDIENTES A LA ENTIDAD PERSONA
  readonly form = this.formBuilder.group({
    nombres: ['', [Validators.required]],
    idTipoDocumento: [0, [Validators.required]],
    numeroDocumento: ['', [Validators.required, Validators.pattern('^\\d+$')]],
    apellidoPaterno: ['', [Validators.required]],
    apellidoMaterno: [''],
    direccion: [''],
    telefono: [''],
  });

  cargando = false;

  //CONSTRUCTOR DONDE SE EJECUTA 
  // UN EFECTO CUANDO LA PROPIEDAD persona CAMBIA, PARA ACTUALIZAR LOS VALORES 
  // DEL FORMULARIO CON LOS DATOS DE LA PERSONA SELECCIONADA
  constructor() {
    effect(() => {
      const persona = this.persona();

      this.form.reset({
        nombres: persona?.nombres ?? '',
        numeroDocumento: persona?.numeroDocumento ?? '',
        apellidoPaterno: persona?.apellidoPaterno ?? '',
        apellidoMaterno: persona?.apellidoMaterno ?? '',
        direccion: persona?.direccion ?? '',
        telefono: persona?.telefono ?? '',
        idTipoDocumento: persona?.idTipoDocumento,
      });
    });
  }

  onCancelar(): void {
    this.cancelado.emit();
  }

  onGuardar(): void {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) {
      return;
    }

    this.cargando = true;

    const valores = this.form.getRawValue();
    const actual = this.persona();
    const nowIso = new Date().toISOString();
    const payload: PersonaDto = {
      id: actual?.id ?? 0,
      idTipoDocumento: valores?.idTipoDocumento ?? 0,
      numeroDocumento: valores.numeroDocumento,
      nombres: valores.nombres,
      apellidoPaterno: valores.apellidoPaterno,
      apellidoMaterno: valores.apellidoMaterno,
      direccion: valores.direccion,
      telefono: valores.telefono,
      userCreate: actual?.userCreate ?? 0,
      userUpdate: actual?.userUpdate ?? 0,
      dateCreated: nowIso,
      dateUpdate: nowIso
    };

    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.personaService.update(payload.id, payload)
      : this.personaService.create(payload);

    request$.subscribe({
      next: () => {
        this.guardado.emit();
      },
      error: (error) => {
        console.error('Error al guardar persona', error);
        this.cargando = false;
      },
      complete: () => {
        this.cargando = false;
      },
    });
  }
}
