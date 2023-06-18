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
-- Category 
ALTER PROCEDURE sp_AHub_AddCategory
	@Name NVARCHAR(100),
	@Date DATETIME,
	@DisplayAreaId INT,
	@CRank INT,
	@Result INT OUTPUT,
	@ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM Categories WHERE Name = @Name)
	BEGIN
		BEGIN TRY
			INSERT INTO Categories(Name, [Date], DisplayArea, CRank)
			VALUES(@Name, @Date, @DisplayAreaId, @CRank)
			SET @Result = 1;
			SET @ErrorMessage = 'Insert Successfully';
		END TRY
		BEGIN CATCH
			SET @Result = 0; -- Failure
			SET @ErrorMessage = 'Error: '+ERROR_MESSAGE();
		END CATCH		
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ErrorMessage = 'Category Already Exists';
	END
END


CREATE PROCEDURE sp_AHub_UpdateCategory
	@CId INT,
	@Name NVARCHAR(100),
	@Date DATETIME,
	@DisplayAreaId INT,
	@CRank INT,
	@Result INT OUTPUT,
	@ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM Categories WHERE CId = @CId)
	BEGIN
		BEGIN TRY
			UPDATE Categories
			SET Name = @Name, [Date] = @Date, DisplayArea = @DisplayAreaId, CRank = @CRank
			WHERE CId = @CId

			SET @Result = 1;
			SET @ErrorMessage = 'Update Successfully';
		END TRY
		BEGIN CATCH
			SET @Result = 0;
			SET @ErrorMessage = 'Error: '+ ERROR_MESSAGE();
		END CATCH
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ErrorMessage = 'Category Not Found'
	END
END


ALTER PROCEDURE sp_AHub_DeleteCategory
	@CId INT,
	@Result INT OUTPUT,
	@ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT 1 FROM Categories WHERE CId = @CId)
	BEGIN
		BEGIN TRY
			DELETE FROM Categories WHERE CId = @CId;
			SET @Result = 1;
			SET @ErrorMessage = 'Delete Successfully.';
		END TRY
		BEGIN CATCH
			SET @Result = 0;
			SET @ErrorMessage = 'Error : '+ERROR_MESSAGE();
		END CATCH
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ErrorMessage = 'Category Not Found';
	END
END


CREATE PROCEDURE sp_AHub_GetCategory
	@Result INT OUTPUT,
	@ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN
		SELECT * FROM Categories

		SET @Result = 1;
		SET @ErrorMessage = 'Success';
	END
END


ALTER PROCEDURE sp_AHub_GetCategoryById
	@CId INT,
	@Result INT OUTPUT,
	@ErrorMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM Categories WHERE CId = @CId)
	BEGIN
		BEGIN
			SELECT * FROM Categories WHERE CId = @CId

			SET @Result = 1;
			SET @ErrorMessage = 'Success';
		END
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ErrorMessage = 'Category Not Found';
	END	
END



------------ Sub Category --------

CREATE PROCEDURE sp_AHub_AddSubCategory
	@Name NVARCHAR(100),
	@Date DATETIME,
	@CategoryId INT,
	@SRank INT,
	@Result INT OUTPUT,
	@ReturnMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM SubCategories WHERE Name = @Name)
	BEGIN
		BEGIN TRY
			INSERT INTO SubCategories(Name, [Date], CategoryId, SRank)
			VALUES(@Name, @Date, @CategoryId, @SRank)
			SET @Result = 1;
			SET @ReturnMessage = 'Insert Successfully';
		END TRY
		BEGIN CATCH
			SET @Result = 0; -- Failure
			SET @ReturnMessage = 'Error: '+ERROR_MESSAGE();
		END CATCH		
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ReturnMessage = 'Sub Category Already Exists';
	END
END


CREATE PROCEDURE sp_AHub_UpdateSubCategory
	@SId INT,
	@Name NVARCHAR(100),
	@Date DATETIME,
	@CategoryId INT,
	@SRank INT,
	@Result INT OUTPUT,
	@ReturnMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM SubCategories WHERE SId = @SId)
	BEGIN
		BEGIN TRY
			UPDATE SubCategories
			SET Name = @Name, [Date] = @Date, CategoryId = @CategoryId, SRank = @SRank
			WHERE SId = @SId

			SET @Result = 1;
			SET @ReturnMessage = 'Update Successfully';
		END TRY
		BEGIN CATCH
			SET @Result = 0;
			SET @ReturnMessage = 'Error: '+ ERROR_MESSAGE();
		END CATCH
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ReturnMessage = 'Sub Category Not Found'
	END
END

CREATE PROCEDURE sp_AHub_DeleteSubCategory
	@SId INT,
	@Result INT OUTPUT,
	@ReturnMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT 1 FROM SubCategories WHERE SId = @SId)
	BEGIN
		BEGIN TRY
			DELETE FROM SubCategories WHERE SId = @SId;
			SET @Result = 1;
			SET @ReturnMessage = 'Delete Successfully.';
		END TRY
		BEGIN CATCH
			SET @Result = 0;
			SET @ReturnMessage = 'Error : '+ERROR_MESSAGE();
		END CATCH
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ReturnMessage = 'Sub Category Not Found';
	END
END



CREATE PROCEDURE sp_AHub_GetSubCategory
	@Result INT OUTPUT,
	@ReturnMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN
		SELECT * FROM SubCategories

		SET @Result = 1;
		SET @ReturnMessage = 'Success';
	END
END


CREATE PROCEDURE sp_AHub_GetSubCategoryById
	@SId INT,
	@Result INT OUTPUT,
	@ReturnMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM SubCategories WHERE SId = @SId)
	BEGIN
		BEGIN
			SELECT * FROM SubCategories WHERE SId = @SId

			SET @Result = 1;
			SET @ReturnMessage = 'Success';
		END
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ReturnMessage = 'Sub Category Not Found';
	END	
END



--------- Post -----------------

CREATE PROCEDURE sp_AHub_AddPost
	@Title NVARCHAR(100),
	@Date DATETIME,
	@Content NVARCHAR(Max),
	@CategoryId INT,
	@SubcategoryId INT,
	@Result INT OUTPUT,
	@ReturnMessage NVARCHAR(200) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM Posts WHERE Title = @Title)
	BEGIN
		BEGIN TRY
			INSERT INTO Posts(Title, [Date], Content, CategoryId, SubcategoryId)
			VALUES(@Title, @Date, @Content, @CategoryId, @SubcategoryId)
			SET @Result = 1;
			SET @ReturnMessage = 'Insert Successfully';
		END TRY
		BEGIN CATCH
			SET @Result = 0; -- Failure
			SET @ReturnMessage = 'Error: '+ERROR_MESSAGE();
		END CATCH		
	END
	ELSE
	BEGIN
		SET @Result = 0;
		SET @ReturnMessage = 'Post Already Exists';
	END
END







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