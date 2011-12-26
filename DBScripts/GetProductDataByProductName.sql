
ALTER FUNCTION [dbo].[GetProductDataByProductName](
	@SearchCriteria nvarchar(200))
RETURNS TABLE
as
return select DISTINCT prod.* , prodProp.[PropertyValue]
			from [dbo].[ProductsRefCategories] ref
				inner join [dbo].[Products] prod on (prod.ProductID = ref.ProductID)
				inner join [dbo].[ProductProperties] prodProp on (prodProp.ProductID = prod.ProductID)
				inner join [dbo].[Categories] cat on (cat.CategoryID = ref.CategoryID)
				where
					 prod.Name like (N'%' +LTRIM(RTRIM(@SearchCriteria)) + N'%') and
					 prodProp.[PropertyName] = 'ProductPhotoPreview'
					 
			
go