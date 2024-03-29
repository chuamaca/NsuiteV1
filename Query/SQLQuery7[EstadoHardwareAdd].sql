USE [InventoryAGr]
GO
/****** Object:  Trigger [dbo].[EstadoHardwareAdd]    Script Date: 12/01/2020 05:48:23 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter TRIGGER [dbo].[EstadoHardwareAdd] ON [dbo].[DetalleOrden]
  AFTER INSERT, update
AS 
BEGIN
   --SET NOCOUNT ON agregado para evitar conjuntos de resultados adicionales
   -- interferir con las instrucciones SELECT.
  SET NOCOUNT ON;

  -- obtener el último valor de identificación del registro insertado o actualizado
  DECLARE @IDO INT, @IDHW INT
  SELECT @IDO = iddetalleorden, @IDHW = Hardware_Id
  FROM INSERTED 

  -- Insertar declaraciones para desencadenar aquí
  UPDATE hardware 
  set idstatusdevice = 2 WHERE 
		idhw=@IDHW

END
