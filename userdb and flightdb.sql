CREATE PROCEDURE USER_PROC_INSERT
	@NAME VARCHAR(8),
	@MOBILENUMBER VARCHAR(10),
	@DOB date,
	@SEX varchar(1),
	@AGE int,
	@USERADDRESS VARCHAR(25),
	@PASSWORD VARCHAR(6)
AS
	begin
	begin try
	begin transaction
            INSERT INTO userdb(Name,MOBILENUMBER,DOB,AGE,Sex,useraddress,passwords)
			VALUES(@NAME,@MOBILENUMBER,@DOB,@AGE,@SEX,@USERADDRESS,@PASSWORD)
	commit transaction
	end try
	begin catch
	rollback transaction 
	select ERROR_MESSAGE() as ErrorMessage
	end catch
END


 drop procedure USER_PROC_INSERT;
-------------------------------------------------

CREATE PROCEDURE USER_PROC_LOGIN
	@mobilenumber VARCHAR(20),
	@passwords varchar(25)
	As
	begin 
		select * FROM userdb WHERE MobileNumber =@mobilenumber and passwords=@passwords 
		END


 drop procedure USER_PROC_LOGIN;


 -------------------------------------------------------

 CREATE PROCEDURE FLIGHT_ADD
	 @flightName varchar(20),
	 @flightNumber varchar(10)
	 as
		begin 
			INSERT INTO flightdb(flightName,flightNumber)VALUES(@flightName,@flightNumber)
		end


 drop procedure FLIGHT_ADD;
-----------------------------------------------

	create procedure FLIGHT_DISPLAY
	as
		begin 
		select * from flightdb;
		end


--------------------------------------------------
create procedure USER_PROC_DISPLAY
as
begin 
     select * from userdb;
	 end
----------------------------------------



CREATE TABLE userdb
(
userid int not null PRIMARY KEY identity(1,1),
Name varchar(20) not null,
MobileNumber varchar(10)unique not null,
DOB date not null,
Sex varchar(1) not null,
age int not null check (age>18),
useraddress varchar(25) not null,
passwords varchar(25) not null,
create_at DateTime not null default Current_timeStamp 
)

INSERT INTO userdb(Name,MobileNumber, DOB,Sex,age,useraddress,passwords)
VALUES ('sridhar','2121212121', '02-Apr-98','m',24,'salem','12345');

drop table userdb;

select * from userdb;

select *FROM userdb WHERE name ='sri1'and passwords='22sri1'


CREATE TABLE flightdb
(
flightId int not null PRIMARY KEY identity(1,1),
flightName varchar(20) not null,
flightNumber varchar(10) not null
)

INSERT INTO flightdb(flightName,flightNumber)
VALUES ('airindia',1234);

drop table flightdb;

select * from flightdb;




