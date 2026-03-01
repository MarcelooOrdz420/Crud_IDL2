import { Routes } from '@angular/router';
import { MantenimientoPersonaListComponent } from './pages/mantenimiento/mantenimiento-persona-list/mantenimiento-persona-list.component';
import { MantenimientoCasaListComponent } from './pages/mantenimiento/mantenimiento-casa-list/mantenimiento-casa-list.component';
import { MantenimientoVehiculoListComponent } from './pages/mantenimiento/mantenimiento-vehiculo-list/mantenimiento-vehiculo-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'persona', pathMatch: 'full' },
  { path: 'persona', component: MantenimientoPersonaListComponent },
  { path: 'casa', component: MantenimientoCasaListComponent },
  { path: 'vehiculo', component: MantenimientoVehiculoListComponent },
  { path: '**', redirectTo: 'persona' },
];
