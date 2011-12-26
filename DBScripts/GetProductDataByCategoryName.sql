USE [MarvelDB]
GO

/****** Object:  UserDefinedFunction [dbo].[GetProductDataByCategoryName]    Script Date: 12/26/2011 16:07:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER FUNCTION [dbo].[GetProductDataByCategoryName](
	@SearchCriteria nvarchar(200))
RETURNS TABLE
as
return		select DISTINCT prod.* , prodProp.[PropertyValue]
			from [dbo].[ProductsRefCategories] ref
				inner join [dbo].[Products] prod on (prod.ProductID = ref.ProductID)
				inner join [dbo].[ProductProperties] prodProp on (prodProp.ProductID = prod.ProductID)
				inner join [dbo].[Categories] cat on (cat.CategoryID = ref.CategoryID)
				where
					cat.Name like (N'%' +LTRIM(RTRIM(@SearchCriteria)) + N'%')
					and  prodProp.[PropertyName] = 'ProductPhotoPreview'

GO


