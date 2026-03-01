import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Vehiculo } from '../../../models/vehiculo/vehiculo.model';
import { VehiculoService } from '../../../services/vehiculo/vehiculo.service';
import { MantenimientoVehiculoEditarComponent } from '../mantenimiento-vehiculo-editar/mantenimiento-vehiculo-editar.component';

@Component({
  selector: 'app-mantenimiento-vehiculo-list',
  standalone: true,
  imports: [MantenimientoVehiculoEditarComponent, RouterModule, FormsModule],
  templateUrl: './mantenimiento-vehiculo-list.component.html',
  styleUrls: ['./mantenimiento-vehiculo-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MantenimientoVehiculoListComponent implements OnInit {
  private readonly vehiculoService = inject(VehiculoService);

  vehiculos = signal<Vehiculo[]>([]);

  mostrarModal = false;
  modoEdicion: 'crear' | 'editar' = 'crear';
  vehiculoSeleccionado: Vehiculo | null = null;
  filtroPlaca = '';
  filtroMarca = '';
  filtroPropietario = '';
  busquedaRealizada = false;

  ngOnInit(): void {
  }

  buscarVehiculos() {
    const placa = this.filtroPlaca.trim().toLowerCase();
    const marca = this.filtroMarca.trim().toLowerCase();
    const propietario = this.filtroPropietario.trim();

    this.vehiculoService.getAll().subscribe({
      next: (data) => {
        const filtrados = data.filter((vehiculo) => {
          const matchPlaca = !placa || vehiculo.placa.toLowerCase().includes(placa);
          const matchMarca = !marca || vehiculo.marca.toLowerCase().includes(marca);
          const matchPropietario = !propietario || `${vehiculo.idPropietarioPersona}`.includes(propietario);
          return matchPlaca && matchMarca && matchPropietario;
        });

        this.vehiculos.set(filtrados);
        this.busquedaRealizada = true;
      },
      error: (err) => console.log('error vehiculos', err),
    });
  }

  limpiarBusqueda() {
    this.filtroPlaca = '';
    this.filtroMarca = '';
    this.filtroPropietario = '';
    this.vehiculos.set([]);
    this.busquedaRealizada = false;
  }

  abrirAgregar() {
    this.modoEdicion = 'crear';
    this.vehiculoSeleccionado = null;
    this.mostrarModal = true;
  }

  abrirEditar(vehiculo: Vehiculo) {
    this.modoEdicion = 'editar';
    this.vehiculoSeleccionado = { ...vehiculo };
    this.mostrarModal = true;
  }

  cerrarModal() {
    this.mostrarModal = false;
    this.vehiculoSeleccionado = null;
  }

  onGuardado() {
    this.cerrarModal();
    if (this.busquedaRealizada) {
      this.buscarVehiculos();
    }
  }

  eliminarVehiculo(vehiculo: Vehiculo) {
    const ok = window.confirm(`Esta seguro de eliminar el vehiculo con placa: ${vehiculo.placa}?`);
    if (!ok) return;

    this.vehiculoService.delete(vehiculo.id).subscribe({
      next: () => {
        if (this.busquedaRealizada) {
          this.buscarVehiculos();
        }
      },
      error: (err) => console.log('error eliminar vehiculo', err),
    });
  }
}
