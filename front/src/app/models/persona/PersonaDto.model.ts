export class PersonaDto {
    id: number = 0;
    idTipoDocumento: number = 0;
    numeroDocumento: string | null = "";
    nombres: string | null = "";
    apellidoPaterno: string | null = "";
    apellidoMaterno: string | null = "";
    direccion: string | null = "";
    telefono: string | null = "";
    userCreate: number = 0;
    userUpdate: number | null = 0;
    dateCreated: string | null = "";
    dateUpdate: string | null = "";
}