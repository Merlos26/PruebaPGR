

SELECT e.Id, 
		e.Nombre,
		COUNT(pe.IdPersona) as empleados_totales
FROM PRUEBA.Empresa as e
LEFT JOIN PRUEBA.PersonaEmpresa pe on e.Id = pe.IdEmpresa
LEFT JOIN PRUEBA.Persona p on pe.IdPersona  =  p.Id
GROUP BY e.id, e.Nombre, e.Telefono;


