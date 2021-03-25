create database session2;
use session2;
drop table if exists Land;
create table Land(
id int auto_increment primary key,
city varchar(50),
houseNumber varchar(50),
apartmentNumber varchar(50),
latitude decimal(4,2),
longitude decimal(4,2),
square int,
street varchar(50)
);
drop table if exists Apartment;
create table Apartment(
id int auto_increment primary key,
city varchar(50),
street varchar(50),
houseNumber varchar(50),
apartmentNumber varchar(50),
latitude decimal(4,2),
longitude decimal(4,2),
roomsAmount int,
square int
);
drop table if exists House;
create table House(
id int auto_increment primary key,
city varchar(50),
street varchar(50),
houseNumber varchar(50),
apartmentNumber varchar(50),
latitude decimal(4,2),
longitude decimal(4,2),
roomsAmount int,
floorsAmount int,
square int
);
