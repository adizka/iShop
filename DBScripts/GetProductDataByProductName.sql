USE [MarvelDB]
GO
/****** Object:  UserDefinedFunction [dbo].[GetProductDataByProductName]    Script Date: 01/03/2012 15:23:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[GetProductDataByProductName](
	@SearchCriteria nvarchar(200))
RETURNS TABLE
as
return select  prod.* , prodProp.[PropertyValue], cat.[CategoryID], cat.[Name] as CategoryName
			from [dbo].[ProductsRefCategories] ref
				inner join [dbo].[Products] prod on (prod.ProductID = ref.ProductID)
				inner join [dbo].[ProductProperties] prodProp on (prodProp.ProductID = prod.ProductID)
				inner join [dbo].[Categories] cat on (cat.CategoryID = ref.CategoryID)
				where
					 lower(prod.Name) like lower(N'%' +LTRIM(RTRIM(@SearchCriteria)) + N'%') and
					 prodProp.[PropertyName] = 'ProductPhotoPreview'
					 
			
