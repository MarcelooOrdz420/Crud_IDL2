import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CasaService } from '../../../services/casa/casa.service';
import { Casa } from '../../../models/casa/casa.model';
import { MantenimientoCasaEditarComponent } from '../mantenimiento-casa-editar/mantenimiento-casa-editar.component';

@Component({
  selector: 'app-mantenimiento-casa-list',
  standalone: true,
  imports: [MantenimientoCasaEditarComponent, RouterModule, FormsModule],
  templateUrl: './mantenimiento-casa-list.component.html',
  styleUrls: ['./mantenimiento-casa-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoCasaListComponent implements OnInit {
  private readonly casaService = inject(CasaService);

  casas = signal<Casa[]>([]);

  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  casaSeleccionada: Casa | null = null;
  filtroNombre = '';
  filtroPropietario = '';
  busquedaRealizada = false;

  ngOnInit(): void {
  }

  buscarCasas() {
    const nombre = this.filtroNombre.trim().toLowerCase();
    const propietario = this.filtroPropietario.trim();

    this.casaService.getAll().subscribe({
      next: (data) => {
        const filtradas = data.filter((casa) => {
          const matchNombre = !nombre || casa.nombre.toLowerCase().includes(nombre);
          const matchPropietario = !propietario || `${casa.idPropietarioPersona ?? ''}`.includes(propietario);
          return matchNombre && matchPropietario;
        });

        this.casas.set(filtradas);
        this.busquedaRealizada = true;
      },
      error: (err) => console.log('error casas', err),
    });
  }

  limpiarBusqueda() {
    this.filtroNombre = '';
    this.filtroPropietario = '';
    this.casas.set([]);
    this.busquedaRealizada = false;
  }

  abrirAgregar() {
    this.modoEdicion = 'crear';
    this.casaSeleccionada = null;
    this.mostrarModal = true;
  }

  abrirEditar(casa: Casa) {
    this.modoEdicion = 'editar';
    this.casaSeleccionada = { ...casa };
    this.mostrarModal = true;
  }

  cerrarModal() {
    this.mostrarModal = false;
    this.casaSeleccionada = null;
  }

  onGuardado() {
    this.cerrarModal();
    if (this.busquedaRealizada) {
      this.buscarCasas();
    }
  }

  eliminarCasa(casa: Casa) {
    const ok = window.confirm(`¿Está seguro de eliminar la casa: ${casa.nombre}?`);
    if (!ok) return;

    this.casaService.delete(casa.id).subscribe({
      next: () => {
        if (this.busquedaRealizada) {
          this.buscarCasas();
        }
      },
      error: (err) => console.log('error eliminar casa', err),
    });
  }
}
