SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

CREATE DATABASE IF NOT EXISTS demo_db_vehiculos
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE demo_db_vehiculos;

/* =========================================================
   1) TIPO DE DOCUMENTO
========================================================= */
CREATE TABLE IF NOT EXISTS persona_tipo_documento (
  id INT AUTO_INCREMENT,
  codigo VARCHAR(20) NOT NULL,
  descripcion VARCHAR(50) NOT NULL,
  user_create INT NOT NULL,
  user_update INT NULL,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_update TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (id),
  UNIQUE KEY uq_persona_tipo_documento_codigo (codigo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/* =========================================================
   2) PERSONA
========================================================= */
CREATE TABLE IF NOT EXISTS persona (
  id INT AUTO_INCREMENT,
  id_tipo_documento INT NOT NULL,
  numero_documento VARCHAR(20) NOT NULL,
  nombres VARCHAR(100) NOT NULL,
  apellido_paterno VARCHAR(100) NOT NULL,
  apellido_materno VARCHAR(100) NULL,
  direccion VARCHAR(300) NULL,
  telefono VARCHAR(30) NULL,
  user_create INT NOT NULL,
  user_update INT NULL,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_update TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (id),
  KEY idx_persona_tipo_documento (id_tipo_documento),
  UNIQUE KEY uq_persona_documento (id_tipo_documento, numero_documento),
  CONSTRAINT fk_persona_tipo_documento
    FOREIGN KEY (id_tipo_documento) REFERENCES persona_tipo_documento(id)
    ON UPDATE CASCADE
    ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/* =========================================================
   3) CASA
========================================================= */
CREATE TABLE IF NOT EXISTS casa (
  id INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(100) NOT NULL,
  direccion VARCHAR(300) DEFAULT NULL,
  referencia VARCHAR(200) DEFAULT NULL,
  id_propietario_persona INT DEFAULT NULL,
  user_create INT NOT NULL,
  user_update INT DEFAULT NULL,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_update TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (id),
  KEY idx_casa_propietario (id_propietario_persona),
  CONSTRAINT fk_casa_propietario_persona
    FOREIGN KEY (id_propietario_persona) REFERENCES persona(id)
    ON UPDATE CASCADE
    ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/* =========================================================
   4) VEHICULO
========================================================= */
CREATE TABLE IF NOT EXISTS vehiculo (
  id INT NOT NULL AUTO_INCREMENT,
  placa VARCHAR(12) NOT NULL,
  marca VARCHAR(80) NOT NULL,
  modelo VARCHAR(80) NOT NULL,
  anio SMALLINT NOT NULL,
  color VARCHAR(40) DEFAULT NULL,
  id_propietario_persona INT NOT NULL,
  id_casa INT DEFAULT NULL,
  estado TINYINT(1) NOT NULL DEFAULT 1,
  user_create INT NOT NULL,
  user_update INT DEFAULT NULL,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  date_update TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (id),
  UNIQUE KEY uq_vehiculo_placa (placa),
  KEY idx_vehiculo_propietario (id_propietario_persona),
  KEY idx_vehiculo_casa (id_casa),
  CONSTRAINT fk_vehiculo_propietario_persona
    FOREIGN KEY (id_propietario_persona) REFERENCES persona(id)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
  CONSTRAINT fk_vehiculo_casa
    FOREIGN KEY (id_casa) REFERENCES casa(id)
    ON UPDATE CASCADE
    ON DELETE SET NULL,
  CONSTRAINT chk_vehiculo_anio
    CHECK (anio BETWEEN 1900 AND 2100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO persona_tipo_documento (codigo, descripcion, user_create)
VALUES
  ('DNI', 'Documento Nacional de Identidad', 1),
  ('CE', 'Carnet de extranjeria', 1)
ON DUPLICATE KEY UPDATE
  descripcion = VALUES(descripcion);
