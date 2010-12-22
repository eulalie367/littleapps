cd C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin\

sqlmetal /conn:"Integrated Security=true;Initial Catalog=ProductRecomendation;Server=DELLLAPTOP" /namespace:ProductRecomendation.DAL /code:C:\LittleApps\ProductRecomendation\DAL\ProductRecomendation.cs /language:csharp /pluralize /views /functions /sprocs /context:dcProductsRecomendation 
