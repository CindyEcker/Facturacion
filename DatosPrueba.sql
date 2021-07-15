USE Facturacion

--Datos de prueba

INSERT INTO Estados VALUES('Activo')
INSERT INTO Estados VALUES('Inactivo')

INSERT INTO Vendedores VALUES('Jorge Bodden','20', 1)
INSERT INTO Vendedores VALUES('Angel Gonzalez','20', 1)
INSERT INTO Vendedores VALUES('Marlon Peña','20', 1)
INSERT INTO Vendedores VALUES('Cindy Ecker','20', 1)
INSERT INTO Vendedores VALUES('Isaí Vargas','20', 1)

INSERT INTO Usuarios VALUES('jbodden','admin', 1);
INSERT INTO Usuarios VALUES('agonzalez','admin', 2);
INSERT INTO Usuarios VALUES('mpeña','admin', 3);
INSERT INTO Usuarios VALUES('cecker','admin', 4);
INSERT INTO Usuarios VALUES('ivargas','admin', 5)

INSERT INTO Articulos VALUES('Laptop Dell',60000, 1)
INSERT INTO Articulos VALUES('Laptop ASUS', 20000.00, 1)
INSERT INTO Articulos VALUES('Laptop Acer',33000.80, 1)
INSERT INTO Articulos VALUES('Laptop HP', 50000.20, 1)

INSERT INTO Clientes VALUES('Ferrex',401223658, 'Urb. Centro Viejo, c/Rodolfo Gomez #35', 154623451, '8096523655', 'ferrex@email.com', 1)
INSERT INTO Clientes VALUES('Agami',40124548, 'Urb. Centro Viejo, c/Rodolfo Gomez #10', 154655251, '8096233655', 'agami@email.com', 1)
INSERT INTO Clientes VALUES('Supermercado Macias',41335548, 'Urb. Máximo Gómez, c/José Martín #29', 122556520, '8097973542', 'macias.market03@gmail.com', 1)
INSERT INTO Clientes VALUES('El Grifo Restaurant',45864548, 'Villa María, c/Nueva Esperanza #57', 275557811, '8095662500', 'info@elgriforestaurant.com', 1)
INSERT INTO Clientes VALUES('Delica2',13563351, 'Villa María, c/Cofre Viejo #10', 233556113, '8095446120', 'Delica2@gmail.com', 1)