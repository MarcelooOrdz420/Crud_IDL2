export interface Casa {
  id: number;
  nombre: string;
  direccion?: string | null;
  referencia?: string | null;
  idPropietarioPersona?: number | null;
}