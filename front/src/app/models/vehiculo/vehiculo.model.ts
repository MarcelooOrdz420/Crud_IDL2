export interface Vehiculo {
  id: number;
  placa: string;
  marca: string;
  modelo: string;
  anio: number;
  color?: string | null;
  idPropietarioPersona: number;
  idCasa?: number | null;
  estado: boolean;
}
