--CREATE TABLE Operators
--(
--    OperatorID INT IDENTITY(1,1) NOT NULL,
--    LoginName NVARCHAR(40) NOT NULL,
--    PasswordHash BINARY(64) NOT NULL,
--    FirstName NVARCHAR(40) NULL,
--    LastName NVARCHAR(40) NULL,
--    CONSTRAINT [PK_Operator_OperatorID] PRIMARY KEY CLUSTERED (OperatorID ASC)
--)

--INSERT INTO Operators (LoginName, PasswordHash, FirstName, LastName)
--VALUES ('Filip_Mystek', HASHBYTES('SHA2_256','Test123!'),'Filip','Mystek')

--SELECT * from Operators WHERE LoginName = 'Filip_Mystek'