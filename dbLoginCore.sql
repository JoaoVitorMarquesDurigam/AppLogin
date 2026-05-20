create database dbLoginCore;
use dbLoginCore;

 create table Cliente(
 Id	int auto_increment primary key,	
 Nome varchar(50) NOT NULL,
 Nascimento DATETIME NOT NULL,
 Sexo CHAR(1),
 CPF VARCHAR(11) NOT NULL,
 Telefone VARCHAR(50) NOT NULL,
 Senha VARCHAR(8) NOT NULL,
 ConfirmaçâoSenha VARCHAR(8) NOT NULL,
 Situacao CHAR(1) NOT NULL
 );
 
 CREATE TABLE Colaborador(
 Id INT AUTO_INCREMENT PRIMARY KEY,
 Nome VARCHAR(50) NOT NULL,
 Email VARCHAR(50) NOT NULL,
 Senha VARCHAR(8) NOT NULL,
 Tipo VARCHAR(8) NOT NULL	
 );
 
 /*oi*/