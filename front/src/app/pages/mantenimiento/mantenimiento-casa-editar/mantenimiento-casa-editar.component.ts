import { ChangeDetectionStrategy, Component, effect, inject, input, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Casa } from '../../../models/casa/casa.model';
import { CasaService } from '../../../services/casa/casa.service';

@Component({
  selector: 'app-mantenimiento-casa-editar',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './mantenimiento-casa-editar.component.html',
  styleUrls: ['./mantenimiento-casa-editar.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoCasaEditarComponent {
  casa = input<Casa | null>(null);
  modo = input<'crear' | 'editar'>('crear');

  cancelado = output<void>();
  guardado = output<void>();

  private readonly casaService = inject(CasaService);
  private readonly fb = inject(FormBuilder);

  readonly form = this.fb.group({
    nombre: ['', [Validators.required]],
    direccion: [''],
    referencia: [''],
    idPropietarioPersona: [null as number | null],
  });

  cargando = false;

  constructor() {
    effect(() => {
      const c = this.casa();
      this.form.reset({
        nombre: c?.nombre ?? '',
        direccion: c?.direccion ?? '',
        referencia: c?.referencia ?? '',
        idPropietarioPersona: c?.idPropietarioPersona ?? null,
      });
    });
  }

  onCancelar() {
    this.cancelado.emit();
  }

  onGuardar() {
    this.form.markAllAsTouched();
    if (this.form.invalid || this.cargando) return;

    this.cargando = true;
    const valores = this.form.getRawValue();
    const actual = this.casa();

    const payload: Casa = {
      id: actual?.id ?? 0,
      nombre: valores.nombre ?? '',
      direccion: valores.direccion,
      referencia: valores.referencia,
      idPropietarioPersona: valores.idPropietarioPersona,
    };

    const request$ = this.modo() === 'editar' && payload.id > 0
      ? this.casaService.update(payload)
      : this.casaService.create(payload);

    request$.subscribe({
      next: () => this.guardado.emit(),
      error: (e) => {
        console.error('Error al guardar casa', e);
        this.cargando = false;
      },
      complete: () => (this.cargando = false),
    });
  }
}
