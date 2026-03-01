import { ChangeDetectionStrategy, Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { Vehiculo } from '../../../models/vehiculo/vehiculo.model';
import { PersonaDto } from '../../../models/persona/PersonaDto.model';
import { PersonaTipoDocumentoDto } from '../../../models/persona/PersonaTipoDocumentoDto.model';
import { Casa } from '../../../models/casa/casa.model';
import { VehiculoService } from '../../../services/vehiculo/vehiculo.service';
import { PersonaService } from '../../../services/persona/persona.service';
import { CasaService } from '../../../services/casa/casa.service';
import { PersonTipoDocumentoServices } from '../../../services/personTipoDocumento/person-tipo-documento.service';

@Component({
  selector: 'app-mantenimiento-vehiculo-editar',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './mantenimiento-vehiculo-editar.component.html',
  styleUrls: ['./mantenimiento-vehiculo-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoVehiculoEditarComponent {
  vehiculo = input<Vehiculo | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  cancelado = output<void>();
  guardado = output<void>();

  private readonly vehiculoService = inject(VehiculoService);
  private readonly personaService = inject(PersonaService);
  private readonly casaService = inject(CasaService);
  private readonly tipoDocumentoService = inject(PersonTipoDocumentoServices);
  private readonly fb = inject(FormBuilder);

  tiposDocumento: PersonaTipoDocumentoDto[] = [];
  cargando = false;
  errorMensaje = '';

  readonly form = this.fb.group({
    placa: ['', [Validators.required, Validators.maxLength(12)]],
    marca: ['', [Validators.required]],
    modelo: ['', [Validators.required]],
    anio: [new Date().getFullYear(), [Validators.required, Validators.min(1900), Validators.max(2100)]],
    color: [''],
    estado: [true],
    personaIdTipoDocumento: [0],
    personaNumeroDocumento: [''],
    personaNombres: [''],
    personaApellidoPaterno: [''],
    personaApellidoMaterno: [''],
    personaDireccion: [''],
    personaTelefono: [''],
    casaNombre: [''],
    casaDireccion: [''],
    casaReferencia: [''],
  });

  constructor() {
    this.tipoDocumentoService.getAll().subscribe({
      next: (data) => {
        this.tiposDocumento = data;

        if (this.modo() === 'crear' && !this.form.controls.personaIdTipoDocumento.value && data.length > 0) {
          this.form.patchValue({ personaIdTipoDocumento: data[0].id });
        }
      },
      error: (error) => console.error('Error al cargar tipos de documento', error),
    });

    effect(() => {
      const v = this.vehiculo();

      this.errorMensaje = '';
      this.form.reset({
        placa: v?.placa ?? '',
        marca: v?.marca ?? '',
        modelo: v?.modelo ?? '',
        anio: v?.anio ?? new Date().getFullYear(),
        color: v?.color ?? '',
        estado: v?.estado ?? true,
        personaIdTipoDocumento: this.tiposDocumento[0]?.id ?? 0,
        personaNumeroDocumento: '',
        personaNombres: '',
        personaApellidoPaterno: '',
        personaApellidoMaterno: '',
        personaDireccion: '',
        personaTelefono: '',
        casaNombre: '',
        casaDireccion: '',
        casaReferencia: '',
      });
    });
  }

  onCancelar() {
    this.cancelado.emit();
  }

  async onGuardar() {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) return;

    this.cargando = true;
    this.errorMensaje = '';

    try {
      if (this.modo() === 'editar') {
        await this.actualizarVehiculo();
      } else {
        await this.crearFlujoCompleto();
      }

      this.guardado.emit();
    } catch (error) {
      console.error('Error al guardar vehiculo', error);
      this.errorMensaje = 'No se pudo guardar. Revisa los datos e intenta nuevamente.';
    } finally {
      this.cargando = false;
    }
  }

  private async actualizarVehiculo() {
    const valores = this.form.getRawValue();
    const actual = this.vehiculo();

    if (!actual) {
      throw new Error('Vehiculo no encontrado');
    }

    const payload: Vehiculo = {
      id: actual.id,
      placa: valores.placa ?? '',
      marca: valores.marca ?? '',
      modelo: valores.modelo ?? '',
      anio: valores.anio ?? new Date().getFullYear(),
      color: valores.color,
      idPropietarioPersona: actual.idPropietarioPersona,
      idCasa: actual.idCasa,
      estado: valores.estado ?? true,
    };

    await firstValueFrom(this.vehiculoService.update(payload));
  }

  private async crearFlujoCompleto() {
    const valores = this.form.getRawValue();

    if (
      !valores.personaIdTipoDocumento ||
      !valores.personaNumeroDocumento?.trim() ||
      !valores.personaNombres?.trim() ||
      !valores.personaApellidoPaterno?.trim() ||
      !valores.casaNombre?.trim()
    ) {
      this.errorMensaje = 'Completa los datos obligatorios de persona y casa.';
      throw new Error('Faltan datos obligatorios');
    }

    const now = new Date().toISOString();

    const personaPayload: PersonaDto = {
      id: 0,
      idTipoDocumento: valores.personaIdTipoDocumento,
      numeroDocumento: valores.personaNumeroDocumento.trim(),
      nombres: valores.personaNombres.trim(),
      apellidoPaterno: valores.personaApellidoPaterno.trim(),
      apellidoMaterno: valores.personaApellidoMaterno?.trim() || '',
      direccion: valores.personaDireccion?.trim() || '',
      telefono: valores.personaTelefono?.trim() || '',
      userCreate: 1,
      userUpdate: null,
      dateCreated: now,
      dateUpdate: now,
    };

    const personaCreada = await firstValueFrom(this.personaService.create(personaPayload));

    const casaPayload: Casa = {
      id: 0,
      nombre: valores.casaNombre.trim(),
      direccion: valores.casaDireccion?.trim() || '',
      referencia: valores.casaReferencia?.trim() || '',
      idPropietarioPersona: personaCreada.id,
    };

    const casaCreada = await firstValueFrom(this.casaService.create(casaPayload));

    const vehiculoPayload: Vehiculo = {
      id: 0,
      placa: (valores.placa ?? '').trim(),
      marca: (valores.marca ?? '').trim(),
      modelo: (valores.modelo ?? '').trim(),
      anio: valores.anio ?? new Date().getFullYear(),
      color: valores.color?.trim() || '',
      idPropietarioPersona: personaCreada.id,
      idCasa: casaCreada.id,
      estado: valores.estado ?? true,
    };

    await firstValueFrom(this.vehiculoService.create(vehiculoPayload));
  }
}
