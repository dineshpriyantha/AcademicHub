-- Table 

CREATE TABLE Categories (
    CId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Date DATETIME NOT NULL,
    DisplayArea INT NOT NULL,
    CRank INT NOT NULL
);

drop table Categories
drop table Subcategories
drop table Posts

CREATE TABLE Subcategories (
    SId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Date DATETIME NOT NULL,
    CategoryId INT NOT NULL,
    SRank INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories (CId)
);

CREATE TABLE Posts (
    PId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Date DATETIME NOT NULL,
    CategoryId INT NOT NULL,
    SubcategoryId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories (CId),
    FOREIGN KEY (SubcategoryId) REFERENCES Subcategories (SId)
);

CREATE TYPE dbo.Categorydata AS TABLE
(
	Name NVARCHAR(100),
	[Date] DATETIME,
	DisplayAreaId INT,
	CRank INT
 );


-- Stroed procedure
alter PROCEDURE sp_AHub_AddCategory
	@Name NVARCHAR(100),
	@Date DATETIME,
	@DisplayAreaId INT,
	@CRank INT,
	@Result INT OUTPUT,
	@ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @OutputTable TABLE(Result INT,  ErrorMessage NVARCHAR(100));

	IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = @Name)
	BEGIN
		INSERT INTO Categories(Name, [Date], DisplayArea, CRank)
		--OUTPUT 1, NULL INTO @OutputTable
		VALUES(@Name, @Date, @DisplayAreaId, @CRank)
		SET @Result = 1;
		SET @ErrorMessage = NULL
	END
	ELSE
	BEGIN
		INSERT INTO @OutputTable(Result, ErrorMessage)
		VALUES (0, 'Category already exists');
	END

	SELECT Result, ErrorMessage FROM @OutputTable
END



sp_AHub_AddCategory 'Efg', '2023/6/11', 1,2


create table test (
id int identity(1,1),
name nvarchar(50)
)

create proc sp_addTest
	@Name nvarchar(50)
as
Begin
	insert into test(name) values(@Name)
end


sp_addTest 'Amal'

select * from test

select * from Categories